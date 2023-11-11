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
    public partial class changePassword : Form
    {
        private Guna.UI2.WinForms.Guna2TextBox gunaTextBox;
        public string myOTP;
        public DateTime otpExpirationTime;
        public changePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPass.Text)) {
                MessageBox.Show("Enter OTP","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            string inputOTP = txtNewPass.Text;
            string generatedOTP = myOTP;
            bool isEqualOTP = otherFunc.compareOTP(inputOTP, generatedOTP);
            if (isEqualOTP){

            }
            else{
                MessageBox.Show("OTP does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure yout want to cancel?", "Cancel", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                this.Hide();
                Login l = new Login();
                l.Show();
            }
        }
        async void retrieveInfo()
        {
            try
            {
                string username = FirebaseData.Instance.Username;
                otherFunc o = new otherFunc();
                DocumentSnapshot snap = await o.logInFunc(username);
                if (snap.Exists)
                {
                    FirebaseData fd = snap.ConvertTo<FirebaseData>();
                    lblFullname.Text = fd.FirstName + " " + fd.LastName;
                    lblUsername.Text = fd.Username;
                    txtEmail.Text = fd.Email;
                    otherFunc.retrieveImage(username, pbProfile);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void changePassword_Load(object sender, EventArgs e)
        {
            retrieveInfo();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
           //gunaTextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;

        
           // string input = txtEmail.Text;

           // int atIndex = input.IndexOf('@');

           // if (atIndex > 1)
           // {
           //     string extracted = input.Substring(0, atIndex);
           //     string maskedText = extracted[0] + new string('*', extracted.Length - 2) + extracted[extracted.Length - 1] + input.Substring(atIndex);
           //     gunaTextBox.Text = maskedText;
           // }
        }
        
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            otherFunc.sendOTP(txtEmail.Text, this);
        }
    }
}
