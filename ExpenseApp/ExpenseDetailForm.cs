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
        public ExpenseDetailForm()
        {
            InitializeComponent();
        }
        public void displayExpenseDetails(Dictionary<string, object> data)
        {
            string amountString = data["Amount"].ToString();
            string category = data["Category"].ToString();
            if (int.TryParse(amountString, out int amount)){
                string beautifyAmount = otherFunc.amountBeautify(amount);
                lblAmount.Text = beautifyAmount;    
            }
            pictureBox1.Image = imageCategory(category);
            lblCategory.Text = category;
            lblDate.Text = data["Date"].ToString();
            lblLocation.Text = data["Location"].ToString();
            lblName.Text = data["Name"].ToString();
            Console.WriteLine(category);
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
    }
}
