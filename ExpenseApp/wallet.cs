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
    public partial class wallet : UserControl
    {
        public wallet()
        {
            InitializeComponent();
        }

        private void wallet_Load(object sender, EventArgs e)
        {

        }

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm AEF = new AddExpensesForm();
            AEF.ShowDialog();
        }
    }
}
