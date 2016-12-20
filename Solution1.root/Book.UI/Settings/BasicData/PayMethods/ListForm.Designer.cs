namespace Book.UI.Settings.BasicData.PayMethods
{
    partial class ListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.colPayMethodId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayMethodName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayMethodDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayMethodId,
            this.colPayMethodName,
            this.colPayMethodDescription});
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colPayMethodId
            // 
            this.colPayMethodId.AppearanceCell.Options.UseTextOptions = true;
            this.colPayMethodId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colPayMethodId.AppearanceHeader.Options.UseTextOptions = true;
            this.colPayMethodId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colPayMethodId, "colPayMethodId");
            this.colPayMethodId.FieldName = "Id";
            this.colPayMethodId.Name = "colPayMethodId";
            this.colPayMethodId.OptionsColumn.AllowEdit = false;
            this.colPayMethodId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayMethodId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colPayMethodId.OptionsFilter.AllowFilter = false;
            // 
            // colPayMethodName
            // 
            this.colPayMethodName.AppearanceCell.Options.UseTextOptions = true;
            this.colPayMethodName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPayMethodName.AppearanceHeader.Options.UseTextOptions = true;
            this.colPayMethodName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colPayMethodName, "colPayMethodName");
            this.colPayMethodName.FieldName = "PayMethodName";
            this.colPayMethodName.Name = "colPayMethodName";
            this.colPayMethodName.OptionsColumn.AllowEdit = false;
            this.colPayMethodName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayMethodName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            // 
            // colPayMethodDescription
            // 
            this.colPayMethodDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colPayMethodDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colPayMethodDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colPayMethodDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colPayMethodDescription, "colPayMethodDescription");
            this.colPayMethodDescription.FieldName = "PayMethodDescription";
            this.colPayMethodDescription.Name = "colPayMethodDescription";
            this.colPayMethodDescription.OptionsColumn.AllowEdit = false;
            this.colPayMethodDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colPayMethodDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colPayMethodDescription.OptionsFilter.AllowFilter = false;
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ListForm";
            this.ShowInTaskbar = false;

            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colPayMethodId;
        private DevExpress.XtraGrid.Columns.GridColumn colPayMethodName;
        private DevExpress.XtraGrid.Columns.GridColumn colPayMethodDescription;
    }
}