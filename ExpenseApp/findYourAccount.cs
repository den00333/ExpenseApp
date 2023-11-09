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
    public partial class findYourAccount : Form
    {
        public findYourAccount()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            findAccount();
        }
        async void findAccount()
        {
            string username = txtUsername.Text;
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                this.Hide();
                FirebaseData.Instance.Username = username;
                changePassword cp = new changePassword();
                cp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Account Doesn't Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
