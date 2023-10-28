using Google.Cloud.Firestore;
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
            //displayExpenses();
            //dgvExpenses.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 14, FontStyle.Regular);
            //dgvExpenses.DefaultCellStyle.Font = new Font("Poppins", 14, FontStyle.Regular);
            //loadWallet();
        }
        /*
        private async void loadWallet()
        {
            String username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            DocumentReference docRef = await o.SavingWalletAmount(username, "Balance");
            int BalanceAmount = await o.getWalletAmount(docRef);
            lblBalance.Text = otherFunc.amountBeautify(BalanceAmount);

            DocumentReference docRef2 = await o.SavingWalletAmount(username, "Expense");
            int ExpenseAmount = await o.getWalletAmount(docRef2);
            lblExpenses.Text = otherFunc.amountBeautify(ExpenseAmount);

        }*/

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm AEF = new AddExpensesForm();
            AEF.ShowDialog();
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            AddingBalanceForm ABF = new AddingBalanceForm(this);
            ABF.ShowDialog();
        }
        //public async void displayExpenses()
        //{
        //    string username = FirebaseData.Instance.Username;
        //    otherFunc o = new otherFunc();
        //    QuerySnapshot snap = await o.displayData(username);

        //    foreach (DocumentSnapshot docsnap in snap.Documents)
        //    {
        //        FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
        //        if (docsnap.Exists)
        //        {
        //            dgvExpenses.Rows.Add(fd.Category, fd.Amount.ToString());
        //        }
        //    }
        //}
    }
}
