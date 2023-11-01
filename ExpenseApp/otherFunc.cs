using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Google.Cloud.Firestore;
using System.Web.Caching;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;
using System.Net.NetworkInformation;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using Guna.UI2.WinForms;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using Grpc.Core;

namespace ExpenseApp
{
    internal class otherFunc
    {
        public static bool internetConn()
        {
            try{
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com");

                if (reply != null && reply.Status == IPStatus.Success){
                    return true;
                }
            }
            catch (Exception){

            }

            return false;
        }

        public async static void checkInternet(connectionForm c, Home h) 
        {
            if (!internetConn())
            {
                
                c.lblConnection.Text = "No Connection!";
                c.lblConnection.ForeColor = Color.Red;
                c.StartPosition = FormStartPosition.Manual;
                c.Location = new Point(h.Location.X + h.Width - (c.Width + 10), h.Location.Y + (c.Height * 15));

                await Task.Delay(2000);
                c.TopMost = true;
                c.Show();
                c.BringToFront();
            }
            else
            {
                c.Hide();
            }
            
            
            
        }

        public static IFirebaseClient conn()
        {
            IFirebaseConfig config = new FirebaseConfig(){
                AuthSecret = "LUA3lFfqrsEMSysOLxV5Lt6ZtDwVeFZ7UNTHDPGe",
                BasePath = "https://xpnsetracker-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };IFirebaseClient client = new FirebaseClient(config);
            return client;
        }
        public static FirestoreDb FirestoreConn()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"xpnsetracker-firebase-adminsdk-9jswd-e2983b2fce.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            FirestoreDb db = FirestoreDb.Create("xpnsetracker");
            return db;
        }
        public static bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public async Task<DocumentSnapshot> logInFunc(String username)
        {
            if (string.IsNullOrEmpty(username)){
                return null;
            }
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            return docSnap;

        }

        public static void recordCurrentLogs(String username)
        {
            FirestoreDb db = FirestoreConn();
            CollectionReference colRef = db.Collection("Users").Document(username).Collection("Logs");

        }

        public async Task<int> DocNameForExpenses(String username)
        {
            int Ename = 0;
            FirestoreDb database = FirestoreConn();
            CollectionReference cRef = database.Collection("Users").Document(username).Collection("Expenses");
            Query q = cRef.OrderByDescending("timestamp").Limit(1);
            QuerySnapshot qSnap = await q.GetSnapshotAsync();
            if(qSnap.Count > 0)
            {
                DocumentSnapshot docSnap = qSnap.Documents[0];
                String docName = docSnap.Id;
                Ename = int.Parse(docName.Trim('E'));
                return Ename;

            }
            else
            {
                Ename = 1;
                return Ename;
            }
            
        }

        public async Task<QuerySnapshot> displayData(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            FirestoreDb database = FirestoreConn();
            CollectionReference collRef = database.Collection("Users").Document(username).Collection("Expenses");
            QuerySnapshot queSnap = await collRef.GetSnapshotAsync();

            return queSnap;
        }
        public async Task<DocumentReference> SavingNewExpenses(String username)
        {
            int docNum = await DocNameForExpenses(username);
            Console.WriteLine("docNum: " + docNum);
            String docName = string.Concat("E", (docNum + 1).ToString());
            Console.WriteLine("docName: " + docName);
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username).Collection("Expenses").Document(docName);
            
            return docRef;

            /*document name should be "E1" then iterate*/
            /*Process: 
             * check if the "Expenses" collection has existing document("E1"*) 
             else create new which starts from "E1"*/
        }

        public async Task<DocumentReference> SavingWalletAmount(String username, String walletName)
        {
            FirestoreDb database = FirestoreConn();
            //CollectionReference colRef = database.Collection("Users").Document(username).Collection("Wallets");
            //QuerySnapshot qSnap = await colRef.GetSnapshotAsync();
            DocumentReference docRef = database.Collection("Users").Document(username).Collection("Wallets").Document(walletName);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if(!docSnap.Exists)
            {
                Dictionary<String, object> data = new Dictionary<String, object>()
                {
                    {"Amount", 0}
                };
                await docRef.SetAsync(data);
            }
            return docRef;
        }

        public static String amountBeautify(int total)
        {
            String output = "₱";
            if (total < 0)
            {
                total = total * -1;
                output = "-₱";
            }

            char[] ordered = total.ToString().ToCharArray();
            if (ordered.Length <= 3)
            {
                return output + total.ToString();
            }

            Array.Reverse(ordered);
            Stack<char> cstack = new Stack<char>();
            
            for (int i = 0; i < ordered.Length; i++)
            {
                cstack.Push(ordered[i]);
                if ((i + 1) % 3 == 0)
                {
                    cstack.Push(',');
                }

            }
            while(cstack.Count > 0)
            {
                output += cstack.Pop().ToString();
            }
            return output;
            
        }

        public async Task<int> SubtractExpensesFromWalletExpenses(String username) 
        {
            //get the latest added expense
            //get the latest docname
            //get the amount of that docname
            //get the current amount availabe in expenses then subtract then update
            
            DocumentReference docRefExpenses = await getDocRefExpenses(username);
            Console.WriteLine("START OF 1ST GETWALLET");
            int expense = await getWalletAmount(docRefExpenses);
            Console.WriteLine("EXPENSE:" + expense);
            DocumentReference docRefWallet = await SavingWalletAmount(username, "Expense");
            int currentAmountInExpenses = await getWalletAmount(docRefWallet);
            Console.WriteLine("Sencond: " + currentAmountInExpenses);
            int total = currentAmountInExpenses - expense;
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", total}
            };
            await docRefWallet.UpdateAsync(data);
            return total;

        }

        public async Task<DocumentReference> getDocRefExpenses(String username)
        {
            int docNum = await DocNameForExpenses(username);
            
            String docName = string.Concat("E", (docNum + 1).ToString());
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username).Collection("Expenses").Document(docName);
            DocumentSnapshot ds = await docRef.GetSnapshotAsync();
            Console.WriteLine("is the " + docName + "from getdocrefexpenses exists?: " + ds.Exists);
            return docRef;
        }

        public async Task<int> getWalletAmount(DocumentReference docRef)
        {
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.Exists)
            {
                FirebaseData am = docSnap.ConvertTo<FirebaseData>();
                int amount = am.Amount;
                return amount;
            }else
            {
                Console.WriteLine("ERROR IN GET WALLET");
                return 0;
            }
        }

        public static async Task<bool> isUsernameExistingAsync(String username)
        {
            if (string.IsNullOrEmpty(username)){
                return false;
            }
            var database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            return docSnap.Exists;
        }

        
        public bool isValidData(Dictionary<String, bool> data)
        {
            List<String> L = new List<String>();
            foreach (var row in data){
                if (!row.Value){
                    L.Add(row.Key);
                }
            }

            if(L.Count == 0){
                return true;
            }
            Signup.runErrorMsg(L);
            return false;
        }

        bool areControlEmpty(params string[] textboxes)
        {
            foreach (string textbox in textboxes){
                if (string.IsNullOrWhiteSpace(textbox)){
                    return true;
                }
            }
            return false;
        }

        public async static Task<int> generateUserID()
        {
            var database = FirestoreConn();
            int id = 1;
            DocumentReference docRef = database.Collection("CreatedAccount").Document("TotalAccount");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            Dictionary<String, object> data = new Dictionary<string, object>
            {
                { "Total",id}
            };
            if (docSnap.Exists)
            {
                int value = docSnap.GetValue<int>("Total");
                id = value + 1;
                data["Total"] = id;

                await docRef.UpdateAsync(data);
            }
            else
            {

                await docRef.SetAsync(data);
            }

            return id;


        }

        public async static Task<bool> CheckAccountStatus(String username)
        {
            var db = FirestoreConn();
            DocumentReference docRef = db.Collection("Users").Document(username);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();

            String status = docSnap.GetValue<String>("status");

            if (status.Equals("offline"))
            {
                Dictionary<String, object> data = new Dictionary<string, object>
                {
                    {"status", "online"}
                };
                await docRef.UpdateAsync(data);

                return true;
            }
            else
            {
                return false;
            }

        }

        public async static void UpdateAccStatusToOffline(String username)
        {
            var db = FirestoreConn();
            DocumentReference docRef = db.Collection("Users").Document(username);
            Dictionary<String, object> data = new Dictionary<string, object>
            {
                {"status", "offline"}
            };
            await docRef.UpdateAsync(data);
        }

        
           
        public async void signingUp(String username, String fname, String lname, String email, String password, String repeatpass, System.Windows.Forms.CheckBox terms, Signup s)
        {
            var database = FirestoreConn();
            otherFunc function = new otherFunc();
            bool validEmail = otherFunc.isValidEmail(email);
            bool validUsername = await otherFunc.isUsernameExistingAsync(username);
            bool isEmpty = areControlEmpty(fname,lname, email, username, password, repeatpass);
            bool passwordMatched = function.passwordMatched(Security.Decrypt(password), repeatpass);
            int generatedID = await generateUserID();
            if (!isEmpty){
                if (terms.Checked){
                    //Validate email and check if the username exists
                    Dictionary<String, bool> validatingData = new Dictionary<string, bool>(){
                        { "username", !validUsername},
                        { "email", validEmail}
                    };
                    bool validData = function.isValidData(validatingData);
                    if (validData){
                        if (passwordMatched){
                            if (function.isValidPassword(password)){
                                try{
                                    DocumentReference docRef = database.Collection("Users").Document(username);
                                    Dictionary<string, object> data = new Dictionary<string, object>()
                                    {
                                        {"First Name", fname },
                                        {"Last Name", lname },
                                        {"Username", username },
                                        {"Email", email },
                                        {"Password", password},
                                        {"ID", generatedID},
                                        {"DateCreated", FieldValue.ServerTimestamp}
                                    };
                                    await docRef.SetAsync(data);
                                    DialogResult res = MessageBox.Show("Successfully created your account!", "Success", MessageBoxButtons.OK);
                                    if (res == DialogResult.OK){
                                        s.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Cannot process your account", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                } 
                            }
                            else {
                                MessageBox.Show("Password do not meet the standards!", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else{
                            MessageBox.Show("Password does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else{
                MessageBox.Show("Please agree to the Terms and Condition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else{
            MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } 
        public bool passwordMatched(string password1, string password2)
        {
            return password1 == password2;
        }

        public static void populateCMBcategory(ctg category, AddExpensesForm f)
        {
            f.cmbCategory.Items.Clear();
            foreach (var c in category.LCategory)
            {
                f.cmbCategory.Items.Add(c);
            }
        }
        public bool isValidPassword(string password)
        {
            String pattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&+=!])[A-Za-z\d@#$%^&+=!]{8,}$";
            return Regex.IsMatch(password, pattern);
            
        }
        public bool validDate(String date)
        {
            DateTime parsedDate; 
            if(DateTime.TryParseExact(date,"yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)){
                if(parsedDate <= DateTime.Now) {
                    return true;
                }
            }
            return false;
        }
        public bool checkExpenseFormControl(params Control[] controls)
        {
            foreach (Control control in controls){
                if(control is Guna2TextBox){
                    if(string.IsNullOrEmpty((control as Guna2TextBox).Text)){
                        return true;
                    }
                }
                else if(control is Guna2ComboBox) { 
                    if(string.IsNullOrEmpty((control as Guna2ComboBox).Text)){
                        return true;
                    }
                }
            }
            return false;
        }
        public async void updateData(string username, string fname, string lname, string email, string bio, string password, updateAcc update, profile p, PictureBox img)
        {
            var database = FirestoreConn();
            otherFunc function = new otherFunc();
            bool validEmail = otherFunc.isValidEmail(email);
            bool isEmpty = areControlEmpty(fname, lname, email, username, password);
            bool validUsername = await otherFunc.isUsernameExistingAsync(username);

            Dictionary<String, bool> validatingData = new Dictionary<string, bool>()
            {
                 { "username", !validUsername},
                 { "email", validEmail}
            };
            bool validData = function.isValidData(validatingData);
            if (!isEmpty)
            {
                if (validData)
                {
                    try
                    {
                        DocumentReference docref = database.Collection("Users").Document(username);
                        Dictionary<string, object> data = new Dictionary<string, object>()
                        {
                            {"First Name", fname},
                            {"Last Name", lname},
                            {"Email", email},
                            {"Username",  username},
                            {"Bio", bio},
                            {"Password", Security.Encrypt(password)}
                        };
                        await docref.SetAsync(data);
                        DialogResult respond = MessageBox.Show("Successfully update your account!", "Success", MessageBoxButtons.OK);
                        if (respond == DialogResult.OK)
                        {
                            p.displayProfile();
                            update.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
