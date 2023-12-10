using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class RecurringExpensesForm : Form
    {
        public RecurringExpensesForm()
        {
            InitializeComponent();
            displayRecurData();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public async void displayRecurData()
        {
            String username = FirebaseData.Instance.Username;
            List<DocumentSnapshot> data = await otherFunc.displayRecurringData(username);
            foreach (DocumentSnapshot dsnap in data)
            {
                if (dsnap.Exists)
                {
                    String title = dsnap.GetValue<String>("Title");
                    String[] date = dsnap.GetValue<String>("Date").Split('|');
                    String target = dsnap.GetValue<String>("TargetDate");

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(329, 92);
                    pnl.BackColor = Color.Transparent;
                    pnl.BorderThickness = 2;
                    pnl.BorderStyle = DashStyle.Solid;
                    pnl.BorderColor = Color.FromArgb(187, 141, 228);
                    pnl.BorderRadius = 13;

                    System.Windows.Forms.Label lblTitle = new System.Windows.Forms.Label();
                    lblTitle.Font = new Font("Poppins", 12f, FontStyle.Bold | FontStyle.Regular);
                    lblTitle.BackColor = Color.Transparent;
                    lblTitle.Size = new Size(100, 24);
                    lblTitle.Location = new Point(27, 22);
                    lblTitle.ForeColor = Color.FromArgb(187, 141, 228);
                    lblTitle.Text = title;

                    System.Windows.Forms.Label lblPeriod = new System.Windows.Forms.Label();
                    lblPeriod.Font = new Font("Poppins", 11f, FontStyle.Bold | FontStyle.Regular);
                    lblPeriod.BackColor = Color.Transparent;
                    lblPeriod.Size = new Size(100, 22);
                    lblPeriod.Location = new Point(27, 49);
                    lblPeriod.ForeColor = Color.FromArgb(187, 141, 228);
                    lblPeriod.Text = date[1];

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 11f, FontStyle.Bold | FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(200, 22);
                    lblDate.Location = new Point(170, 22);
                    lblDate.ForeColor = Color.FromArgb(187, 141, 228);
                    String nameDay = otherFunc.dateBeautifyForRE(date[0], date[1]);
                    
                    lblDate.Text = nameDay;

                    System.Windows.Forms.Label lblTarget = new System.Windows.Forms.Label();
                    lblTarget.Font = new Font("Poppins", 11f, FontStyle.Bold | FontStyle.Regular);
                    lblTarget.BackColor = Color.Transparent;
                    lblTarget.Size = new Size(200, 22);
                    lblTarget.Location = new Point(170, 50);
                    lblTarget.ForeColor = Color.FromArgb(187, 141, 228);
                    lblTarget.Text = target;

                    pnl.Controls.Add(lblTitle);
                    pnl.Controls.Add(lblPeriod);
                    pnl.Controls.Add(lblDate);
                    pnl.Controls.Add(lblTarget);
                    flpRE.Controls.Add(pnl);
                }
            }
        }
    }
}
