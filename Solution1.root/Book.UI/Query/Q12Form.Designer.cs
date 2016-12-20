namespace Book.UI.Query
{
    partial class Q12Form
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Q12Form));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.accountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.accountBindingSource;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colAccountBalance0,
            this.colAccountBalance1,
            this.colAccountDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colAccountId
            // 
            this.colAccountId.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colAccountId.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colAccountId, "colAccountId");
            this.colAccountId.FieldName = "AccountId";
            this.colAccountId.Name = "colAccountId";
            this.colAccountId.OptionsColumn.AllowEdit = false;
            this.colAccountId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountId.OptionsColumn.ReadOnly = true;
            this.colAccountId.OptionsFilter.AllowFilter = false;
            // 
            // colAccountName
            // 
            this.colAccountName.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAccountName.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colAccountName, "colAccountName");
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.OptionsColumn.AllowEdit = false;
            this.colAccountName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountName.OptionsColumn.ReadOnly = true;
            this.colAccountName.OptionsFilter.AllowFilter = false;
            // 
            // colAccountBalance0
            // 
            this.colAccountBalance0.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountBalance0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAccountBalance0.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAccountBalance0, "colAccountBalance0");
            this.colAccountBalance0.FieldName = "AccountBalance0";
            this.colAccountBalance0.Name = "colAccountBalance0";
            this.colAccountBalance0.OptionsColumn.AllowEdit = false;
            this.colAccountBalance0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountBalance0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountBalance0.OptionsColumn.ReadOnly = true;
            this.colAccountBalance0.OptionsFilter.AllowFilter = false;
            // 
            // colAccountBalance1
            // 
            this.colAccountBalance1.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAccountBalance1.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAccountBalance1, "colAccountBalance1");
            this.colAccountBalance1.DisplayFormat.FormatString = "0";
            this.colAccountBalance1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAccountBalance1.FieldName = "AccountBalance1";
            this.colAccountBalance1.Name = "colAccountBalance1";
            this.colAccountBalance1.OptionsColumn.AllowEdit = false;
            this.colAccountBalance1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountBalance1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountBalance1.OptionsColumn.ReadOnly = true;
            this.colAccountBalance1.OptionsFilter.AllowFilter = false;
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
            this.colAccountDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAccountDescription.OptionsColumn.ReadOnly = true;
            this.colAccountDescription.OptionsFilter.AllowFilter = false;
            // 
            // Q12Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "Q12Form";
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource accountBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance0;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance1;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDescription;
    }
}