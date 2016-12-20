namespace Book.UI.Settings.BasicData.AreaCategory
{
    partial class ChooseAreaCategoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseAreaCategoryForm));
            this.gridColumnAreaCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAreaCategoryName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridColumnAreaCategoryId,
            this.gridColumnAreaCategoryName,
            this.gridColumnDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnAreaCategoryId
            // 
            resources.ApplyResources(this.gridColumnAreaCategoryId, "gridColumnAreaCategoryId");
            this.gridColumnAreaCategoryId.FieldName = "Id";
            this.gridColumnAreaCategoryId.Name = "gridColumnAreaCategoryId";
            // 
            // gridColumnAreaCategoryName
            // 
            resources.ApplyResources(this.gridColumnAreaCategoryName, "gridColumnAreaCategoryName");
            this.gridColumnAreaCategoryName.FieldName = "AreaCategoryName";
            this.gridColumnAreaCategoryName.Name = "gridColumnAreaCategoryName";
            // 
            // gridColumnDescription
            // 
            resources.ApplyResources(this.gridColumnDescription, "gridColumnDescription");
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            // 
            // ChooseAreaCategoryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChooseAreaCategoryForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAreaCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAreaCategoryName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
    }
}