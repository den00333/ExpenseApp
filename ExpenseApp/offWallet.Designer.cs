namespace ExpenseApp
{
    partial class offWallet
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
            this.pnlWallet = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlWallet
            // 
            this.pnlWallet.Location = new System.Drawing.Point(12, 12);
            this.pnlWallet.Name = "pnlWallet";
            this.pnlWallet.Size = new System.Drawing.Size(1443, 699);
            this.pnlWallet.TabIndex = 0;
            // 
            // offWallet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 723);
            this.Controls.Add(this.pnlWallet);
            this.Name = "offWallet";
            this.Text = "offWallet";
            this.Load += new System.EventHandler(this.offWallet_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlWallet;
    }
}