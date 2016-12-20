namespace Book.UI.Invoices
{
    partial class ChooseDutyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseDutyForm));
            this.gridColumnDutyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDutyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDutyNote = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDutyId,
            this.gridColumnDutyName,
            this.gridColumnDutyNote});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButtonCancel
            // 
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            // 
            // simpleButtonOK
            // 
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            // 
            // simpleButtonNew
            // 
            resources.ApplyResources(this.simpleButtonNew, "simpleButtonNew");
            // 
            // gridColumnDutyId
            // 
            resources.ApplyResources(this.gridColumnDutyId, "gridColumnDutyId");
            this.gridColumnDutyId.FieldName = "Id";
            this.gridColumnDutyId.Name = "gridColumnDutyId";
            // 
            // gridColumnDutyName
            // 
            resources.ApplyResources(this.gridColumnDutyName, "gridColumnDutyName");
            this.gridColumnDutyName.FieldName = "DutyName";
            this.gridColumnDutyName.Name = "gridColumnDutyName";
            // 
            // gridColumnDutyNote
            // 
            resources.ApplyResources(this.gridColumnDutyNote, "gridColumnDutyNote");
            this.gridColumnDutyNote.FieldName = "DutyNote";
            this.gridColumnDutyNote.Name = "gridColumnDutyNote";
            // 
            // ChooseDutyForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChooseDutyForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDutyId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDutyName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDutyNote;
    }
}