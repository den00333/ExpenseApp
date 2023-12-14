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
using System.Xml.Linq;

namespace ExpenseApp
{
    public partial class ListOfOfflineDatacs : Form
    {
        bool flagOFForON = true;
        wallet w;
        public ListOfOfflineDatacs(bool f, wallet wal)
        {
            InitializeComponent();
            this.flagOFForON = f;
            this.w = wal;
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

                
                w.flpExpenses.Controls.Clear();
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
                //LOADING TIME
                uploadingOfflineDataToFirestore(title);
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
            await uploadLogs(pathOfChosenFile, username);
            await uploadExpenses(pathOfChosenFile, username);
            File.Delete(pathOfChosenFile);

            flpTitles.Controls.Clear();

            String path = "listOfOffline.txt";
            loadTitles(path);
            MessageBox.Show("uploaded successfully!");
            float[] threelbl = await otherFunc.get3label();

            String[] lines = File.ReadAllLines(path);
            lines[0] = lines[0].Replace(pathOfChosenFile + "|", "");
            File.WriteAllLines(path, lines);

            w.flpExpenses.Controls.Clear();
            w.displayData();
            w.lblBalance.Text = otherFunc.amountBeautify(threelbl[0]);
            w.lblExpenses.Text = otherFunc.amountBeautify(threelbl[1]);
            if (threelbl[2] >= 0)
            {
                w.lblShort.Hide();

            }
            else
            {
                w.lblShort.Text = otherFunc.amountBeautify(threelbl[2]);
                w.lblShort.ForeColor = Color.Red;
            }


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
            double current_short = dSnapExpense.GetValue<double>("short");
            double current_expense = dSnapExpense.GetValue<double>("Amount");
            

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
                        if (dSnapBalance.Exists)
                        {
                            await drefBalance.UpdateAsync(data);
                        }
                        else
                        {
                            await drefBalance.SetAsync(data);
                        }
                        data.Clear();
                        break;
                    case 1:
                        newTotal = current_expense + double.Parse(off_data[1]);
                        data.Add("Amount", newTotal);
                        if (dSnapExpense.Exists)
                        {
                            await drefExpense.UpdateAsync(data);
                        }
                        else
                        {
                            await drefExpense.SetAsync(data);
                        }
                        data.Clear();
                        break;
                    case 2:
                        newTotal = current_short + double.Parse(off_data[1]);
                        data.Add("short", newTotal);
                        if (dSnapExpense.Exists)
                        {
                            await drefExpense.UpdateAsync(data);
                        }
                        else
                        {
                            await drefExpense.SetAsync(data);
                        }
                        data.Clear();
                        break;
                }
            }
        }
        
        private async Task uploadLogs(String path, String username)
        {
            String col = "Wallets";
            String[] lines = File.ReadAllLines(path);
            ArrayList ldata = new ArrayList();
            String[] records = lines[3].Split('!');
            if (records[1] == "")
            {
                return;
            }
            String[] each_record = records[1].Split('~');
            Console.WriteLine(each_record.Length+" the line" + records[1]);
            for (int i = 0; i < each_record.Length - 1; i++)
            {
                ldata.Add(each_record[i]);
                Console.WriteLine(i + " each record " + each_record[i]);
            }

            Dictionary<String, object> data = new Dictionary<String, object>
            {
                {"LogWallet", ldata}
            };

            DocumentReference docref = otherFunc.editInsideUser(username).Collection(col).Document("LogWallet");
            DocumentSnapshot dsnap = await docref.GetSnapshotAsync();
            Console.WriteLine("is it existing?" + dsnap.Exists);
            if (dsnap.Exists)
            {
                /*for (int i = 0; i < each_record.Length - 1; i++)
                {
                    await docref.UpdateAsync("LogWallet", FieldValue.ArrayUnion(each_record[i]));
                }*/
                await docref.SetAsync(data, SetOptions.MergeAll);
            }
            else
            {
                await docref.SetAsync(data);
            }
            

        }

        private async Task uploadExpenses(String path, String username)
        {
            otherFunc o = new otherFunc();
            
            String[] lines = File.ReadAllLines(path);
            String[] splitted = lines[4].Split(':');
            if (splitted[1] == "")
            {
                return;
            }
            String[] E = splitted[1].Split('~');
            int e_len = E.Length - 1;
            for(int i = 0; i <= e_len-1; i++)
            {
                String[] inside_E = E[i].Split('|');
                /*String amount = inside_E[0];
                String category = inside_E[1];
                String Date = inside_E[2];
                String loc = inside_E[3];
                String desc = inside_E[4];*/
                Dictionary<String, object> data = new Dictionary<String, object>
                {
                    {"Amount", double.Parse(inside_E[0]) },
                    {"Category", inside_E[1]},
                    {"Date", inside_E[2]},
                    {"Location", inside_E[3]},
                    {"Name", inside_E[4]},
                    {"timestamp", FieldValue.ServerTimestamp}
                };

                DocumentReference docRef = await o.SavingNewExpenses(username);
                await docRef.SetAsync(data);

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
            if (!flagOFForON)
            {
                this.Hide();
            }
            else
            {
                this.Hide();
                Login l = new Login();
                l.Show();
            }
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