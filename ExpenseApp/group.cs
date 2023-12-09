using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    //string[] members = docsnap.GetValue<string[]>("Members");
                    //int totalMembers = members.Count();

                    lblGN.Text = groupN;

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(195, 52);
                    pnl.BorderRadius = 13;
                    pnl.FillColor = Color.FromArgb(227, 180, 72);
                    pnl.FillColor2 = Color.FromArgb(83, 123, 47);

                    System.Windows.Forms.Label lblGroupname = new System.Windows.Forms.Label();
                    lblGroupname.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    lblGroupname.BackColor = Color.Transparent;
                    lblGroupname.Size = new Size(200, 33);
                    lblGroupname.Location = new Point(19, 12);
                    lblGroupname.ForeColor = Color.White;
                    lblGroupname.Text = groupN;


                    //System.Windows.Forms.Label lbltotalMembers = new System.Windows.Forms.Label();
                    //lbltotalMembers.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    //lbltotalMembers.BackColor = Color.Transparent;
                    //lbltotalMembers.Size = new Size(60, 33);  
                    //lbltotalMembers.Location = new Point(301, 24);
                    //lbltotalMembers.ForeColor = Color.White;
                    //lbltotalMembers.Text = totalMembers.ToString();

                    pnl.Click += (sender, e) => pnlGroups_Click(sender, e, groupC);
                    //pnl.Controls.Add(lbltotalMembers);
                    pnl.Controls.Add(lblGroupname);
                    flpGroup.Controls.Add(pnl);
                }
            }
        }
        public void pnlGroups_Click(object sender, EventArgs e, string gc)
        {
            groupCode = gc;
            AddExpensesForm adf = new AddExpensesForm(new wallet(), true, groupCode, this);
            Console.WriteLine(groupCode);
            loadWalletGroup();
            flpMembers.Controls.Clear();
            displayMembers(groupCode);
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            AddingBalanceForm abf = new AddingBalanceForm(new wallet(), true, groupCode, this);
            abf.ShowDialog();
        }

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm adf = new AddExpensesForm(new wallet(), true, groupCode, this);
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

                    Panel pnl = new Panel();
                    pnl.Size = new Size(229, 40);

                    System.Windows.Forms.Label lblname = new System.Windows.Forms.Label();
                    lblname.Font = new Font("Poppins", 9.75f, FontStyle.Bold);
                    lblname.BackColor = Color.Transparent;
                    lblname.Size = new Size(185, 19);
                    lblname.Location = new Point(35, 9);
                    lblname.ForeColor = Color.Black;
                    lblname.Text = firstname + " " + lastname;

                    pnl.Controls.Add(lblname);
                    flpMembers.Controls.Add(pnl);
                }
            }
        }
    }
}
