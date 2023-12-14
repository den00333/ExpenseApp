using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class ExpenseDetailForm : Form
    {
        private string docname;
        private wallet w;
        private group g;
        private string username = FirebaseData.Instance.Username;
        private bool flag;
        public string groupCode { get; set; }
        public ExpenseDetailForm(string dn, wallet pitaka, group grupo, bool f)
        {
            InitializeComponent();
            this.docname = dn;
            this.g = grupo;
            this.w = pitaka;
            this.flag = f;
        }
        public async Task displayExpenseDetails(Dictionary<string, object> data, bool flag)
        {
            //true -> USER
            //false -> GROUP
            if (flag)
            {
                string amountString = data["Amount"].ToString();
                string category = data["Category"].ToString();
                if (int.TryParse(amountString, out int amount))
                {
                    string beautifyAmount = otherFunc.amountBeautify(amount);
                    lblAmount.Text = beautifyAmount;
                }
                pictureBox1.Image = imageCategory(category);
                lblCategory.Text = category;
                lblDate.Text = data["Date"].ToString();
                lblLocation.Text = data["Location"].ToString();
                lblName.Text = data["Name"].ToString();
                pnlCreator.Visible = false;
                Console.WriteLine(category);
            }
            else
            {
                String username = FirebaseData.Instance.Username;
                string amountString = data["Amount"].ToString();
                string category = data["Category"].ToString();
                if (int.TryParse(amountString, out int amount))
                {
                    string beautifyAmount = otherFunc.amountBeautify(amount);
                    lblAmount.Text = beautifyAmount;
                }
                pictureBox1.Image = imageCategory(category);
                lblCategory.Text = category;
                lblDate.Text = data["Date"].ToString();
                lblLocation.Text = data["Location"].ToString();
                lblName.Text = data["Name"].ToString();
                String fullName = await otherFunc.getFullName(username);
                lblCreator.Text = $"By: {fullName}";
                Console.WriteLine(category);
            }
        }
        private Image imageCategory(string category)
        {
            if (category == "Food")
            {
                return Properties.Resources.food;
            }
            else if (category == "Transportation")
            {
                return Properties.Resources.transportation;
            }
            else if (category == "Entertainment")
            {
                return Properties.Resources.entertainment;
            }
            else if (category == "Housing")
            {
                return Properties.Resources.housing;
            }
            else if (category == "Medical")
            {
                return Properties.Resources.medical;
            }
            return Properties.Resources.noimage;
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();    
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {

                if (flag)
                {
                    var db = otherFunc.FirestoreConn();
                    DocumentReference docref = db.Collection("Users").Document(username).Collection("Expenses").Document(docname);
                    docref.DeleteAsync();
                    this.Hide();
                    w.flpExpenses.Controls.Clear();
                    w.displayData(); 
                }
                else
                {
                    var db = otherFunc.FirestoreConn();
                    DocumentReference docref = db.Collection("Groups").Document(groupCode).Collection("Expenses").Document(docname);
                    docref.DeleteAsync();
                    this.Hide();
                    g.flpExpenses.Controls.Clear();
                    g.displayData();
                }
            }
        }
    }
}
