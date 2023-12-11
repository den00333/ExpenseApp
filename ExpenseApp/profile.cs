using FireSharp.Interfaces;
using Google.Cloud.Firestore;
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
                Home h = new Home();

                h.getFirstName(username);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateAcc ua = new updateAcc(this);
            ua.ShowDialog();

        }
    }
}
