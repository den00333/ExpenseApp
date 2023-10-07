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
    public partial class Terms : Form
    {
        public Terms()
        {
            InitializeComponent();
        }

        public bool Accepted { get; private set; } = false;
        private void btnAccept_Click(object sender, EventArgs e)
        {
            Accepted = true;
            this.Hide();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Accepted = false;
            this.Hide();
        }
    }
}
