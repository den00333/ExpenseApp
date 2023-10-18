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
    public partial class AddExpensesForm : Form
    {
        public int R { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int B { get; set; }
        public AddExpensesForm()
        {
            InitializeComponent();
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            LocationForm location = new LocationForm(this);
            location.Show();
        }

        private void AddExpensesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void AddExpensesForm_Load(object sender, EventArgs e)
        {

        }

        private void usernameTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
