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
    public partial class NoGroupUC : UserControl
    {
        public NoGroupUC()
        {
            InitializeComponent();
        }

        private void btnCreate_MouseEnter(object sender, EventArgs e)
        {
            btnCreate.BackColor = Color.Aqua;
            btnCreate.BorderRadius = 10;
        }

        private void btnCreate_MouseLeave(object sender, EventArgs e)
        {
            btnCreate.BackColor = Color.Transparent;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }
    }
}
