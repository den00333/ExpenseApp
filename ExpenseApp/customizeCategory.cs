using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace ExpenseApp
{
    public partial class customizeCategory : Form
    {
        AddExpensesForm AEF;
        public customizeCategory(AddExpensesForm A)
        {
            InitializeComponent();
            initializeCHB();
            this.AEF = A;
        }
        private ctg catGs;
        private ctg newCategory;
        private List<String> ListOfNewCategories;
        private List<String> ListOfUnwantedCategories;
        private void initializeCHB()
        {
            catGs = FileFunc.initializeData();
            foreach (var category in catGs.LCategory)
            {
                chbCategories.Items.Add(category);
            }
        }
        
        private void customizeCategory_Load(object sender, EventArgs e)
        {
            ListOfNewCategories = new List<String>();
            ListOfUnwantedCategories = new List<String>();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure that you are done?", "Exiting the Tab.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(res == DialogResult.OK)
            {
                FileFunc.updatingDataThroughList(ListOfNewCategories);
                FileFunc.deletingDataThroughList(ListOfUnwantedCategories);
                ctg categry = FileFunc.initializeData();
                otherFunc.populateCMBcategory(categry, AEF);
                this.Hide();
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCategory.Text)) {
                String newItem = txtCategory.Text.ToString().Trim();
                chbCategories.Items.Add(newItem);
                ListOfNewCategories.Add(newItem);
                txtCategory.Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete this item/s?", "Deleting Items.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if(res == DialogResult.OK)
            {
                for (int i = 0; i < chbCategories.Items.Count; i++)
                {
                    if (chbCategories.GetItemChecked(i))
                    {
                        ListOfUnwantedCategories.Add(chbCategories.Items[i].ToString());
                        if (ListOfNewCategories.Count != 0)
                        {
                            ListOfNewCategories.Remove(chbCategories.Items[i].ToString());
                        }
                        chbCategories.Items.RemoveAt(i);

                    }
                }
            }
        }
    }
}
