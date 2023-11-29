using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Google.Api.ResourceDescriptor.Types;

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
            //displayExpenses();
            //ExpenseGridDesign();
            //loadWallet();
            displayData();
            displayGoals();
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
        //public async void displayExpenses()
        //{
        //    string username = FirebaseData.Instance.Username;
        //    otherFunc o = new otherFunc();
        //    List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.displayDataWithDocNames(username);

        //    dgvExpenses.Rows.Clear();

        //    foreach ((string docName, DocumentSnapshot docsnap) in documentData)
        //    {
        //        FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
        //        if (docsnap.Exists)
        //        {
        //            dgvExpenses.Rows.Add(docName, fd.Category, fd.Amount.ToString(), fd.Date.ToString());
        //        }
        //    }
        //}

        public async void displayGoals()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.getGoalsWithDocNames(username);
            //dgvExpenses.Rows.Clear();
            foreach ((string docName, DocumentSnapshot docsnap) in documentData)
            {
                float am = docsnap.GetValue<float>("Amount");
                String d = docsnap.GetValue<String>("GoalDate");
                if (docsnap.Exists)
                {
                    string docname = docName;
                    string amount = am.ToString();
                    string date = d.ToString();

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(329, 92);
                    pnl.BackColor = Color.Transparent;
                    pnl.BorderThickness = 2;
                    pnl.BorderStyle = DashStyle.Solid;
                    pnl.BorderColor = Color.FromArgb(187, 141, 228);
                    pnl.BorderRadius = 13;

                    System.Windows.Forms.Label lblDocname = new System.Windows.Forms.Label();
                    lblDocname.Font = new Font("Poppins", 14.25f, FontStyle.Bold | FontStyle.Regular);
                    lblDocname.BackColor = Color.Transparent;
                    lblDocname.Size = new Size(100, 24);
                    lblDocname.Location = new Point(23, 22);
                    lblDocname.ForeColor = Color.FromArgb(187, 141, 228);
                    lblDocname.Text = docname;

                    System.Windows.Forms.Label lblAmount = new System.Windows.Forms.Label();
                    lblAmount.Font = new Font("Poppins", 14.25f, FontStyle.Bold | FontStyle.Regular);
                    lblAmount.BackColor = Color.Transparent;
                    lblAmount.Size = new Size(113, 24);
                    lblAmount.Location = new Point(193, 22);
                    lblAmount.ForeColor = Color.FromArgb(187, 141, 228);
                    lblAmount.Text = "₱" + amount;

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 12f, FontStyle.Bold | FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(140, 20);
                    lblDate.Location = new Point(23, 49);
                    lblDate.ForeColor = Color.FromArgb(187, 141, 228);
                    lblDate.Text = date;

                    pnl.DoubleClick += (sender, e) => PnlGoals_DoubleClick(sender, e, docname);

                    pnl.Controls.Add(lblDocname);
                    pnl.Controls.Add(lblAmount);
                    pnl.Controls.Add(lblDate);
                    flpGoals.Controls.Add(pnl);
                }
            }
        }
        public async void PnlGoals_DoubleClick(object sender, EventArgs e, string dn)
        {
            otherFunc function = new otherFunc();
            string goalId = dn;
            Dictionary<string, object> data = await function.getItemsInsideGoalsID(username, goalId);
            GoalDetails gd = new GoalDetails(this);
            gd.displayGoalDetails(data, goalId);
            gd.displaySuggestions(goalId);
            gd.StartPosition = FormStartPosition.CenterScreen;
            gd.ShowDialog();
        }
        //private void ExpenseGridDesign()
        //{
        //    dgvExpenses.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 16, FontStyle.Regular);
        //    dgvExpenses.DefaultCellStyle.Font = new Font("Poppins", 14, FontStyle.Regular);
        //    dgvExpenses.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Top, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Right, dgvExpenses.ColumnHeadersDefaultCellStyle.Padding.Bottom);
        //    dgvExpenses.DefaultCellStyle.Padding = new Padding(15, dgvExpenses.DefaultCellStyle.Padding.Top, dgvExpenses.DefaultCellStyle.Padding.Right, dgvExpenses.DefaultCellStyle.Padding.Bottom);
        //    dgvExpenses.ColumnHeadersHeight = 35;
        //    dgvExpenses.RowTemplate.Height = 30;
        //    dgvExpenses.ReadOnly = true;
        //    dgvExpenses.Columns[0].Width = 150;
        //}
        //private async void dgvExpenses_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    otherFunc function = new otherFunc();
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        if (flagGoal)
        //        {
        //            string expenseId = dgvExpenses.Rows[e.RowIndex].Cells[0].Value.ToString();
        //            Dictionary<string, object> data = await function.getItemsInsideExpenseId(username, expenseId);

        //            ExpenseDetailForm edf = new ExpenseDetailForm();
        //            edf.displayExpenseDetails(data);
        //            edf.StartPosition = FormStartPosition.Manual;
        //            int x = Screen.PrimaryScreen.WorkingArea.Right - edf.Width;
        //            int y = Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - edf.Height) / 2;
        //            edf.Location = new Point(x, y);
        //            edf.ShowDialog();
        //        }
        //        else
        //        {
        //            string goalId = dgvExpenses.Rows[e.RowIndex].Cells[0].Value.ToString();
        //            Dictionary<string, object> data = await function.getItemsInsideGoalsID(username, goalId);
        //            GoalDetails gd = new GoalDetails(this);
        //            gd.displayGoalDetails(data, goalId);
        //            gd.StartPosition = FormStartPosition.CenterScreen;
        //            gd.ShowDialog();

        //        }
        //    }
        //}

        //private void dgvExpenses_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        DataGridViewCell cell = dgvExpenses.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //        toolTip1.SetToolTip(dgvExpenses, "View Full Details");
        //    }
        //}

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            AddGoals ag = new AddGoals(this);
            ag.StartPosition = FormStartPosition.CenterScreen;
            ag.ShowDialog();
        }

        //public bool flagGoal = true;
        //private void btnGoals_Click(object sender, EventArgs e)
        //{
        //    if (flagGoal)
        //    {
        //        //button
        //        panelSwitch.FillColor = Color.FromArgb(227, 180, 72);
        //        panelSwitch.FillColor2 = Color.FromArgb(83, 123, 47);
        //        btnGoals.ForeColor = Color.DarkGreen;
        //        btnGoals.Text = "Transactions";
        //        //
        //        //DATA GRID
        //        panelTitle.FillColor = Color.FromArgb(187, 141, 228);
        //        label7.Text = "Goals";
        //        label7.Anchor = AnchorStyles.Right;
        //        dgvExpenses.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Amethyst;
        //        dgvExpenses.Rows.Clear();
        //        dgvExpenses.Columns.Clear();
        //        setDGVHeaders(flagGoal);
        //        //displayGoals();
        //        flagGoal = false;
        //    }
        //    else
        //    {
        //        //button
        //        panelSwitch.FillColor = Color.FromArgb(187, 141, 228);
        //        panelSwitch.FillColor2 = Color.FromArgb(229, 148, 98);
        //        btnGoals.ForeColor = Color.DarkOrchid;
        //        btnGoals.Text = "Goals";
        //        //
        //        //DATA GRID
        //        panelTitle.FillColor = Color.FromArgb(83, 123, 47);
        //        label7.Text = "Transactions";
        //        dgvExpenses.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.LightGreen;
        //        dgvExpenses.Rows.Clear();
        //        dgvExpenses.Columns.Clear();
        //        setDGVHeaders(flagGoal);
        //        flagGoal = true;
        //    }
        //}

        //private void setDGVHeaders(bool flag)
        //{
        //    if (flag)
        //    {
        //        String[] transactionHeaders = { "Title", "Amount", "Date" };
        //        foreach (String header in transactionHeaders)
        //        {
        //            DataGridViewTextBoxColumn d = new DataGridViewTextBoxColumn();
        //            d.HeaderText = header;
        //            dgvExpenses.Columns.Add(d);
        //        }
        //    }
        //    else
        //    {
        //        String[] transactionHeaders = { "Expense ID", "Category", "Amount", "Date" };
        //        foreach (String header in transactionHeaders)
        //        {
        //            DataGridViewTextBoxColumn d = new DataGridViewTextBoxColumn();
        //            d.HeaderText = header;
        //            dgvExpenses.Columns.Add(d);
        //        }
        //    }
        //}

        public async void displayData()
        {
            string username = FirebaseData.Instance.Username;
            otherFunc o = new otherFunc();
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.displayDataWithDocNames(username);

            //dgvExpenses.Rows.Clear();

            foreach ((string docName, DocumentSnapshot docsnap) in documentData)
            {
                FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
                if (docsnap.Exists)
                {
                    string dn = docName;
                    string category = fd.Category;
                    string amount = fd.Amount.ToString();
                    string date = fd.Date.ToString();

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(657, 106);
                    pnl.BackColor = Color.Transparent;
                    pnl.BorderThickness = 2;
                    pnl.BorderStyle = DashStyle.Solid;
                    pnl.BorderColor = Color.FromArgb(83, 123, 47);
                    pnl.BorderRadius = 20;

                    System.Windows.Forms.Label lblCat = new System.Windows.Forms.Label();
                    lblCat.Font = new Font("Poppins", 15.75f, FontStyle.Bold | FontStyle.Regular);
                    lblCat.BackColor = Color.Transparent;
                    lblCat.Size = new Size(220, 37);
                    lblCat.Location = new Point(27, 23);
                    lblCat.ForeColor = Color.FromArgb(83, 123, 47);
                    lblCat.Text = category;

                    System.Windows.Forms.Label lblAmount = new System.Windows.Forms.Label();
                    lblAmount.Font = new Font("Poppins", 15.75f, FontStyle.Bold | FontStyle.Regular);
                    lblAmount.BackColor = Color.Transparent;
                    lblAmount.Size = new Size(220, 37);
                    lblAmount.Location = new Point(500, 23);
                    lblAmount.ForeColor = Color.FromArgb(83, 123, 47);
                    lblAmount.Text = "₱" + amount;

                    System.Windows.Forms.Label lbldocname = new System.Windows.Forms.Label();
                    lbldocname.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    lbldocname.BackColor = Color.Transparent;
                    lbldocname.Size = new Size(276, 37);
                    lbldocname.Location = new Point(28, 56);
                    lbldocname.ForeColor = Color.FromArgb(83, 123, 47);
                    lbldocname.Text = docName;

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(220, 37);
                    lblDate.Location = new Point(500, 56);
                    lblDate.ForeColor = Color.FromArgb(83, 123, 47);
                    lblDate.Text = date;

                    pnl.DoubleClick += (sender, e) => PnlExpenses_DoubleClick(sender, e, dn);

                    pnl.Controls.Add(lblCat);
                    pnl.Controls.Add(lblAmount);
                    pnl.Controls.Add(lbldocname);
                    pnl.Controls.Add(lblDate);
                    flpExpenses.Controls.Add(pnl);
                }
            }
        }   
        public async void PnlExpenses_DoubleClick(object sender, EventArgs e, string dn)
        {
            otherFunc function = new otherFunc();
            string expenseId = dn;
            Dictionary<string, object> data = await function.getItemsInsideExpenseId(username, expenseId);

            ExpenseDetailForm edf = new ExpenseDetailForm();
            edf.displayExpenseDetails(data);
            edf.StartPosition = FormStartPosition.Manual;
            int x = Screen.PrimaryScreen.WorkingArea.Right - edf.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - edf.Height) / 2;
            edf.Location = new Point(x, y);
            edf.ShowDialog();
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
