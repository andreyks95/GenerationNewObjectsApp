namespace MorphAnalysis.TablesExpertEvaluation
{
    partial class TableParametersOfGoals
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonSaveResultGoals = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCalcGoals = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCalcParams = new System.Windows.Forms.Button();
            this.buttonSaveResultParamsOfGoal = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.buttonSaveResultGoals);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonCalcGoals);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 50);
            this.panel1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(620, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "0";
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // buttonSaveResultGoals
            // 
            this.buttonSaveResultGoals.Enabled = false;
            this.buttonSaveResultGoals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveResultGoals.Location = new System.Drawing.Point(728, 9);
            this.buttonSaveResultGoals.Name = "buttonSaveResultGoals";
            this.buttonSaveResultGoals.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveResultGoals.TabIndex = 3;
            this.buttonSaveResultGoals.Text = "Занести";
            this.buttonSaveResultGoals.UseVisualStyleBackColor = true;
            this.buttonSaveResultGoals.Click += new System.EventHandler(this.buttonSaveResultGoals_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(528, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 34);
            this.label4.TabIndex = 2;
            this.label4.Text = "Занести цілі в таблицю,\r\nвага яких >=";
            // 
            // buttonCalcGoals
            // 
            this.buttonCalcGoals.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCalcGoals.Location = new System.Drawing.Point(199, 12);
            this.buttonCalcGoals.Name = "buttonCalcGoals";
            this.buttonCalcGoals.Size = new System.Drawing.Size(152, 30);
            this.buttonCalcGoals.TabIndex = 1;
            this.buttonCalcGoals.Text = "Розрахунок цілей";
            this.buttonCalcGoals.UseVisualStyleBackColor = true;
            this.buttonCalcGoals.Click += new System.EventHandler(this.buttonCalcGoals_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(15, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Розрахувати вагу цілей";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 50);
            this.dataGridView1.MinimumSize = new System.Drawing.Size(780, 220);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(984, 245);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            this.dataGridView1.Enter += new System.EventHandler(this.dataGridView1_Enter);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.buttonCalcParams);
            this.panel2.Controls.Add(this.buttonSaveResultParamsOfGoal);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 50);
            this.panel2.TabIndex = 7;
            // 
            // buttonCalcParams
            // 
            this.buttonCalcParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCalcParams.Location = new System.Drawing.Point(283, 7);
            this.buttonCalcParams.Name = "buttonCalcParams";
            this.buttonCalcParams.Size = new System.Drawing.Size(205, 30);
            this.buttonCalcParams.TabIndex = 0;
            this.buttonCalcParams.Text = "Розрахунок параметрів";
            this.buttonCalcParams.UseVisualStyleBackColor = true;
            this.buttonCalcParams.Click += new System.EventHandler(this.buttonCalcParams_Click);
            // 
            // buttonSaveResultParamsOfGoal
            // 
            this.buttonSaveResultParamsOfGoal.Enabled = false;
            this.buttonSaveResultParamsOfGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveResultParamsOfGoal.Location = new System.Drawing.Point(784, 3);
            this.buttonSaveResultParamsOfGoal.Name = "buttonSaveResultParamsOfGoal";
            this.buttonSaveResultParamsOfGoal.Size = new System.Drawing.Size(98, 23);
            this.buttonSaveResultParamsOfGoal.TabIndex = 3;
            this.buttonSaveResultParamsOfGoal.Text = "Занести";
            this.buttonSaveResultParamsOfGoal.UseVisualStyleBackColor = true;
            this.buttonSaveResultParamsOfGoal.Click += new System.EventHandler(this.buttonSaveResultParamsOfGoal_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(528, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Занести параметри цілей в таблицю";
            this.label3.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "Розрахувати ср. значення параметрів \r\nв межах обраної цілі:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 345);
            this.dataGridView2.MinimumSize = new System.Drawing.Size(0, 210);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(984, 214);
            this.dataGridView2.TabIndex = 8;
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView2_CellValidating);
            this.dataGridView2.Enter += new System.EventHandler(this.dataGridView2_Enter);
            this.dataGridView2.MouseLeave += new System.EventHandler(this.dataGridView2_MouseLeave);
            // 
            // TableParametersOfGoals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "TableParametersOfGoals";
            this.Text = "TableParametersOfGoals";
            this.Load += new System.EventHandler(this.TableParametersOfGoals_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonSaveResultGoals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCalcGoals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonSaveResultParamsOfGoal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCalcParams;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label3;
    }
}