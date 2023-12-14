using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class ListOfOfflineDatacs : Form
    {
        bool flagOFForON = true;
        public ListOfOfflineDatacs(bool f)
        {
            InitializeComponent();
            this.flagOFForON = f;
            if (!flagOFForON)
            {
                txtTitle.Enabled = false;
                btnSaveTitle.Enabled = false;
            }

        }

        private void btnSaveTitle_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length <= 10 && !string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                String path = "listOfOffline.txt";
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                String t = $"offlineData_{txtTitle.Text}_{timestamp}.txt";
                offlineFunc.createTXT(t);
                addTitle("listOfOffline.txt", t + "|");
                txtTitle.Clear();

                flpTitles.Controls.Clear();
                loadTitles(path);
            }
            else
            {
                DialogResult res = MessageBox.Show("Please limit to 10 characters maximum!", "Cannot use your title.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (res == DialogResult.OK)
                {
                    txtTitle.Clear();
                }
            }
        }

        private void ListOfOfflineDatacs_Load(object sender, EventArgs e)
        {
            String path = "listOfOffline.txt";
            if (!File.Exists(path))
            {
                String text = "Title:";
                File.WriteAllText(path, text);
            }
            Console.WriteLine("asd");
            loadTitles(path);
            Console.WriteLine("asasd");

        }

        private void loadTitles(String path)
        {
            if (File.Exists(path))
            {
                String[] lines = File.ReadAllLines(path);
                String[] splitted = lines[0].Split(':');
                String[] titles = splitted[1].Split('|');

                int end = titles.Length - 1;
                for (int i = end - 1; i >= 0; i--)
                {
                    Guna2Panel pnl = new Guna2Panel();
                    pnl.Size = new Size(390, 50);
                    pnl.FillColor = Color.FromArgb(83, 123, 47);
                    pnl.BorderRadius = 10;

                    System.Windows.Forms.Label lblCat = new System.Windows.Forms.Label();
                    lblCat.Font = new Font("Poppins", 8f, FontStyle.Bold | FontStyle.Regular);
                    lblCat.BackColor = Color.Transparent;
                    lblCat.Size = new Size(240, 39);
                    lblCat.Location = new Point(20, 17);
                    lblCat.ForeColor = Color.White;
                    lblCat.Text = titles[i];

                    Guna.UI2.WinForms.Guna2Button btnDelete = new Guna.UI2.WinForms.Guna2Button();
                    btnDelete.Font = new Font("Poppins", 8f, FontStyle.Bold | FontStyle.Regular);
                    btnDelete.Size = new Size(58, 40);
                    btnDelete.Location = new Point(320, 2);
                    btnDelete.FillColor = Color.FromArgb(210, 4, 45);
                    btnDelete.BorderRadius = 19;
                    btnDelete.BackColor = Color.Transparent;
                    btnDelete.Text = "Delete";
                    btnDelete.Visible = flagShow;

                    pnl.DoubleClick += (sender, e) => pnlT_Click(sender, e, lblCat.Text.ToString());
                    lblCat.DoubleClick += (sender, e) => pnlT_Click(sender, e, lblCat.Text.ToString());
                    btnDelete.Click += (sender, e) => deleting_Click(sender, e, lblCat.Text.ToString());

                    pnl.Controls.Add(lblCat);
                    pnl.Controls.Add(btnDelete);
                    flpTitles.Controls.Add(pnl);

                    
                }

            }
        }

        private void deleting_Click(object sender, EventArgs e, String name)
        {
            String path = "listOfOffline.txt";
            String[] lines = File.ReadAllLines(path);
            lines[0] = lines[0].Replace(name+"|", "");
            File.WriteAllLines(path, lines);
            flpTitles.Controls.Clear();
            loadTitles(path);
            File.Delete(name);
        }

        
        private void pnlT_Click(object sender, EventArgs e, String title)
        {
            if (flagOFForON)
            {
                offWallet o = new offWallet(title);
                o.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("asd");
                //uploadingOfflineDataToFirestore(title);
            }
        }
        //WES -> Wallets|Balance|Amount
        //WEA -> Wallets|Expense|Amount
        //WES -> Wallets|Expense|Short
        //WLL -> Wallets|LogWallet|LogWallet
        //E -> Expenses
        private async void uploadingOfflineDataToFirestore(String pathOfChosenFile)
        {
            String username = FirebaseData.Instance.Username;
            await uploadWES(pathOfChosenFile, username);

        }

        private async Task uploadWES(String path, String username)
        {
            String col = "Wallets";
            String[] lines = File.ReadAllLines(path);
            
            DocumentReference drefBalance = otherFunc.editInsideUser(username).Collection(col).Document("Balance");
            DocumentReference drefExpense = otherFunc.editInsideUser(username).Collection(col).Document("Expense");

            DocumentSnapshot dSnapBalance = await drefBalance.GetSnapshotAsync();
            DocumentSnapshot dSnapExpense = await drefExpense.GetSnapshotAsync();

            double current_balance = dSnapBalance.GetValue<double>("Amount");
            double current_expense = dSnapExpense.GetValue<double>("Amount");
            double current_short = dSnapExpense.GetValue<double>("short");

            Dictionary<String, object> data = new Dictionary<String, object>();
            double newTotal;
            for(int i = 0; i <= 2; i++)
            {
                String[] off_data = lines[i].Split(':');
                switch (i)
                {
                    case 0:
                        newTotal = current_balance + double.Parse(off_data[1]);
                        data.Add("Amount", newTotal);
                        await drefBalance.UpdateAsync(data);
                        break;
                    case 1:
                        newTotal = current_expense + double.Parse(off_data[1]);
                        data.Add("Amount", newTotal);
                        await drefExpense.UpdateAsync(data);
                        break;
                    case 2:
                        newTotal = current_short + double.Parse(off_data[1]);
                        data.Add("short", newTotal);
                        await drefExpense.UpdateAsync(data);
                        break;
                }
            }
        }
        
        private async void uploadLogs(String path, String username)
        {
            String col = "Wallets";
            String[] lines = File.ReadAllLines(path);
            ArrayList ldata = new ArrayList();
            String[] records = lines[3].Split(':');
            String[] each_record = records[1].Split('~');
            for(int i = 0; i < each_record.Length - 1; i++)
            {
                ldata.Add(each_record[i]);
            }

            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"LogWallet", ldata}
            };

            DocumentReference docref = otherFunc.editInsideUser(username).Collection(col).Document("LogWallet");
            DocumentSnapshot dsnap = await docref.GetSnapshotAsync();

            if (dsnap.Exists)
            {
                for (int i = 0; i < each_record.Length - 1; i++)
                {
                    await docref.UpdateAsync("LogWallet", FieldValue.ArrayUnion(each_record[i]));
                }
            }
            else
            {
                await docref.SetAsync(data);
            }
            

        }

        private static void addTitle(String path, String title)
        {
            String[] lines = File.ReadAllLines(path);
            lines[0] += title;
            File.WriteAllLines(path, lines);
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // If not an alphanumeric character or a control key, suppress the key press
                e.Handled = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login l = new Login();
            l.Show();
        }
        bool flagShow = false;
        private void btnAccessDelete_Click(object sender, EventArgs e)
        {
            String path = "listOfOffline.txt";
            if (flagShow)
            {
                flagShow = false;
            }
            else
            {
                flagShow = true;
            }


            Thread thread = new Thread(() => reload(path));
            thread.Start();
        }

        private void reload(String path)
        {
            if (flpTitles.InvokeRequired)
            {
                flpTitles.Invoke(new Action<String>(reload), path);
            }
            else
            {
                flpTitles.Controls.Clear();

                loadTitles(path);
            }
        }
    }
}