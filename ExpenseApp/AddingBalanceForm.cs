using Google.Api;
using Google.Cloud.Firestore;
using Google.Type;
using MaxMind.GeoIP2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        bool offlineFlag = false;
        offWalletUC owuc;
        public AddingBalanceForm(bool f, offWalletUC ow)
        {
            InitializeComponent();
            this.offlineFlag = f;
            this.owuc = ow;
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

        /*private void addWalletAmount_Offline(String path, String wName, double wAmount)
        {
            List<Double> list = new List<Double>();
            list.Add(wAmount);
            if (File.Exists(path))
            {

            }
            else
            {
                var data = new { wName = list };
            }
        }*/
        
        private void addWalletLogs(String path, String amount, String walletName)
        {
            String d = System.DateTime.Now.ToString("yyyy-MM-dd");
            String t = System.DateTime.Now.ToString("HH:mm:ss");

            String[] lines = File.ReadAllLines(path);
            lines[3] += $"{amount}|{walletName}|{d}|{t}~";

            File.WriteAllLines(path, lines);

        }

        private void addWalletTXT(String path, int loc, String toBeAdded, bool flag)
        {
            String d = System.DateTime.Now.ToString("yyyy-MM-dd");
            String t = System.DateTime.Now.ToString("HH:mm:ss");
            if (flag)
            {
                String[] lines = File.ReadAllLines(path);
                String[] splittedLines = lines[loc].Split(':');
                double newTotal = double.Parse(splittedLines[1]) + double.Parse(toBeAdded);
                lines[loc] = lines[loc].Replace(splittedLines[1], newTotal.ToString());
                File.WriteAllLines(path, lines);
            }
            else
            {
                String[] lines = File.ReadAllLines(path);
                String[] splittedLines = lines[loc].Split(':');
                double newTotal = double.Parse(splittedLines[1]) + double.Parse(toBeAdded);
                Console.WriteLine($"NEWTOTAL: {newTotal}");
                if (newTotal < 0)
                {
                    lines[loc] = lines[loc].Replace(splittedLines[1], newTotal.ToString());
                }
                else
                {
                    lines[loc] = lines[loc].Replace(splittedLines[1], "0");
                }



                File.WriteAllLines(path, lines);

            }
        }



        String walletN = "";
        bool inWallet = true;
        private async void btnAddAmount_Click(object sender, EventArgs e)
        {
            if (offlineFlag)
            {

                String walletName = btnChangeWallet.Text;
                double amount = double.Parse(txtAmount.Text);
                String path = owuc.p;
                if (File.Exists(path))
                {
                    if (walletName.Equals("Expenses"))
                    {
                        walletN = "Balance";
                        addWalletTXT(path, 0, amount.ToString(), true);
                        float fAmount = float.Parse(offlineFunc.readTxt(path, 0));
                        owuc.lblBalance.Text = otherFunc.amountBeautify(fAmount);
                        addWalletLogs(path, amount.ToString(), walletN);
                    }
                    else
                    {
                        walletN = "Expense";

                        addWalletLogs(path, amount.ToString(), walletN);
                        double currentShort = double.Parse(offlineFunc.readTxt(path, 2));
                        if (currentShort < 0)
                        {

                            DialogResult res = MessageBox.Show($"Do you want to return {otherFunc.amountBeautify((float)currentShort)}?", "Returning short", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                //addWalletTXT(path, 2, amount.ToString(), true);
                                double newTotal = amount + currentShort;
                                Console.WriteLine($"addexpenses {newTotal} = {amount}+{currentShort}");

                                if (newTotal < 0)
                                {
                                    addWalletTXT(path, 2, amount.ToString(), false);

                                    addWalletTXT(path, 0, amount.ToString(), true);
                                }
                                else
                                {
                                    double newAmount = amount - newTotal;
                                    Console.WriteLine($"{newAmount} = {amount} + {newTotal}");
                                    //addWalletTXT(path, 0, "", true);
                                    addWalletTXT(path, 0, newAmount.ToString(), true);
                                    addWalletTXT(path, 1, newTotal.ToString(), true);
                                    addWalletTXT(path, 2, amount.ToString(), false);


                                }

                            }
                            else
                            {
                                addWalletTXT(path, 1, amount.ToString(), true);
                            }
                        }
                        else
                        {

                            addWalletTXT(path, 1, amount.ToString(), true);
                            Console.WriteLine("outer if of expense");
                        }

                        float fBalance = float.Parse(offlineFunc.readTxt(path, 0));
                        owuc.lblBalance.Text = otherFunc.amountBeautify(fBalance);

                        float fAmount = float.Parse(offlineFunc.readTxt(path, 1));
                        owuc.lblExpenses.Text = otherFunc.amountBeautify(fAmount);

                        float fShort = float.Parse(offlineFunc.readTxt(path, 2));
                        owuc.lblShort.Text = otherFunc.amountBeautify(fShort);

                        owuc.checkShort(fShort);



                    }

                }
                else
                {
                    MessageBox.Show("created new txtfile");
                    offlineFunc.createTXT(path);
                    //Expenses means it is in balance, vise versa
                    // 0 -> balance | 1 -> expense | 2 -> short
                    if (walletName.Equals("Expenses"))
                    {
                        walletN = "Balance";
                        addWalletTXT(path, 0, amount.ToString(), true);
                        float fAmount = float.Parse(offlineFunc.readTxt(path, 0));
                        owuc.lblBalance.Text = otherFunc.amountBeautify(fAmount);
                        addWalletLogs(path, amount.ToString(), walletN);

                    }
                    else
                    {
                        walletN = "Expense";
                        addWalletTXT(path, 1, amount.ToString(), true);
                        float fAmount = float.Parse(offlineFunc.readTxt(path, 1));
                        owuc.lblExpenses.Text = otherFunc.amountBeautify(fAmount);
                        addWalletLogs(path, amount.ToString(), walletN);

                    }
                }

            }
            else
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
                DocumentReference docRef = await function.SavingWalletAmountOfGroup(currentGroup, wallet);
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
            float val = (addedAmount >= Bshort * -1) ? 0 : (Bshort * -1) - addedAmount;
            DocumentReference docRef = await o.SavingWalletAmount(username, "Balance");
            float amount = await o.getWalletAmount(docRef);
            amount += ((Bshort * -1) - val);
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
            if (currentColor == System.Drawing.Color.FromArgb(227, 180, 72))
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

        private void AddingBalanceForm_Load(object sender, EventArgs e)
        {

        }
    }
}