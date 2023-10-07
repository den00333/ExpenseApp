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
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            dashboard1.Show();
            dashboard1.BringToFront();
        }

        private void btnWallet_Click(object sender, EventArgs e)
        {
            wallet1.Show();
            wallet1.BringToFront();
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            group1.Show();
            group1.BringToFront();
        }

        private void btnTips_Click(object sender, EventArgs e)
        {
            tips1.Show();
            tips1.BringToFront();
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
    }
}
