using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp
{
    [FirestoreData]
    public class FirebaseData
    {
        private static FirebaseData instance;

        public static FirebaseData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FirebaseData();
                }
                return instance;
            }
        }
        [FirestoreProperty]
        public String Username { get; set; }

        [FirestoreProperty]
        public String Password { get; set; }

        [FirestoreProperty("First Name")]
        public String FirstName { get; set; }

        [FirestoreProperty("Last Name")]
        public String LastName { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }

        [FirestoreProperty]
        public string Bio { get; set; }

        [FirestoreProperty]
        public string Category { get; set; }

        [FirestoreProperty]
        public int Amount { get; set; }
        public string imgString {get; set;}
        [FirestoreProperty]
        public string Date { get; set; }
        [FirestoreProperty]
        public string GroupName { get; set; }
        [FirestoreProperty]
        public string status {  get; set; }
        [FirestoreProperty]
        public string Creator { get; set; }

    }

    public class ctg 
    {
        [JsonProperty("listOfCategories")]
        public List<String> LCategory { get; set; }
    }

}
