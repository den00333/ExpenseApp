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
        public profile()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            updateAcc ua = new updateAcc();
            ua.ShowDialog();
        }
        async void diplayProfile()
        {

        }

        private void profile_Load(object sender, EventArgs e)
        {
            displayProfile();
        }
        async void displayProfile()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                lblFirstname.Text = fd.FirstName;
                lblFirstname1.Text = fd.FirstName;
                lblUsername.Text = fd.Username;
                lblLastname.Text = fd.LastName;
                lblEmail.Text = fd.Email;
                rtbBio.Text = fd.Bio;
            }
        }
    }
}
