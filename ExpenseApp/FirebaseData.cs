using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApp
{
    [FirestoreData]
    internal class FirebaseData
    {
        [FirestoreProperty]
        public String Username { get; set; }

        [FirestoreProperty]
        public String Password { get; set; }
    }
}
