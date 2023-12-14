namespace ExpenseApp
{
    partial class AddingBalanceForm
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
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.panel = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.btnChangeWallet = new Guna.UI2.WinForms.Guna2Button();
            this.lblAdd = new System.Windows.Forms.Label();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.closeBTN = new System.Windows.Forms.PictureBox();
            this.txtAmount = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnAddAmount = new Guna.UI2.WinForms.Guna2Button();
            this.guna2ShadowPanel1.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBTN)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.panel);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(21, 15);
            this.guna2ShadowPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 10;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowDepth = 130;
            this.guna2ShadowPanel1.ShadowShift = 10;
            this.guna2ShadowPanel1.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.ForwardDiagonal;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(505, 86);
            this.guna2ShadowPanel1.TabIndex = 4;
            // 
            // panel
            // 
            this.panel.BorderRadius = 20;
            this.panel.Controls.Add(this.btnChangeWallet);
            this.panel.Controls.Add(this.lblAdd);
            this.panel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(180)))), ((int)(((byte)(72)))));
            this.panel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(496, 74);
            this.panel.TabIndex = 5;
            // 
            // btnChangeWallet
            // 
            this.btnChangeWallet.BorderRadius = 15;
            this.btnChangeWallet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChangeWallet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChangeWallet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChangeWallet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChangeWallet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(180)))), ((int)(((byte)(72)))));
            this.btnChangeWallet.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeWallet.ForeColor = System.Drawing.Color.White;
            this.btnChangeWallet.Location = new System.Drawing.Point(341, 16);
            this.btnChangeWallet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnChangeWallet.Name = "btnChangeWallet";
            this.btnChangeWallet.PressedDepth = 34;
            this.btnChangeWallet.Size = new System.Drawing.Size(137, 41);
            this.btnChangeWallet.TabIndex = 18;
            this.btnChangeWallet.Text = "Expenses";
            this.btnChangeWallet.Click += new System.EventHandler(this.btnChangeWallet_Click);
            // 
            // lblAdd
            // 
            this.lblAdd.AutoSize = true;
            this.lblAdd.BackColor = System.Drawing.Color.Transparent;
            this.lblAdd.Font = new System.Drawing.Font("Poppins", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdd.ForeColor = System.Drawing.Color.White;
            this.lblAdd.Location = new System.Drawing.Point(20, 16);
            this.lblAdd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(276, 42);
            this.lblAdd.TabIndex = 0;
            this.lblAdd.Text = "Add Balance Amount:";
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // closeBTN
            // 
            this.closeBTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBTN.Image = global::ExpenseApp.Properties.Resources.delete;
            this.closeBTN.Location = new System.Drawing.Point(572, 9);
            this.closeBTN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closeBTN.Name = "closeBTN";
            this.closeBTN.Size = new System.Drawing.Size(60, 33);
            this.closeBTN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeBTN.TabIndex = 16;
            this.closeBTN.TabStop = false;
            this.closeBTN.Click += new System.EventHandler(this.closeBTN_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.Transparent;
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
            this.txtAmount.Location = new System.Drawing.Point(21, 108);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.txtAmount.MaxLength = 6;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.PasswordChar = '\0';
            this.txtAmount.PlaceholderText = "Add New Amount";
            this.txtAmount.SelectedText = "";
            this.txtAmount.Size = new System.Drawing.Size(421, 64);
            this.txtAmount.TabIndex = 17;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // btnAddAmount
            // 
            this.btnAddAmount.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAmount.BorderRadius = 25;
            this.btnAddAmount.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAmount.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddAmount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(123)))), ((int)(((byte)(47)))));
            this.btnAddAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAmount.ForeColor = System.Drawing.Color.White;
            this.btnAddAmount.Location = new System.Drawing.Point(449, 108);
            this.btnAddAmount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddAmount.Name = "btnAddAmount";
            this.btnAddAmount.PressedDepth = 34;
            this.btnAddAmount.Size = new System.Drawing.Size(77, 66);
            this.btnAddAmount.TabIndex = 19;
            this.btnAddAmount.Text = "+";
            this.btnAddAmount.Click += new System.EventHandler(this.btnAddAmount_Click);
            // 
            // AddingBalanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ExpenseApp.Properties.Resources.profileBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(645, 194);
            this.Controls.Add(this.btnAddAmount);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.closeBTN);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddingBalanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddingBalanceForm";
            this.Load += new System.EventHandler(this.AddingBalanceForm_Load);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBTN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2GradientPanel panel;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.PictureBox closeBTN;
        private Guna.UI2.WinForms.Guna2TextBox txtAmount;
        private Guna.UI2.WinForms.Guna2Button btnChangeWallet;
        private Guna.UI2.WinForms.Guna2Button btnAddAmount;
    }
}