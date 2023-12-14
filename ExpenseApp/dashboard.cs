using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenseApp
{
    public partial class dashboard : UserControl
    {
        public string username = FirebaseData.Instance.Username;
        private List<DataPoint> expensesDataPoints = new List<DataPoint>();
        private ToolTip tooltip = new ToolTip();
        private DataTable expensesDataTable;
        private DataTable expensesCatDataTable;
        private DataTable totalExpensesTable;
        private DataTable group;
        string groupCode = string.Empty;
        private DataTable groupSpendings;
        private DataTable groupCat;
        private DataTable totalGroupExpenses;
        public dashboard()
        {
            InitializeComponent();
        }
        private async void dashboard_Load(object sender, EventArgs e)
        {
            expensesDataTable = await otherFunc.GetExpensesGroupedByDate(username);
            expensesCatDataTable = await otherFunc.GetExpensesGroupedByCategories(username);
            totalExpensesTable = await otherFunc.GetTransactions(username);
            displayExpensesChart(expensesDataTable);
            displayDonut(expensesCatDataTable);
            displayExpensesTransaction(totalExpensesTable);
            group = await populateCmbGroupCode();
            populateGroupCombo(group);
        }
        private void displayExpensesChart(DataTable tbl)
        {
            expensesChart.Series.Clear();
            if (tbl == null) return;

            var sortedExpenses = tbl.AsEnumerable()
                .OrderBy(row => row.Field<DateTime>("Date"));

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
            foreach (var row in sortedExpenses)
            {
                DateTime date = row.Field<DateTime>("Date");
                double totalAmount = row.Field<double>("Amount");

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
        private void displayDonut(DataTable tbl)
        {
            Series series = expenseCategoryDonut.Series["Series1"];
            series.Points.Clear();
            DataTable expensesDataTable = tbl;

            var expensesByCategory = new Dictionary<string, double>();

            foreach (DataRow row in expensesDataTable.Rows)
            {
                if (row["Category"] != null && row["Amount"] != null &&
                    double.TryParse(row["Amount"].ToString(), out double amount))
                {
                    string category = row["Category"].ToString();

                    if (expensesByCategory.ContainsKey(category))
                    {
                        expensesByCategory[category] += amount;
                    }
                    else
                    {
                        expensesByCategory[category] = amount;
                    }
                }
            }
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
        private void displayExpensesTransaction(DataTable tbl)
        {
            int totalTransaction = getTotalTransactions(tbl);
            lblTransaction.Text = totalTransaction.ToString();
            float totalSpending = getTotalExpenses(tbl);
            string spendingBeautify = otherFunc.amountBeautify(totalSpending);
            lblSpendings.Text = spendingBeautify;
        }
        private int getTotalTransactions(DataTable expensesDataTable)
        {
            return expensesDataTable.Rows.Count;
        }
        private float getTotalExpenses(DataTable expensesDataTable)
        {
            float totalAmount = expensesDataTable.AsEnumerable()
            .Where(row => !row.IsNull("Amount"))
            .Sum(row => Convert.ToSingle(row["Amount"]));
            return totalAmount;
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
        private void displayExpensesChart(DataTable tbl, int days)
        {
            expensesChart.Series.Clear();
            Dictionary<DateTime, double> expensesByDate = customDayExpenses(tbl, days);
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
        private void displayDonut(DataTable tbl, int days)
        {
            Series series = expenseCategoryDonut.Series["Series1"];
            series.Points.Clear();
            Dictionary<string, double> expensesByCategory = customDayCategories(tbl, days);
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
            if (!flag)
            {
                displayExpensesChart(expensesDataTable);
                displayDonut(expensesCatDataTable);
                displayExpensesTransaction(totalExpensesTable);
            }
            else
            {
                displayExpensesChart(totalGroupExpenses);
                displayDonut(groupCat);
                displayExpensesTransaction(groupSpendings);
            }
        }
        private void displayCustomExpensesTransaction(DataTable tbl, int days)
        {
            int totalTransaction = customTotalTransactions(tbl, days);
            lblTransaction.Text = totalTransaction.ToString();
            float totalSpending = customTotalExpenses(tbl, days);
            string spendingBeautify = otherFunc.amountBeautify(totalSpending);
            lblSpendings.Text = spendingBeautify;
        }
        private void btnWeek_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                displayCustomExpensesTransaction(expensesDataTable, 7);
                displayDonut(expensesCatDataTable, 7);
                displayExpensesChart(expensesDataTable, 7);
            }
            else
            {
                displayExpensesChart(totalGroupExpenses, 7);
                displayDonut(groupCat, 7);
                displayCustomExpensesTransaction(groupSpendings, 7);
            }

        }
        private void btnMonth_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                displayCustomExpensesTransaction(expensesDataTable, 30);
                displayDonut(expensesCatDataTable, 30);
                displayExpensesChart(expensesDataTable, 30);
            }
            else
            {
                displayCustomExpensesTransaction(groupSpendings, 30);
                displayDonut(groupCat, 30);
                displayExpensesChart(totalGroupExpenses, 30);
            }
        }
        private void btnToday_Click(object sender, EventArgs e)
        {
            if (!flag)
            {
                displayCustomExpensesTransaction(expensesDataTable, 0);
                displayDonut(expensesCatDataTable, 0);
                displayExpensesChart(expensesDataTable, 0);
            }
            else
            {
                displayCustomExpensesTransaction(groupSpendings, 0);
                displayDonut(groupCat, 0);
                displayExpensesChart(totalGroupExpenses, 0);
            }

        }
        private static int customTotalTransactions(DataTable expensesDataTable, int customDays = 0)
        {
            int totalTransactions = 0;

            if (customDays == 0)
            {
                DateTime startDate = DateTime.UtcNow.Date;
                DateTime endDate = startDate.AddDays(1);
                var filteredRows = expensesDataTable.AsEnumerable()
                    .Where(row => row.Field<DateTime>("Date") >= startDate && row.Field<DateTime>("Date") < endDate);

                totalTransactions = filteredRows.Count();
            }
            else
            {
                DateTime startDate = DateTime.UtcNow.Date.AddDays(-customDays);
                var filteredRows = expensesDataTable.AsEnumerable()
                    .Where(row => row.Field<DateTime>("Date") >= startDate);

                totalTransactions = filteredRows.Count();
            }
            return totalTransactions;
        }
        public static float customTotalExpenses(DataTable expensesDataTable, int customDays = 0)
        {
            float totalAmount = 0;
            if (customDays == 0)
            {
                DateTime startDate = DateTime.UtcNow.Date;
                DateTime endDate = startDate.AddDays(1);
                var filteredRows = expensesDataTable.AsEnumerable()
                    .Where(row => row.Field<DateTime>("Date") >= startDate && row.Field<DateTime>("Date") < endDate);

                foreach (var row in filteredRows)
                {
                    if (row["Amount"] != null && float.TryParse(row["Amount"].ToString(), out float amount))
                    {
                        totalAmount += amount;
                    }
                }
            }
            else
            {
                DateTime startDate = DateTime.UtcNow.Date.AddDays(-customDays);

                var filteredRows = expensesDataTable.AsEnumerable()
                    .Where(row => row.Field<DateTime>("Date") >= startDate);

                foreach (var row in filteredRows)
                {
                    if (row["Amount"] != null && float.TryParse(row["Amount"].ToString(), out float amount))
                    {
                        totalAmount += amount;
                    }
                }
            }

            return totalAmount;
        }
        private static Dictionary<DateTime, double> customDayExpenses(DataTable expensesDataTable, int days)
        {
            DateTime startDate = DateTime.UtcNow.Date.AddDays(-days);
            var expensesByDate = new Dictionary<DateTime, double>();
            var filteredRows = expensesDataTable.AsEnumerable()
                .Where(row => row.Field<DateTime>("Date") >= startDate);

            foreach (var row in filteredRows)
            {
                DateTime date = row.Field<DateTime>("Date");
                double amount = row.Field<double>("Amount");

                if (expensesByDate.ContainsKey(date))
                {
                    expensesByDate[date] += amount;
                }
                else
                {
                    expensesByDate[date] = amount;
                }
            }
            return expensesByDate;
        }
        private static Dictionary<string, double> customDayCategories(DataTable expensesCatDataTable, int days)
        {
            DateTime startDate = DateTime.UtcNow.Date.AddDays(-days);
            var expenseByCategories = new Dictionary<string, double>();
            var filteredRows = expensesCatDataTable.AsEnumerable()
                .Where(row => DateTime.TryParse(row.Field<string>("Date"), out DateTime rowDate) && rowDate >= startDate);

            foreach (var row in filteredRows)
            {
                if (row["Category"] != null && row["Amount"] != null &&
                    double.TryParse(row["Amount"].ToString(), out double amount))
                {
                    string category = row["Category"].ToString();

                    if (expenseByCategories.ContainsKey(category))
                    {
                        expenseByCategories[category] += amount;
                    }
                    else
                    {
                        expenseByCategories[category] = amount;
                    }
                }
            }
            return expenseByCategories;
        }
        async Task<DataTable> populateCmbGroupCode()
        {
            otherFunc function = new otherFunc();
            var db = otherFunc.FirestoreConn();
            string[] groupCodes = await function.getGroups(username);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("GroupCode", typeof(string));
            dataTable.Columns.Add("GroupName", typeof(string));

            foreach (string groupcode in groupCodes)
            {
                DocumentReference docref = db.Collection("Groups").Document(groupcode);
                DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

                if (docsnap.Exists)
                {
                    string groupname = docsnap.GetValue<string>("GroupName");
                    dataTable.Rows.Add(groupcode, groupname);
                }
            }
            return dataTable;
        }
        void populateGroupCombo(DataTable table)
        {
            cmbGroup.Items.Clear();

            foreach (DataRow row in table.Rows)
            {
                string groupName = row["GroupName"].ToString();
                cmbGroup.Items.Add(groupName);
            }
        }

        private async void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGroupname = cmbGroup.SelectedItem.ToString();

            var dataTable = await populateCmbGroupCode();

            DataRow[] foundRows = dataTable.Select($"GroupName = '{selectedGroupname}'");

            if (foundRows.Length > 0)
            {
                groupCode = foundRows[0]["GroupCode"].ToString();
                MessageBox.Show(groupCode);
                groupSpendings = await otherFunc.getGroupExpenses(groupCode);
                groupCat = await otherFunc.GetGxpensesGroupedByCategories(groupCode);
                totalGroupExpenses = await otherFunc.GetGroupExpensesGroupedByDate(groupCode);

                displayExpensesChart(totalGroupExpenses);
                displayDonut(groupCat);
                displayExpensesTransaction(groupSpendings);
            }
            else
            {
                MessageBox.Show("Group not found", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool flag = false;
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                btnSwitch.Text = "User";
                cmbGroup.Enabled = false;
                displayExpensesChart(expensesDataTable);
                displayDonut(expensesCatDataTable);
                displayExpensesTransaction(totalExpensesTable);
            }
            else
            {
                flag = true;
                btnSwitch.Text = "Group";
                cmbGroup.Enabled = true;
            }
        }
    }
}