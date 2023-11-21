using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class GoalDetails : Form
    {
        wallet w;
        public GoalDetails(wallet wal)
        {
            InitializeComponent();
            this.w = wal;
        }

        public void displayGoalDetails(Dictionary<string, object> data, String title)
        {
            lblTitle.Text = title;
            lblDate.Text = data["GoalDate"].ToString();
            lblAmount.Text = otherFunc.amountBeautify(float.Parse(data["Amount"].ToString()));
            rtbDesc.Text = data["Description"].ToString();
        }
        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String username = FirebaseData.Instance.Username;
            String title = lblTitle.Text.ToString();
            DialogResult res = MessageBox.Show("Are you sure you want to delete this?", "Deleting Goal", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                otherFunc.deleteInsideUser(username, "Goals", title);
                this.Hide();
                w.displayGoals();
            }
        }

        private void GoalDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
