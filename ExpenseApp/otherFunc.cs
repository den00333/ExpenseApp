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
        public bool isValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
