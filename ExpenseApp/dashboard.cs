using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Globalization;
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
            Series splineSeries = new Series("Total Expenses")
            {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = System.Drawing.ColorTranslator.FromHtml("#FF1616"),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 10
            };
            Series areaSeries = new Series("Gradient Area")
            {
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 0

            };
            areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            foreach (var entry in sortedExpenses)
            {
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
                datapoint.LabelForeColor = System.Drawing.Color.White;
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
        private void expensesChart_MouseEnter(object sender, EventArgs e)
        {
            expensesChart.MouseMove += expensesChart_MouseMove;
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
        private async void displayExpensesChart(int days)
        {
            expensesChart.Series.Clear();
            Dictionary<DateTime, double> expensesByDate = await customDayExpenses(username, days);
            var sortedExpenses = expensesByDate.OrderBy(x => x.Key);
            Series splineSeries = new Series("Total Expenses")
            {
                ChartType = SeriesChartType.Spline,
                BorderWidth = 3,
                Color = System.Drawing.ColorTranslator.FromHtml("#FF1616"),
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 10
            };
            Series areaSeries = new Series("Gradient Area")
            {
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 0

            };
            areaSeries.BackGradientStyle = GradientStyle.TopBottom;
            foreach (var entry in sortedExpenses)
            {
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
        private async void displayDonut(int days)
        {
            Series series = expenseCategoryDonut.Series["Series1"];
            series.Points.Clear();
            Dictionary<string, double> expensesByCategory = await customDayCategories(username,days);
            double totalExpenses = expensesByCategory.Values.Sum();
            foreach (var entry in expensesByCategory)
            {
                double percentage = (entry.Value / totalExpenses) * 100;
                DataPoint datapoint = new DataPoint();
                datapoint.AxisLabel = "";
                datapoint.Label = $"{percentage:F2}%";
                datapoint.YValues = new double[] { entry.Value };
                datapoint.LegendText = entry.Key;
                datapoint.LabelForeColor = System.Drawing.Color.White;
                datapoint.Font = new Font("Poppins Regular", 8, FontStyle.Bold);
                series.Points.Add(datapoint);
            }
        }
        private void btnAllExpenses_Click(object sender, EventArgs e)
        {
            displayExpensesChart();
            displayDonut();
            displayExpensesTransaction();
        }
        private async void displayCustomExpensesTransaction(int days)
        {
            int totalTransaction = await customTotalTransactions(username, days);
            lblTransaction.Text = totalTransaction.ToString();
            float totalSpending = await customTotalExpenses(username, days);
            string spendingBeautify = otherFunc.amountBeautify(totalSpending);
            lblSpendings.Text = spendingBeautify;
        }
        private void btnWeek_Click(object sender, EventArgs e)
        {
            displayCustomExpensesTransaction(7);
            displayDonut(7);
            displayExpensesChart(7);
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            displayCustomExpensesTransaction(30);
            displayDonut(30);
            displayExpensesChart(30);
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            displayCustomExpensesTransaction(1);
            displayDonut(1);
            displayExpensesChart(1);
        }

        private void btnYear_Click(object sender, EventArgs e)
        {
            displayCustomExpensesTransaction(365);
            displayDonut(365);
            displayExpensesChart(365);
        }

        private static async Task<int> customTotalTransactions(string username, int customDays = 0)
        {
            int totalTransaction = 0;
            CollectionReference colRef = otherFunc.editInsideUser(username).Collection("Expenses");

            if (customDays == 0)
            {
                DateTime startDate = DateTime.UtcNow.Date;
                DateTime endDate = startDate.AddDays(1);

                QuerySnapshot transactionSnap = await colRef
                    .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                    .WhereLessThan("Date", endDate.ToString("yyyy-MM-dd"))
                    .GetSnapshotAsync();

                totalTransaction = transactionSnap.Documents.Count;
            }
            else
            {
                DateTime startDate = DateTime.UtcNow.Date.AddDays(-customDays);

                QuerySnapshot transactionSnap = await colRef
                    .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                    .GetSnapshotAsync();

                totalTransaction = transactionSnap.Documents.Count;
            }

            return totalTransaction;
        }
        public static async Task<float> customTotalExpenses(string username, int customDays = 0)
        {
            float totalAmount = 0;
            CollectionReference colRef = otherFunc.editInsideUser(username).Collection("Expenses");

            if (customDays == 0){
                DateTime startDate = DateTime.UtcNow.Date;
                DateTime endDate = startDate.AddDays(1);

                QuerySnapshot documentSnapshots = await colRef
                    .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                    .WhereLessThan("Date", endDate.ToString("yyyy-MM-dd"))
                    .GetSnapshotAsync();

                foreach (DocumentSnapshot docSnap in documentSnapshots.Documents){
                    Dictionary<string, object> expenses = docSnap.ToDictionary();
                    if (expenses.TryGetValue("Amount", out var amountObj) && float.TryParse(amountObj.ToString(), out float amount))
                    {
                        totalAmount += amount;
                    }
                }
            }
            else
            {
                DateTime startDate = DateTime.UtcNow.Date.AddDays(-customDays);

                QuerySnapshot documentSnapshots = await colRef
                    .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                    .GetSnapshotAsync();

                foreach (DocumentSnapshot docSnap in documentSnapshots.Documents)
                {
                    Dictionary<string, object> expenses = docSnap.ToDictionary();
                    if (expenses.TryGetValue("Amount", out var amountObj) && float.TryParse(amountObj.ToString(), out float amount))
                    {
                        totalAmount += amount;
                    }
                }
            }

            return totalAmount;
        }
        private static async Task<Dictionary<DateTime, double>> customDayExpenses(string username, int days)
        {
            var db = otherFunc.FirestoreConn();
            CollectionReference expensesCollection = otherFunc.editInsideUser(username).Collection("Expenses");
            DateTime startDate = DateTime.UtcNow.Date.AddDays(-days);
            QuerySnapshot expensesSnapshot = await expensesCollection
                .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                .GetSnapshotAsync();
            var expensesByDate = new Dictionary<DateTime, double>();
            foreach (DocumentSnapshot expenseDoc in expensesSnapshot.Documents){
                Dictionary<string, object> expenseData = expenseDoc.ToDictionary();
                if (expenseData.TryGetValue("Date", out var dateObj) &&
                    DateTime.TryParseExact(dateObj.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) &&
                    expenseData.TryGetValue("Amount", out var amountObj) &&
                    double.TryParse(amountObj.ToString(), out double amount)){
                    if (expensesByDate.ContainsKey(date)){
                        expensesByDate[date] += amount;
                    }
                    else{
                        expensesByDate[date] = amount;
                    }
                }
            }
            return expensesByDate;
        }
        private static async Task<Dictionary<string, double>> customDayCategories(string username, int days)
        {
            CollectionReference colRef = otherFunc.editInsideUser(username).Collection("Expenses");
            DateTime startDate = DateTime.UtcNow.Date.AddDays(-days);
            QuerySnapshot expensesSnapshot = await colRef
                .WhereGreaterThanOrEqualTo("Date", startDate.ToString("yyyy-MM-dd"))
                .GetSnapshotAsync();
            var expenseByCategories = new Dictionary<string, double>();
            foreach (DocumentSnapshot docSnap in expensesSnapshot.Documents){
                Dictionary<string, object> expenseData = docSnap.ToDictionary();
                if (expenseData.TryGetValue("Category", out var categoryObj) &&
                    expenseData.TryGetValue("Amount", out var amountObj) &&
                    double.TryParse(amountObj.ToString(), out double amount)){
                    string category = categoryObj.ToString();
                    if (expenseByCategories.ContainsKey(category)){
                        expenseByCategories[category] += amount;
                    }
                    else{
                        expenseByCategories[category] = amount;
                    }
                }
            }
            return expenseByCategories;
        }
    }
}
