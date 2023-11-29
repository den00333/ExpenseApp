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
            lblGoalStatus.Text = data["Status"].ToString();

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

        public async void displaySuggestions(String title)
        {
            String username = FirebaseData.Instance.Username;
            //double goalAmount = await otherFunc.getTotalGoalAmount(username);
            double currentSavings = await otherFunc.getCurrentSavings(username, title);
            //rtbSuggestion.Text = Math.Round(currentSavings, 2).ToString();
            double goalAmount = await otherFunc.getGoalAmount(username, title);

            double requiredSavings = goalAmount - currentSavings;
            Console.WriteLine($"{goalAmount} - {currentSavings} = {requiredSavings}");

            double daysToGoal = requiredSavings / await otherFunc.dateTargetMinusCurrent(username, title);
            double daily_savings = daysToGoal != 0 ? requiredSavings / daysToGoal : 0;

            double daysFromStart = await otherFunc.dateCurrentMinusStart(username, title);
            double currentSavingsRate = daysFromStart != 0 ? currentSavings / daysFromStart : 0;
            Console.WriteLine($"dailySavings: {daily_savings} and currentSavingsRate: {currentSavingsRate}");
            Console.WriteLine($"daysToGOal: {daysToGoal} -    - daysFromStart: {daysFromStart}");
            if (currentSavings >= goalAmount)
            {
                rtbSuggestion.Text = "Goal has beena achieved";
            }
            else
            {
                if (daily_savings < currentSavingsRate)
                {
                    rtbSuggestion.Text = "Maintain your current savings or increase";
                }
                else
                {
                    rtbSuggestion.Text = "increase your savings or reduce your expenses";
                }
            }
        }
    }
}