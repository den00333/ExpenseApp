using Google.Cloud.Firestore;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseApp
{
    public partial class group : UserControl
    {
        string username = FirebaseData.Instance.Username;
        public group()
        {
            InitializeComponent();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void btnGroup_Click(object sender, EventArgs e)
        {
            
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            createGroup cg = new createGroup();
            cg.ShowDialog();
        }

        private void group_Load(object sender, EventArgs e)
        {
            displayGroups();
        }

        public async void displayGroups()
        {
            otherFunc o = new otherFunc();
            string[] groups = await o.getGroups(username);
            var db = otherFunc.FirestoreConn();
            CollectionReference colref = db.Collection("Groups");
            foreach (string g in groups)
            {
                DocumentReference docref = colref.Document(g);
                DocumentSnapshot docsnap = await docref.GetSnapshotAsync();

                if (docsnap.Exists)
                {
                    string groupN = docsnap.GetValue<string>("GroupName");
                    //string[] members = docsnap.GetValue<string[]>("Members");
                    //int totalMembers = members.Count();

                    Guna2GradientPanel pnl = new Guna2GradientPanel();
                    pnl.Size = new Size(355, 82);
                    pnl.BorderRadius = 13;
                    pnl.FillColor = Color.FromArgb(227, 180, 72);
                    pnl.FillColor2 = Color.FromArgb(83, 123, 47);

                    System.Windows.Forms.Label lblGroupname = new System.Windows.Forms.Label();
                    lblGroupname.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    lblGroupname.BackColor = Color.Transparent;
                    lblGroupname.Size = new Size(200, 33);
                    lblGroupname.Location = new Point(26, 24);
                    lblGroupname.ForeColor = Color.White;
                    lblGroupname.Text = groupN;


                    //System.Windows.Forms.Label lbltotalMembers = new System.Windows.Forms.Label();
                    //lbltotalMembers.Font = new Font("Poppins", 14.25f, FontStyle.Regular);
                    //lbltotalMembers.BackColor = Color.Transparent;
                    //lbltotalMembers.Size = new Size(60, 33);
                    //lbltotalMembers.Location = new Point(301, 24);
                    //lbltotalMembers.ForeColor = Color.White;
                    //lbltotalMembers.Text = totalMembers.ToString();

                 
                    //pnl.Controls.Add(lbltotalMembers);
                    pnl.Controls.Add(lblGroupname);
                    flpGroup.Controls.Add(pnl);
                }
            }
        }
    }
}
