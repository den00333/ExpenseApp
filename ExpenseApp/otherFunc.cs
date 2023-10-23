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

        public async Task<String> DocNameForExpenses(String username)
        {
            String Ename = null;
            FirestoreDb database = FirestoreConn();
            CollectionReference cRef = database.Collection("Users").Document(username).Collection("Expenses");
            Query q = cRef.OrderByDescending("timestamp").Limit(1);
            QuerySnapshot qSnap = await q.GetSnapshotAsync();
            if(qSnap.Count > 0)
            {
                DocumentSnapshot docSnap = qSnap.Documents[0];
                String docName = docSnap.Id;
                int num = int.Parse(docName.Trim('E'))+1;
                Ename = string.Concat("E", num.ToString());
                return Ename;

            }
            else
            {
                Ename = "E1";
                return Ename;
            }
            
        }

        public async Task<DocumentReference> SavingNewExpenses(String username)
        {
            String docName = await DocNameForExpenses(username);
            FirestoreDb database = FirestoreConn();
            DocumentReference docRef = database.Collection("Users").Document(username).Collection("Expenses").Document(docName);
            
            return docRef;

            /*document name should be "E1" then iterate*/
            /*Process: 
             * check if the "Expenses" collection has existing document("E1"*) 
             else create new which starts from "E1"*/
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

        bool AreTextboxesEmpty(params string[] textboxes)
        {
            foreach (string textbox in textboxes){
                if (string.IsNullOrWhiteSpace(textbox)){
                    return true;
                }
            }
            return false;
        }
           
        public async void signingUp(String username, String fname, String lname, String email, String password, String repeatpass, CheckBox terms, Signup s)
        {
            var database = FirestoreConn();
            otherFunc function = new otherFunc();
            bool validEmail = otherFunc.isValidEmail(email);
            bool validUsername = await otherFunc.isUsernameExistingAsync(username);
            bool isEmpty = AreTextboxesEmpty(fname,lname, email, username, password, repeatpass);
            bool passwordMatched = function.passwordMatched(password, repeatpass);

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
                            try{
                                DocumentReference docRef = database.Collection("Users").Document(username);
                                Dictionary<string, object> data = new Dictionary<string, object>(){
                                    {"First Name", fname },
                                    {"Last Name", lname },
                                    {"Username", username },
                                    {"Email", email },
                                    {"Password", password}
                                    };
                                await docRef.SetAsync(data);
                                DialogResult res = MessageBox.Show("Successfully created your account!", "Success", MessageBoxButtons.OK);
                                if (res == DialogResult.OK){
                                    s.Close();
                                }
                            }
                            catch (Exception ex){
                                MessageBox.Show("Cannot process your account", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else{
                            MessageBox.Show("Password do not matched", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            return password1.Trim() == password2.Trim();
        }

        public static void populateCMBcategory(ctg category, AddExpensesForm f)
        {
            f.cmbCategory.Items.Clear();
            foreach (var c in category.LCategory)
            {
                f.cmbCategory.Items.Add(c);
            }
        }
    }
}
