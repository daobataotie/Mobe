namespace Book.UI.Settings.PeriodBeginning.ProductCost
{
    partial class ListForm
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
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductBarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductRetailPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCost0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCost1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
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
            this.gridControl1.DataSource = this.productBindingSource;
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId,
            this.colProductCategoryId,
            this.colProductName,
            this.colProductUnit,
            this.colProductBarCode,
            this.colProductSpecification,
            this.colProductPriceA,
            this.colProductPriceB,
            this.colProductPriceC,
            this.colProductRetailPrice,
            this.colProductCost0,
            this.colProductCost1,
            this.colProductDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId
            // 
            this.colProductId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId, "colProductId");
            this.colProductId.FieldName = "Id";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.AllowFocus = false;
            this.colProductId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductId.OptionsFilter.AllowFilter = false;
            // 
            // colProductCategoryId
            // 
            resources.ApplyResources(this.colProductCategoryId, "colProductCategoryId");
            this.colProductCategoryId.FieldName = "ProductCategory";
            this.colProductCategoryId.Name = "colProductCategoryId";
            this.colProductCategoryId.OptionsColumn.AllowEdit = false;
            this.colProductCategoryId.OptionsColumn.AllowFocus = false;
            this.colProductCategoryId.OptionsColumn.ReadOnly = true;
            // 
            // colProductName
            // 
            resources.ApplyResources(this.colProductName, "colProductName");
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.AllowFocus = false;
            this.colProductName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductName.OptionsFilter.AllowFilter = false;
            // 
            // colProductUnit
            // 
            this.colProductUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colProductUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductUnit, "colProductUnit");
            this.colProductUnit.FieldName = "MainUnit";
            this.colProductUnit.Name = "colProductUnit";
            this.colProductUnit.OptionsColumn.AllowEdit = false;
            this.colProductUnit.OptionsColumn.AllowFocus = false;
            this.colProductUnit.OptionsFilter.AllowFilter = false;
            // 
            // colProductBarCode
            // 
            resources.ApplyResources(this.colProductBarCode, "colProductBarCode");
            this.colProductBarCode.FieldName = "ProductBarCode";
            this.colProductBarCode.Name = "colProductBarCode";
            this.colProductBarCode.OptionsColumn.AllowEdit = false;
            this.colProductBarCode.OptionsColumn.AllowFocus = false;
            this.colProductBarCode.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductSpecification
            // 
            this.colProductSpecification.AppearanceCell.Options.UseTextOptions = true;
            this.colProductSpecification.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductSpecification.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductSpecification.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductSpecification, "colProductSpecification");
            this.colProductSpecification.FieldName = "ProductSpecification";
            this.colProductSpecification.Name = "colProductSpecification";
            this.colProductSpecification.OptionsColumn.AllowEdit = false;
            this.colProductSpecification.OptionsColumn.AllowFocus = false;
            this.colProductSpecification.OptionsFilter.AllowFilter = false;
            // 
            // colProductPriceA
            // 
            resources.ApplyResources(this.colProductPriceA, "colProductPriceA");
            this.colProductPriceA.FieldName = "ProductPriceA";
            this.colProductPriceA.Name = "colProductPriceA";
            this.colProductPriceA.OptionsColumn.AllowEdit = false;
            this.colProductPriceA.OptionsColumn.ReadOnly = true;
            // 
            // colProductPriceB
            // 
            resources.ApplyResources(this.colProductPriceB, "colProductPriceB");
            this.colProductPriceB.FieldName = "ProductPriceB";
            this.colProductPriceB.Name = "colProductPriceB";
            this.colProductPriceB.OptionsColumn.AllowEdit = false;
            this.colProductPriceB.OptionsColumn.ReadOnly = true;
            // 
            // colProductPriceC
            // 
            resources.ApplyResources(this.colProductPriceC, "colProductPriceC");
            this.colProductPriceC.FieldName = "ProductPriceC";
            this.colProductPriceC.Name = "colProductPriceC";
            this.colProductPriceC.OptionsColumn.AllowEdit = false;
            this.colProductPriceC.OptionsColumn.ReadOnly = true;
            // 
            // colProductRetailPrice
            // 
            resources.ApplyResources(this.colProductRetailPrice, "colProductRetailPrice");
            this.colProductRetailPrice.FieldName = "ProductRetailPrice";
            this.colProductRetailPrice.Name = "colProductRetailPrice";
            this.colProductRetailPrice.OptionsColumn.AllowEdit = false;
            this.colProductRetailPrice.OptionsColumn.ReadOnly = true;
            // 
            // colProductCost0
            // 
            this.colProductCost0.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCost0.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProductCost0.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCost0.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProductCost0, "colProductCost0");
            this.colProductCost0.DisplayFormat.FormatString = "0";
            this.colProductCost0.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colProductCost0.FieldName = "ProductBeginCost";
            this.colProductCost0.Name = "colProductCost0";
            this.colProductCost0.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductCost1
            // 
            resources.ApplyResources(this.colProductCost1, "colProductCost1");
            this.colProductCost1.FieldName = "ProductStandardCost";
            this.colProductCost1.Name = "colProductCost1";
            this.colProductCost1.OptionsColumn.AllowEdit = false;
            this.colProductCost1.OptionsColumn.ReadOnly = true;
            // 
            // colProductDescription
            // 
            resources.ApplyResources(this.colProductDescription, "colProductDescription");
            this.colProductDescription.FieldName = "ProductDescription";
            this.colProductDescription.Name = "colProductDescription";
            this.colProductDescription.OptionsColumn.AllowEdit = false;
            this.colProductDescription.OptionsColumn.ReadOnly = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource productBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colProductBarCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceA;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceB;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceC;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRetailPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCost0;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCost1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}