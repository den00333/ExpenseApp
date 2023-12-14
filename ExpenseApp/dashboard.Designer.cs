namespace ExpenseApp
{
    partial class dashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSpendings = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.expensesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.expenseCategoryDonut = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnToday = new Guna.UI2.WinForms.Guna2Button();
            this.btnAllExpenses = new Guna.UI2.WinForms.Guna2Button();
            this.btnWeek = new Guna.UI2.WinForms.Guna2Button();
            this.btnMonth = new Guna.UI2.WinForms.Guna2Button();
            this.cmbGroup = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUploadData = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSwitch = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expensesChart)).BeginInit();
            this.guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryDonut)).BeginInit();
            this.guna2Panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 5;
            this.guna2Panel1.Controls.Add(this.lblSpendings);
            this.guna2Panel1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Location = new System.Drawing.Point(53, 78);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(315, 96);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblSpendings
            // 
            this.lblSpendings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpendings.AutoSize = true;
            this.lblSpendings.BackColor = System.Drawing.Color.Transparent;
            this.lblSpendings.Font = new System.Drawing.Font("Poppins SemiBold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpendings.Location = new System.Drawing.Point(43, 14);
            this.lblSpendings.Name = "lblSpendings";
            this.lblSpendings.Size = new System.Drawing.Size(218, 70);
            this.lblSpendings.TabIndex = 1;
            this.lblSpendings.Text = "₱100,000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(88, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Spendings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(425, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 36);
            this.label3.TabIndex = 1;
            this.label3.Text = "Total Transactions";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.guna2Panel2.BorderRadius = 15;
            this.guna2Panel2.BorderThickness = 5;
            this.guna2Panel2.Controls.Add(this.lblTransaction);
            this.guna2Panel2.FillColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.Location = new System.Drawing.Point(389, 78);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(284, 96);
            this.guna2Panel2.TabIndex = 2;
            // 
            // lblTransaction
            // 
            this.lblTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.BackColor = System.Drawing.Color.Transparent;
            this.lblTransaction.Font = new System.Drawing.Font("Poppins SemiBold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaction.Location = new System.Drawing.Point(99, 14);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(79, 70);
            this.lblTransaction.TabIndex = 1;
            this.lblTransaction.Text = "20";
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 15;
            this.guna2Panel3.Controls.Add(this.expensesChart);
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(53, 186);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(964, 496);
            this.guna2Panel3.TabIndex = 4;
            // 
            // expensesChart
            // 
            this.expensesChart.BorderlineColor = System.Drawing.Color.Transparent;
            this.expensesChart.BorderlineWidth = 0;
            chartArea5.AxisX.IsLabelAutoFit = false;
            chartArea5.AxisX.LabelStyle.Font = new System.Drawing.Font("Poppins", 12.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea5.AxisX.LineWidth = 0;
            chartArea5.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea5.AxisX.MajorGrid.LineWidth = 0;
            chartArea5.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea5.AxisX.MajorTickMark.LineWidth = 0;
            chartArea5.AxisX.MajorTickMark.Size = 5F;
            chartArea5.AxisY.IsLabelAutoFit = false;
            chartArea5.AxisY.LabelStyle.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea5.AxisY.LabelStyle.Format = "₱{0}";
            chartArea5.AxisY.LineWidth = 0;
            chartArea5.AxisY.MajorGrid.LineWidth = 0;
            chartArea5.AxisY.MajorTickMark.LineWidth = 0;
            chartArea5.AxisY.MajorTickMark.Size = 5F;
            chartArea5.Name = "ChartArea1";
            this.expensesChart.ChartAreas.Add(chartArea5);
            this.expensesChart.Location = new System.Drawing.Point(8, 14);
            this.expensesChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.expensesChart.Name = "expensesChart";
            this.expensesChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series5.BorderWidth = 5;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            series5.MarkerSize = 10;
            series5.Name = "Series1";
            series5.YValuesPerPoint = 3;
            this.expensesChart.Series.Add(series5);
            this.expensesChart.Size = new System.Drawing.Size(948, 466);
            this.expensesChart.TabIndex = 3;
            this.expensesChart.Text = "chart1";
            title3.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title3.Font = new System.Drawing.Font("Poppins SemiBold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title3.Name = "Title1";
            title3.Text = "Spending Trend";
            this.expensesChart.Titles.Add(title3);
            this.expensesChart.MouseEnter += new System.EventHandler(this.expensesChart_MouseEnter);
            this.expensesChart.MouseLeave += new System.EventHandler(this.expensesChart_MouseLeave);
            this.expensesChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.expensesChart_MouseMove);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Poppins SemiBold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(44, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 53);
            this.label5.TabIndex = 5;
            this.label5.Text = "Dashboard";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.Controls.Add(this.label6);
            this.guna2Panel4.Controls.Add(this.expenseCategoryDonut);
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(1037, 268);
            this.guna2Panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(408, 414);
            this.guna2Panel4.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(338, 36);
            this.label6.TabIndex = 8;
            this.label6.Text = "Categories Expenses Distribution";
            // 
            // expenseCategoryDonut
            // 
            chartArea6.Name = "ChartArea1";
            this.expenseCategoryDonut.ChartAreas.Add(chartArea6);
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend3.Name = "Legend1";
            this.expenseCategoryDonut.Legends.Add(legend3);
            this.expenseCategoryDonut.Location = new System.Drawing.Point(15, 39);
            this.expenseCategoryDonut.Margin = new System.Windows.Forms.Padding(4);
            this.expenseCategoryDonut.Name = "expenseCategoryDonut";
            this.expenseCategoryDonut.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.expenseCategoryDonut.Series.Add(series6);
            this.expenseCategoryDonut.Size = new System.Drawing.Size(389, 370);
            this.expenseCategoryDonut.TabIndex = 6;
            this.expenseCategoryDonut.Text = "chart1";
            this.expenseCategoryDonut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.expenseCategoryDonut_MouseMove);
            // 
            // btnToday
            // 
            this.btnToday.BorderRadius = 10;
            this.btnToday.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnToday.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnToday.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnToday.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnToday.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnToday.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToday.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnToday.Location = new System.Drawing.Point(688, 128);
            this.btnToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(157, 46);
            this.btnToday.TabIndex = 8;
            this.btnToday.Text = "Today";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnAllExpenses
            // 
            this.btnAllExpenses.BorderRadius = 10;
            this.btnAllExpenses.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAllExpenses.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAllExpenses.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAllExpenses.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAllExpenses.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnAllExpenses.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllExpenses.ForeColor = System.Drawing.Color.White;
            this.btnAllExpenses.Location = new System.Drawing.Point(688, 78);
            this.btnAllExpenses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAllExpenses.Name = "btnAllExpenses";
            this.btnAllExpenses.Size = new System.Drawing.Size(157, 46);
            this.btnAllExpenses.TabIndex = 9;
            this.btnAllExpenses.Text = "All Expenses";
            this.btnAllExpenses.Click += new System.EventHandler(this.btnAllExpenses_Click);
            // 
            // btnWeek
            // 
            this.btnWeek.BorderRadius = 10;
            this.btnWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWeek.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnWeek.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeek.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnWeek.Location = new System.Drawing.Point(861, 78);
            this.btnWeek.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(157, 46);
            this.btnWeek.TabIndex = 10;
            this.btnWeek.Text = "This Week";
            this.btnWeek.Click += new System.EventHandler(this.btnWeek_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.BorderRadius = 10;
            this.btnMonth.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMonth.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMonth.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMonth.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMonth.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnMonth.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonth.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnMonth.Location = new System.Drawing.Point(861, 128);
            this.btnMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(157, 46);
            this.btnMonth.TabIndex = 11;
            this.btnMonth.Text = "This Month";
            this.btnMonth.Click += new System.EventHandler(this.btnMonth_Click);
            // 
            // cmbGroup
            // 
            this.cmbGroup.BackColor = System.Drawing.Color.Transparent;
            this.cmbGroup.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.cmbGroup.BorderRadius = 10;
            this.cmbGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.Enabled = false;
            this.cmbGroup.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbGroup.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbGroup.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbGroup.ItemHeight = 30;
            this.cmbGroup.Location = new System.Drawing.Point(1211, 23);
            this.cmbGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(215, 36);
            this.cmbGroup.TabIndex = 18;
            this.cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            // 
            // btnSwitch
            // 
            this.btnSwitch.BorderRadius = 10;
            this.btnSwitch.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSwitch.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSwitch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSwitch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSwitch.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnSwitch.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitch.ForeColor = System.Drawing.Color.White;
            this.btnSwitch.Location = new System.Drawing.Point(1046, 24);
            this.btnSwitch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(157, 46);
            this.btnSwitch.TabIndex = 20;
            this.btnSwitch.Text = "User";
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // btnUploadData
            // 
            this.btnUploadData.BackColor = System.Drawing.Color.Transparent;
            this.btnUploadData.BorderRadius = 10;
            this.btnUploadData.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnUploadData.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnUploadData.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnUploadData.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnUploadData.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(180)))), ((int)(((byte)(72)))));
            this.btnUploadData.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadData.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnUploadData.Location = new System.Drawing.Point(183, 9);
            this.btnUploadData.Margin = new System.Windows.Forms.Padding(2);
            this.btnUploadData.Name = "btnUploadData";
            this.btnUploadData.Size = new System.Drawing.Size(85, 44);
            this.btnUploadData.TabIndex = 20;
            this.btnUploadData.Text = "Uplaod your txt";
            this.btnUploadData.Click += new System.EventHandler(this.btnUploadData_Click);
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.BorderRadius = 15;
            this.guna2Panel5.Controls.Add(this.btnUploadData);
            this.guna2Panel5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.guna2Panel5.Location = new System.Drawing.Point(789, 140);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(281, 61);
            this.guna2Panel5.TabIndex = 21;
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.guna2Panel5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.btnMonth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnWeek);
            this.Controls.Add(this.btnAllExpenses);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "dashboard";
            this.Size = new System.Drawing.Size(1469, 708);
            this.Load += new System.EventHandler(this.dashboard_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            this.guna2Panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.expensesChart)).EndInit();
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryDonut)).EndInit();
            this.guna2Panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblSpendings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label lblTransaction;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.DataVisualization.Charting.Chart expenseCategoryDonut;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2Button btnToday;
        private Guna.UI2.WinForms.Guna2Button btnAllExpenses;
        private Guna.UI2.WinForms.Guna2Button btnWeek;
        private Guna.UI2.WinForms.Guna2Button btnMonth;
        private System.Windows.Forms.DataVisualization.Charting.Chart expensesChart;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGroup;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnUploadData;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel5;
        private Guna.UI2.WinForms.Guna2Button btnSwitch;
    }
}