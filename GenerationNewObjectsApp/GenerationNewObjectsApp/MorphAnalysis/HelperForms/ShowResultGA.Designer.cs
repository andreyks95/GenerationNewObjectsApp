namespace MorphAnalysis.HelperForms
{
    partial class ShowResultGA
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
            this.resultGA_DGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.resultGA_DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // resultGA_DGV
            // 
            this.resultGA_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGA_DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultGA_DGV.Location = new System.Drawing.Point(0, 0);
            this.resultGA_DGV.Name = "resultGA_DGV";
            this.resultGA_DGV.Size = new System.Drawing.Size(800, 450);
            this.resultGA_DGV.TabIndex = 0;
            // 
            // ShowResultGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.resultGA_DGV);
            this.Name = "ShowResultGA";
            this.Text = "ShowResultGA";
            this.Load += new System.EventHandler(this.ShowResultGA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultGA_DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultGA_DGV;
    }
}