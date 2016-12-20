namespace Book.UI.Tools
{
    partial class ReportViewer
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
            this.printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
            this.printingSystem1 = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).BeginInit();
            this.SuspendLayout();
            // 
            // printControl1
            // 
            this.printControl1.ForeColor = System.Drawing.Color.White;
            this.printControl1.IsMetric = true;
            this.printControl1.Location = new System.Drawing.Point(79, 24);
            this.printControl1.Name = "printControl1";
            this.printControl1.PrintingSystem = this.printingSystem1;
            this.printControl1.Size = new System.Drawing.Size(488, 344);
            this.printControl1.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 380);
            this.Controls.Add(this.printControl1);
            this.Name = "ReportViewer";
            this.Text = "ReportViewer";
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.Control.PrintControl printControl1;
        private DevExpress.XtraPrinting.PrintingSystem printingSystem1;
    }
}