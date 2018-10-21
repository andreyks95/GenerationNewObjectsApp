namespace MorphAnalysis.HelperForms
{
    partial class ChartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartForm));
            this.winChartViewerResult = new ChartDirector.WinChartViewer();
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewerResult)).BeginInit();
            this.SuspendLayout();
            // 
            // winChartViewerResult
            // 
            this.winChartViewerResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winChartViewerResult.Location = new System.Drawing.Point(0, 0);
            this.winChartViewerResult.MinimumSize = new System.Drawing.Size(750, 550);
            this.winChartViewerResult.Name = "winChartViewerResult";
            this.winChartViewerResult.Size = new System.Drawing.Size(784, 561);
            this.winChartViewerResult.TabIndex = 0;
            this.winChartViewerResult.TabStop = false;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.winChartViewerResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 800);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ChartForm";
            this.Text = "Графік роботи генетичного алгоритму";
            this.Load += new System.EventHandler(this.ChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.winChartViewerResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ChartDirector.WinChartViewer winChartViewerResult;
    }
}