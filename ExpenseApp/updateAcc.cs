using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class updateAcc : Form
    {
        public updateAcc()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pbPic.ImageLocation = imageLocation;
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void updateAcc_Load(object sender, EventArgs e)
        {
            updateAccount();
        }
        async void updateAccount()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                txtFirstname.Text = fd.FirstName;
                txtUsername.Text = fd.Username;
                txtLastname.Text = fd.LastName;
                txtEmail.Text = fd.Email;
                rtbBio.Text = fd.Bio;
            }
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            otherFunc o = new otherFunc();
            string firstname = txtFirstname.Text;
            string lastname = txtFirstname.Text;
            string email = txtEmail.Text;
            string username = txtUsername.Text;
            string bio = rtbBio.Text;
            
            o.updateData(username, firstname, lastname, email, bio, this);
        }
    }
}
