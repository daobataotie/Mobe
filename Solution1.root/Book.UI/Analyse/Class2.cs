using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;

namespace Book.UI.Analyse
{
    class Class2:DateEdit
    {
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit fProperties;

        private void InitializeComponent()
        {
            this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties.VistaTimeProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fProperties.DisplayFormat.FormatString = "m";
            this.fProperties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fProperties.Name = "fProperties";
            this.fProperties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // Class2
            // 
            this.Size = new System.Drawing.Size(100, 21);
            ((System.ComponentModel.ISupportInitialize)(this.fProperties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
