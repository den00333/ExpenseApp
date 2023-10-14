namespace ExpenseApp
{
    partial class wallet
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddXpns = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(310, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "sa wallet ito";
            // 
            // btnAddXpns
            // 
            this.btnAddXpns.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddXpns.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddXpns.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddXpns.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddXpns.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddXpns.ForeColor = System.Drawing.Color.White;
            this.btnAddXpns.Location = new System.Drawing.Point(507, 160);
            this.btnAddXpns.Name = "btnAddXpns";
            this.btnAddXpns.Size = new System.Drawing.Size(180, 45);
            this.btnAddXpns.TabIndex = 2;
            this.btnAddXpns.Text = "Add Expenses";
            this.btnAddXpns.Click += new System.EventHandler(this.btnAddXpns_Click);
            // 
            // wallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.btnAddXpns);
            this.Controls.Add(this.label1);
            this.Name = "wallet";
            this.Size = new System.Drawing.Size(929, 589);
            this.Load += new System.EventHandler(this.wallet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnAddXpns;
    }
}
