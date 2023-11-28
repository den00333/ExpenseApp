using Google.Api;
using Google.Cloud.Firestore;
using MaxMind.GeoIP2.Model;
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
                String username = FirebaseData.Instance.Username;
                if (inWallet)
                {
                    float b_total = await AddMoney("Balance");
                    otherFunc.addWalletLogs(username, "Balance", addedAmount);
                    w.lblBalance.Text = otherFunc.amountBeautify(b_total);
                }
                else
                {
                    float e_total = await AddMoney("Expense");
                    otherFunc.addWalletLogs(username, "Expense", addedAmount);
                    w.lblExpenses.Text = otherFunc.amountBeautify(e_total);
                    
                }
            }
        }
        float addedAmount = 0;
        private async Task<float> AddMoney(String wallet)
        {
            otherFunc o = new otherFunc();
            String username = FirebaseData.Instance.Username;
            if (wallet.Equals("Expense"))
            {
                DocumentReference docRef = await o.SavingWalletAmount(username, wallet);
                float amount = await o.getWalletAmount(docRef);
                addedAmount = float.Parse(txtAmount.Text.ToString());
                float balanceAmount = await otherFunc.getShort(username);
                if (balanceAmount < 0)
                {
                    DialogResult res = MessageBox.Show($"You are short by {otherFunc.amountBeautify(balanceAmount)}\nDo you want to return it?", "Your Balance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == res) 
                    {
                        float excess = addedAmount + balanceAmount;

                        if (excess >= 0)
                        {
                            w.lblShort.Text = "";
                            otherFunc.setShort(0, username);
                            amount += excess;
                        }
                        else
                        {
                            w.lblShort.Text = otherFunc.amountBeautify(excess);
                            otherFunc.setShort(excess, username);
                        }

                        Dictionary<String, object> data = new Dictionary<String, object>()
                        {
                            {"Amount", amount},
                            /*{"Date", DateTime.Now.ToString("yyyy-MM-dd")}*/
                        };
                        await docRef.UpdateAsync(data);

                        //adding the returned money in the balance
                        addingReturnedAmountInBalance(username, addedAmount, balanceAmount);


                        txtAmount.Clear();
                        return amount;

                    }
                    else
                    {
                        amount += addedAmount;
                        Dictionary<String, object> data = new Dictionary<String, object>()
                        {
                            {"Amount", amount},
                            /*{"Date", DateTime.Now.ToString("yyyy-MM-dd")}*/
                        };
                        await docRef.UpdateAsync(data);

                        txtAmount.Clear();
                        return amount;
                    }
                }
                else
                {
                    float total = amount + addedAmount;
                    Dictionary<String, object> data = new Dictionary<String, object>()
                    {
                        {"Amount", total}
                    };
                    await docRef.UpdateAsync(data);

                    txtAmount.Clear();
                    return total;
                }
                
            }
            else
            {
                DocumentReference docRef = await o.SavingWalletAmount(username, wallet);
                float amount = await o.getWalletAmount(docRef);
                addedAmount = float.Parse(txtAmount.Text.ToString());
                float total = addedAmount + amount;
                Dictionary<String, object> data = new Dictionary<String, object>()
                {
                    {"Amount", total}
                };
                await docRef.UpdateAsync(data);

                txtAmount.Clear();
                return total;
            }
        }

        public async void addingReturnedAmountInBalance(String username, float addedAmount, float Bshort)
        {
            otherFunc o = new otherFunc();
            float val = (addedAmount >= Bshort*-1) ? 0 : (Bshort*-1) - addedAmount;
            DocumentReference docRef = await o.SavingWalletAmount(username, "Balance");
            float amount = await o.getWalletAmount(docRef);
            amount += ((Bshort * -1)  - val );
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", amount}
            };
            await docRef.UpdateAsync(data);
            w.lblBalance.Text = otherFunc.amountBeautify(amount);

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
