namespace ExpenseApp
{
    partial class AddExpensesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTxtDesc = new System.Windows.Forms.RichTextBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbPayment = new System.Windows.Forms.ComboBox();
            this.btnLocation = new Guna.UI2.WinForms.Guna2Button();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTxtDesc
            // 
            this.richTxtDesc.Location = new System.Drawing.Point(12, 23);
            this.richTxtDesc.Name = "richTxtDesc";
            this.richTxtDesc.Size = new System.Drawing.Size(308, 129);
            this.richTxtDesc.TabIndex = 0;
            this.richTxtDesc.Text = "Description or Name";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(358, 23);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 20);
            this.dtpDate.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(358, 79);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 21);
            this.cmbCategory.TabIndex = 2;
            this.cmbCategory.Text = "Cetegory";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(358, 132);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(177, 20);
            this.txtAmount.TabIndex = 3;
            this.txtAmount.Text = "Amount";
            // 
            // cmbPayment
            // 
            this.cmbPayment.FormattingEnabled = true;
            this.cmbPayment.Location = new System.Drawing.Point(358, 180);
            this.cmbPayment.Name = "cmbPayment";
            this.cmbPayment.Size = new System.Drawing.Size(200, 21);
            this.cmbPayment.TabIndex = 4;
            this.cmbPayment.Text = "Payment Method";
            // 
            // btnLocation
            // 
            this.btnLocation.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLocation.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLocation.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLocation.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLocation.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLocation.ForeColor = System.Drawing.Color.White;
            this.btnLocation.Location = new System.Drawing.Point(12, 210);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(103, 39);
            this.btnLocation.TabIndex = 5;
            this.btnLocation.Text = "Location";
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(135, 229);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.Size = new System.Drawing.Size(248, 20);
            this.txtLocation.TabIndex = 6;
            // 
            // AddExpensesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 337);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.cmbPayment);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.richTxtDesc);
            this.Name = "AddExpensesForm";
            this.Text = "AddExpensesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddExpensesForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTxtDesc;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cmbPayment;
        private Guna.UI2.WinForms.Guna2Button btnLocation;
        public System.Windows.Forms.TextBox txtLocation;
    }
}