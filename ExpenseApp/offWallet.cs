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
    public partial class offWallet : Form
    {
        String path = "";
        public offWallet(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void offWallet_Load(object sender, EventArgs e)
        {
            offWalletUC wal = new offWalletUC(path);
            addUserControl(wal);
        }

        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            //homePanel.Controls.Clear();
            pnlWallet.Controls.Add(userControl);
            userControl.BringToFront();
        }

        
    }
}
