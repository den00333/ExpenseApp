using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class ListOfOfflineDatacs : Form
    {
        public ListOfOfflineDatacs()
        {
            InitializeComponent();
            
        }

        private void btnSaveTitle_Click(object sender, EventArgs e)
        {
            if(txtTitle.Text.Length <= 10)
            {
                String timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                String t = $"offlineData_{txtTitle.Text}_{timestamp}.txt";
                offlineFunc.createTXT(t);
                addTitle("listOfOffline.txt",t+"|");
                txtTitle.Clear();

            }
            else
            {
                DialogResult res = MessageBox.Show("Please limit to 10 characters maximum!", "Cannot use your title.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if(res == DialogResult.OK)
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
                    pnl.Size = new Size(315, 50);
                    pnl.FillColor = Color.FromArgb(83, 123, 47);
                    pnl.BorderRadius = 10;

                    System.Windows.Forms.Label lblCat = new System.Windows.Forms.Label();
                    lblCat.Font = new Font("Poppins", 8f, FontStyle.Bold | FontStyle.Regular);
                    lblCat.BackColor = Color.Transparent;
                    lblCat.Size = new Size(300, 39);
                    lblCat.Location = new Point(20, 17);
                    lblCat.ForeColor = Color.White;
                    lblCat.Text = titles[i];

                    pnl.DoubleClick += (sender, e) => pnlT_Click(sender, e, lblCat.Text.ToString());
                    lblCat.DoubleClick += (sender, e) => pnlT_Click(sender, e, lblCat.Text.ToString());

                    pnl.Controls.Add(lblCat);
                    flpTitle.Controls.Add(pnl);
                }
                
            }
        }


        private void pnlT_Click(object sender, EventArgs e, String title)
        {
            offWallet o = new offWallet(title);
            o.Show();
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
    }
}
