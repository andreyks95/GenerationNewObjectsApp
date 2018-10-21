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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowResultGA));
            this.resultGA_DGV = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.projectNameComboBox = new System.Windows.Forms.ComboBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.saveToFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.resultGA_DGV)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultGA_DGV
            // 
            this.resultGA_DGV.AllowUserToAddRows = false;
            this.resultGA_DGV.AllowUserToDeleteRows = false;
            this.resultGA_DGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultGA_DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultGA_DGV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.resultGA_DGV.Location = new System.Drawing.Point(0, 62);
            this.resultGA_DGV.Name = "resultGA_DGV";
            this.resultGA_DGV.Size = new System.Drawing.Size(638, 388);
            this.resultGA_DGV.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.projectNameComboBox);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Controls.Add(this.textLabel);
            this.panel1.Controls.Add(this.saveToFileButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 1;
            // 
            // projectNameComboBox
            // 
            this.projectNameComboBox.FormattingEnabled = true;
            this.projectNameComboBox.Location = new System.Drawing.Point(130, 35);
            this.projectNameComboBox.Name = "projectNameComboBox";
            this.projectNameComboBox.Size = new System.Drawing.Size(300, 21);
            this.projectNameComboBox.TabIndex = 3;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(13, 38);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(82, 13);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Назва проекту";
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new System.Drawing.Point(13, 12);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(409, 13);
            this.textLabel.TabIndex = 1;
            this.textLabel.Text = "Зберегти найкращий результат (покоління) генетичного алгоритмуу в xml-файл";
            // 
            // saveToFileButton
            // 
            this.saveToFileButton.Location = new System.Drawing.Point(463, 33);
            this.saveToFileButton.Name = "saveToFileButton";
            this.saveToFileButton.Size = new System.Drawing.Size(75, 23);
            this.saveToFileButton.TabIndex = 0;
            this.saveToFileButton.Text = "Зберегти";
            this.saveToFileButton.UseVisualStyleBackColor = true;
            this.saveToFileButton.Click += new System.EventHandler(this.saveToFileButton_Click);
            // 
            // ShowResultGA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.resultGA_DGV);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowResultGA";
            this.Text = "Результат роботи генетичного алгоритму";
            this.Load += new System.EventHandler(this.ShowResultGA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultGA_DGV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView resultGA_DGV;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Button saveToFileButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.ComboBox projectNameComboBox;
    }
}