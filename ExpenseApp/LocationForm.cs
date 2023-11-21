using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Newtonsoft.Json;

namespace ExpenseApp
{
    public partial class LocationForm : Form
    {
        private AddExpensesForm AEF;
        String Address;
        public LocationForm(AddExpensesForm A)
        {
            this.AEF = A;
            InitializeComponent();
            initializeData();
            PopulateRegionCMB();
        }


        private void LocationForm_Load(object sender, EventArgs e)
        {
            EnterData();
        }
        private Dictionary<String, RegionData> data;
        private locationData LD = new locationData() ;
        private void initializeData()
        {
            String path = "Resources/philippine_provinces_cities_municipalities_and_barangays_2019v2.json";
            String jsonData = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<Dictionary<string, RegionData>>(jsonData);

            

        }

        public void EnterData()
        {
            if (!string.IsNullOrEmpty(AEF.txtLocation.Text))
            {
                cmbRegion.SelectedIndex = AEF.R;
                cmbProvince.SelectedIndex = AEF.P;
                cmbMunicipal.SelectedIndex = AEF.M;
                //cmbBrgy.SelectedIndex = AEF.B;
            }
        }

    

        List<ComboBox> boxes;
        private void clearAll(List<ComboBox> cmbList)
        {
            foreach (var box in cmbList)
            {
                box.Text = string.Empty;
            }
        }
        private void PopulateRegionCMB()
        {
            cmbRegion.Items.Clear();
            foreach (var region in data.Keys)
            {
                cmbRegion.Items.Add(region);
            }
            
        }

        RegionData RD;
        ProvinceData PD;
        private void UpdateProvinceCMB()
        {
            try
            {
                cmbProvince.Items.Clear();
                String selectedRegion = cmbRegion.SelectedItem.ToString();
                RD = data[selectedRegion];
                foreach (var province in RD.provinceList.Keys)
                {
                    cmbProvince.Items.Add(province);
                }
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("There's something wrong... \n Closing the Form. Please try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if(res == DialogResult.OK)
                {
                    this.Hide();
                }
            }

        }

        private void UpdateMunicipalCMB()
        {
            try
            {
                cmbMunicipal.Items.Clear();
                String selectedProvince = cmbProvince.SelectedItem.ToString();
                PD = RD.provinceList[selectedProvince];
                foreach (var municipal in PD.MunicipalList.Keys)
                {
                    cmbMunicipal.Items.Add(municipal);
                }
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("There's something wrong... \n Closing the Form. Please try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                {
                    this.Hide();
                }
            }
        }

        /*private void UpdateBrgyCMB()
        {
            try
            {
                cmbBrgy.Items.Clear();
                String selectedMunicipal = cmbMunicipal.SelectedItem.ToString();
                MunicipalData MD = PD.MunicipalList[selectedMunicipal];
                foreach (var brgy in MD.BarangayList)
                {
                    cmbBrgy.Items.Add(brgy);
                }
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("There's something wrong... \n Closing the Form. Please try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (res == DialogResult.OK)
                {
                    this.Hide();
                }
            }
        }*/
        
        
        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

            boxes = new List<ComboBox> { cmbProvince, cmbMunicipal };
            clearAll(boxes);
            UpdateProvinceCMB();

        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbMunicipal};
            clearAll(boxes);
            UpdateMunicipalCMB();
            
        }

        private void cmbMunicipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            //boxes = new List<ComboBox> { cmbBrgy };
            clearAll(boxes);
            //UpdateBrgyCMB();
            
        }

        private void checkingAllBoxes(List<ComboBox> boxes)
        {
            if (cmbRegion.SelectedItem != null && cmbProvince.SelectedItem != null && cmbMunicipal.SelectedItem != null)
            {
                foreach (var box in boxes)
                {
                    Address = box.SelectedItem.ToString() + ", " + Address;
                }
            }
            else
            {
                Address = null;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbProvince, cmbMunicipal };
            checkingAllBoxes(boxes);

            if(Address != null)
            {
                AEF.txtLocation.Text = Address.Trim(',', ' ');
            }
            else
            {
                AEF.txtLocation.PlaceholderText = "Please select your address...";
                AEF.txtLocation.Text = null;
            }
            AEF.R = cmbRegion.SelectedIndex;
            AEF.P = cmbProvince.SelectedIndex;
            AEF.M = cmbMunicipal.SelectedIndex;
            //AEF.B = cmbBrgy.SelectedIndex;

            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
