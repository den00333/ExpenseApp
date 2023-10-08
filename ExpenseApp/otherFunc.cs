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

namespace ExpenseApp
{
    internal class otherFunc
    {
        
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

            if(L.Count == 0)
            {
                return true;
            }
            Signup.runErrorMsg(L);
            return false;
        }

        public async void signingUp(String username, String fname, String lname, String email, String password, Signup s)
        {
            var database = FirestoreConn();

            bool validEmail = otherFunc.isValidEmail(email);
            bool validUsername = await otherFunc.isUsernameExistingAsync(username);

            Dictionary<String, bool> validatingData = new Dictionary<string, bool>()
            {
                { "username", !validUsername},
                { "email", validEmail}
            };

            otherFunc o = new otherFunc();
            bool validData = o.isValidData(validatingData);
            
            if (validData)
            {
                try
                {
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
                    if(res == DialogResult.OK)
                    {
                        
                        s.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot process your account", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        } 
    }
}
