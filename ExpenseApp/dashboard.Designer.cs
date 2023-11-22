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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSpendings = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTransaction = new System.Windows.Forms.Label();
            this.expensesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.expenseCategoryDonut = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expensesChart)).BeginInit();
            this.guna2Panel3.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryDonut)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.BorderThickness = 5;
            this.guna2Panel1.Controls.Add(this.lblSpendings);
            this.guna2Panel1.FillColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Location = new System.Drawing.Point(54, 77);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(314, 118);
            this.guna2Panel1.TabIndex = 0;
            // 
            // lblSpendings
            // 
            this.lblSpendings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpendings.AutoSize = true;
            this.lblSpendings.Font = new System.Drawing.Font("Poppins Medium", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpendings.Location = new System.Drawing.Point(42, 27);
            this.lblSpendings.Name = "lblSpendings";
            this.lblSpendings.Size = new System.Drawing.Size(240, 76);
            this.lblSpendings.TabIndex = 1;
            this.lblSpendings.Text = "₱100,000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(87, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Spendings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(423, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 41);
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
            this.guna2Panel2.Location = new System.Drawing.Point(390, 77);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(314, 118);
            this.guna2Panel2.TabIndex = 2;
            // 
            // lblTransaction
            // 
            this.lblTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTransaction.AutoSize = true;
            this.lblTransaction.Font = new System.Drawing.Font("Poppins Medium", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransaction.Location = new System.Drawing.Point(110, 27);
            this.lblTransaction.Name = "lblTransaction";
            this.lblTransaction.Size = new System.Drawing.Size(87, 76);
            this.lblTransaction.TabIndex = 1;
            this.lblTransaction.Text = "20";
            // 
            // expensesChart
            // 
            this.expensesChart.BorderlineColor = System.Drawing.Color.Transparent;
            this.expensesChart.BorderlineWidth = 0;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Poppins", 12.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineWidth = 0;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.MajorTickMark.LineWidth = 0;
            chartArea1.AxisX.MajorTickMark.Size = 5F;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.Format = "₱{0}";
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.LineWidth = 0;
            chartArea1.AxisY.MajorTickMark.LineWidth = 0;
            chartArea1.AxisY.MajorTickMark.Size = 5F;
            chartArea1.Name = "ChartArea1";
            this.expensesChart.ChartAreas.Add(chartArea1);
            this.expensesChart.Location = new System.Drawing.Point(8, 11);
            this.expensesChart.Name = "expensesChart";
            this.expensesChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.BorderWidth = 5;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.MarkerColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            series1.MarkerSize = 10;
            series1.Name = "Series1";
            series1.YValuesPerPoint = 3;
            this.expensesChart.Series.Add(series1);
            this.expensesChart.Size = new System.Drawing.Size(966, 502);
            this.expensesChart.TabIndex = 3;
            this.expensesChart.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title1.Font = new System.Drawing.Font("Poppins SemiBold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Spending Trend";
            this.expensesChart.Titles.Add(title1);
            this.expensesChart.MouseLeave += new System.EventHandler(this.expensesChart_MouseLeave);
            this.expensesChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.expensesChart_MouseMove);
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 15;
            this.guna2Panel3.Controls.Add(this.expensesChart);
            this.guna2Panel3.FillColor = System.Drawing.Color.White;
            this.guna2Panel3.Location = new System.Drawing.Point(54, 216);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(983, 519);
            this.guna2Panel3.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(33, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 39);
            this.label5.TabIndex = 5;
            this.label5.Text = "Dashboard";
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.BorderRadius = 15;
            this.guna2Panel4.Controls.Add(this.label6);
            this.guna2Panel4.FillColor = System.Drawing.Color.White;
            this.guna2Panel4.Location = new System.Drawing.Point(1054, 216);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(408, 414);
            this.guna2Panel4.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(344, 35);
            this.label6.TabIndex = 8;
            this.label6.Text = "Categories Expenses Distribution";
            // 
            // expenseCategoryDonut
            // 
            chartArea2.Name = "ChartArea1";
            this.expenseCategoryDonut.ChartAreas.Add(chartArea2);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            this.expenseCategoryDonut.Legends.Add(legend1);
            this.expenseCategoryDonut.Location = new System.Drawing.Point(1074, 265);
            this.expenseCategoryDonut.Name = "expenseCategoryDonut";
            this.expenseCategoryDonut.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.expenseCategoryDonut.Series.Add(series2);
            this.expenseCategoryDonut.Size = new System.Drawing.Size(376, 362);
            this.expenseCategoryDonut.TabIndex = 6;
            this.expenseCategoryDonut.Text = "chart1";
            this.expenseCategoryDonut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.expenseCategoryDonut_MouseMove);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.expenseCategoryDonut);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Panel3);
            this.Controls.Add(this.guna2Panel4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "dashboard";
            this.Size = new System.Drawing.Size(1479, 763);
            this.Load += new System.EventHandler(this.dashboard_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expensesChart)).EndInit();
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.expenseCategoryDonut)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart expensesChart;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel4;
        private System.Windows.Forms.DataVisualization.Charting.Chart expenseCategoryDonut;
        private System.Windows.Forms.Label label6;
    }
}
