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
        public string username = FirebaseData.Instance.Username;
        public changePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            compareOTP();
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
            panelPassword.Visible= false;

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

        private void btnSavepass_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string confirmPass = txtConfirmPass.Text;
            if(string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirmPass.Text)) {
                MessageBox.Show("Please input your password", "Input credentials", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            otherFunc function = new otherFunc();
            bool equalPassword = function.passwordMatched(password, confirmPass);
            bool isValidPassword = function.isValidPassword(password);

            if(isValidPassword) {
                if (equalPassword){
                    function.updatePassword(username,Security.Encrypt(password));
                    MessageBox.Show("Successfully updated your password!", "Success", MessageBoxButtons.OK);
                    OTPManager.ClearOTP();
                    this.Hide();
                }
                else{
                    MessageBox.Show("Password does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else{
                MessageBox.Show("Password must contain uppercase and special character", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            string pass = txtPassword.Text;

            if (!string.IsNullOrEmpty(txtPassword.Text)){
                if (pass.Length > 0 && pass.Length < 8){
                    errorProvider.SetError(txtPassword, "Password must be at least 8 characters long");
                }
                else if ((!pass.Any(char.IsUpper) || !pass.Any(char.IsLower))){
                    errorProvider.SetError(txtPassword, "Password must contain uppercase");
                }
                else if (!pass.Any(char.IsPunctuation)){
                    errorProvider.SetError(txtPassword, "Password must contain at least one symbol");
                }
            }
            else{
                errorProvider.Clear();
            }
        }

        private void showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(showPassword.Checked){
                txtPassword.PasswordChar = '\0';
                txtConfirmPass.PasswordChar = '\0';
            }
            else{
                txtPassword.PasswordChar = '●';
                txtConfirmPass.PasswordChar = '●';
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void compareOTP()
        {
            string inputOTP = txtNewPass.Text;

            if (string.IsNullOrEmpty(inputOTP)){
                MessageBox.Show("Please enter OTP", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else{
                string generatedOTP = myOTP;
                bool isEqualOTP = otherFunc.compareOTP(inputOTP, generatedOTP);

                if (isEqualOTP){
                    panelPassword.Visible = true;
                    panelPassword.BringToFront();
                }
                else{
                    Tuple<string, DateTime> otpData = OTPManager.LoadOTP();
                    DateTime expirationTime = otpData.Item2;
                    string storedOTP = otpData.Item1;
                    bool isEqualStoredOTP = otherFunc.compareOTP(inputOTP, storedOTP);
                    if (isEqualStoredOTP){
                        if (DateTime.Now < expirationTime){
                            panelPassword.Visible = true;
                            panelPassword.BringToFront();
                        }
                        else{
                            MessageBox.Show("OTP has expired. Click the button to send a new one.", "OTP Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else{
                        MessageBox.Show("OTP does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
