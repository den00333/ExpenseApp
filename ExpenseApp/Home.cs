using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class Home : Form
    {

        string username;
        connectionForm cf;
        Timer timer1;
        public Home()
        {
            InitializeComponent();

            btnDashboard.FillColor = SystemColors.Control;
            btnDashboard.ForeColor = Color.Black;
            changeButtonColor(btnWallet, btnAccount, btnGroup);
            changeFontColor(btnWallet, btnAccount, btnGroup);

            dashboard dashboard = new dashboard();
            addUserControl(dashboard);
            username = FirebaseData.Instance.Username;

            cf = new connectionForm();
            otherFunc.checkInternet(cf, this);
            timer1 = new Timer();
            timer1.Interval = 5000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer tick event occurred.");
            if (!otherFunc.internetConn())
            {
                otherFunc.checkInternet(cf, this);
            }else
            {
                otherFunc.checkInternet(cf, this);
            }
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            Application.Exit();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            getFirstName(username);
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            btnDashboard.FillColor = SystemColors.Control;
            btnDashboard.ForeColor = Color.Black;
            changeButtonColor(btnWallet, btnAccount, btnGroup);
            changeFontColor(btnWallet, btnAccount, btnGroup);
            if (otherFunc.internetConn())
            {
                dashboard dashboard = new dashboard();
                addUserControl(dashboard);
            }
            else
            {
                NoConnectionUC nc = new NoConnectionUC();
                addUserControl(nc);
            }
        }

        private void btnWallet_Click(object sender, EventArgs e)
        {
            btnWallet.FillColor = SystemColors.Control;
            btnWallet.ForeColor = Color.Black;
            changeButtonColor(btnDashboard, btnAccount, btnGroup);
            changeFontColor(btnDashboard, btnAccount, btnGroup);

            wallet wallet = new wallet();
            addUserControl(wallet);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            btnGroup.FillColor = SystemColors.Control;
            btnGroup.ForeColor = Color.Black;
            changeButtonColor(btnDashboard, btnAccount, btnWallet);
            changeFontColor(btnDashboard, btnAccount, btnWallet);
            if (otherFunc.internetConn())
            {
                group group = new group();
                addUserControl(group);
            }
            else
            {
                NoConnectionUC nc = new NoConnectionUC();
                addUserControl(nc);
            }
        }

        private void btnTips_Click(object sender, EventArgs e)
        {
            btnAccount.FillColor = SystemColors.Control;
            btnAccount.ForeColor = Color.Black;
            changeButtonColor(btnDashboard, btnWallet, btnGroup);
            changeFontColor(btnDashboard, btnWallet, btnGroup);
            if (otherFunc.internetConn())
            {
                profile tips = new profile();
                addUserControl(tips);
            }
            else
            {
                NoConnectionUC nc = new NoConnectionUC();
                addUserControl(nc);
            }
        }

        private void btnLogut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                timer1.Stop();
                this.Hide();
                Login login = new Login();
                login.Show();
            }
        }

        private void guna2Panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            homePanel.Controls.Clear();
            homePanel.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void changeButtonColor(Guna2Button button1, Guna2Button button2, Guna2Button button3)
        {
            button1.FillColor = Color.FromArgb(83, 123, 47);
            button2.FillColor = Color.FromArgb(83, 123, 47);
            button3.FillColor = Color.FromArgb(83, 123, 47);
        }
        private void changeFontColor(Guna2Button button1, Guna2Button button2, Guna2Button button3)
        {
            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
            button3.ForeColor = Color.White;
        }
        async void getFirstName(string username)
        {
            otherFunc o = new otherFunc();
            DocumentSnapshot snap = await o.logInFunc(username);
            if (snap.Exists)
            {
                FirebaseData fd = snap.ConvertTo<FirebaseData>();
                lblFirstname.Text = "Hello, " + fd.FirstName + "!";
            }
        }

    }
}