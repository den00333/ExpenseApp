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
    public partial class group : UserControl
    {
        public group()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }
    }
}
