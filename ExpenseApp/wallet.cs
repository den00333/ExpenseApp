using Google.Cloud.Firestore;
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
    public partial class wallet : UserControl
    {
        string username = FirebaseData.Instance.Username;
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
            float BalanceAmount = await o.getWalletAmount(docRef);
            lblBalance.Text = otherFunc.amountBeautify(BalanceAmount);

            DocumentReference docRef2 = await o.SavingWalletAmount(username, "Expense");
            float ExpenseAmount = await o.getWalletAmount(docRef2);
            lblExpenses.Text = otherFunc.amountBeautify(ExpenseAmount);

            float negVal = await otherFunc.getShort(username);
            if(negVal != 0)
            {
                lblShort.Text = otherFunc.amountBeautify(negVal);
                lblShort.ForeColor = Color.Red;
            }
            else
            {
                lblShort.Text = "";
            }
            

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
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.displayDataWithDocNames(username);

            dgvExpenses.Rows.Clear();

            foreach ((string docName, DocumentSnapshot docsnap) in documentData) {
                FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
                if (docsnap.Exists){
                    dgvExpenses.Rows.Add(docName, fd.Category, fd.Amount.ToString(), fd.Date.ToString());
                }
            }
        }

        public async void displayGoals()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.getGoalsWithDocNames(username);
            dgvExpenses.Rows.Clear();
            foreach ((string docName, DocumentSnapshot docsnap) in documentData)
            {
                float am = docsnap.GetValue<float>("Amount");
                String d = docsnap.GetValue<String>("GoalDate");
                if (docsnap.Exists)
                {
                    dgvExpenses.Rows.Add(docName, am.ToString(), d);
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
            dgvExpenses.ReadOnly = true;
            dgvExpenses.Columns[0].Width = 150;
        }
        private async void dgvExpenses_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            otherFunc function = new otherFunc();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (flagGoal)
                {
                    string expenseId = dgvExpenses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Dictionary<string, object> data = await function.getItemsInsideExpenseId(username, expenseId);

                    ExpenseDetailForm edf = new ExpenseDetailForm();
                    edf.displayExpenseDetails(data);
                    edf.StartPosition = FormStartPosition.Manual;
                    int x = Screen.PrimaryScreen.WorkingArea.Right - edf.Width;
                    int y = Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - edf.Height) / 2;
                    edf.Location = new Point(x, y);
                    edf.ShowDialog();
                }
                else
                {
                    string goalId = dgvExpenses.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Dictionary<string, object> data = await function.getItemsInsideGoalsID(username, goalId);
                    GoalDetails gd = new GoalDetails(this);
                    gd.displayGoalDetails(data, goalId);
                    gd.StartPosition = FormStartPosition.CenterScreen;
                    gd.ShowDialog();

                }
            }
        }

        private void dgvExpenses_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0){
                DataGridViewCell cell = dgvExpenses.Rows[e.RowIndex].Cells[e.ColumnIndex];
                toolTip1.SetToolTip(dgvExpenses,"View Full Details");
            }
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            AddGoals ag = new AddGoals(this);
            ag.StartPosition = FormStartPosition.CenterScreen;
            ag.ShowDialog();
        }

        public bool flagGoal = true;
        private void btnGoals_Click(object sender, EventArgs e)
        {
            if (flagGoal)
            {
                //button
                panelSwitch.FillColor = Color.FromArgb(227, 180, 72);
                panelSwitch.FillColor2 = Color.FromArgb(83, 123, 47);
                btnGoals.ForeColor = Color.DarkGreen;
                btnGoals.Text = "Transactions";
                //
                //DATA GRID
                panelTitle.FillColor = Color.FromArgb(187, 141, 228);
                label7.Text = "Goals";
                label7.Anchor = AnchorStyles.Right;
                dgvExpenses.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Amethyst;
                dgvExpenses.Rows.Clear();
                dgvExpenses.Columns.Clear();
                setDGVHeaders(flagGoal);
                displayGoals();
                flagGoal = false;
            }
            else
            {
                //button
                panelSwitch.FillColor = Color.FromArgb(187, 141, 228);
                panelSwitch.FillColor2 = Color.FromArgb(229, 148, 98);
                btnGoals.ForeColor = Color.DarkOrchid;
                btnGoals.Text = "Goals";
                //
                //DATA GRID
                panelTitle.FillColor = Color.FromArgb(83, 123, 47);
                label7.Text = "Transactions";
                dgvExpenses.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGreen;
                dgvExpenses.Rows.Clear();
                dgvExpenses.Columns.Clear();
                setDGVHeaders(flagGoal);
                displayExpenses();
                flagGoal = true;
            }
        }

        private void setDGVHeaders(bool flag)
        {
            if (flag)
            {
                String[] transactionHeaders = { "Title","Amount", "Date" };
                foreach (String header in transactionHeaders)
                {
                    DataGridViewTextBoxColumn d = new DataGridViewTextBoxColumn();
                    d.HeaderText = header;
                    dgvExpenses.Columns.Add(d);
                }
            }
            else
            {
                String[] transactionHeaders = { "Expense ID", "Category", "Amount", "Date" };
                foreach(String header in transactionHeaders)
                {
                    DataGridViewTextBoxColumn d = new DataGridViewTextBoxColumn();
                    d.HeaderText = header;
                    dgvExpenses.Columns.Add(d);
                }
            }
        }
    }
}
