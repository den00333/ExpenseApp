﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

using Google.Cloud.Firestore;


namespace ExpenseApp
{
    public partial class Login : Form
    {
        IFirebaseClient cliente;
        public Login()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            cliente = otherFunc.conn();
            /*checking connection*/
            if(cliente != null){
                /*it can be changed*/
                /*IDEA 1: pedeng makita sa login form kung connected o hindi, gamit yung label*/
                MessageBox.Show("Connection","Connected Successfully", MessageBoxButtons.OK);
            }
            else{
                MessageBox.Show("Connection error","No connection", MessageBoxButtons.OK);
            }
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes){
                Application.Exit();
            }
        }

        private void signupBTN_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.ForeColor = ColorTranslator.FromHtml("#4C96FF");
        }

        private void signupBTN_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.ForeColor = DefaultForeColor;
        }

        private void signupBTN_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.ShowDialog();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            logIn();
        }

        async void logIn()
        {
            String username = usernameTB.Text.ToString().Trim();
            String password = passwordTB.Text.ToString();

            otherFunc o = new otherFunc();
            DocumentSnapshot docSnap = await o.logInFunc(username);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in the required information. \n Your username & password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (docSnap.Exists)
            {
                FirebaseData userData = docSnap.ConvertTo<FirebaseData>();
                if (password == Security.Decrypt(userData.Password.ToString()))
                {
                    this.Hide();
                    Home home = new Home();
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
