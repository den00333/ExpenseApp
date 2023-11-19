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
    public partial class dashboard : UserControl
    {
        public string username = FirebaseData.Instance.Username;
        public dashboard()
        {
            InitializeComponent();

        }
        private void dashboard_Load(object sender, EventArgs e)
        {
            displayExpensesChart();
        }
        private async void displayExpensesChart()
        {
            expensesChart.Series.Clear();
            Dictionary<DateTime, decimal> expensesByDate = await otherFunc.GetExpensesGroupedByDate(username);
            var sortedExpenses = expensesByDate.OrderBy(x => x.Key);
            Series splineSeries = new Series("Total Expenses"){
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3
            };
            Series areaSeries = new Series("Gradient Area"){
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 0

            };
            areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            foreach (var entry in sortedExpenses){
                DateTime date = entry.Key;
                decimal totalAmount = entry.Value;
                splineSeries.Points.AddXY(date.ToString("MM/dd"), totalAmount);
                areaSeries.Points.AddXY(date.ToString("MM/dd"), totalAmount);
            }
            areaSeries.Color = System.Drawing.Color.FromArgb(128, 135, 206, 250);
            expensesChart.Series.Add(splineSeries);
            expensesChart.Series.Add(areaSeries);
        }
    }
}
