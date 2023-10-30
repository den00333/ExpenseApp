using Google.Api;
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
    public partial class AddingBalanceForm : Form
    {
        wallet w;
        public AddingBalanceForm(wallet wal)
        {
            InitializeComponent();
            this.w = wal;
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            string pattern = @"[\d\b]";
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), pattern))
            {
                e.Handled = true;
            }
        }
        bool inWallet = true;
        private async void btnAddAmount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                if (inWallet)
                {
                    int b_total = await AddMoney("Balance");
                    w.lblBalance.Text = otherFunc.amountBeautify(b_total);
                }
                else
                {
                    int e_total = await AddMoney("Expense");
                    w.lblExpenses.Text = otherFunc.amountBeautify(e_total);
                }
            }
        }
        
        private async Task<int> AddMoney(String wallet)
        {
            otherFunc o = new otherFunc();
            String username = FirebaseData.Instance.Username;
            DocumentReference docRef = await o.SavingWalletAmount(username, wallet);
            int amount = await o.getWalletAmount(docRef);
            int total = int.Parse(txtAmount.Text.ToString()) + amount;
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", total}
            };
            await docRef.UpdateAsync(data);

            txtAmount.Clear();
            return total;
        }

        private void btnChangeWallet_Click(object sender, EventArgs e)
        {
            Color currentColor = btnChangeWallet.FillColor;
            if(currentColor == Color.FromArgb(227, 180, 72))
            {
                inWallet = false;
                btnChangeWallet.FillColor = Color.FromArgb(222, 66, 86);
                panel.FillColor = Color.FromArgb(183, 142, 81);
                panel.FillColor2 = Color.FromArgb(222, 66, 86);
                btnAddAmount.FillColor = Color.FromArgb(222, 66, 86);
                lblAdd.Text = "Add Expenses Amount:";
                btnChangeWallet.Text = "Balance";
            }
            else
            {
                inWallet = true;
                btnChangeWallet.FillColor = Color.FromArgb(227, 180, 72);
                panel.FillColor = Color.FromArgb(227, 180, 72);
                panel.FillColor2 = Color.FromArgb(83, 123, 47);
                btnAddAmount.FillColor = Color.FromArgb(83, 123, 47);
                lblAdd.Text = "Add Balance Amount:";
                btnChangeWallet.Text = "Expenses";
            }

        }
    }
}
