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
            this.components = new System.ComponentModel.Container();
            this.txtLocation = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtAmount = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnBack = new Guna.UI2.WinForms.Guna2Button();
            this.dtpDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnCustomize = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLocation = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextNote = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2ShadowPanel4 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.richTxtDesc = new System.Windows.Forms.RichTextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.guna2ShadowPanel4.SuspendLayout();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLocation
            // 
            this.txtLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.txtLocation.BorderRadius = 15;
            this.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLocation.DefaultText = "";
            this.txtLocation.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLocation.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLocation.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocation.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLocation.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocation.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.ForeColor = System.Drawing.Color.Black;
            this.txtLocation.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtLocation.Location = new System.Drawing.Point(44, 370);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 9, 4, 9);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.PasswordChar = '\0';
            this.txtLocation.PlaceholderText = "Location";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.SelectedText = "";
            this.txtLocation.Size = new System.Drawing.Size(311, 60);
            this.txtLocation.TabIndex = 13;
            // 
            // txtAmount
            // 
            this.txtAmount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.txtAmount.BorderRadius = 15;
            this.txtAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAmount.DefaultText = "";
            this.txtAmount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAmount.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAmount.Location = new System.Drawing.Point(45, 87);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.PasswordChar = '\0';
            this.txtAmount.PlaceholderText = "Amount";
            this.txtAmount.SelectedText = "";
            this.txtAmount.Size = new System.Drawing.Size(397, 60);
            this.txtAmount.TabIndex = 15;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // cmbCategory
            // 
            this.cmbCategory.BackColor = System.Drawing.Color.Transparent;
            this.cmbCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.cmbCategory.BorderRadius = 10;
            this.cmbCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbCategory.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.ForeColor = System.Drawing.Color.Black;
            this.cmbCategory.ItemHeight = 30;
            this.cmbCategory.Location = new System.Drawing.Point(45, 202);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(309, 36);
            this.cmbCategory.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 158);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 42);
            this.label1.TabIndex = 18;
            this.label1.Text = "Category:";
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 20;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // btnSave
            // 
            this.btnSave.AutoRoundedCorners = true;
            this.btnSave.BorderColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderRadius = 26;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnSave.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(625, 459);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 55);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBack
            // 
            this.btnBack.AutoRoundedCorners = true;
            this.btnBack.BorderColor = System.Drawing.Color.Transparent;
            this.btnBack.BorderRadius = 26;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(4)))), ((int)(((byte)(45)))));
            this.btnBack.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(791, 459);
            this.btnBack.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.ShadowDecoration.Depth = 100;
            this.btnBack.Size = new System.Drawing.Size(156, 55);
            this.btnBack.TabIndex = 21;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.BorderRadius = 10;
            this.dtpDate.Checked = true;
            this.dtpDate.FillColor = System.Drawing.SystemColors.Control;
            this.dtpDate.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDate.Location = new System.Drawing.Point(44, 298);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(399, 55);
            this.dtpDate.TabIndex = 22;
            this.dtpDate.TextOffset = new System.Drawing.Point(5, 0);
            this.dtpDate.Value = new System.DateTime(2023, 11, 1, 0, 0, 0, 0);
            // 
            // btnCustomize
            // 
            this.btnCustomize.AutoRoundedCorners = true;
            this.btnCustomize.BorderColor = System.Drawing.Color.Transparent;
            this.btnCustomize.BorderRadius = 23;
            this.btnCustomize.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomize.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCustomize.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCustomize.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCustomize.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnCustomize.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomize.ForeColor = System.Drawing.Color.White;
            this.btnCustomize.Location = new System.Drawing.Point(373, 202);
            this.btnCustomize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCustomize.Name = "btnCustomize";
            this.btnCustomize.Size = new System.Drawing.Size(69, 49);
            this.btnCustomize.TabIndex = 23;
            this.btnCustomize.Text = "...";
            this.btnCustomize.Click += new System.EventHandler(this.btnCustomize_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 254);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 42);
            this.label2.TabIndex = 24;
            this.label2.Text = "Date:";
            // 
            // btnLocation
            // 
            this.btnLocation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnLocation.BorderRadius = 20;
            this.btnLocation.BorderThickness = 2;
            this.btnLocation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLocation.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLocation.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLocation.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLocation.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLocation.FillColor = System.Drawing.Color.Empty;
            this.btnLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocation.ForeColor = System.Drawing.Color.Black;
            this.btnLocation.Image = global::ExpenseApp.Properties.Resources.location;
            this.btnLocation.ImageSize = new System.Drawing.Size(30, 30);
            this.btnLocation.Location = new System.Drawing.Point(383, 370);
            this.btnLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLocation.Name = "btnLocation";
            this.btnLocation.Size = new System.Drawing.Size(60, 60);
            this.btnLocation.TabIndex = 5;
            this.btnLocation.Click += new System.EventHandler(this.btnLocation_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(467, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(243, 42);
            this.label3.TabIndex = 25;
            this.label3.Text = "Name/Description:";
            // 
            // richTextNote
            // 
            this.richTextNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextNote.Font = new System.Drawing.Font("Poppins", 12F);
            this.richTextNote.Location = new System.Drawing.Point(13, 15);
            this.richTextNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextNote.Name = "richTextNote";
            this.richTextNote.Size = new System.Drawing.Size(432, 110);
            this.richTextNote.TabIndex = 26;
            this.richTextNote.Text = "Note";
            this.richTextNote.Enter += new System.EventHandler(this.richTextNote_Enter);
            this.richTextNote.Leave += new System.EventHandler(this.richTextNote_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(467, 249);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 42);
            this.label4.TabIndex = 27;
            this.label4.Text = "Note:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins SemiBold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(250, 60);
            this.label5.TabIndex = 28;
            this.label5.Text = "New Expense";
            // 
            // guna2ShadowPanel4
            // 
            this.guna2ShadowPanel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel4.Controls.Add(this.richTextNote);
            this.guna2ShadowPanel4.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel4.Font = new System.Drawing.Font("Poppins", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2ShadowPanel4.Location = new System.Drawing.Point(472, 292);
            this.guna2ShadowPanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2ShadowPanel4.Name = "guna2ShadowPanel4";
            this.guna2ShadowPanel4.Radius = 10;
            this.guna2ShadowPanel4.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel4.ShadowDepth = 130;
            this.guna2ShadowPanel4.ShadowShift = 10;
            this.guna2ShadowPanel4.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel4.Size = new System.Drawing.Size(475, 148);
            this.guna2ShadowPanel4.TabIndex = 30;
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.richTxtDesc);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(472, 94);
            this.guna2ShadowPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 10;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowDepth = 130;
            this.guna2ShadowPanel1.ShadowShift = 10;
            this.guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(475, 148);
            this.guna2ShadowPanel1.TabIndex = 29;
            // 
            // richTxtDesc
            // 
            this.richTxtDesc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTxtDesc.Font = new System.Drawing.Font("Poppins", 12F);
            this.richTxtDesc.Location = new System.Drawing.Point(13, 11);
            this.richTxtDesc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTxtDesc.Name = "richTxtDesc";
            this.richTxtDesc.Size = new System.Drawing.Size(432, 107);
            this.richTxtDesc.TabIndex = 0;
            this.richTxtDesc.Text = "Name";
            this.richTxtDesc.TextChanged += new System.EventHandler(this.richTxtDesc_TextChanged);
            this.richTxtDesc.Enter += new System.EventHandler(this.richTxtDesc_Enter);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this.txtLocation;
            // 
            // AddExpensesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(973, 528);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCustomize);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.btnLocation);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.guna2ShadowPanel4);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(973, 528);
            this.Name = "AddExpensesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddExpensesForm";
            this.Load += new System.EventHandler(this.AddExpensesForm_Load);
            this.guna2ShadowPanel4.ResumeLayout(false);
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnLocation;
        private Guna.UI2.WinForms.Guna2TextBox txtAmount;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        public Guna.UI2.WinForms.Guna2TextBox txtLocation;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnBack;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDate;
        private Guna.UI2.WinForms.Guna2Button btnCustomize;
        public Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextNote;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel4;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.RichTextBox richTxtDesc;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}