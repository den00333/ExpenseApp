using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
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
            displayDonut();
            displayExpensesTransaction();
        }

        private List<DataPoint> expensesDataPoints = new List<DataPoint>();
        private ToolTip tooltip = new ToolTip();
        private async void displayExpensesChart()
        {
            expensesChart.Series.Clear();
            Dictionary<DateTime, double> expensesByDate = await otherFunc.GetExpensesGroupedByDate(username);
            var sortedExpenses = expensesByDate.OrderBy(x => x.Key);
            Series splineSeries = new Series("Total Expenses") {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = System.Drawing.ColorTranslator.FromHtml("#FF1616"),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 10
            };
            Series areaSeries = new Series("Gradient Area"){
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 0

            };
            areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            foreach (var entry in sortedExpenses){
                DateTime date = entry.Key;
                double totalAmount = entry.Value;

                DataPoint dataPoint = new DataPoint();
                dataPoint.AxisLabel = date.ToString("MM/dd");
                dataPoint.SetValueXY(date, totalAmount);

                splineSeries.Points.AddXY(date.ToString("MM/dd"), totalAmount);
                areaSeries.Points.AddXY(date.ToString("MM/dd"), totalAmount);
                expensesDataPoints.Add(dataPoint);
            }
            areaSeries.Color = System.Drawing.Color.FromArgb(128, 255, 0, 0);
            expensesChart.Series.Add(splineSeries);
            expensesChart.Series.Add(areaSeries);

        }
        private async void displayDonut()
        {
            Series series = expenseCategoryDonut.Series["Series1"];
            series.Points.Clear();
            Dictionary<string, double> expensesByCategory = await otherFunc.getExpensesGroupedByCategories(username);
            double totalExpenses = expensesByCategory.Values.Sum();
            foreach (var entry in expensesByCategory)
            {
                double percentage = (entry.Value / totalExpenses) * 100;
                DataPoint datapoint = new DataPoint();
                datapoint.AxisLabel = "";
                datapoint.Label = $"{percentage:F2}%";
                datapoint.YValues = new double[] { entry.Value };
                datapoint.LegendText = entry.Key;
                datapoint.LabelForeColor= System.Drawing.Color.White;
                datapoint.Font = new Font("Poppins Regular", 8, FontStyle.Bold);
                series.Points.Add(datapoint);
            }
        }
        private async void displayExpensesTransaction()
        {
            int totalTransaction = await otherFunc.getTotalExpensesTransaction(username);
            lblTransaction.Text = totalTransaction.ToString();
            float totalSpending = await otherFunc.getTotalExpenses(username);
            string spendingBeautify = otherFunc.amountBeautify(totalSpending);
            lblSpendings.Text = spendingBeautify;
        }
        private void expensesChart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var results = expensesChart.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);

            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint dataPoint = expensesDataPoints[result.PointIndex];
                    double xValue = dataPoint.XValue;
                    double yValue = dataPoint.YValues[0];

                    tooltip.RemoveAll();
                    tooltip.Show($"Total Expenses: {yValue:C}", expensesChart, pos.X, pos.Y - 15);
                }
            }
        }

        private void expensesChart_MouseLeave(object sender, EventArgs e)
        {
            tooltip.RemoveAll();
        }

        private void expenseCategoryDonut_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var result = expenseCategoryDonut.HitTest(pos.X, pos.Y, false);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DataPoint dataPoint = expenseCategoryDonut.Series[0].Points[result.PointIndex];
                string category = dataPoint.LegendText;
                double yValue = dataPoint.YValues[0];

                tooltip.RemoveAll();
                tooltip.Show($"Category: {category}\nTotal Amount: {yValue:C}", expenseCategoryDonut, pos.X, pos.Y - 15);
            }
        }
    }
}
