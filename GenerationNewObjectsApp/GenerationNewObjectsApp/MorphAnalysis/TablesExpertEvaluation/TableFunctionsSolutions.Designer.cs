namespace MorphAnalysis.TablesExpertEvaluation
{
    partial class TableFunctionsSolutions
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
            this.buttonSaveResultFuncs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonCalcFuncs = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSaveResultSolutionsOfFunc = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCalcSols = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.buttonSaveResultFuncs);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonCalcFuncs);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 50);
            this.panel1.TabIndex = 0;
            // 
            // buttonSaveResultFuncs
            // 
            this.buttonSaveResultFuncs.Enabled = false;
            this.buttonSaveResultFuncs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveResultFuncs.Location = new System.Drawing.Point(815, 10);
            this.buttonSaveResultFuncs.Name = "buttonSaveResultFuncs";
            this.buttonSaveResultFuncs.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveResultFuncs.TabIndex = 3;
            this.buttonSaveResultFuncs.Text = "Занести";
            this.buttonSaveResultFuncs.UseVisualStyleBackColor = true;
            this.buttonSaveResultFuncs.Click += new System.EventHandler(this.buttonSaveResultFuncs_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(528, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(285, 34);
            this.label4.TabIndex = 2;
            this.label4.Text = "Занести функції в морфологічну таблицю,\r\nвага яких >=";
            // 
            // buttonCalcFuncs
            // 
            this.buttonCalcFuncs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCalcFuncs.Location = new System.Drawing.Point(199, 12);
            this.buttonCalcFuncs.Name = "buttonCalcFuncs";
            this.buttonCalcFuncs.Size = new System.Drawing.Size(152, 30);
            this.buttonCalcFuncs.TabIndex = 1;
            this.buttonCalcFuncs.Text = "Розрахунок функцій";
            this.buttonCalcFuncs.UseVisualStyleBackColor = true;
            this.buttonCalcFuncs.Click += new System.EventHandler(this.buttonCalcFuncs_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(15, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Розрахувати вагу функцій";
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
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView1_RowStateChanged);
            this.dataGridView1.Enter += new System.EventHandler(this.dataGridView1_Enter);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.buttonSaveResultSolutionsOfFunc);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.buttonCalcSols);
            this.panel2.Location = new System.Drawing.Point(0, 295);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(984, 50);
            this.panel2.TabIndex = 2;
            // 
            // buttonSaveResultSolutionsOfFunc
            // 
            this.buttonSaveResultSolutionsOfFunc.Enabled = false;
            this.buttonSaveResultSolutionsOfFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSaveResultSolutionsOfFunc.Location = new System.Drawing.Point(874, 11);
            this.buttonSaveResultSolutionsOfFunc.Name = "buttonSaveResultSolutionsOfFunc";
            this.buttonSaveResultSolutionsOfFunc.Size = new System.Drawing.Size(98, 23);
            this.buttonSaveResultSolutionsOfFunc.TabIndex = 3;
            this.buttonSaveResultSolutionsOfFunc.Text = "Занести";
            this.buttonSaveResultSolutionsOfFunc.UseVisualStyleBackColor = true;
            this.buttonSaveResultSolutionsOfFunc.Click += new System.EventHandler(this.buttonSaveResultSolutionsOfFunc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(528, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(343, 34);
            this.label3.TabIndex = 2;
            this.label3.Text = "Занести рішення функції в морфологічну таблицю,\r\nвага яких >=";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Розрахувати вагу рішень в межах обраної функції:";
            // 
            // buttonCalcSols
            // 
            this.buttonCalcSols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.buttonCalcSols.Location = new System.Drawing.Point(342, 11);
            this.buttonCalcSols.Name = "buttonCalcSols";
            this.buttonCalcSols.Size = new System.Drawing.Size(147, 30);
            this.buttonCalcSols.TabIndex = 0;
            this.buttonCalcSols.Text = "Розрахунок рішень";
            this.buttonCalcSols.UseVisualStyleBackColor = true;
            this.buttonCalcSols.Click += new System.EventHandler(this.buttonCalcSols_Click);
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
            this.dataGridView2.Location = new System.Drawing.Point(0, 347);
            this.dataGridView2.MinimumSize = new System.Drawing.Size(0, 210);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(984, 214);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView2_CellValidating);
            this.dataGridView2.Enter += new System.EventHandler(this.dataGridView2_Enter);
            this.dataGridView2.MouseEnter += new System.EventHandler(this.dataGridView2_Enter);
            this.dataGridView2.MouseLeave += new System.EventHandler(this.dataGridView2_MouseLeave);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(620, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(85, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "0";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(620, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(85, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "0";
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // TableFunctionsSolutions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "TableFunctionsSolutions";
            this.Text = "TableFunctionsSolutions";
            this.Load += new System.EventHandler(this.TableFunctionsSolutions_Load);
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
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button buttonCalcFuncs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCalcSols;
        private System.Windows.Forms.Button buttonSaveResultSolutionsOfFunc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSaveResultFuncs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}