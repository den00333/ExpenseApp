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
using System.Drawing.Imaging;
using Grpc.Core;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using MaxMind.GeoIP2;
using System.Collections;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using static Google.Cloud.Firestore.V1.Firestore;
using System.Web;
using System.Collections.Specialized;
using System.Data;

namespace ExpenseApp
{
    internal class otherFunc
    {

        public static bool internetConn()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("www.google.com");

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {

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
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "LUA3lFfqrsEMSysOLxV5Lt6ZtDwVeFZ7UNTHDPGe",
                BasePath = "https://xpnsetracker-default-rtdb.asia-southeast1.firebasedatabase.app/"
            }; IFirebaseClient client = new FirebaseClient(config);
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
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            return docSnap;

        }



        public async Task<int> DocNameForExpenses(String username)
        {
            int Ename = 0;
            FirestoreDb database = FirestoreConn();
            CollectionReference cRef = database.Collection("Users").Document(username).Collection("Expenses");
            Query q = cRef.OrderByDescending("timestamp").Limit(1);
            QuerySnapshot qSnap = await q.GetSnapshotAsync();
            if (qSnap.Count > 0)
            {
                DocumentSnapshot docSnap = qSnap.Documents[0];
                String docName = docSnap.Id;
                Ename = int.Parse(docName.Trim('E'));
                return Ename;

            }
            else
            {
                Ename = 0;
                return Ename;
            }
        }
        public async Task<int> DocNameForGroupExpenses(String groupCode)
        {
            int Ename = 0;
            FirestoreDb database = FirestoreConn();
            CollectionReference cRef = database.Collection("Groups").Document(groupCode).Collection("Expenses");
            Query q = cRef.OrderByDescending("timestamp").Limit(1);
            QuerySnapshot qSnap = await q.GetSnapshotAsync();
            if (qSnap.Count > 0)
            {
                DocumentSnapshot docSnap = qSnap.Documents[0];
                String docName = docSnap.Id;
                Ename = int.Parse(docName.Trim('E'));
                return Ename;
            }
            else
            {
                Ename = 0;
                return Ename;
            }
        }

        public static DocumentReference editInsideGroup(String groupCode)
        {
            var db = FirestoreConn();
            Console.WriteLine($"EDITINSIDEGROUP: {groupCode}");
            DocumentReference docRef = db.Collection("Groups").Document(groupCode);
            return docRef;
        }

        public async Task<int> NameForExpenses(String groupCode)
        {
            Console.WriteLine($"this part00-{groupCode}-");
            int Ename = 0;
            CollectionReference cRef = editInsideGroup(groupCode).Collection("Expenses");
            Console.WriteLine("this part01");
            Query q = cRef.OrderByDescending("timestamp").Limit(1);
            Console.WriteLine("this part02");
            QuerySnapshot qSnap = await q.GetSnapshotAsync();
            Console.WriteLine("this part03");
            if (qSnap.Count > 0)
            {
                DocumentSnapshot docSnap = qSnap.Documents[0];
                String docName = docSnap.Id;
                Ename = int.Parse(docName.Trim('E'));
                return Ename;

            }
            else
            {
                Ename = 0;
                return Ename;
            }
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
        public async Task<DocumentReference> saveGroupExpenses(String groupCode)
        {
            int docNum = await NameForExpenses(groupCode);
            Console.WriteLine($"this partSavegroupExpenses-{groupCode}-");
            Console.WriteLine("docNum: " + docNum);
            String docName = string.Concat("E", (docNum + 1).ToString());
            Console.WriteLine("docName: " + docName);
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Groups").Document(groupCode).Collection("Expenses").Document(docName);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            return docRef;

        }
        public static async void addWalletLogs(String username, String walletName, float amount)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Wallets").Document("LogWallet");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            String dateInputted = DateTime.Now.ToString("yyyy-MM-dd");
            String timeInputted = DateTime.Now.ToString("HH:mm:ss");
            String item = $"{amount}|{walletName}|{dateInputted}|{timeInputted}";
            ArrayList logWallet = new ArrayList();
            logWallet.Add(item);

            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"LogWallet", logWallet}
            };

            if (!docSnap.Exists)
            {
                await docRef.SetAsync(data);
            }
            else
            {
                await docRef.UpdateAsync("LogWallet", FieldValue.ArrayUnion(item));
            }
        }
        public static async void addGroupWalletLogs(string groupCode, string walletName, float amount, string username)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Wallets").Document("LogWallet");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            String dateInputted = DateTime.Now.ToString("yyyy-MM-dd");
            String timeInputted = DateTime.Now.ToString("HH:mm:ss");
            String item = $"{amount}|{walletName}|{dateInputted}|{timeInputted}|{username}";
            ArrayList logWallet = new ArrayList();
            logWallet.Add(item);

            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"LogWallet", logWallet}
            };

            if (!docSnap.Exists)
            {
                await docRef.SetAsync(data);
            }
            else
            {
                await docRef.UpdateAsync("LogWallet", FieldValue.ArrayUnion(item));
            }
        }
        //public static async void addGroupWalletLogs(string groupCode, string walletName, float amount)
        //{
        //    var db = otherFunc.FirestoreConn();
        //    DocumentReference docRef = db.Collection("Groups").Document(groupCode).Collection("Wallets").Document("LogWallet");
        //    DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
        //    String dateInputted = DateTime.Now.ToString("yyyy-MM-dd");
        //    String timeInputted = DateTime.Now.ToString("HH:mm:ss");
        //    String item = $"{amount}|{walletName}|{dateInputted}|{timeInputted}";
        //    ArrayList logWallet = new ArrayList();
        //    logWallet.Add(item);

        //    Dictionary<String, object> data = new Dictionary<String, object>
        //    {
        //        {"LogWallet", logWallet}
        //    };

        //    if (!docSnap.Exists)
        //    {
        //        await docRef.SetAsync(data);
        //    }
        //    else
        //    {
        //        await docRef.UpdateAsync("LogWallet", FieldValue.ArrayUnion(item));
        //    }
        //}
        //public async Task<DocumentReference> SavingGroupWalletAmount(string groupCode, string walletName)
        //{
        //    var db = otherFunc.FirestoreConn();
        //    DocumentReference docRef = db.Collection("Groups").Document(groupCode).Collection("Wallets").Document(walletName);
        //    DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
        //    if (!docSnap.Exists)
        //    {
        //        Dictionary<String, object> data = new Dictionary<String, object>()
        //        {
        //            {"Amount", 0}
        //        };
        //        await docRef.SetAsync(data);
        //    }
        //    return docRef;
        //}
        public async Task<DocumentReference> SavingWalletAmount(String username, String walletName)
        {
            //CollectionReference colRef = database.Collection("Users").Document(username).Collection("Wallets");
            //QuerySnapshot qSnap = await colRef.GetSnapshotAsync();
            DocumentReference docRef = editInsideUser(username).Collection("Wallets").Document(walletName);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (!docSnap.Exists)
            {
                Dictionary<String, object> data = new Dictionary<String, object>()
                {
                    {"Amount", 0}
                };
                await docRef.SetAsync(data);
            }
            return docRef;
        }
        public async Task<DocumentReference> SavingWalletAmountOfGroup(String groupcode, String walletName)
        {
            //CollectionReference colRef = database.Collection("Users").Document(username).Collection("Wallets");
            //QuerySnapshot qSnap = await colRef.GetSnapshotAsync();
            var db = otherFunc.FirestoreConn();
            DocumentReference docRef = db.Collection("Groups").Document(groupcode).Collection("Wallets").Document(walletName);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (!docSnap.Exists)
            {
                Dictionary<String, object> data = new Dictionary<String, object>()
                {
                    {"Amount", 0}
                };
                await docRef.SetAsync(data);
            }
            return docRef;
        }

        public static String amountBeautify(float total)
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
                if ((i + 1) % 3 == 0 && i + 1 != ordered.Length)
                {
                    cstack.Push(',');
                }

            }
            while (cstack.Count > 0)
            {
                output += cstack.Pop().ToString();
            }
            return output;

        }

        public async Task<float[]> SubtractExpensesFromWalletExpenses(String username)
        {
            //get the latest added expense
            //get the latest docname
            //get the amount of that docname
            //get the current amount availabe in expenses then subtract then update
            float[] twoReturnVal = new float[2];
            float negativeVal = 0;
            DocumentReference docRefExpenses = await getDocRefExpenses(username);
            Console.WriteLine("START OF 1ST GETWALLET");
            float expense = await getWalletAmount(docRefExpenses);
            Console.WriteLine("EXPENSE:" + expense);
            DocumentReference docRefWallet = await SavingWalletAmount(username, "Expense");
            float currentAmountInExpenses = await getWalletAmount(docRefWallet);
            Console.WriteLine("Sencond: " + currentAmountInExpenses);
            float total = currentAmountInExpenses - expense;
            Console.WriteLine("Total Value: " + total);
            //check if it is negative total then return 0
            if (total < 0)
            {
                negativeVal = total;
                total = 0;

            }
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", total}
            };
            await docRefWallet.UpdateAsync(data);
            Console.WriteLine("negative Value: " + negativeVal);
            twoReturnVal[0] = total;
            twoReturnVal[1] = negativeVal;
            return twoReturnVal;

        }

        public async Task<bool> checkSubtractCurrentExpenses(double useramount, String username)
        {
            DocumentReference docRefWallet = await SavingWalletAmount(username, "Expense");
            float currentAmountInExpenses = await getWalletAmount(docRefWallet);

            double newBal = currentAmountInExpenses - useramount;

            if (newBal < 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public async Task<float[]> SubtractExpensesFromWalletExpensesGroup(String groupcode)
        {
            //get the latest added expense
            //get the latest docname
            //get the amount of that docname
            //get the current amount availabe in expenses then subtract then update
            float[] twoReturnVal = new float[2];
            float negativeVal = 0;
            DocumentReference docRefExpenses = await getDocRefExpensesGroup(groupcode);
            Console.WriteLine("START OF 1ST GETWALLET");

            float expense = await getGroupWalletAmount(docRefExpenses);
            Console.WriteLine("EXPENSE:" + expense);
           DocumentReference docRefWallet = await SavingWalletAmountOfGroup(groupcode, "Expense");
            float currentAmountInExpenses = await getWalletAmount(docRefWallet);
            Console.WriteLine("Sencond: " + currentAmountInExpenses);
            float total = currentAmountInExpenses - expense;
            Console.WriteLine("Total Value: " + total);
            //check if it is negative total then return 0
            if (total < 0)
            {
                negativeVal = total;
                total = 0;
            }
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", total}
            };
            await docRefWallet.UpdateAsync(data);
            Console.WriteLine("negative Value: " + negativeVal);
            twoReturnVal[0] = total;
            twoReturnVal[1] = negativeVal;
            return twoReturnVal;

        }
        public static DocumentReference editInsideUser(String username)
        {
            var db = FirestoreConn();
            DocumentReference docRef = db.Collection("Users").Document(username);
            return docRef;
        }
        public async static Task<String> addNewGroupGoal(string groupCode, string goalDate, float amount, string title, string desc,string username)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(title);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (!docSnap.Exists) {
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"Amount", amount},
                    {"GoalDate", goalDate},
                    {"Description", desc},
                    {"Creator", username},
                    {"timestamp", FieldValue.ServerTimestamp},
                    {"Percentage", 0},
                    {"Status", "Ongoing"}
                };
                await docRef.SetAsync(data);
                await updatePercentagePerGroupGoal(groupCode);
                return "Successfully Added!";
            }else{
                return "Title already existing!";
            }
        }
        public async static Task<String> addNewGoal(String username, String goalDate, float amount, String title, String desc)
        {

            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(title);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (!docSnap.Exists)
            {
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"Amount", amount},
                    {"GoalDate", goalDate},
                    {"Description", desc},
                    {"timestamp", FieldValue.ServerTimestamp},
                    {"Percentage", 0},
                    {"Status", "Ongoing"}
                };
                await docRef.SetAsync(data);
                await updatePercentagePerGoal(username);
                return "Successfully Added!";
            }
            else
            {
                return "Title already existing!";
            }
        }
        public static void setShort(float negativeVal, String username)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Wallets").Document("Expense");
            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"short", negativeVal}
            };

            docRef.UpdateAsync(data);
        }
        public static void setShortGroup(float negativeVal, String groupcode)
        {
            var db = otherFunc.FirestoreConn();
            DocumentReference docRef = db.Collection("Groups").Document(groupcode).Collection("Wallets").Document("Expense");
            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"short", negativeVal}
            };

            docRef.UpdateAsync(data);
        }
        //public static void setGroupShort(float negativeVal, string groupCode)
        //{
        //    var db = otherFunc.FirestoreConn();
        //    DocumentReference docRef = db.Collection("Groups").Document(groupCode).Collection("Wallets").Document("Expense");
        //    Dictionary<String, object> data = new Dictionary<String, object>
        //    {
        //        {"short", negativeVal}
        //    };

        //    docRef.UpdateAsync(data);
        //}

        public async static Task<float> getShort(String username)
        {
            float shrt = 0;
            DocumentReference docRef = editInsideUser(username).Collection("Wallets").Document("Expense");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.ContainsField("short"))
            {
                shrt = docSnap.GetValue<float>("short");
                return shrt;
            }
            else
            {
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"short", 0}
                };
                await docRef.SetAsync(data);
                return shrt;
            }
        }
        public async static Task<float> getShortGroup(String groupcode)
        {
            float shrt = 0;
            var db = otherFunc.FirestoreConn();
            DocumentReference docRef = db.Collection("Groups").Document(groupcode).Collection("Wallets").Document("Expense");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.ContainsField("short"))
            {
                shrt = docSnap.GetValue<float>("short");
                return shrt;
            }
            else
            {
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"short", 0}
                };
                await docRef.SetAsync(data);
                return shrt;
            }
        }
        public async static Task<float> getGroupShort(string groupCode)
        {
            var db = otherFunc.FirestoreConn();
            float shrt = 0;
            DocumentReference docRef = db.Collection("Groups").Document(groupCode).Collection("Wallets").Document("Expense");
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.ContainsField("short"))
            {
                shrt = docSnap.GetValue<float>("short");
                return shrt;
            }
            else
            {
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"short", 0}
                };
                await docRef.SetAsync(data);
                return shrt;
            }
        }

        public async Task<DocumentReference> getDocRefExpenses(String username)
        {
            int docNum = await DocNameForExpenses(username);
            
            String docName = string.Concat("E", (docNum).ToString());
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username).Collection("Expenses").Document(docName);
            DocumentSnapshot ds = await docRef.GetSnapshotAsync();
            Console.WriteLine("is the " + docName + "from getdocrefexpenses exists?: " + ds.Exists);
            return docRef;
        }
        public async Task<DocumentReference> getDocRefExpensesGroup(String groupCode)
        {
            int docNum = await DocNameForGroupExpenses(groupCode);
            String docName = string.Concat("E", (docNum).ToString());
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Groups").Document(groupCode).Collection("Expenses").Document(docName);
            
            DocumentSnapshot ds = await docRef.GetSnapshotAsync();
            Console.WriteLine("ndajsdjas " + ds.Exists);
            return docRef;
        }


        public async Task<float> getWalletAmount(DocumentReference docRef)
        {
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.Exists)
            {
                FirebaseData am = docSnap.ConvertTo<FirebaseData>();
                float amount = am.Amount;
                return amount;
            }
            else
            {
                Console.WriteLine("ERROR IN GET WALLET");
                return 0;
            }
        }
        public async Task<float>getGroupWalletAmount(DocumentReference docRef)
        {
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            if (docSnap.Exists)
            {
                FirebaseData am = docSnap.ConvertTo<FirebaseData>();
                float amount = am.Amount;
                return amount;
            }
            else
            {
                Console.WriteLine("ERROR IN GET WALLET");
                return 0;
            }
        }

        public async static void setNewWalletAmount(String username, String walletName, float newAmount)
        {
            DocumentReference docref = editInsideUser(username).Collection("Wallets").Document(walletName);
            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"Amount", newAmount}
            };
            await docref.UpdateAsync(data);

        }
        public async static void setNewWalletAmountGroup(String groupcode, String walletName, float newAmount)
        {
            var db = otherFunc.FirestoreConn();
            DocumentReference docref = db.Collection("Groups").Document(groupcode).Collection("Wallets").Document(walletName);
            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"Amount", newAmount}
            };
            await docref.UpdateAsync(data);

        }

        public static async Task<bool> isUsernameExistingAsync(String username)
        {
            if (string.IsNullOrEmpty(username))
            {
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
            foreach (var row in data)
            {
                if (!row.Value)
                {
                    L.Add(row.Key);
                }
            }

            if (L.Count == 0)
            {
                return true;
            }
            Signup.runErrorMsg(L);
            return false;
        }

        public bool areControlEmpty(params string[] textboxes)
        {
            foreach (string textbox in textboxes)
            {
                if (string.IsNullOrWhiteSpace(textbox))
                {
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

        /*public async static Task<bool> CheckAccountStatus(String username)
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

        }*/

        public async static void CheckAccountStatus(String username)
        {
            DocumentReference docRef = editInsideUser(username);
            DocumentSnapshot docSnap = await docRef.GetSnapshotAsync();
            Dictionary<String, object> data = new Dictionary<string, object>
                {
                    {"status", "online"}
                };
            await docRef.UpdateAsync(data);
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
            bool isEmpty = areControlEmpty(fname, lname, email, username, password, repeatpass);
            bool passwordMatched = function.passwordMatched(Security.Decrypt(password), repeatpass);
            int generatedID = await generateUserID();
            if (!isEmpty)
            {
                if (terms.Checked)
                {
                    //Validate email and check if the username exists
                    Dictionary<String, bool> validatingData = new Dictionary<string, bool>(){
                        { "username", !validUsername},
                        { "email", validEmail}
                    };
                    bool validData = function.isValidData(validatingData);
                    if (validData)
                    {
                        if (passwordMatched)
                        {
                            if (function.isValidPassword(password))
                            {
                                try
                                {
                                    DocumentReference docRef = database.Collection("Users").Document(username);
                                    Dictionary<string, object> data = new Dictionary<string, object>()
                                    {
                                        {"First Name", fname },
                                        {"Last Name", lname },
                                        {"Username", username },
                                        {"Email", email },
                                        {"Password", password},
                                        {"ID", generatedID},
                                        {"DateCreated", FieldValue.ServerTimestamp},
                                        {"status", "offline"}
                                    };
                                    await docRef.SetAsync(data);

                                    DialogResult res = MessageBox.Show("Successfully created your account!", "Success", MessageBoxButtons.OK);
                                    if (res == DialogResult.OK)
                                    {
                                        s.Close();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Cannot process your account", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Password do not meet the standards!", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please agree to the Terms and Condition", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
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
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                if (parsedDate <= DateTime.Now)
                {
                    return true;
                }
            }
            return false;
        }
        public bool checkFormControlEmpty(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                if (control is Guna2TextBox)
                {
                    if (string.IsNullOrEmpty((control as Guna2TextBox).Text))
                    {
                        return true;
                    }
                }
                else if (control is Guna2ComboBox)
                {
                    if (string.IsNullOrEmpty((control as Guna2ComboBox).Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async void updateData(string username, string fname, string lname, string email, string bio, string password, string confirmPass,updateAcc update, profile p)
        {
            var database = FirestoreConn();
            otherFunc function = new otherFunc();
            bool validEmail = otherFunc.isValidEmail(email);
            bool isEmpty = areControlEmpty(fname, lname, email, password);
            bool validUsername = await otherFunc.isUsernameExistingAsync(username);
            bool passMatched = function.passwordMatched(Security.Decrypt(password), confirmPass);

            if (!isEmpty)
            {
                if (passMatched)
                {
                    if (function.isValidPassword(password))
                    {
                        try
                        {
                            DocumentReference docref = database.Collection("Users").Document(username);
                            Dictionary<string, object> data = new Dictionary<string, object>()
                        {
                            {"First Name", fname},
                            {"Last Name", lname},
                            {"Email", email},
                            {"Bio", bio},
                            {"Password", Security.Encrypt(password)},
                        };
                            DocumentSnapshot snap = await docref.GetSnapshotAsync();
                            if (snap.Exists)
                            {
                                await docref.UpdateAsync(data);
                                DialogResult respond = MessageBox.Show("Successfully update your account!", "Success", MessageBoxButtons.OK);
                                if (respond == DialogResult.OK)
                                {
                                    p.displayProfile();
                                    update.Hide();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password do not meet the standards!", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Password does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public async void updatePassword(string username, string password)
        {
            var db = FirestoreConn();
            try
            {
                DocumentReference docref = db.Collection("Users").Document(username);
                Dictionary<string, object> data = new Dictionary<string, object>() {
                    {"Password", password }
                };
                DocumentSnapshot snap = await docref.GetSnapshotAsync();
                if (snap.Exists)
                {
                    await docref.UpdateAsync(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        public static string ImageIntoBase64String(PictureBox img)
        {
            if (img != null && img.Image != null)
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    using (Bitmap bmp = new Bitmap(img.Image))
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Empty PictureBox");
                return null;
            }
        }
        public static System.Drawing.Image Base64StringIntoImage(string str64)
        {
            byte[] img = Convert.FromBase64String(str64);
            MemoryStream ms = new MemoryStream(img);
            return System.Drawing.Image.FromStream(ms);
        }
        public static void retrieveImage(string username, PictureBox img)
        {
            string defaultImg = "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";
            if (img != null)
            {
                var res = otherFunc.conn().Get("images" + username);
                FirebaseData fd = res.ResultAs<FirebaseData>();
                if (fd != null && !string.IsNullOrEmpty(fd.imgString))
                {
                    img.Image = otherFunc.Base64StringIntoImage(fd.imgString);
                }
                else
                {
                    img.Image = otherFunc.Base64StringIntoImage(defaultImg);
                }
            }
        }

        public static int dateDifference(String GoalDate)
        {
            DateTime targetDate = DateTime.ParseExact(GoalDate, "yyyy-MM-dd", null);
            DateTime currentDate = DateTime.Now;
            int remainingDays = (int)(targetDate - currentDate).TotalDays;
            return remainingDays;
        }
        public async static void updateAllGoals()
        {
            GoalDetails GD = new GoalDetails(new wallet(), new group());
            String username = FirebaseData.Instance.Username;
            CollectionReference colRef = editInsideUser(username).Collection("Goals");
            QuerySnapshot qsnap = await colRef.GetSnapshotAsync();
            foreach (DocumentSnapshot dsnap in qsnap.Documents)
            {
                String docTitle = dsnap.Id;
                String goalDate = dsnap.GetValue<String>("GoalDate");
                int remainingDays = dateDifference(goalDate);
                if(remainingDays <= 0)
                {
                    DocumentReference dRef = dsnap.Reference;
                    Dictionary<String, object> data = new Dictionary<String, object>();
                    int code = await GD.checkSuggestion(docTitle, true, "");
                    if(code == 1)
                    {
                        data.Add("Status", "Achieved");
                    }
                    else
                    {
                        data.Add("Status", "Failed");
                    }
                    await dRef.UpdateAsync(data);
                }
            }

        }
        public async static void updateAllGroupGoals(string groupCode)
        {
            GoalDetails G = new GoalDetails(new wallet(), new group());
            string username  = FirebaseData.Instance.Username;
            CollectionReference colRef = editInsideGroup(groupCode).Collection("Goals");
            QuerySnapshot qsnap = await colRef.GetSnapshotAsync();
            foreach (DocumentSnapshot dsnap in qsnap.Documents)
            {
                String docTitle = dsnap.Id;
                String goalDate = dsnap.GetValue<String>("GoalDate");
                int remainingDays = dateDifference(goalDate);
                if (remainingDays <= 0)
                {
                    DocumentReference dRef = dsnap.Reference;
                    Dictionary<String, object> data = new Dictionary<String, object>();
                    int code = await G.checkSuggestion(docTitle, false, groupCode);
                    if (code == 1)
                    {
                        data.Add("Status", "Achieved");
                    }
                    else
                    {
                        data.Add("Status", "Failed");
                    }
                    await dRef.UpdateAsync(data);
                }
            }

        }

        public async Task<List<(string DocName, DocumentSnapshot DocSnapshot)>> getGoalsWithDocNames(String username)
        {
            CollectionReference colRef = editInsideUser(username).Collection("Goals");
            QuerySnapshot snap = await colRef.OrderByDescending("timestamp").GetSnapshotAsync();

            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = new List<(string, DocumentSnapshot)>();
            foreach (DocumentSnapshot docSnap in snap.Documents)
            {
                if (docSnap.Exists)
                {
                    string docName = docSnap.Id;
                    documentData.Add((docName, docSnap));
                }
            }
            return documentData;
        }
        public async Task<List<(string DocName, DocumentSnapshot DocSnapshot)>> getGoalsWithDocNamesGroup(String groupcode)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference colRef = db.Collection("Groups").Document(groupcode).Collection("Goals");
            QuerySnapshot snap = await colRef.OrderByDescending("timestamp").GetSnapshotAsync();

            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = new List<(string, DocumentSnapshot)>();
            foreach (DocumentSnapshot docSnap in snap.Documents)
            {
                if (docSnap.Exists)
                {
                    string docName = docSnap.Id;
                    documentData.Add((docName, docSnap));
                }
            }
            return documentData;
        }

        public async static Task<List<DocumentSnapshot>> displayRecurringData(String username)
        {
            CollectionReference colRef = editInsideUser(username).Collection("RecurringExpenses");
            QuerySnapshot qsnap = await colRef.OrderByDescending("timestamp").GetSnapshotAsync();

            List<DocumentSnapshot> data = new List<DocumentSnapshot>();
            foreach(DocumentSnapshot snap in qsnap.Documents)
            {
                if (snap.Exists)
                {
                    data.Add(snap);
                }
            }
            return data;

        }

        public async Task<List<(string DocName, DocumentSnapshot DocSnapshot)>> displayDataWithDocNames(string username)
        {
            CollectionReference colRef = editInsideUser(username).Collection("Expenses");
            QuerySnapshot snap = await colRef.OrderByDescending("timestamp").GetSnapshotAsync();

            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = new List<(string, DocumentSnapshot)>();

            foreach (DocumentSnapshot docSnap in snap.Documents)
            {
                if (docSnap.Exists)
                {
                    string docName = docSnap.Id;
                    documentData.Add((docName, docSnap));
                }
            }
            return documentData;
        }
        public async Task<List<(string DocName, DocumentSnapshot DocSnapshot)>> displayDataWithDocNamesGroup(string groupcode)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference colRef = db.Collection("Groups").Document(groupcode).Collection("Expenses");
            QuerySnapshot snap = await colRef.OrderByDescending("timestamp").GetSnapshotAsync();

            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = new List<(string, DocumentSnapshot)>();

            foreach (DocumentSnapshot docSnap in snap.Documents)
            {
                if (docSnap.Exists)
                {
                    string docName = docSnap.Id;
                    documentData.Add((docName, docSnap));
                }
            }
            return documentData;
        }
        public async Task<Dictionary<string, object>> getItemsInsideExpenseId(string username, string expenseId)
        {
            FirestoreDb db = otherFunc.FirestoreConn();
            DocumentReference docRef = db.Collection("Users").Document(username).Collection("Expenses").Document(expenseId);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if (snap.Exists)
            {
                Dictionary<string, object> data = snap.ToDictionary();
                return data;
            }
            return null;

        }
        public async Task<Dictionary<string, object>> getItemsInsideExpenseIdGroup(string groupcode, string expenseId)
        {
            FirestoreDb db = otherFunc.FirestoreConn();
            DocumentReference docRef = db.Collection("Groups").Document(groupcode).Collection("Expenses").Document(expenseId);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if (snap.Exists)
            {
                Dictionary<string, object> data = snap.ToDictionary();
                return data;
            }
            return null;

        }

        public async Task<Dictionary<string, object>> getItemsInsideGoalsID(string username, string goalsId)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(goalsId);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if (snap.Exists)
            {
                Dictionary<string, object> data = snap.ToDictionary();
                return data;
            }
            return null;
        }

        public async Task<Dictionary<string, object>> getItemsInsideGoalsIDGroup(string groupCode, string goalsId)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(goalsId);
            DocumentSnapshot snap = await docRef.GetSnapshotAsync();
            if (snap.Exists)
            {
                Dictionary<string, object> data = snap.ToDictionary();
                return data;
            }
            return null;
        }

        public static async void deleteInsideUser(String username, String collectionName, String docName)
        {
            DocumentReference docRef = editInsideUser(username).Collection(collectionName).Document(docName);
            await docRef.DeleteAsync();

        }

        public static async Task<String> getIP()
        {
            using (HttpClient hc = new HttpClient())
            {
                String ipifyUrl = "https://api.ipify.org?format=json";
                HttpResponseMessage response = await hc.GetAsync(ipifyUrl);
                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonResponse);
                    String publicAddress = (String)json["ip"];

                    //Console.WriteLine("Public IP Address: " + publicAddress);
                    return publicAddress;
                }
                else
                {
                    Console.WriteLine("Failed to retrieve the public IP address.");
                    return null;
                }
            }
        }

        public static async Task<bool> checkLog(String username)
        {
            var db = FirestoreConn();
            var query = db.Collection("Users").Document(username).Collection("Logs").Limit(1);
            var snapshot = await query.GetSnapshotAsync();
            return snapshot.Count > 0;
        }

        public static async void RecordLogs(String username, bool HasAccount, bool LoggingIn)
        {
            String IP = await getIP();
            using (var reader = new DatabaseReader("Resources//GeoLite2-City.mmdb"))
            {
                var response = reader.City(IP);

                double? latitude = response.Location.Latitude;
                double? longitude = response.Location.Longitude;

                String country = response.Country.Name;
                String city = response.City.Name;
                String region = response.MostSpecificSubdivision.Name;

                String address = $"{city}, {region}, {country}";

                DateTime currentUtcDateTime = DateTime.UtcNow;
                String timeApiUrl = $"https://maps.googleapis.com/maps/api/timezone/json?location={latitude},{longitude}&timestamp={(long)currentUtcDateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds}&key=YOUR_GOOGLE_TIMEZONE_API_KEY";

                using (HttpClient hc = new HttpClient())
                {
                    HttpResponseMessage timeResponse = await hc.GetAsync(timeApiUrl);
                    if (timeResponse.IsSuccessStatusCode)
                    {
                        DateTime localTime = currentUtcDateTime.ToLocalTime();
                        String Date = localTime.Date.ToString("yyyy-MM-dd");
                        String Time = localTime.TimeOfDay.ToString(@"hh\:mm\:ss");

                        var db = FirestoreConn();
                        DocumentReference docRef = db.Collection("Users").Document(username).Collection("Logs").Document(Date);
                        Dictionary<String, object> data = new Dictionary<String, object>();
                        DocumentSnapshot docsnap = await docRef.GetSnapshotAsync();
                        String item = Time + "|" + address;
                        if (HasAccount)
                        {
                            if (!docsnap.Exists)
                            {
                                await docRef.SetAsync(new Dictionary<String, object>()); //Create current date document if does not exists
                            }
                            if (LoggingIn)
                            {
                                await docRef.UpdateAsync("Login", FieldValue.ArrayUnion(item));
                            }
                            else
                            {
                                await docRef.UpdateAsync("Logout", FieldValue.ArrayUnion(Time));
                            }
                        }
                        else
                        {

                            ArrayList Log = new ArrayList();
                            Log.Add(item);


                            data.Add("Login", Log);

                            await docRef.SetAsync(data);
                        }
                    }
                }
            }
        }
        public static void sendOTP(string email, changePassword cp)
        {
            Tuple<string, DateTime> savedOTP = OTPManager.LoadOTP();
            if (savedOTP != null && DateTime.Now < savedOTP.Item2)
            {
                MessageBox.Show("You still have a valid OTP. Please use the existing one.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Generate new OTP
            Tuple<string, DateTime> otpTuple = generateOTPWithExpiration();
            string otp = otpTuple.Item1;
            cp.myOTP = otp;
            DateTime expirationTime = otpTuple.Item2;

            // Save the new OTP
            OTPManager.SaveOTP(otp, expirationTime);

            MailMessage message = new MailMessage();
            message.From = new MailAddress("expensetracker273@gmail.com");
            message.To.Add(email);
            message.Subject = "One-Time Password (OTP)";
            message.Body = "Your OTP is: " + otp;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential("expensetracker273@gmail.com", "kfei gmyb dukz sgli");
            smtpClient.EnableSsl = true;
            DateTime currentTime = DateTime.Now;

            try
            {
                smtpClient.Send(message);
                MessageBox.Show("Email sent successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private static Tuple<string, DateTime> generateOTPWithExpiration()
        {
            Random ran = new Random();
            int otp = ran.Next(100000, 999999);

            // Set expiration time to 5 minutes from the current time
            DateTime expirationTime = DateTime.Now.AddMinutes(5);

            return new Tuple<string, DateTime>(otp.ToString(), expirationTime);
        }
        public static bool compareOTP(string otp, string inputOTP)
        {
            if (string.IsNullOrEmpty(inputOTP))
            {
                return false;
            }
            return otp.Equals(inputOTP);
        }
        public static async Task<DataTable> GetExpensesGroupedByDate(string username)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference expensesCollection = editInsideUser(username).Collection("Expenses");
            QuerySnapshot expensesSnapshot = await expensesCollection.GetSnapshotAsync();

            DataTable expensesTable = new DataTable();
            expensesTable.Columns.Add("Date", typeof(DateTime));
            expensesTable.Columns.Add("Amount", typeof(double));

            foreach (DocumentSnapshot expenseDoc in expensesSnapshot.Documents)
            {
                Dictionary<string, object> expenseData = expenseDoc.ToDictionary();
                if (expenseData.TryGetValue("Date", out var dateObj) &&
                    DateTime.TryParseExact(dateObj.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) &&
                    expenseData.TryGetValue("Amount", out var amountObj) &&
                    double.TryParse(amountObj.ToString(), out double amount))
                {
                    DataRow existingRow = expensesTable.Rows.Cast<DataRow>()
                        .FirstOrDefault(row => (DateTime)row["Date"] == date);

                    if (existingRow != null)
                    {
                        existingRow["Amount"] = (double)existingRow["Amount"] + amount;
                    }
                    else
                    {
                        DataRow newRow = expensesTable.NewRow();
                        newRow["Date"] = date;
                        newRow["Amount"] = amount;
                        expensesTable.Rows.Add(newRow);
                    }
                }
            }

            return expensesTable;
        }
        public static async Task<DataTable> GetExpensesGroupedByCategories(string username)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference expensesCollection = editInsideUser(username).Collection("Expenses");
            QuerySnapshot expensesSnapshot = await expensesCollection.GetSnapshotAsync();

            DataTable expensesTable = new DataTable();
            expensesTable.Columns.Add("Date", typeof(string));
            expensesTable.Columns.Add("Category", typeof(string));
            expensesTable.Columns.Add("Amount", typeof(double));

            foreach (DocumentSnapshot expenseDoc in expensesSnapshot.Documents)
            {
                Dictionary<string, object> expenseData = expenseDoc.ToDictionary();
                if (expenseData.TryGetValue("Date", out var dateObj) &&
                    DateTime.TryParseExact(dateObj.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) &&
                    expenseData.TryGetValue("Category", out var categoryObj) &&
                    expenseData.TryGetValue("Amount", out var amountObj) &&
                    double.TryParse(amountObj.ToString(), out double amount))
                {
                    DataRow existingRow = expensesTable.Rows.Cast<DataRow>()
                            .FirstOrDefault(row => (string)row["Category"] == categoryObj.ToString() &&
                            DateTime.TryParse(row["Date"].ToString(), out DateTime rowDate) &&
                            rowDate == date);

                    if (existingRow != null)
                    {
                        existingRow["Amount"] = (double)existingRow["Amount"] + amount;
                    }
                    else
                    {
                        DataRow newRow = expensesTable.NewRow();
                        newRow["Date"] = date;
                        newRow["Category"] = categoryObj.ToString();
                        newRow["Amount"] = amount;
                        expensesTable.Rows.Add(newRow);
                    }
                }
            }
            return expensesTable;
        }
        public static async Task<DataTable> GetTransactions(string username)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference expensesCollection = editInsideUser(username).Collection("Expenses");
            QuerySnapshot expensesSnapshot = await expensesCollection.GetSnapshotAsync();

            DataTable transactionsTable = new DataTable();
            transactionsTable.Columns.Add("Date", typeof(string));
            transactionsTable.Columns.Add("Amount", typeof(double));

            foreach (DocumentSnapshot expenseDoc in expensesSnapshot.Documents)
            {
                Dictionary<string, object> expenseData = expenseDoc.ToDictionary();
                if (expenseData.TryGetValue("Date", out var dateObj) &&
                    DateTime.TryParseExact(dateObj.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) &&
                    expenseData.TryGetValue("Amount", out var amountObj) &&
                    double.TryParse(amountObj.ToString(), out double amount))
                {
                    DataRow newRow = transactionsTable.NewRow();
                    newRow["Date"] = date;
                    newRow["Amount"] = amount;
                    transactionsTable.Rows.Add(newRow);
                }
            }
            return transactionsTable;
        }

        public static async Task<double> getTotalGroupGoalAmount(string groupCode)
        {
            double totalAmount = 0;
            CollectionReference colRef = editInsideGroup(groupCode).Collection("Goals");
            QuerySnapshot qSnap = await colRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in qSnap.Documents)
            {
                double amount = docSnap.GetValue<double>("Amount");
                totalAmount += amount;
            }
            return totalAmount;
        }
        public static async Task<double> getTotalGoalAmount(String username)
        {
            double totalAmount = 0;
            CollectionReference colRef = editInsideUser(username).Collection("Goals");
            QuerySnapshot qSnap = await colRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in qSnap.Documents)
            {
                double amount = docSnap.GetValue<double>("Amount");
                totalAmount += amount;
            }

            return totalAmount;

        }

        public static async Task updatePercentagePerGoal(String username)
        {
            double totalAmount = await getTotalGoalAmount(username);
            CollectionReference colRef = editInsideUser(username).Collection("Goals");
            QuerySnapshot qSnap = await colRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in qSnap.Documents)
            {
                DocumentReference docRef = docSnap.Reference;
                double amount = docSnap.GetValue<double>("Amount");
                double percentage = amount / totalAmount;
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"Percentage", percentage}
                };

                await docRef.UpdateAsync(data);
            }
        }
        public static async Task updatePercentagePerGroupGoal(string groupCode)
        {
            double totalAmount = await getTotalGroupGoalAmount(groupCode);
            CollectionReference colRef = editInsideGroup(groupCode).Collection("Goals");
            QuerySnapshot qSnap = await colRef.GetSnapshotAsync();

            foreach (DocumentSnapshot docSnap in qSnap.Documents)
            {
                DocumentReference docRef = docSnap.Reference;
                double amount = docSnap.GetValue<double>("Amount");
                double percentage = amount / totalAmount;
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"Percentage", percentage}
                };

                await docRef.UpdateAsync(data);
            }
        }

        public static async Task<double> getCurrentSavings(String username, String titleGoal)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            double percentage = dSnap.GetValue<double>("Percentage");
            DocumentReference docRefWallet = editInsideUser(username).Collection("Wallets").Document("Balance");
            DocumentSnapshot dSnapWallet = await docRefWallet.GetSnapshotAsync();
            double amountWallet = dSnapWallet.GetValue<double>("Amount");

            double currentSavings = amountWallet * percentage;

            return currentSavings;
        }

        public static async Task<double> getCurrentSavingsGroup(String groupCode, String titleGoal)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            double percentage = dSnap.GetValue<double>("Percentage");
            DocumentReference docRefWallet = editInsideGroup(groupCode).Collection("Wallets").Document("Balance");
            DocumentSnapshot dSnapWallet = await docRefWallet.GetSnapshotAsync();
            double amountWallet = dSnapWallet.GetValue<double>("Amount");

            double currentSavings = amountWallet * percentage;

            return currentSavings;
        }

        public async static Task<double> getGoalAmount(String username, String titleGoal)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            double GoalAmount = dSnap.GetValue<double>("Amount");

            return GoalAmount;
        }

        public async static Task<double> getGoalAmountGroup(String groupCode, String titleGoal)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            double GoalAmount = dSnap.GetValue<double>("Amount");

            return GoalAmount;
        }

        public async static Task<int> dateTargetMinusCurrent(String username, String titleGoal)
        {
            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            String sDate = dSnap.GetValue<String>("GoalDate");
            DateTime fDate = DateTime.ParseExact(sDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Today;

            int daysDifference = (int)(fDate - currentDate).TotalDays;

            return daysDifference;

        }
        public async static Task<int> dateTargetMinusCurrentGroup(String groupCode, String titleGoal)
        {
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            String sDate = dSnap.GetValue<String>("GoalDate");
            DateTime fDate = DateTime.ParseExact(sDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime currentDate = DateTime.Today;

            int daysDifference = (int)(fDate - currentDate).TotalDays;

            return daysDifference;

        }

        public async static Task<int> dateCurrentMinusStart(String username, String titleGoal)
        {
            DateTime currentDate = DateTime.Today;
            DocumentReference docRef = editInsideUser(username).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            Timestamp fTimeStamp = dSnap.GetValue<Timestamp>("timestamp");
            DateTime startedDate = fTimeStamp.ToDateTime();

            int daysDifference = (int)(currentDate - startedDate).TotalDays;

            return daysDifference;
        }
        public async static Task<int> dateCurrentMinusStartGroup(String groupCode, String titleGoal)
        {
            DateTime currentDate = DateTime.Today;
            DocumentReference docRef = editInsideGroup(groupCode).Collection("Goals").Document(titleGoal);
            DocumentSnapshot dSnap = await docRef.GetSnapshotAsync();
            Timestamp fTimeStamp = dSnap.GetValue<Timestamp>("timestamp");
            DateTime startedDate = fTimeStamp.ToDateTime();

            int daysDifference = (int)(currentDate - startedDate).TotalDays;

            return daysDifference;
        }
        public async Task<string[]> getGroups(string username)
        {
            otherFunc o = new otherFunc();
            DocumentSnapshot docsnap = await o.logInFunc(username);

            if (docsnap.Exists)
            {
                if (docsnap.ContainsField("Groups"))
                {
                    return docsnap.GetValue<string[]>("Groups");
                }
                else
                {
                    return new string[0];
                }

            }
            return null;
        }
        public static String dateBeautifyForRE(String dateText, String period)
        {
            //Console.WriteLine($"date: {dateText} | period: {period}");
            String toBeReturned = "";
            switch (period)
            {
                case "Daily":
                    toBeReturned = "Everyday";
                    break;
                case "Weekly":
                    String[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                    int ndays = int.Parse(dateText);
                    toBeReturned = "Every " + days[ndays - 1];

                    break;
                case "Monthly":
                    char[] dayCA = dateText.ToCharArray();
                    if (dayCA[dayCA.Length-1] == '1')
                    {
                        toBeReturned = dateText + "st";
                    } else if(dayCA[dayCA.Length - 1] == '2')
                    {
                        toBeReturned = dateText + "nd";
                    }
                    else if (dayCA[dayCA.Length - 1] == '3')
                    {
                        toBeReturned = dateText + "rd";
                    }
                    else
                    {
                        toBeReturned = dateText + "th";
                    }
                    break;
                default:
                    toBeReturned = "";
                    break;
            }
            return toBeReturned;
        }
        public async Task<string[]> getMembers(string groupCode)
        {
            var db = otherFunc.FirestoreConn();
            DocumentReference docref = db.Collection("Groups").Document(groupCode);
            DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

            if(docsnap.Exists)
            {
                return docsnap.GetValue<string[]>("Members");
            }
            return null;
        }
        public async Task<string> getFirstname(string username)
        {
            var db = otherFunc.FirestoreConn();
            DocumentReference colref = db.Collection("Users").Document(username);
            DocumentSnapshot docsnap = await colref.GetSnapshotAsync();

            if (docsnap.Exists)
            {
                return docsnap.GetValue<string>("First Name");
            }
            return null;
        }

        public async static Task<String> getFullName(String username)
        {
            String name = "";
            DocumentReference docref = editInsideUser(username);
            DocumentSnapshot docSnap = await docref.GetSnapshotAsync();
            if (docSnap.Exists)
            {
                String fname = docSnap.GetValue<String>("First Name");
                String lname = docSnap.GetValue<String>("Last Name");
                name = $"{fname} {lname}";
                return name;
            }
            return "Cannot find it...";

        }
    }
}
