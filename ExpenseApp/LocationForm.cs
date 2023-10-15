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

        }
        private Dictionary<String, RegionData> data;
        private void initializeData()
        {
            String path = "C:\\Users\\Personal Laptop\\source\\repos\\ExpenseApp\\ExpenseApp\\Resources\\philippine_provinces_cities_municipalities_and_barangays_2019v2.json";
            String jsonData = File.ReadAllText(path);
            data = JsonConvert.DeserializeObject<Dictionary<string, RegionData>>(jsonData);

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
            cmbProvince.Items.Clear();
            String selectedRegion = cmbRegion.SelectedItem.ToString();
            RD = data[selectedRegion];
            foreach (var province in RD.provinceList.Keys)
            {
                cmbProvince.Items.Add(province);
            }

        }

        private void UpdateMunicipalCMB()
        {
            cmbMunicipal.Items.Clear();
            String selectedProvince = cmbProvince.SelectedItem.ToString();
            PD = RD.provinceList[selectedProvince];
            foreach (var municipal in PD.MunicipalList.Keys)
            {
                cmbMunicipal.Items.Add(municipal);
            }

        }

        private void UpdateBrgyCMB()
        {
            cmbBrgy.Items.Clear();
            String selectedMunicipal = cmbMunicipal.SelectedItem.ToString();
            MunicipalData MD = PD.MunicipalList[selectedMunicipal];
            foreach (var brgy in MD.BarangayList)
            {
                cmbBrgy.Items.Add(brgy);
            }
        }
        
        
        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbProvince, cmbMunicipal, cmbBrgy };
            clearAll(boxes);
            UpdateProvinceCMB();
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbMunicipal, cmbBrgy };
            clearAll(boxes);
            UpdateMunicipalCMB();
        }

        private void cmbMunicipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbBrgy };
            clearAll(boxes);
            UpdateBrgyCMB();
        }

        private void checkingAllBoxes(List<ComboBox> boxes)
        {
            if (cmbRegion.SelectedItem != null && cmbProvince.SelectedItem != null && cmbMunicipal.SelectedItem != null && cmbBrgy.SelectedItem != null)
            {
                foreach (var box in boxes)
                {
                    Address = box.SelectedItem.ToString() + ", " + Address;
                }
            }
            else
            {
                Address = "Please select your address...";
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            boxes = new List<ComboBox> { cmbProvince, cmbMunicipal, cmbBrgy };
            checkingAllBoxes(boxes);
            AEF.txtLocation.Text = Address.Trim(',');
            this.Hide();
        }
    }
}
