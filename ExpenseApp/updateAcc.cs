using FireSharp.Interfaces;
using Google.Cloud.Firestore;
using Google.Protobuf;
using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ExpenseApp
{
    public partial class updateAcc : Form
    {
        string username = FirebaseData.Instance.Username;
        private profile profile;
        string password = string.Empty;
        String fname, lname, email, userN, pass, repeatpass;
        bool flag = false;
        public string otp;
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
            catch (Exception ex)
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

            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                txtFirstname.Text = fd.FirstName;
                txtLastname.Text = fd.LastName;
                txtEmail.Text = fd.Email;
                rtbBio.Text = fd.Bio;
                password = Security.Decrypt(fd.Password.ToString());
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            otherFunc o = new otherFunc();
            fname = txtFirstname.Text;
            lname = txtLastname.Text;
            email = txtEmail.Text;
            string bio = rtbBio.Text;
            pass = txtNewPass.Text;
            repeatpass = txtConfirmPass.Text;
            profile p = profile;
            bool isEmpty = otherFunc.areControlEmpty(fname, lname, pass, repeatpass, email);
            bool hasInternet = otherFunc.internetConn();
            if (hasInternet)
            {
                otherFunc function = new otherFunc();
                if (!isEmpty)
                {
                    if (flag)
                    {
                        o.updateData(username, fname, lname, email, bio, pass, repeatpass, this, p);
                        storeImage();
                    }
                    else
                    {
                        MessageBox.Show("Email not verified!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            checkPassword();
        }
        public void checkPassword()
        {
            string pass = txtCurrentPassword.Text;

            if (!string.IsNullOrEmpty(txtCurrentPassword.Text))
            {
                if (pass == password)
                {
                    txtNewPass.Enabled = true;
                    txtConfirmPass.Enabled = true;
                    errorProvider.Clear();
                }
                else
                {
                    errorProvider.SetError(txtCurrentPassword, "Invalid Current Password");
                }
            }
            else
            {
                txtNewPass.Enabled = false;
                txtConfirmPass.Enabled = false;
                errorProvider.Clear();
            }
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            checkPassword();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            Tuple<string, DateTime, string> storedOTP = OTPManager.LoadOTP();
            string prevEmail = storedOTP.Item3;
            string inputOTP = txtOTP.Text.ToString();

            if (inputOTP.Equals(storedOTP.Item1))
            {
                txtOTP.Visible = false;
                btnVerify.Visible = false;
                btnSendOTP.Visible = false;
                txtEmail.Text = email;
                ptbVerified.Visible = true;
                MessageBox.Show("Email Verified", "Success", MessageBoxButtons.OK);
                flag = true;
                txtEmail.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Please enter your OTP", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtLastname_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && char.IsLower(textBox.Text[0]))
            {
                textBox.Text = char.ToUpper(textBox.Text[0]) + textBox.Text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void txtFirstname_TextChanged(object sender, EventArgs e)
        {
            Guna2TextBox textBox = (Guna2TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && char.IsLower(textBox.Text[0]))
            {
                textBox.Text = char.ToUpper(textBox.Text[0]) + textBox.Text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void UpdatePasswordMatchLabel()
        {
            if (string.IsNullOrWhiteSpace(txtNewPass.Text) && string.IsNullOrWhiteSpace(txtConfirmPass.Text) || string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                checkPass.Text = "";
            }
            else if (txtNewPass.Text == txtConfirmPass.Text)
            {
                checkPass.Text = "Password Matched";
                checkPass.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                checkPass.Text = "Password Doesn't Match";
                checkPass.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordMatchLabel();
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordMatchLabel();
        }

        private void btnSendOTP_Click(object sender, EventArgs e)
        {
            Tuple<string, DateTime, string> storedOTP = OTPManager.LoadOTP();
            DateTime expireDate = storedOTP.Item2;
            DateTime currentDate = DateTime.Now;
            string prevCode = storedOTP.Item1;
            string prevEmail = storedOTP.Item3;

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Enter your email", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (otherFunc.isValidEmail(txtEmail.Text.Trim()))
                {
                    string inputEmail = txtEmail.Text.Trim();
                    if (expireDate > currentDate && prevEmail.Equals(inputEmail))
                    {
                        MessageBox.Show("You still have a valid OTP. Please use the existing one.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        otherFunc.sendOTP(inputEmail, false);
                        txtOTP.Visible = true;
                        btnVerify.Visible = true;
                        email = inputEmail;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid email", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void otpTimer_Tick(object sender, EventArgs e)
        {
            txtOTP.Visible = false;
            btnVerify.Visible = false;
            otpTimer.Stop();
        }
    }
}