using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class AddExpensesForm : Form
    {

        private wallet walletInstance;
        public int R { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int B { get; set; }
        private ctg catG; /*category*/
        
        
        private wallet w;
        public AddExpensesForm(wallet wal)
        {
            this.w = wal;
            InitializeComponent();
            initializeCMB();
            this.walletInstance = w;
        }

        private void initializeCMB()
        {
            catG = FileFunc.initializeData();
            otherFunc.populateCMBcategory(catG, this);
            richTextNote.Margin = new Padding(10);
        }

        

        private void btnLocation_Click(object sender, EventArgs e)
        {
            LocationForm location = new LocationForm(this);
            location.Show();
        }

        private void AddExpensesForm_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
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

        private void btnCustomize_Click(object sender, EventArgs e)
        {
            customizeCategory ccg = new customizeCategory(this);
            ccg.Show();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            otherFunc o = new otherFunc();
            String user = FirebaseData.Instance.Username;
            bool hasInternet = otherFunc.internetConn();
            if (hasInternet) {
                try{
                    Save();
                    //arr[0] is the total and arr[1] is the negativetotal
                    float[] arrNum = await o.SubtractExpensesFromWalletExpenses(user);
                    if (arrNum[1] != 0)
                    {
                        otherFunc.setShort(arrNum[1], user);
                        w.lblExpenses.Text = otherFunc.amountBeautify(arrNum[0]);
                        float newNegativeVal = arrNum[1] + await otherFunc.getShort(user);
                        otherFunc.setShort(newNegativeVal, user);
                        DocumentReference dRef = await o.SavingWalletAmount(user, "Balance");
                        float currentWalletAmount = await o.getWalletAmount(dRef);
                        float newBalance = currentWalletAmount + arrNum[1];
                        Dictionary<String, object> data = new Dictionary<String, object>
                        {
                            {"Amount", newBalance}
                        };

                        await dRef.UpdateAsync(data);
                        otherFunc.setNewWalletAmount(user, "Balance", newBalance);
                        w.lblBalance.Text = otherFunc.amountBeautify(newBalance);
                        w.lblShort.Text = otherFunc.amountBeautify(newNegativeVal);
                        w.lblShort.ForeColor = Color.Red;
                    }
                    else
                    {
                        w.lblExpenses.Text = otherFunc.amountBeautify(arrNum[0]);
                    }


                }
                catch {
                    MessageBox.Show("Error occured during saving!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Save()
        {
            String userN = FirebaseData.Instance.Username;
            otherFunc function = new otherFunc();
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            bool isEmpty = function.checkExpenseFormControl(txtAmount, cmbCategory,txtLocation);
            if (!isEmpty){
                if (function.validDate(date)){
                    double amount = double.Parse(txtAmount.Text);
                    string category = cmbCategory.Text.ToString();
                    string location = txtLocation.Text.ToString();
                    string name = richTxtDesc.Text.ToString();
                    try{
                        DocumentReference docRef = await function.SavingNewExpenses(userN);
                        Dictionary<String, object> data = new Dictionary<string, object>(){
                            {"Amount", amount},
                            {"Category", category},
                            {"Date", date},
                            {"Location", location},
                            {"Name", name},
                            {"timestamp", FieldValue.ServerTimestamp}
                        };
                        await docRef.SetAsync(data);
                        DialogResult res = MessageBox.Show("Succesfully added to your expenses!", "Saved Expenses!", MessageBoxButtons.OK);
                        if (res == DialogResult.OK)
                        {
                            txtAmount.Clear();
                            cmbCategory.Text = null;
                            dtpDate.Value = DateTime.Now;
                            txtLocation.Clear();
                            richTxtDesc.Clear();
                            walletInstance.displayExpenses();
                            this.Hide();
                        }
                    }
                    catch{
                        MessageBox.Show("We cannot process your transaction right now", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else{
                    MessageBox.Show("Invalid date selected!", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else{
                MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void richTxtDesc_TextChanged(object sender, EventArgs e)
        {
            RichTextBox textBox = (RichTextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && char.IsLower(textBox.Text[0]))
            {
                textBox.Text = char.ToUpper(textBox.Text[0]) + textBox.Text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void richTxtDesc_Enter(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Text = "";
        }

        private void richTxtDesc_Leave(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Text = "Name";
        }

        private void richTextNote_Leave(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Text = "Note";
        }

        private void richTextNote_Enter(object sender, EventArgs e)
        {
            RichTextBox rtb = (RichTextBox)sender;
            rtb.Text = "";
        }
    }
}
