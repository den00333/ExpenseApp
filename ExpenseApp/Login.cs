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
            if(cliente != null)
            {
                /*it can be changed*/
                /*IDEA 1: pedeng makita sa login form kung connected o hindi, gamit yung label*/
                MessageBox.Show("Connection","Connected Successfully", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Connection error","No connection", MessageBoxButtons.OK);
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
