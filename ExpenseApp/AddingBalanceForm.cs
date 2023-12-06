using Google.Api;
using Google.Cloud.Firestore;
using Google.Type;
using MaxMind.GeoIP2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class AddingBalanceForm : Form
    {
        string username = FirebaseData.Instance.Username;
        bool flag = true;
        string currentGroup;

        wallet w;
        group g;
        public AddingBalanceForm(wallet wal, bool f, string groupCode, group g)
        {
            InitializeComponent();
            this.w = wal;
            this.flag = f;
            this.currentGroup = groupCode;
            this.g = g;
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
                if (flag)
                {
                    if (inWallet)
                    {
                        float b_total = await AddGroupMoney("Balance");
                        otherFunc.addGroupWalletLogs(currentGroup, "Balance", addedAmount, username);
                        g.lblBalance.Text = otherFunc.amountBeautify(b_total);
                    }
                    else
                    {
                        float e_total = await AddGroupMoney("Expense");
                        otherFunc.addGroupWalletLogs(currentGroup, "Expense", addedAmount, username);
                        g.lblExpenses.Text = otherFunc.amountBeautify(e_total);
                    }
                }
                else
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
        }
        //private async Task<float> AddGroupMoney(String walletName)
        //{
        //    otherFunc o = new otherFunc();
        //    if (walletName.Equals("Expense"))
        //    {
        //        DocumentReference docRef = await o.SavingGroupWalletAmount(groupCode, walletName);
        //        float amount = await o.getWalletAmount(docRef);
        //        addedAmount = float.Parse(txtAmount.Text.ToString());
        //        float balanceAmount = await otherFunc.getGroupShort(groupCode);
        //        if (balanceAmount < 0)
        //        {
        //            DialogResult res = MessageBox.Show($"You are short by {otherFunc.amountBeautify(balanceAmount)}\nDo you want to return it?", "Your Balance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (DialogResult.Yes == res)
        //            {
        //                float excess = addedAmount + balanceAmount;

        //                if (excess >= 0)
        //                {
        //                    w.lblShort.Text = "";
        //                    otherFunc.setGroupShort(0, groupCode);
        //                    amount += excess;
        //                }
        //                else
        //                {
        //                    w.lblShort.Text = otherFunc.amountBeautify(excess);
        //                    otherFunc.setGroupShort(excess, groupCode);
        //                }

        //                Dictionary<String, object> data = new Dictionary<String, object>()
        //                {
        //                    {"Amount", amount},
        //                    /*{"Date", DateTime.Now.ToString("yyyy-MM-dd")}*/
        //                };
        //                addingReturnedGroupAmountInBalance(groupCode, addedAmount, balanceAmount);
        //                await docRef.UpdateAsync(data);
        //                                        txtAmount.Clear();
        //                return amount;
        //            }
        //            else
        //            {
        //                amount += addedAmount;
        //                Dictionary<String, object> data = new Dictionary<String, object>()
        //                {
        //                    {"Amount", amount},
        //                    /*{"Date", DateTime.Now.ToString("yyyy-MM-dd")}*/
        //                };
        //                await docRef.UpdateAsync(data);
        //                txtAmount.Clear();
        //                return amount;
        //            }
        //        }
        //        else
        //        {
        //            float total = amount + addedAmount;
        //            Dictionary<String, object> data = new Dictionary<String, object>()
        //            {
        //                {"Amount", total}
        //            };
        //            await docRef.UpdateAsync(data);

        //            txtAmount.Clear();
        //            return total;
        //        }
        //    }
        //    else
        //    {
        //        DocumentReference docRef = await o.SavingGroupWalletAmount(groupCode, walletName);
        //        float amount = await o.getWalletAmount(docRef);
        //        addedAmount = float.Parse(txtAmount.Text.ToString());
        //        float total = addedAmount + amount;
        //        Dictionary<string, object> data = new Dictionary<string, object>()
        //        {
        //            {"Amount", total}
        //        };
        //        await docRef.UpdateAsync(data);
        //        txtAmount.Clear();
        //        return total;
        //    }
        //}
        //            else
        //    {
        //        DocumentReference docRef = await o.SavingWalletAmount(username, wallet);
        //float amount = await o.getWalletAmount(docRef);
        //addedAmount = float.Parse(txtAmount.Text.ToString());
        //float total = addedAmount + amount;
        //Dictionary<String, object> data = new Dictionary<String, object>()
        //        {
        //            {"Amount", total}
        //        };
        //await docRef.UpdateAsync(data);

        //txtAmount.Clear();
        //        return total;
        //    }
    float addedAmount = 0;
        private async Task<float> AddGroupMoney(string wallet)
        {
            otherFunc function = new otherFunc();
            if (wallet.Equals("Expense"))
            {
                DocumentReference docRef = await function.SavingWalletAmountOfGroup(currentGroup, "Expense");
                float amount = await function.getGroupWalletAmount(docRef);
                addedAmount = float.Parse(txtAmount.Text.ToString());
                float balanceAmount = await otherFunc.getGroupShort(currentGroup);
                if (balanceAmount < 0)
                {
                    float excess = addedAmount + balanceAmount;
                    DialogResult res = MessageBox.Show($"You are short by {otherFunc.amountBeautify(balanceAmount)}\nDo you want to return it?", "Your Balance", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DialogResult.Yes == res)
                    {
                        if (excess >= 0)
                        {
                            g.lblShort.Text = "";
                            otherFunc.setShortGroup(0, currentGroup);
                            amount += excess;
                        }
                        else
                        {
                            g.lblShort.Text = otherFunc.amountBeautify(excess);
                            otherFunc.setShortGroup(excess, currentGroup);
                        }

                        Dictionary<String, object> data = new Dictionary<String, object>()
                        {
                            {"Amount", amount},
                            /*{"Date", DateTime.Now.ToString("yyyy-MM-dd")}*/
                        };
                        await docRef.UpdateAsync(data);

                        //adding the returned money in the balance
                        addingReturnedAmountInBalanceGroup(currentGroup, addedAmount, balanceAmount);

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
                DocumentReference docRef = await function.SavingWalletAmountOfGroup(currentGroup,wallet);
                float amount = await function.getGroupWalletAmount(docRef);
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
        private async void addingReturnedAmountInBalanceGroup(string groupCode, float addedAmount, float Bshort)
        {
            otherFunc function = new otherFunc();
            float val = (addedAmount >= Bshort * -1) ? 0 : (Bshort * -1) - addedAmount;
            DocumentReference docRef = await function.SavingWalletAmountOfGroup(groupCode, "Balance");
            float amount = await function.getGroupWalletAmount(docRef);
            amount += ((Bshort * -1) - val);
            Dictionary<String, object> data = new Dictionary<String, object>()
            {
                {"Amount", amount}
            };
            await docRef.UpdateAsync(data);
            w.lblBalance.Text = otherFunc.amountBeautify(amount);
        }
        //public async void addingReturnedGroupAmountInBalance(string groupCode, float addedAmount, float Bshort)
        //{
        //    otherFunc o = new otherFunc();
        //    float val = (addedAmount >= Bshort * -1) ? 0 : (Bshort * -1) - addedAmount;
        //    DocumentReference docRef = await o.SavingGroupWalletAmount(groupCode, "Balance");
        //    float amount = await o.getWalletAmount(docRef);
        //    amount += ((Bshort * -1) - val);
        //    Dictionary<String, object> data = new Dictionary<String, object>()
        //    {
        //        {"Amount", amount}
        //    };
        //    await docRef.UpdateAsync(data);
        //    w.lblBalance.Text = otherFunc.amountBeautify(amount);
        //}

        private void btnChangeWallet_Click(object sender, EventArgs e)
        {
            System.Drawing.Color currentColor = btnChangeWallet.FillColor;
            if(currentColor == System.Drawing.Color.FromArgb(227, 180, 72))
            {
                inWallet = false;
                btnChangeWallet.FillColor = System.Drawing.Color.FromArgb(222, 66, 86);
                panel.FillColor = System.Drawing.Color.FromArgb(183, 142, 81);
                panel.FillColor2 = System.Drawing.Color.FromArgb(222, 66, 86);
                btnAddAmount.FillColor = System.Drawing.Color.FromArgb(222, 66, 86);
                lblAdd.Text = "Add Expenses Amount:";
                btnChangeWallet.Text = "Balance";
            }
            else
            {
                inWallet = true;
                btnChangeWallet.FillColor = System.Drawing.Color.FromArgb(227, 180, 72);
                panel.FillColor = System.Drawing.Color.FromArgb(227, 180, 72);
                panel.FillColor2 = System.Drawing.Color.FromArgb(83, 123, 47);
                btnAddAmount.FillColor = System.Drawing.Color.FromArgb(83, 123, 47);
                lblAdd.Text = "Add Balance Amount:";
                btnChangeWallet.Text = "Expenses";
            }

        }
    }
}
