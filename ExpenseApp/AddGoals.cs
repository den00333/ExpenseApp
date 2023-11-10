using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class AddGoals : Form
    {
        wallet w;
        public AddGoals(wallet wal)
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Now.Date;
            this.w = wal;
        }

        private void AddGoals_Load(object sender, EventArgs e)
        {

        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            String username = FirebaseData.Instance.Username;
            String gd = dtpDate.Value.Date.ToString("yyyy-MM-dd");
            float amount = float.Parse(txtAmount.Text);
            String title = txtTitle.Text.ToString();
            String desc = richTxtDesc.Text.ToString();
            String res = await otherFunc.addNewGoal(username,gd, amount, title, desc);
            if (!w.flagGoal)
            {
                w.displayGoals();
            }
            DialogResult r = MessageBox.Show(res, "Response", MessageBoxButtons.OK);
            if(r == DialogResult.OK)
            {
                if (res.Equals("Successfully Added!"))
                {
                    dtpDate.Value = DateTime.Now.Date;
                    txtAmount.Clear();
                    txtTitle.Clear();
                    richTxtDesc.Clear();
                }
                else
                {
                    txtTitle.Clear();
                }
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            string pattern = @"[\d\b]";
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), pattern))
            {
                e.Handled = true;
            }
        }
    }
}
