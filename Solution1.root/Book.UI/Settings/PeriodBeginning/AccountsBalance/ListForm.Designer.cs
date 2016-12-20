namespace Book.UI.Settings.PeriodBeginning.AccountsBalance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.accountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountBalance1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemSave});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 6;
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItemSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemSave
            // 
            resources.ApplyResources(this.barButtonItemSave, "barButtonItemSave");
            this.barButtonItemSave.Id = 0;
            this.barButtonItemSave.ImageIndex = 0;
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
            // 
            // imageCollection1
            // 
            resources.ApplyResources(this.imageCollection1, "imageCollection1");
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.accountBindingSource;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountId,
            this.colAccountName,
            this.colAccountBalance0,
            this.colAccountBalance1,
            this.colAccountDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colAccountId
            // 
            resources.ApplyResources(this.colAccountId, "colAccountId");
            this.colAccountId.FieldName = "Id";
            this.colAccountId.Name = "colAccountId";
            this.colAccountId.OptionsColumn.AllowFocus = false;
            // 
            // colAccountName
            // 
            resources.ApplyResources(this.colAccountName, "colAccountName");
            this.colAccountName.FieldName = "AccountName";
            this.colAccountName.Name = "colAccountName";
            this.colAccountName.OptionsColumn.AllowEdit = false;
            this.colAccountName.OptionsColumn.AllowFocus = false;
            this.colAccountName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountName.OptionsFilter.AllowFilter = false;
            // 
            // colAccountBalance0
            // 
            this.colAccountBalance0.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAccountBalance0, "colAccountBalance0");
            this.colAccountBalance0.DisplayFormat.FormatString = "0";
            this.colAccountBalance0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAccountBalance0.FieldName = "AccountBalance0";
            this.colAccountBalance0.Name = "colAccountBalance0";
            this.colAccountBalance0.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountBalance0.OptionsFilter.AllowFilter = false;
            // 
            // colAccountBalance1
            // 
            this.colAccountBalance1.AppearanceCell.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colAccountBalance1.AppearanceHeader.Options.UseTextOptions = true;
            this.colAccountBalance1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colAccountBalance1, "colAccountBalance1");
            this.colAccountBalance1.DisplayFormat.FormatString = "c";
            this.colAccountBalance1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colAccountBalance1.FieldName = "AccountBalance1";
            this.colAccountBalance1.Name = "colAccountBalance1";
            this.colAccountBalance1.OptionsColumn.AllowEdit = false;
            this.colAccountBalance1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountBalance1.OptionsFilter.AllowFilter = false;
            // 
            // colAccountDescription
            // 
            resources.ApplyResources(this.colAccountDescription, "colAccountDescription");
            this.colAccountDescription.FieldName = "AccountDescription";
            this.colAccountDescription.Name = "colAccountDescription";
            this.colAccountDescription.OptionsColumn.AllowEdit = false;
            this.colAccountDescription.OptionsColumn.AllowFocus = false;
            this.colAccountDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colAccountDescription.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemCalcEdit1
            // 
            resources.ApplyResources(this.repositoryItemCalcEdit1, "repositoryItemCalcEdit1");
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCalcEdit1.Buttons"))))});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ListForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource accountBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance0;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountBalance1;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}