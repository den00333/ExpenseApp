using Google.Cloud.Firestore;
using Google.Type;
using Guna.UI2.WinForms;
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
    public partial class Logs : Form
    {
        private DataTable WalletLogs;
        private string username = FirebaseData.Instance.Username;
        public Logs()
        {
            InitializeComponent();
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            displayLogs();
        }
        async void displayLogs()
        {
            WalletLogs = await getWalletLogs(username);

            foreach (DataRow row in WalletLogs.Rows)
            {
                int amount = (int)row["Amount"];
                string type = (string)row["Type"];
                string logDate = (string)row["Date"];
                string logTime = (string)row["Time"];

                Guna2GradientButton pnl = new Guna2GradientButton();
                pnl.Size = new Size(378, 146);
                pnl.BorderRadius = 15;
                pnl.FillColor = System.Drawing.Color.FromArgb(227, 180, 72);
                pnl.FillColor2 = System.Drawing.Color.FromArgb(83, 123, 47);
                pnl.BackColor = System.Drawing.Color.Transparent;

                Label lblAmount = new Label();
                lblAmount.Font = new Font("Poppins", 11.25f, FontStyle.Regular);
                lblAmount.BackColor = System.Drawing.Color.Transparent;
                lblAmount.Size = new Size(171, 26);
                lblAmount.Location = new Point(40, 44);
                lblAmount.ForeColor = System.Drawing.Color.White;
                lblAmount.Text = "Amount: " + amount.ToString();

                Label lblType = new Label();
                lblType.Font = new Font("Poppins", 11.25f, FontStyle.Regular);
                lblType.BackColor = System.Drawing.Color.Transparent;
                lblType.Size = new Size(171, 26);
                lblType.Location = new Point(40, 17);
                lblType.ForeColor = System.Drawing.Color.White;
                lblType.Text = "Type: " + type;

                Label lblDate = new Label();
                lblDate.Font = new Font("Poppins", 11.25f, FontStyle.Regular);
                lblDate.BackColor = System.Drawing.Color.Transparent;
                lblDate.Size = new Size(171, 26);
                lblDate.Location = new Point(40, 72);
                lblDate.ForeColor = System.Drawing.Color.White;
                lblDate.Text =  "Date: " + logDate;

                Label lblTime = new Label();
                lblTime.Font = new Font("Poppins", 11.25f, FontStyle.Regular);
                lblTime.BackColor = System.Drawing.Color.Transparent;
                lblTime.Size = new Size(171, 26);
                lblTime.Location = new Point(40, 99);
                lblTime.ForeColor = System.Drawing.Color.White;
                lblTime.Text = "Time: " + logTime;

                pnl.Controls.Add(lblType);
                pnl.Controls.Add(lblAmount);
                pnl.Controls.Add(lblDate);
                pnl.Controls.Add(lblTime);
                flpLogs.Controls.Add(pnl);
            }

        }
        private async Task<DataTable> getWalletLogs(string username)
        {
            var db = otherFunc.FirestoreConn();
            DocumentReference docref = db.Collection("Users").Document(username).Collection("Wallets").Document("LogWallet");
            DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

            DataTable walletLog = new DataTable();
            walletLog.Columns.Add("Amount", typeof(int));
            walletLog.Columns.Add("Type", typeof(string));
            walletLog.Columns.Add("Date", typeof(string));
            walletLog.Columns.Add("Time", typeof(string));

            if (docsnap.Exists)
            {
                string[] logs = docsnap.GetValue<string[]>("LogWallet");

                foreach (string log in logs)
                {
                    string[] logParts = log.Split('|');
                    if (logParts.Length == 4)
                    {
                        int amount;
                        if (int.TryParse(logParts[0], out amount))
                        {
                            string type = logParts[1];
                            string date = logParts[2];
                            string time = logParts[3];

                            walletLog.Rows.Add(amount, type, date, time);
                        }
                        else
                        {
                            // Handle parsing error for Amount if needed
                            Console.WriteLine($"Error parsing amount: {logParts[0]}");
                        }
                    }
                    else
                    {
                        // Handle incorrect log format if needed
                        Console.WriteLine($"Incorrect log format: {log}");
                    }
                }
            }

            return walletLog;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
