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
            loadWallet();
            displayExpenses();
            ExpenseGridDesign();
            //loadWallet();
        }

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

        }

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm AEF = new AddExpensesForm(this);
            AEF.ShowDialog();
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            AddingBalanceForm ABF = new AddingBalanceForm(this);
            ABF.ShowDialog();
        }
        public async void displayExpenses()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            QuerySnapshot snap = await o.displayData(username);

            dgvExpenses.Rows.Clear();

            List<DocumentSnapshot> sortedByDateDocSnap = snap.Documents
                        .OrderByDescending(docsnap => docsnap.ConvertTo<FirebaseData>().Date)
                        .ToList();
            foreach (DocumentSnapshot docsnap in sortedByDateDocSnap){
                FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
                if (docsnap.Exists){
                    dgvExpenses.Rows.Add(fd.Category, fd.Amount.ToString(), fd.Date.ToString());
                }
            }
        }
        private void ExpenseGridDesign()
        {
            dgvExpenses.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 16, FontStyle.Regular);
            dgvExpenses.DefaultCellStyle.Font = new Font("Poppins", 14, FontStyle.Regular);
            dgvExpenses.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Top, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Right, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Bottom);
            dgvExpenses.DefaultCellStyle.Padding = new Padding(15, dgvExpenses.DefaultCellStyle.Padding.Top, dgvExpenses.DefaultCellStyle.Padding.Right, dgvExpenses.DefaultCellStyle.Padding.Bottom);
            dgvExpenses.ColumnHeadersHeight = 35;
            dgvExpenses.RowTemplate.Height = 30;
        }
    }
}
