namespace Book.UI.Query
{
    partial class Q03Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Q03Form));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCompanyId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyName0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyR1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyP1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyR0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyP0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.companyBindingSource;
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
            this.colCompanyId,
            this.colCompanyName1,
            this.colCompanyName0,
            this.colCompanyR1,
            this.colCompanyP1,
            this.colCompanyR0,
            this.colCompanyP0,
            this.colCompanyDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colCompanyId
            // 
            this.colCompanyId.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colCompanyId.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colCompanyId, "colCompanyId");
            this.colCompanyId.FieldName = "CompanyId";
            this.colCompanyId.Name = "colCompanyId";
            this.colCompanyId.OptionsColumn.AllowEdit = false;
            this.colCompanyId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyId.OptionsColumn.ReadOnly = true;
            this.colCompanyId.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyName1
            // 
            this.colCompanyName1.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyName1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyName1, "colCompanyName1");
            this.colCompanyName1.FieldName = "CompanyName1";
            this.colCompanyName1.Name = "colCompanyName1";
            this.colCompanyName1.OptionsColumn.AllowEdit = false;
            this.colCompanyName1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyName1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyName1.OptionsColumn.ReadOnly = true;
            this.colCompanyName1.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyName0
            // 
            this.colCompanyName0.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyName0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyName0.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyName0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyName0, "colCompanyName0");
            this.colCompanyName0.FieldName = "CompanyName0";
            this.colCompanyName0.Name = "colCompanyName0";
            this.colCompanyName0.OptionsColumn.AllowEdit = false;
            this.colCompanyName0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyName0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyName0.OptionsColumn.ReadOnly = true;
            this.colCompanyName0.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyR1
            // 
            this.colCompanyR1.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyR1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCompanyR1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyR1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colCompanyR1, "colCompanyR1");
            this.colCompanyR1.DisplayFormat.FormatString = "0";
            this.colCompanyR1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCompanyR1.FieldName = "CompanyR1";
            this.colCompanyR1.Name = "colCompanyR1";
            this.colCompanyR1.OptionsColumn.AllowEdit = false;
            this.colCompanyR1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyR1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyR1.OptionsColumn.ReadOnly = true;
            this.colCompanyR1.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyP1
            // 
            this.colCompanyP1.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyP1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCompanyP1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyP1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colCompanyP1, "colCompanyP1");
            this.colCompanyP1.DisplayFormat.FormatString = "0";
            this.colCompanyP1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCompanyP1.FieldName = "CompanyP1";
            this.colCompanyP1.Name = "colCompanyP1";
            this.colCompanyP1.OptionsColumn.AllowEdit = false;
            this.colCompanyP1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyP1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colCompanyP1.OptionsColumn.ReadOnly = true;
            this.colCompanyP1.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyR0
            // 
            resources.ApplyResources(this.colCompanyR0, "colCompanyR0");
            this.colCompanyR0.FieldName = "CompanyR0";
            this.colCompanyR0.Name = "colCompanyR0";
            this.colCompanyR0.OptionsColumn.AllowEdit = false;
            this.colCompanyR0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyR0.OptionsColumn.ReadOnly = true;
            // 
            // colCompanyP0
            // 
            resources.ApplyResources(this.colCompanyP0, "colCompanyP0");
            this.colCompanyP0.FieldName = "CompanyP0";
            this.colCompanyP0.Name = "colCompanyP0";
            this.colCompanyP0.OptionsColumn.AllowEdit = false;
            this.colCompanyP0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyP0.OptionsColumn.ReadOnly = true;
            // 
            // colCompanyDescription
            // 
            resources.ApplyResources(this.colCompanyDescription, "colCompanyDescription");
            this.colCompanyDescription.FieldName = "CompanyDescription";
            this.colCompanyDescription.Name = "colCompanyDescription";
            this.colCompanyDescription.OptionsColumn.AllowEdit = false;
            this.colCompanyDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyDescription.OptionsColumn.ReadOnly = true;
            // 
            // Q03Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Name = "Q03Form";
            this.ShowInTaskbar = false;
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyId;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName1;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyName0;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyR0;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyR1;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyP0;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyP1;
    }
}