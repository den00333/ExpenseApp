using FireSharp.Interfaces;
using Google.Cloud.Firestore;
using Google.Protobuf;
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
        private profile profile;
        public updateAcc(profile p)
        {
            InitializeComponent();
            this.profile = p;
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
            string username = FirebaseData.Instance.Username;
            try
            {
                IFirebaseClient client = otherFunc.conn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            updateAccount();
            otherFunc.retrieveImage(username, pbPic);
        }
        async void updateAccount()
        {
            Security s = new Security();
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                txtFirstname.Text = fd.FirstName;
                txtLastname.Text = fd.LastName;
                txtEmail.Text = fd.Email;
                rtbBio.Text = fd.Bio;
                txtPassword.Text = Security.Decrypt(fd.Password.ToString());
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            otherFunc o = new otherFunc();
            string firstname = txtFirstname.Text;
            string lastname = txtLastname.Text;
            string email = txtEmail.Text;
            string bio = rtbBio.Text;
            string password = txtPassword.Text;
            profile p = profile;

            string username = FirebaseData.Instance.Username;

            o.updateData(username, firstname, lastname, email, bio, password, this, p);
            storeImage();
        }

        private void txtFirstname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Cannot Enter Numerical Values and Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLastname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Cannot Enter Numerical Values and Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void storeImage()
        {
            string username = FirebaseData.Instance.Username;
            

            FirebaseData fd = new FirebaseData()
            {
                imgString = otherFunc.ImageIntoBase64String(pbPic)
            };
            var set = otherFunc.conn().Set("images" + username, fd);
        }
        
    }
}
