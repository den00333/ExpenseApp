using System;
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
using static Google.Cloud.Firestore.V1.StructuredAggregationQuery.Types.Aggregation.Types;


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
            bool connection = otherFunc.internetConn();
            timer1.Start();
            if (cliente != null && connection)
            {
                count = 5;
            }
            else
            {
                timer1.Stop();
                lblConnection.Text = "No Connection...";
                lblConnection.ForeColor = System.Drawing.Color.Red;
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
            String username = usernameTB.Text.ToString().Trim().ToLower();
            String password = passwordTB.Text.ToString();
            otherFunc o = new otherFunc();
            DocumentSnapshot docSnap = await o.logInFunc(username);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
                MessageBox.Show("Please fill in the required information. \n Your username & password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (docSnap.Exists){
                FirebaseData userData = docSnap.ConvertTo<FirebaseData>();
                if (password == Security.Decrypt(userData.Password.ToString())){
                    this.Hide();
                    Home home = new Home();
                    home.Show();
                }
                else{
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else{
                MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void minimizeBTN_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (count > 0)
            {
                /*checking connection*/
                lblConnection.Text = "Connected Succesfully!";
                lblConnection.ForeColor = System.Drawing.Color.Green;
                count--;
            }
            else
            {
                timer1.Stop();
                lblConnection.Text = string.Empty;
            }
        }
    }
}
