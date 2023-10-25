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
        public int R { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int B { get; set; }
        private ctg catG; /*category*/
        public AddExpensesForm()
        {
            InitializeComponent();
            initializeCMB();
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
            bool hasInternet = otherFunc.internetConn();
            if (hasInternet) {
                try{
                    Save();
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
    }
}
