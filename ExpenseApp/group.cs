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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseApp
{
    public partial class group : UserControl
    {
        string username = FirebaseData.Instance.Username;
        private string groupCode;
        public group()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void btnGroup_Click(object sender, EventArgs e)
        {
            
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }

        private void group_Load(object sender, EventArgs e)
        {
            displayGroups();
        }

        public async void displayGroups()
        {
            otherFunc o = new otherFunc();
            string[] groups = await o.getGroups(username);
            var db = otherFunc.FirestoreConn();
            CollectionReference colref = db.Collection("Groups");
            foreach (string g in groups)
            {
                DocumentReference docref = colref.Document(g);
                DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

                if (docsnap.Exists)
                {
                    string groupN = docsnap.GetValue<string>("GroupName");
                    string groupC = docsnap.GetValue<string>("GroupCode");

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(195, 52);
                    pnl.BorderRadius = 13;
                    pnl.FillColor = Color.FromArgb(227, 180, 72);
                    pnl.FillColor2 = Color.FromArgb(83, 123, 47);
                    pnl.Cursor = Cursors.Hand;

                    System.Windows.Forms.Label lblGroupname = new System.Windows.Forms.Label();
                    lblGroupname.Font = new Font("Poppins Regular", 14.25f);
                    lblGroupname.BackColor = Color.Transparent;
                    lblGroupname.Size = new Size(200, 33);
                    lblGroupname.Location = new Point(19, 12);
                    lblGroupname.ForeColor = Color.White;
                    lblGroupname.Text = groupN;
                    lblGroupname.Cursor = Cursors.Hand;

                    pnl.Click += (sender, e) => {
                        ResetPanelColors();
                        pnlGroups_Click(sender, e, groupC, groupN);
                        lblGroupname.ForeColor = Color.Black;
                        pnl.FillColor = Color.Transparent;
                        pnl.BorderColor = Color.FromArgb(83, 123, 47);
                        pnl.BorderThickness = 3;
                        pnl.BorderRadius = 13;
                    };
                    lblGroupname.Click += (sender, e) => {
                        ResetPanelColors();
                        lblGroupname_Click(lblGroupname, e, groupC, groupN);
                        lblGroupname.ForeColor = Color.Black;
                        pnl.FillColor = Color.Transparent;
                        pnl.BorderColor = Color.FromArgb(83, 123, 47);
                        pnl.BorderThickness = 3;
                        pnl.BorderRadius = 13;
                    };
                    pnl.Controls.Add(lblGroupname);
                    flpGroup.Controls.Add(pnl);
                }
            }
        }
        private void ResetPanelColors()
        {
            foreach (Control control in flpGroup.Controls)
            {
                if (control is Guna2GradientPanel panel )
                {

                    panel.FillColor = Color.FromArgb(227, 180, 72);
                    panel.FillColor2 = Color.FromArgb(83, 123, 47);
                    panel.BorderThickness = 0;
                    foreach(Control control2 in panel.Controls)
                    {
                        if(control2 is System.Windows.Forms.Label label)
                        {
                            label.ForeColor = Color.White;
                        }
                    }
                }
            }
        }
        public void pnlGroups_Click(object sender, EventArgs e, string gc, string groupN)
        {
            groupCode = gc;
            AddExpensesForm adf = new AddExpensesForm(new wallet(), true, groupCode, this);
            lblGN.Text = groupN;
            Console.WriteLine(groupCode);
            loadWalletGroup();
            flpMembers.Controls.Clear();
            displayMembers(groupCode);
        }
        public void lblGroupname_Click(object sender, EventArgs e, string gc, string groupN)
        {
            groupCode = gc;
            AddExpensesForm adf = new AddExpensesForm(new wallet(), true, groupCode, this);
            lblGN.Text = groupN;
            loadWalletGroup();
            flpMembers.Controls.Clear();
            displayMembers(groupCode);
            flpExpenses.Controls.Clear();
            displayData();
            flpGoals.Controls.Clear();
            displayGoals();
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            AddingBalanceForm abf = new AddingBalanceForm(new wallet(), true, groupCode, this);
            abf.StartPosition = FormStartPosition.CenterScreen; 
            abf.ShowDialog();
        }

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm adf = new AddExpensesForm(new wallet(), true, groupCode, this);
            adf.StartPosition = FormStartPosition.CenterScreen;
            adf.ShowDialog();
        }
        private async void loadWalletGroup()
        {
            otherFunc o = new otherFunc();
            DocumentReference docRef = await o.SavingWalletAmountOfGroup(groupCode, "Balance");
            float BalanceAmount = await o.getWalletAmount(docRef);
            lblBalance.Text = otherFunc.amountBeautify(BalanceAmount);

            DocumentReference docRef2 = await o.SavingWalletAmountOfGroup(groupCode, "Expense");
            float ExpenseAmount = await o.getWalletAmount(docRef2);
            lblExpenses.Text = otherFunc.amountBeautify(ExpenseAmount);

            float negVal = await otherFunc.getShortGroup(groupCode);
            if (negVal != 0)
            {
                lblShort.Text = otherFunc.amountBeautify(negVal);
                lblShort.ForeColor = Color.Red;
            }
            else
            {
                lblShort.Text = "";
            }
        }
        public async void displayMembers(string groupCode)
        {
            Console.WriteLine(groupCode);
            otherFunc o = new otherFunc();
            string[] members = await o.getMembers(groupCode);
            foreach (string member in members)
            {

                Console.WriteLine(member);
                var db = otherFunc.FirestoreConn();
                DocumentReference docref = db.Collection("Users").Document(member);
                DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

                if (docsnap.Exists)
                {
                    FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
                    string firstname = fd.FirstName;
                    string lastname = fd.LastName;
                    string status = fd.status;
                    Panel pnl = new Panel();
                    pnl.Size = new Size(229, 40);

                    System.Windows.Forms.Label lblname = new System.Windows.Forms.Label();
                    lblname.Font = new Font("Poppins", 9.75f,FontStyle.Bold);
                    lblname.BackColor = Color.Transparent;
                    lblname.Size = new Size(185, 19);
                    lblname.Location = new Point(47, 13);
                    lblname.ForeColor = Color.Black;
                    lblname.Text = firstname + " " + lastname;

                    Guna2CirclePictureBox ptb = new Guna2CirclePictureBox();
                    ptb.SizeMode = PictureBoxSizeMode.Zoom;
                    ptb.Size = new Size(28, 26);
                    ptb.Location = new Point(14, 11);

                    if (status == "online"){
                        ptb.Image = Properties.Resources.online;
                    }
                    else{
                        ptb.Image = Properties.Resources.offline;
                    }
                    pnl.Controls.Add(lblname);
                    pnl.Controls.Add(ptb);
                    flpMembers.Controls.Add(pnl);
                }
            }
        }

        private void btnAddGoal_Click(object sender, EventArgs e)
        {
            AddGoals addGoal = new AddGoals(new wallet(), true, groupCode, this);
            addGoal.StartPosition = FormStartPosition.CenterScreen;
            addGoal.ShowDialog();
        }
        public async void displayData()
        {
            otherFunc o = new otherFunc();
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.displayDataWithDocNamesGroup(groupCode);

            //dgvExpenses.Rows.Clear();

            foreach ((string docName, DocumentSnapshot docsnap) in documentData)
            {
                FirebaseData fd = docsnap.ConvertTo<FirebaseData>();
                if (docsnap.Exists)
                {
                    string dn = docName;
                    string creator = docsnap.GetValue<String>("Creator");
                    string category = fd.Category;
                    string amount = fd.Amount.ToString();
                    string date = fd.Date.ToString();

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(440, 106);
                    pnl.BackColor = Color.Transparent;
                    pnl.BorderThickness = 2;
                    pnl.BorderStyle = DashStyle.Solid;
                    pnl.BorderColor = Color.FromArgb(83, 123, 47);
                    pnl.BorderRadius = 20;

                    System.Windows.Forms.Label lblCat = new System.Windows.Forms.Label();
                    lblCat.Font = new Font("Poppins", 11.25f, FontStyle.Bold | FontStyle.Regular);
                    lblCat.BackColor = Color.Transparent;
                    lblCat.Size = new Size(220, 37);
                    lblCat.Location = new Point(37, 19);
                    lblCat.ForeColor = Color.FromArgb(83, 123, 47);
                    lblCat.Text = category;

                    System.Windows.Forms.Label lblAmount = new System.Windows.Forms.Label();
                    lblAmount.Font = new Font("Poppins", 11.25f, FontStyle.Bold | FontStyle.Regular);
                    lblAmount.BackColor = Color.Transparent;
                    lblAmount.Size = new Size(220, 37);
                    lblAmount.Location = new Point(302, 19);
                    lblAmount.ForeColor = Color.FromArgb(83, 123, 47);
                    lblAmount.Text = "₱" + amount;

                    System.Windows.Forms.Label lblcreator = new System.Windows.Forms.Label();
                    lblcreator.Font = new Font("Poppins", 9.75f, FontStyle.Regular);
                    lblcreator.BackColor = Color.Transparent;
                    lblcreator.Size = new Size(276, 37);
                    lblcreator.Location = new Point(37, 52);
                    lblcreator.ForeColor = Color.FromArgb(83, 123, 47);
                    lblcreator.Text = "Added By: " + await o.getFirstname(creator);

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 9.75f, FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(220, 37);
                    lblDate.Location = new Point(302, 52);
                    lblDate.ForeColor = Color.FromArgb(83, 123, 47);
                    lblDate.Text = date;

                    pnl.Click += (sender, e) => PnlExpenses_Click(sender, e, dn);
                    lblAmount.Click += (sender, e) => PnlExpenses_Click(sender, e, dn);
                    lblCat.Click += (sender, e) => PnlExpenses_Click(sender, e, dn);
                    lblcreator.Click += (sender, e) => PnlExpenses_Click(sender, e, dn);
                    lblDate.Click += (sender, e) => PnlExpenses_Click(sender, e, dn);

                    pnl.Controls.Add(lblCat);
                    pnl.Controls.Add(lblAmount);
                    pnl.Controls.Add(lblcreator);
                    pnl.Controls.Add(lblDate);
                    flpExpenses.Controls.Add(pnl);
                }
            }
        }
        public async void PnlExpenses_Click(object sender, EventArgs e, string dn)
        {
            Console.WriteLine("pnlexpensesDoubleclick is working");
            otherFunc function = new otherFunc();
            string expenseId = dn;
            Dictionary<string, object> data = await function.getItemsInsideExpenseIdGroup(groupCode, expenseId);

            ExpenseDetailForm edf = new ExpenseDetailForm();

            await edf.displayExpenseDetails(data, false);
            edf.StartPosition = FormStartPosition.CenterScreen;
            edf.ShowDialog();
        }
        public async void displayGoals()
        {
            otherFunc o = new otherFunc();
            List<(string DocName, DocumentSnapshot DocSnapshot)> documentData = await o.getGoalsWithDocNamesGroup(groupCode);
            //dgvExpenses.Rows.Clear();
            int r = 0;
            int g = 0;
            int b = 0;
            foreach ((string docName, DocumentSnapshot docsnap) in documentData)
            {
                float am = docsnap.GetValue<float>("Amount");
                String d = docsnap.GetValue<String>("GoalDate");
                String status = docsnap.GetValue<String>("Status");
                string c = docsnap.GetValue<String>("Creator");
                if (docsnap.Exists)
                {
                    string docname = docName;
                    float amount = am;
                    string date = d.ToString();
                    string creator = c;


                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(306, 107);
                    pnl.BackColor = Color.Transparent;
                    pnl.BorderThickness = 2;
                    pnl.BorderStyle = DashStyle.Solid;

                    if (status.Equals("Ongoing"))
                    {
                        r = 187;
                        g = 141;
                        b = 228;
                    }
                    else if (status.Equals("Achieved"))
                    {
                        r = 139;
                        g = 237;
                        b = 19;
                    }
                    else
                    {
                        pnl.BorderColor = Color.FromArgb(217, 28, 28);
                        r = 217;
                        g = 28;
                        b = 28;
                    }
                    pnl.BorderColor = Color.FromArgb(r, g, b);
                    pnl.BorderRadius = 13;

                    System.Windows.Forms.Label lblDocname = new System.Windows.Forms.Label();
                    lblDocname.Font = new Font("Poppins", 9.75f, FontStyle.Bold | FontStyle.Regular);
                    lblDocname.BackColor = Color.Transparent;
                    lblDocname.Size = new Size(180, 24);
                    lblDocname.Location = new Point(23, 22);
                    lblDocname.ForeColor = Color.FromArgb(r, g, b);
                    lblDocname.Text = "Added By: "+ await o.getFirstname(creator);

                    System.Windows.Forms.Label lblAmount = new System.Windows.Forms.Label();
                    lblAmount.Font = new Font("Poppins", 9.75f, FontStyle.Bold | FontStyle.Regular);
                    lblAmount.BackColor = Color.Transparent;
                    lblAmount.Size = new Size(113, 24);
                    lblAmount.Location = new Point(188, 22);
                    lblAmount.ForeColor = Color.FromArgb(r, g, b);
                    lblAmount.Text = otherFunc.amountBeautify(amount);

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 9.75f, FontStyle.Bold | FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(140, 20);
                    lblDate.Location = new Point(23, 49);
                    lblDate.ForeColor = Color.FromArgb(r, g, b);
                    lblDate.Text = date;

                    pnl.DoubleClick += (sender, e) => PnlGoals_DoubleClick(sender, e, docname);
                    lblDocname.DoubleClick += (sender, e) => PnlGoals_DoubleClick(sender, e, docname);
                    lblDate.DoubleClick += (sender, e) => PnlGoals_DoubleClick(sender, e, docname);
                    lblAmount.DoubleClick += (sender, e) => PnlGoals_DoubleClick(sender, e, docname);

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
            Dictionary<string, object> data = await function.getItemsInsideGoalsIDGroup(groupCode, goalId);
            GoalDetails gd = new GoalDetails(new wallet(), this); ;
            gd.displayGoalDetails(data, goalId);
            gd.displaySuggestions(goalId, false, groupCode);
            gd.StartPosition = FormStartPosition.CenterScreen;
            gd.ShowDialog();
        }
    }
}
