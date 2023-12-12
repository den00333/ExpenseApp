using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DateTime = System.DateTime;

namespace ExpenseApp
{
    public partial class AddExpensesForm : Form
    {

        private wallet walletInstance;
        public int R { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int B { get; set; }
        private string username = FirebaseData.Instance.Username;
        private string myGroup;
        private ctg catG; /*category*/

        bool flag = true;
        private wallet w;
        group g;
        public AddExpensesForm(wallet wal, bool f, string groupCode, group g)
        {
            this.w = wal;
            InitializeComponent();
            initializeCMB();
            this.walletInstance = w;
            this.flag = f;
            this.myGroup = groupCode;
            this.g = g;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"flag{flag}");
            //if(groupWallet)
            //{
            //    updateGroupExpenses();
            //}
            //else if (soloWallet)
            //{
            //    updateUserExpenses();
            //}
            if (flag)
            {
                updateGroupExpenses();
            }
            else
            {
                updateUserExpenses();
            }
            Console.WriteLine($"flag{flag}");
           //onsole.WriteLine(groupWallet);
        }
        private async void updateGroupExpenses()
        {
            otherFunc o = new otherFunc();
            if (otherFunc.internetConn())
            {
                try
                {
                    await saveGroupExpenses();
                    float[] arrNum = await o.SubtractExpensesFromWalletExpensesGroup(myGroup);
                    Console.WriteLine($"Total: {arrNum[0]}  negativeVal: {arrNum[1]}  ");
                    if (arrNum[1] != 0)
                    {
                        otherFunc.setShortGroup(arrNum[1], myGroup);
                        g.lblExpenses.Text = otherFunc.amountBeautify(arrNum[0]);
                        float newNegativeVal = arrNum[1] + await otherFunc.getShortGroup(myGroup);
                        otherFunc.setShortGroup(newNegativeVal, myGroup);
                        DocumentReference dRef = await o.SavingWalletAmountOfGroup(myGroup, "Balance");
                        float currentWalletAmount = await o.getWalletAmount(dRef);
                        float newBalance = currentWalletAmount + arrNum[1];
                        Dictionary<String, object> data = new Dictionary<String, object>
                        {
                            {"Amount", newBalance}
                        };

                        await dRef.UpdateAsync(data);
                        otherFunc.setNewWalletAmountGroup(myGroup, "Balance", newBalance);
                        g.lblBalance.Text = otherFunc.amountBeautify(newBalance);
                        g.lblShort.Text = otherFunc.amountBeautify(newNegativeVal);
                        g.lblShort.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        g.lblExpenses.Text = otherFunc.amountBeautify(arrNum[0]);
                    }
                }
                catch
                {
                    MessageBox.Show("Error occured during saving!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task saveGroupExpenses()
        {
            Console.WriteLine($"start save group0:-{myGroup}");
            otherFunc o = new otherFunc();
            //var db = otherFunc.FirestoreConn();
            //CollectionReference colref = db.Collection("Groups").Document(groupCode).Collection("Expenses");
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            bool isEmpty = o.checkFormControlEmpty(txtAmount, cmbCategory, txtLocation);
            if (!isEmpty)
            {
                if (o.validDate(date))
                {
                    double amount = double.Parse(txtAmount.Text);
                    string category = cmbCategory.Text.ToString();
                    string location = txtLocation.Text.ToString();
                    string name = richTxtDesc.Text.ToString();
                    try
                    {
                        DocumentReference docRef = await o.saveGroupExpenses(myGroup);
                        Dictionary<String, object> data = new Dictionary<string, object>(){
                            {"Amount", amount},
                            {"Category", category},
                            {"Date", date},
                            {"Location", location},
                            {"Description", name},
                            {"Creator", username},
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
                            g.flpExpenses.Controls.Clear();
                            g.displayData();
                            this.DialogResult = DialogResult.OK;
                            this.Hide();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("We cannot process your transaction right now", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid date selected!", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Something is missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void updateUserExpenses()
        {
            otherFunc o = new otherFunc();
            String user = FirebaseData.Instance.Username;
            bool hasInternet = otherFunc.internetConn();
            if (hasInternet)
            {
                try
                {
                    await Save();
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
                        float newBalance = 0;
                        if (currentWalletAmount <= 0)
                        {
                            newBalance = 0;
                        }
                        else
                        {
                            newBalance = currentWalletAmount + arrNum[1];
                        }
                        Dictionary<String, object> data = new Dictionary<String, object>
                        {
                            {"Amount", newBalance}
                        };

                        await dRef.UpdateAsync(data);
                        otherFunc.setNewWalletAmount(user, "Balance", newBalance);
                        w.lblBalance.Text = otherFunc.amountBeautify(newBalance);
                        w.lblShort.Text = otherFunc.amountBeautify(newNegativeVal);
                        w.lblShort.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        w.lblExpenses.Text = otherFunc.amountBeautify(arrNum[0]);
                    }
                }
                catch
                {
                    MessageBox.Show("Error occured during saving!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No Internet Connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task Save()
        {
            String userN = FirebaseData.Instance.Username;
            otherFunc function = new otherFunc();
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            bool isEmpty = function.checkFormControlEmpty(txtAmount, cmbCategory,txtLocation);
            if (!isEmpty){
                if (function.validDate(date)){
                    double amount = double.Parse(txtAmount.Text);
                    string category = cmbCategory.Text.ToString();
                    string location = txtLocation.Text.ToString();
                    string name = richTxtDesc.Text.ToString();
                    try{
                        bool f = await function.checkSubtractCurrentExpenses(amount, username);
                        if (f)
                        {
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
                                w.flpExpenses.Controls.Clear();
                                w.displayData();
                                this.DialogResult = DialogResult.OK;
                                this.Hide();
                            }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Insufficient Balance. Do you want to deduct from your savings?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                DocumentReference docRefWallet = await function.SavingWalletAmount(username, "Expense");
                                float currentAmountInExpenses = await function.getWalletAmount(docRefWallet);

                                DocumentReference docRefBal = await function.SavingWalletAmount(username, "Balance");
                                float currentAmountInBal = await function.getWalletAmount(docRefBal);
                                float total = currentAmountInExpenses + currentAmountInBal;
                                if (total >= amount)
                                {
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
                                    DialogResult ress = MessageBox.Show("Succesfully added to your expenses!", "Saved Expenses!", MessageBoxButtons.OK);
                                    if (ress == DialogResult.OK)
                                    {
                                        txtAmount.Clear();
                                        cmbCategory.Text = null;
                                        dtpDate.Value = DateTime.Now;
                                        txtLocation.Clear();
                                        richTxtDesc.Clear();
                                        w.flpExpenses.Controls.Clear();
                                        w.displayData();
                                        this.DialogResult = DialogResult.OK;
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Insufficient Balance", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Insufficient Balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
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
