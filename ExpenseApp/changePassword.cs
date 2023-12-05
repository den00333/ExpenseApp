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
        private string email = string.Empty;
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
            DialogResult result = MessageBox.Show("Are you sure you want to cancel retrieving your account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
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
            gunaTextBox = (Guna.UI2.WinForms.Guna2TextBox)sender;
            string input = txtEmail.Text;

            int atIndex = input.IndexOf('@');

            if (atIndex > 1)
            {
                string extracted = input.Substring(0, atIndex);
                string maskedText = extracted[0] + new string('*', extracted.Length - 2) + extracted[extracted.Length - 1] + input.Substring(atIndex);
                gunaTextBox.Text = maskedText;
                email = input;
            }
        }
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            otherFunc.sendOTP(email, this);
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
            DialogResult result = MessageBox.Show("Are you sure you want to cancel retrieving your account?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);            
            if (result == DialogResult.Yes){
                this.Hide();
            }
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
                    if (otpData != null)
                    {
                        DateTime expirationTime = otpData.Item2;
                        string storedOTP = otpData.Item1;
                        bool isEqualStoredOTP = otherFunc.compareOTP(inputOTP, storedOTP);
                        if (isEqualStoredOTP)
                        {
                            if (DateTime.Now < expirationTime)
                            {
                                panelPassword.Visible = true;
                                panelPassword.BringToFront();
                            }
                            else
                            {
                                MessageBox.Show("OTP has expired. Click the button to send a new one.", "OTP Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("OTP does not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else {
                        MessageBox.Show("Please click the button to send new OTP", "OTP Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

       

        private void txtNewPass_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
