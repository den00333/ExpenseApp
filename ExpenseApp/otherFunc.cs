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

namespace ExpenseApp
{
    internal class otherFunc
    {
        public static IFirebaseClient conn()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "LUA3lFfqrsEMSysOLxV5Lt6ZtDwVeFZ7UNTHDPGe",
                BasePath = "https://xpnsetracker-default-rtdb.asia-southeast1.firebasedatabase.app/"
            };

            IFirebaseClient client = new FirebaseClient(config);
            return client;

        }
    }
}
