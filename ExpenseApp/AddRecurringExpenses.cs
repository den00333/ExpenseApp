using Google.Cloud.Firestore;
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
using System.Windows.Forms.VisualStyles;

namespace ExpenseApp
{
    public partial class AddRecurringExpenses : Form
    {
        public AddRecurringExpenses()
        {
            InitializeComponent();
        }

        String period = "";
        String day = "0";
        DateTime dt = DateTime.Today;
        private void cmbPeriodical_SelectedIndexChanged(object sender, EventArgs e)
        {
            period = cmbPeriodical.Text;
            
            switch (period)
            {
                case "Daily":
                    lblDate.Hide();
                    lblDay.Hide();
                    txtSetDate.Enabled = false;
                    txtSetDate.PlaceholderText = "Disabled";
                    dtpDate.Enabled = false;

                    break;
                case "Weekly":
                    lblDay.Show();
                    lblDate.Hide();
                    txtSetDate.Enabled = true;
                    dtpDate.Enabled = false;
                    txtSetDate.PlaceholderText = "Choose from 1 to 7";
                    //day = txtSetDate.Text;
                    break;
                case "Monthly":
                    lblDay.Show();
                    lblDate.Hide();
                    txtSetDate.Enabled = true;
                    dtpDate.Enabled = false;
                    txtSetDate.PlaceholderText = "Choose from 1 to 30";
                    //day = txtSetDate.Text;
                    break;
                case "Quarterly":
                    lblDay.Hide();
                    lblDate.Show();
                    txtSetDate.Enabled = false;
                    txtSetDate.PlaceholderText = "Disabled";
                    dtpDate.Enabled = true;
                    break;
                case "Annually":
                    lblDay.Hide();
                    lblDate.Show();
                    txtSetDate.Enabled = false;
                    txtSetDate.PlaceholderText = "Disabled";
                    dtpDate.Enabled = true;
                    break;

            }

        }

        private String save(String period, String day, DateTime dt)
        {
            String username = FirebaseData.Instance.Username;
            CollectionReference colRef = otherFunc.editInsideUser(username).Collection("RecurringExpenses");
            String d = dt.ToString("yyyy-MM-dd");
            int nday = int.Parse(day);
            Dictionary<String, object> data = new Dictionary<string, object>();
            bool flag = true;
            data.Add("Title", txtTitle.Text.ToString().Trim());
            data.Add("timestamp", FieldValue.ServerTimestamp);
            switch (period)
            {
                case "Daily":
                    data.Add("Date", $"day|{period}");
                    break;
                case "Weekly":
                    if (day == "0" || nday >= 8)
                    {
                        flag = false;
                    }
                    else
                    {
                        data.Add("Date", $"{day}|{period}");
                    }
                    break;

                case "Monthly":
                    if (day == "0" || nday >= 32)
                    {
                        flag = false;
                    }
                    else
                    {
                        data.Add("Date", $"{day}|{period}");
                    }
                    break;
                case "Quarterly":
                    
                    data.Add("Date", $"{d}|{period}");
                    break;
                case "Annually":

                    data.Add("Date", $"{d}|{period}");
                    break;
                default:
                    flag = false;
                    break;
            }
            
            if (flag)
            {
                colRef.AddAsync(data);
                return "Successfully Added!";
            }
            else
            {
                return "Wrong input! Please enter a valid input!";
            }

        }
        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtTitle.Text) || cmbPeriodical.SelectedIndex == -1)
            {
                MessageBox.Show("Missing values!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                day = (string.IsNullOrWhiteSpace(txtSetDate.Text)) ? "0" : txtSetDate.Text.ToString();
                dt = dtpDate.Value.Date;
                Console.WriteLine($"date: {dt}");
                String r = save(period, day, dt);
                DialogResult res = MessageBox.Show(r, "Response.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (res == DialogResult.OK)
                {
                    cmbPeriodical.SelectedIndex = -1;
                    txtSetDate.Clear();
                    dtpDate.Value = DateTime.Today;
                }
            }
        }

        private void txtSetDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            string pattern = @"[\d\b]";
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), pattern))
            {
                e.Handled = true;
            }
        }

        private void AddRecurringExpenses_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
        }
    }
}
