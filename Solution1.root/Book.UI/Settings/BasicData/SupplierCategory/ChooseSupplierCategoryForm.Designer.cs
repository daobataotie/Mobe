namespace Book.UI.Settings.BasicData.SupplierCategory
{
    partial class ChooseSupplierCategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseSupplierCategoryForm));
            this.gridColumnSupplierCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSupplierCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSupplierCategoryDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSupplierCategoryId,
            this.gridColumnSupplierCategoryName,
            this.gridColumnSupplierCategoryDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnSupplierCategoryId
            // 
            resources.ApplyResources(this.gridColumnSupplierCategoryId, "gridColumnSupplierCategoryId");
            this.gridColumnSupplierCategoryId.FieldName = "Id";
            this.gridColumnSupplierCategoryId.Name = "gridColumnSupplierCategoryId";
            // 
            // gridColumnSupplierCategoryName
            // 
            resources.ApplyResources(this.gridColumnSupplierCategoryName, "gridColumnSupplierCategoryName");
            this.gridColumnSupplierCategoryName.FieldName = "SupplierCategoryName";
            this.gridColumnSupplierCategoryName.Name = "gridColumnSupplierCategoryName";
            // 
            // gridColumnSupplierCategoryDescription
            // 
            resources.ApplyResources(this.gridColumnSupplierCategoryDescription, "gridColumnSupplierCategoryDescription");
            this.gridColumnSupplierCategoryDescription.FieldName = "SupplierCategoryDescription";
            this.gridColumnSupplierCategoryDescription.Name = "gridColumnSupplierCategoryDescription";
            // 
            // ChooseSupplierCategoryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChooseSupplierCategoryForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSupplierCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSupplierCategoryName;
                                                       
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSupplierCategoryDescription;
    }
}