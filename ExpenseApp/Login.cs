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
        //IFirebaseClient cliente;
        public Login()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            //cliente = otherFunc.conn();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            
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
            if (otherFunc.internetConn())
            {
                logIn();
            }
            else
            {
                DialogResult res = MessageBox.Show("No internet connection!\nDo you want to use our offline version?", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    MessageBox.Show("Gagawin pa lang, antay ka lang");
                }
            }
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

                //ENTER LOGS ATTEMPTS
                // - number of attempts

                //CHECK THE ACCOUT STATUS IF IT IS ONLINE THEN YOU CANNOT PROCEED LOGGING IN
                // - submit ticket to the admin

                if (password == Security.Decrypt(userData.Password.ToString()))
                {

                    //ENTER LOGS ACTIVITY

                    this.Hide();
                    FirebaseData.Instance.Username = username;
                    Home home = new Home();
                    String userN = FirebaseData.Instance.Username;
                    bool hasExistingAcc = await otherFunc.checkLog(userN);
                    otherFunc.RecordLogs(userN, hasExistingAcc, true);
                    otherFunc.CheckAccountStatus(username);
                    otherFunc.updateAllGoals();
                    home.Show();
                }
                else
                {

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

        int count = 4;
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            bool connection = otherFunc.internetConn();
            if (connection)
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
                    lblConnection.Text = string.Empty;
                }
                
                
            }
            else
            {
                count = 4;
                lblConnection.Text = "No Connection...";
                lblConnection.ForeColor = System.Drawing.Color.Red;
            }
            
        }

        private void forgotPassBTN_Click(object sender, EventArgs e)
        {
            findYourAccount fya = new findYourAccount();
            fya.ShowDialog();
        }

        bool flag = true;
        private void passwordTB_IconRightClick(object sender, EventArgs e)
        {
            if (flag){
                passwordTB.PasswordChar= '\0';
                flag = false;
                passwordTB.IconRight = Properties.Resources.hide;
            }
            else {
                passwordTB.PasswordChar = '●';
                flag = true;
                passwordTB.IconRight = Properties.Resources.show;
            }
        }
    }
}
