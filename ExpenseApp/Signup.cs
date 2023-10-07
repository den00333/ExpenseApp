using Google.Cloud.Firestore;
using Guna.UI2.WinForms.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class Signup : Form
    {
        FirestoreDb database;
        String fname, lname, email, username, password;
        public Signup()
        {
            InitializeComponent();
        }
        private void closeBTN_Click(object sender, EventArgs e)
        {
            /*bool notEmptyTextBox = false;
            foreach (Control control in signupPanel.Controls){
                if (control is Guna.UI2.WinForms.Internal.PlaceholderTextBox textbox){
                    if (!string.IsNullOrEmpty(textbox.Text)){
                        notEmptyTextBox = true;
                        break;
                    }
                }
            }
            if (notEmptyTextBox) {
                DialogResult result = MessageBox.Show("Are you sure you want to close?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No){
                    return;
                }

            }*/
            this.Close();
        }
        private void guna2TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar)){
                e.Handled = true;
                MessageBox.Show("Cannot Enter Numerical Values and Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar)){
                e.Handled = true;
                MessageBox.Show("Cannot Enter Numerical Values and Special Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2TextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8){
                e.Handled = true;
                MessageBox.Show("Cannot Enter Characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar)){
                e.Handled = true;
                MessageBox.Show("Username cannot contain whitespaces and special characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar)){
                e.Handled = true;
                MessageBox.Show("Password cannot contain whitespaces and special characters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Signup_Load(object sender, EventArgs e)
        {
            
        }
        private void UpdatePasswordMatchLabel()
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) && string.IsNullOrWhiteSpace(txtrepeatpass.Text) || string.IsNullOrEmpty(txtrepeatpass.Text)) {
                checkPass.Text = "";
            }else if (txtPassword.Text == txtrepeatpass.Text){
                checkPass.Text = "Password Matched";
                checkPass.ForeColor = System.Drawing.Color.Green;
            }else{
                checkPass.Text = "Password Doesn't Match";
                checkPass.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordMatchLabel();
        }

        private void txtrepeatpass_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordMatchLabel();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text.ToString().Trim();
            fname = txtFirstname.Text.ToString().Trim();
            lname = txtLastname.Text.ToString().Trim();
            email = txtEmail.Text.ToString().Trim();
            password = txtPassword.Text.ToString();
            database = otherFunc.FirestoreConn();

            DocumentReference docRef = database.Collection("Users").Document(username);
            Dictionary<string, object> data = new Dictionary<string, object>(){
                {"First Name", fname },
                {"Last Name", lname },
                {"Username", username },
                {"Email", email },
                {"Password", password}
            };
            try{
                docRef.SetAsync(data);
                MessageBox.Show("Successfully created your account!", "Success");
                this.Close();
            }
            catch (Exception ex){
                MessageBox.Show("Cannot process your account", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
