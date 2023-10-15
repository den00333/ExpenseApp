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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();

            btnDashboard.FillColor = Color.FromArgb(83, 123, 47);
            btnDashboard.ForeColor = Color.White;
            dashboard dashboard= new dashboard();
            addUserControl(dashboard);
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            btnDashboard.FillColor = Color.FromArgb(83, 123, 47);
            btnDashboard.ForeColor = Color.White;
            changeButtonColor(btnWallet,btnTips,btnGroup);
            changeFontColor(btnWallet,btnTips,btnGroup);

            dashboard dashboard= new dashboard();
            addUserControl(dashboard);
        }

        private void btnWallet_Click(object sender, EventArgs e)
        {
            btnWallet.FillColor = Color.FromArgb(83, 123, 47);
            btnWallet.ForeColor = Color.White;
            changeButtonColor(btnDashboard, btnTips, btnGroup);
            changeFontColor(btnDashboard, btnTips, btnGroup);

            wallet wallet = new wallet();
            addUserControl(wallet);
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            btnGroup.FillColor = Color.FromArgb(83, 123, 47);
            btnGroup.ForeColor = Color.White;
            changeButtonColor(btnDashboard, btnTips, btnWallet);
            changeFontColor(btnDashboard, btnTips, btnWallet);

            group group = new group();
            addUserControl(group);
        }

        private void btnTips_Click(object sender, EventArgs e)
        {
            btnTips.FillColor = Color.FromArgb(83, 123, 47);
            btnTips.ForeColor = Color.White;
            changeButtonColor(btnDashboard, btnWallet, btnGroup);
            changeFontColor(btnDashboard, btnWallet, btnGroup);
            tips tips   = new tips();
            addUserControl(tips);
        }

        private void btnLogut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes){
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
            button1.FillColor = SystemColors.Control;
            button2.FillColor = SystemColors.Control;
            button3.FillColor = SystemColors.Control;
        }
        private void changeFontColor(Guna2Button button1, Guna2Button button2, Guna2Button button3)
        {
            button1.ForeColor = Color.Black;
            button2.ForeColor = Color.Black;
            button3.ForeColor = Color.Black;
        }
    }
}
