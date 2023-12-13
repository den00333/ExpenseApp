using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class offWalletUC : UserControl
    {
        public String p = "";
        public offWalletUC(String fPath)
        {
            InitializeComponent();
            this.p = fPath;
            loadData(p);
        }

        private void offWalletUC_Load(object sender, EventArgs e)
        {
            lblBalance.Text = otherFunc.amountBeautify(float.Parse(offlineFunc.readTxt(p, 0)));
            lblExpenses.Text = otherFunc.amountBeautify(float.Parse(offlineFunc.readTxt(p, 1))); 
            lblShort.Text = otherFunc.amountBeautify(float.Parse(offlineFunc.readTxt(p, 2))); ;
            checkShort(offlineFunc.readTxt(p, 2));
        }

        private void checkShort(String val)
        {
            if (val.Equals("0"))
            {
                lblShort.Hide();
            }
            else
            {
                lblShort.ForeColor = Color.Red;
                lblShort.Show();
            }
        }

        public void checkShort(float val)
        {
            if (val == 0)
            {
                lblShort.Hide();
            }
            else
            {
                lblShort.ForeColor = Color.Red;
                lblShort.Show();
            }
        }

        private void btnAddMoney_Click(object sender, EventArgs e)
        {
            AddingBalanceForm abf = new AddingBalanceForm(true, this);
            abf.ShowDialog();
        }

        private void btnAddXpns_Click(object sender, EventArgs e)
        {
            AddExpensesForm aef = new AddExpensesForm(true, this);
            aef.ShowDialog();
        }

        public void loadData(String path)
        {
            if (File.Exists(path))
            {
                String[] lines = File.ReadAllLines(path);
                String[] splitted1 = lines[4].Split(':');
                String[] eField = splitted1[1].Split('~');
                
                int end = eField.Length - 1;

                //foreach (String s in eField)
                for (int i = end-1; i >= 0; i--)
                {
                    String[] items = eField[i].Split('|');

                    

                    Console.WriteLine("length items:" + items[0].Length);
                    String amount = items[0];
                    String category = items[1];
                    String date = items[2];
                    String location = items[3];
                    String Desc = items[4];

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

                    System.Windows.Forms.Label lblDate = new System.Windows.Forms.Label();
                    lblDate.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    lblDate.BackColor = Color.Transparent;
                    lblDate.Size = new Size(220, 37);
                    lblDate.Location = new Point(500, 56);
                    lblDate.ForeColor = Color.FromArgb(83, 123, 47);
                    lblDate.Text = date;

                    
                    /*pnl.DoubleClick += (sender, e) => PnlExpenses_DoubleClick(sender, e, dn);
                    lblAmount.DoubleClick += (sender, e) => PnlExpenses_DoubleClick(sender, e, dn);
                    lblCat.DoubleClick += (sender, e) => PnlExpenses_DoubleClick(sender, e, dn);
                    lblDate.DoubleClick += (sender, e) => PnlExpenses_DoubleClick(sender, e, dn);*/

                    pnl.Controls.Add(lblCat);
                    pnl.Controls.Add(lblAmount);
                    pnl.Controls.Add(lblDate);
                    flpExpenses.Controls.Add(pnl);

                }
            }
        }
    }
}
