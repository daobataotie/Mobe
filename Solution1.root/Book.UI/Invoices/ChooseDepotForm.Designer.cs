namespace Book.UI.Invoices
{
    partial class ChooseDepotForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseDepotForm));
            this.colDepotId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepotName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDepotDescription = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colDepotId,
            this.colDepotName,
            this.colDepotDescription});
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
            // colDepotId
            // 
            this.colDepotId.AppearanceCell.Options.UseTextOptions = true;
            this.colDepotId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDepotId.AppearanceHeader.Options.UseTextOptions = true;
            this.colDepotId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colDepotId, "colDepotId");
            this.colDepotId.FieldName = "Id";
            this.colDepotId.Name = "colDepotId";
            this.colDepotId.OptionsColumn.AllowEdit = false;
            // 
            // colDepotName
            // 
            this.colDepotName.AppearanceCell.Options.UseTextOptions = true;
            this.colDepotName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDepotName.AppearanceHeader.Options.UseTextOptions = true;
            this.colDepotName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colDepotName, "colDepotName");
            this.colDepotName.FieldName = "DepotName";
            this.colDepotName.Name = "colDepotName";
            this.colDepotName.OptionsColumn.AllowEdit = false;
            // 
            // colDepotDescription
            // 
            this.colDepotDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colDepotDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDepotDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDepotDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colDepotDescription, "colDepotDescription");
            this.colDepotDescription.FieldName = "DepotDescription";
            this.colDepotDescription.Name = "colDepotDescription";
            this.colDepotDescription.OptionsColumn.AllowEdit = false;
            // 
            // ChooseDepotForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ChooseDepotForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colDepotId;
        private DevExpress.XtraGrid.Columns.GridColumn colDepotName;
        private DevExpress.XtraGrid.Columns.GridColumn colDepotDescription;
    }
}