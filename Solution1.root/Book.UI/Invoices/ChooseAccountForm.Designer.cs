namespace Book.UI.Invoices
{
    partial class ChooseAccountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseAccountForm));
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colAccountBalance0,
            this.colAccountBalance1,
            this.colAccountDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
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
            // colAccountId
            // 
            this.colAccountId.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountId.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountId, "colAccountId");
            this.colAccountId.FieldName = "Id";
            this.colAccountId.Name = "colAccountId";
            this.colAccountId.OptionsColumn.AllowEdit = false;
            // 
            // colAccountName
            // 
            this.colAccountName.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountName, "colAccountName");
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.OptionsColumn.AllowEdit = false;
            // 
            // colAccountBalance0
            // 
            this.colAccountBalance0.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountBalance0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountBalance0.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountBalance0, "colAccountBalance0");
            this.colAccountBalance0.FieldName = "AccountBalance0";
            this.colAccountBalance0.Name = "colAccountBalance0";
            this.colAccountBalance0.OptionsColumn.AllowEdit = false;
            // 
            // colAccountBalance1
            // 
            this.colAccountBalance1.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountBalance1.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountBalance1, "colAccountBalance1");
            this.colAccountBalance1.FieldName = "AccountBalance1";
            this.colAccountBalance1.Name = "colAccountBalance1";
            this.colAccountBalance1.OptionsColumn.AllowEdit = false;
            // 
            // colAccountDescription
            // 
            this.colAccountDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountDescription, "colAccountDescription");
            this.colAccountDescription.FieldName = "AccountDescription";
            this.colAccountDescription.Name = "colAccountDescription";
            this.colAccountDescription.OptionsColumn.AllowEdit = false;
            // 
            // ChooseAccountForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseAccountForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance0;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance1;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDescription;
    }
}