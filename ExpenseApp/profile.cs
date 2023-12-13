using FireSharp.Interfaces;
using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class profile : UserControl
    {
        string username = FirebaseData.Instance.Username;
        public profile()
        {
            InitializeComponent();
        }

        
        private void profile_Load(object sender, EventArgs e)
        {
            try
            {
                IFirebaseClient client = otherFunc.conn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            displayProfile();
            displayLogs();
        }
        public async void displayProfile()
        {
            otherFunc.retrieveImage(username, pbProfilepic);
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                Timestamp timestamp = fd.DateCreated;
                lblFullname.Text = fd.FirstName + " " + fd.LastName;
                lblEmail.Text = fd.Email;
                lblUsername.Text = fd.Username;
                lblBio.Text = fd.Bio;
                if (timestamp != null)
                {
                    DateTime date = timestamp.ToDateTime();
                    string dateString = date.ToString("yyyy-MM-dd");
                    lblDate.Text = dateString;
                }
                homeForm home = new homeForm();
                home.lblFirstname.Text = await otherFunc.getFirstname(username);
            }
        }
        async Task<List<List<String>>> getDate(string username)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference colref = db.Collection("Users").Document(username).Collection("Logs");
            QuerySnapshot qsnap = await colref.GetSnapshotAsync();
            List<List<String>> arrL = new List<List<String>>();
            foreach(DocumentSnapshot docsnap in qsnap.Documents)
            {
                List<String> arrLogs = docsnap.GetValue<List<String>>("Login");
                arrL.Add(arrLogs);
            }
            return arrL;
        } 
        async void displayLogs()
        {
             List<List<String>> logs = await getDate(username);
            foreach (List<String> innerlist in logs)
            {
                foreach (string logEntry in innerlist)
                {
                    string[] timeLocation = logEntry.Split('|');

                    string time = timeLocation[0];
                    string location = timeLocation[1];

                    Guna2Panel pnl = new Guna2Panel();
                    pnl.Size = new Size(465, 100);
                    pnl.BorderRadius = 15;
                    pnl.FillColor = Color.FromArgb(83, 123, 47);

                    Label lblTime = new Label();
                    lblTime.Font = new Font("Poppins", 9.75f, FontStyle.Regular);
                    lblTime.Size = new Size(131, 23);
                    lblTime.Location = new Point(38, 19);
                    lblTime.BackColor = Color.Transparent;
                    lblTime.ForeColor = Color.White;
                    lblTime.Text = time;

                    Label lblLocation = new Label();
                    lblLocation.Font = new Font("Poppins", 9.75f, FontStyle.Regular);
                    lblLocation.Size = new Size(380, 23);
                    lblLocation.Location = new Point(38, 46);
                    lblLocation.BackColor = Color.Transparent;
                    lblLocation.ForeColor = Color.White;
                    lblLocation.Text = location;
                    

                    pnl.Controls.Add(lblLocation);
                    pnl.Controls.Add(lblTime);
                    flpLogs.Controls.Add(pnl);
                }
            }
            
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            updateAcc ua = new updateAcc(this);
            ua.ShowDialog();
        }
    }
}
