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
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class homeForm : Form
    {
        string username;
        connectionForm cf;
        public homeForm()
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
            Timer = new Timer();
            Timer.Interval = 5000;
            Timer.Tick += new EventHandler(timer1_Tick);
            Timer.Start();
        }

        private async void homeForm_Load(object sender, EventArgs e)
        {
            lblFirstname.Text = "Hello, " + await otherFunc.getFirstname(username) + "!";
            otherFunc.retrieveImage(username, pbProfile);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer tick event occurred.");
            if (!otherFunc.internetConn())
            {
                otherFunc.checkInternet(cf, this);
            }
            else
            {
                otherFunc.checkInternet(cf, this);
            }
        }

        private void homeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer.Stop();
            String username = FirebaseData.Instance.Username;
            otherFunc.UpdateAccStatusToOffline(username);
            otherFunc.RecordLogs(username, true, false);
            Application.Exit();
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
            //soloWallet = true;
            //groupWallet = false;
            btnWallet.FillColor = SystemColors.Control;
            btnWallet.ForeColor = Color.Black;
            changeButtonColor(btnDashboard, btnAccount, btnGroup);
            changeFontColor(btnDashboard, btnAccount, btnGroup);

            if (otherFunc.internetConn())
            {

                wallet wallet = new wallet();
                addUserControl(wallet);
                AddExpensesForm adf = new AddExpensesForm(new wallet(), false, null, new group());
                AddingBalanceForm abf = new AddingBalanceForm(wallet, false, null, new group());

                //adf.SetWalletValues(groupWallet, soloWallet);
                //abf.SetWalletValues(groupWallet, soloWallet);


            }
            else
            {
                NoConnectionUC nc = new NoConnectionUC();
                addUserControl(nc);
            }
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            //groupWallet = true;
            //soloWallet = false;
            btnGroup.FillColor = SystemColors.Control;
            btnGroup.ForeColor = Color.Black;
            changeButtonColor(btnDashboard, btnAccount, btnWallet);
            changeFontColor(btnDashboard, btnAccount, btnWallet);
            if (otherFunc.internetConn())
            {
                checkGroupExists();
                AddExpensesForm adf = new AddExpensesForm(new wallet(), true, null, new group());
                AddingBalanceForm abf = new AddingBalanceForm(new wallet(), true, null, new group());
                
                //adf.SetWalletValues(groupWallet, soloWallet);
                //abf.SetWalletValues(groupWallet, soloWallet);
            }
            else
            {
                NoConnectionUC nc = new NoConnectionUC();
                addUserControl(nc);
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Timer.Stop();
                String username = FirebaseData.Instance.Username;
                otherFunc.UpdateAccStatusToOffline(username);
                otherFunc.RecordLogs(username, true, false);
                this.Hide();
                Login login = new Login();
                login.Show();
            }
        }
        public void addUserControl(UserControl userControl)
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
        public async void checkGroupExists()
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference userCollectionRef = db.Collection("Users");
            DocumentReference userDocRef = userCollectionRef.Document(username);
            DocumentSnapshot userDocSnap = await userDocRef.GetSnapshotAsync();
            if (userDocSnap.Exists && userDocSnap.ContainsField("Groups"))
            {
                group groupControl = new group();
                addUserControl(groupControl);
            }
            else
            {
                NoGroupUC ng = new NoGroupUC();
                addUserControl(ng);
            }
        }

        private void cbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to close the application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
