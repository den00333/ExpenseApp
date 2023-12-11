using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseApp
{
    public partial class GoalDetails : Form
    {
        wallet w;
        group group;
        public GoalDetails(wallet wal, group g)
        {
            InitializeComponent();
            this.w = wal;
            this.group = g;
            
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
        public async void displaySuggestions(String title, bool flag, String groupCode)
        {
            double currentSavings, goalAmount, requiredSavings, daysToGoal, daily_savings, daysFromStart, currentSavingsRate;

            if (flag)
            {
                String username = FirebaseData.Instance.Username;
                //double goalAmount = await otherFunc.getTotalGoalAmount(username);
                currentSavings = await otherFunc.getCurrentSavings(username, title);
                //rtbSuggestion.Text = Math.Round(currentSavings, 2).ToString();
                goalAmount = await otherFunc.getGoalAmount(username, title);

                requiredSavings = goalAmount - currentSavings;
                Console.WriteLine($"{goalAmount} - {currentSavings} = {requiredSavings}");

                daysToGoal = requiredSavings / await otherFunc.dateTargetMinusCurrent(username, title);
                daily_savings = daysToGoal != 0 ? requiredSavings / daysToGoal : 0;

                daysFromStart = await otherFunc.dateCurrentMinusStart(username, title);
                currentSavingsRate = daysFromStart != 0 ? currentSavings / daysFromStart : 0;
            }
            else
            {
                currentSavings = await otherFunc.getCurrentSavingsGroup(groupCode, title);
                //rtbSuggestion.Text = Math.Round(currentSavings, 2).ToString();
                goalAmount = await otherFunc.getGoalAmountGroup(groupCode, title);

                requiredSavings = goalAmount - currentSavings;
                Console.WriteLine($"{goalAmount} - {currentSavings} = {requiredSavings}");

                daysToGoal = requiredSavings / await otherFunc.dateTargetMinusCurrentGroup(groupCode, title);
                daily_savings = daysToGoal != 0 ? requiredSavings / daysToGoal : 0;

                daysFromStart = await otherFunc.dateCurrentMinusStartGroup(groupCode, title);
                currentSavingsRate = daysFromStart != 0 ? currentSavings / daysFromStart : 0;
            }
            //Console.WriteLine($"dailySavings: {daily_savings} and currentSavingsRate: {currentSavingsRate}");
            //Console.WriteLine($"daysToGOal: {daysToGoal} -    - daysFromStart: {daysFromStart}");
            if (currentSavings >= goalAmount)
            {
                rtbSuggestion.Text = "Goal has been achieved";
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

        public async Task<int> checkSuggestion(String title, bool flag, String groupCode)
        {
            int suggestionCode = 0;
            double currentSavings, goalAmount, requiredSavings, daysToGoal, daily_savings, daysFromStart, currentSavingsRate;
            if (flag)
            {
                String username = FirebaseData.Instance.Username;
                currentSavings = await otherFunc.getCurrentSavings(username, title);
                goalAmount = await otherFunc.getGoalAmount(username, title);

                requiredSavings = goalAmount - currentSavings;
                Console.WriteLine($"{goalAmount} - {currentSavings} = {requiredSavings}");

                daysToGoal = requiredSavings / await otherFunc.dateTargetMinusCurrent(username, title);
                daily_savings = daysToGoal != 0 ? requiredSavings / daysToGoal : 0;

                daysFromStart = await otherFunc.dateCurrentMinusStart(username, title);
                currentSavingsRate = daysFromStart != 0 ? currentSavings / daysFromStart : 0;
                //Console.WriteLine($"40404dailySavings: {daily_savings} and currentSavingsRate: {currentSavingsRate}");
                //Console.WriteLine($"40404daysToGOal: {daysToGoal} -    - daysFromStart: {daysFromStart}");
                Console.WriteLine($"CurrentSavings: {currentSavings} -  -   goalAmount: {goalAmount}");
                if (currentSavings >= goalAmount)
                {
                    suggestionCode = 1;
                }
                Console.WriteLine($"code: {suggestionCode}");
            }
            else
            {
                currentSavings = await otherFunc.getCurrentSavingsGroup(groupCode, title);
                goalAmount = await otherFunc.getGoalAmountGroup(groupCode, title);

                requiredSavings = goalAmount - currentSavings;
                Console.WriteLine($"{goalAmount} - {currentSavings} = {requiredSavings}");

                daysToGoal = requiredSavings / await otherFunc.dateTargetMinusCurrentGroup(groupCode, title);
                daily_savings = daysToGoal != 0 ? requiredSavings / daysToGoal : 0;

                daysFromStart = await otherFunc.dateCurrentMinusStartGroup(groupCode, title);
                currentSavingsRate = daysFromStart != 0 ? currentSavings / daysFromStart : 0;
                //Console.WriteLine($"40404dailySavings: {daily_savings} and currentSavingsRate: {currentSavingsRate}");
                //Console.WriteLine($"40404daysToGOal: {daysToGoal} -    - daysFromStart: {daysFromStart}");
                Console.WriteLine($"CurrentSavings: {currentSavings} -  -   goalAmount: {goalAmount}");
                if (currentSavings >= goalAmount)
                {
                    suggestionCode = 1;
                }
                Console.WriteLine($"code: {suggestionCode}");
            }
            return suggestionCode;
        }

    }
}