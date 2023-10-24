using Google.Cloud.Firestore;
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
            Save();
        }

        private async void Save()
        {
            String userN = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            DocumentReference docRef = await o.SavingNewExpenses(userN);
            Dictionary<String, object> data = new Dictionary<string, object>()
            {
                {"Amount", int.Parse(txtAmount.Text)},
                {"Category", cmbCategory.Text.ToString()},
                {"Date", dtpDate.Value.ToString("yyyy-MM-dd")},
                {"Location", txtLocation.Text.ToString()},
                {"Name", richTxtDesc.Text.ToString()},
                {"timestamp", FieldValue.ServerTimestamp}
            };
            await docRef.SetAsync(data);
            DialogResult res = MessageBox.Show("Succesfully added to your expenses!", "Saved Expenses!", MessageBoxButtons.OK);
            if(res == DialogResult.OK)
            {
                txtAmount.Clear();
                cmbCategory.Text = null;
                dtpDate.Value = DateTime.Now;
                txtLocation.Clear();
                richTxtDesc.Clear();
            }



        }
    }
}
