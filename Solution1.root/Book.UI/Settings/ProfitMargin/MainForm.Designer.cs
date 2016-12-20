namespace Book.UI.Settings.ProfitMargin
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.companyLevelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCompanyLevelName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyLevelProfitMargin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductBarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductRetailPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCost0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCost1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompanyLevelId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProfitMargin = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyLevelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            resources.ApplyResources(this.xtraTabControl1, "xtraTabControl1");
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            resources.ApplyResources(this.xtraTabPage1, "xtraTabPage1");
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.companyLevelBindingSource;
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompanyLevelName,
            this.colCompanyLevelProfitMargin});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colCompanyLevelName
            // 
            this.colCompanyLevelName.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyLevelName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyLevelName.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyLevelName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyLevelName, "colCompanyLevelName");
            this.colCompanyLevelName.FieldName = "CompanyLevelName";
            this.colCompanyLevelName.Name = "colCompanyLevelName";
            this.colCompanyLevelName.OptionsColumn.AllowEdit = false;
            this.colCompanyLevelName.OptionsColumn.AllowFocus = false;
            this.colCompanyLevelName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyLevelName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyLevelName.OptionsColumn.ReadOnly = true;
            this.colCompanyLevelName.OptionsFilter.AllowAutoFilter = false;
            this.colCompanyLevelName.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyLevelProfitMargin
            // 
            this.colCompanyLevelProfitMargin.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyLevelProfitMargin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCompanyLevelProfitMargin.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyLevelProfitMargin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colCompanyLevelProfitMargin, "colCompanyLevelProfitMargin");
            this.colCompanyLevelProfitMargin.FieldName = "CompanyLevelProfitMargin";
            this.colCompanyLevelProfitMargin.Name = "colCompanyLevelProfitMargin";
            this.colCompanyLevelProfitMargin.OptionsFilter.AllowFilter = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridControl2);
            this.xtraTabPage2.Controls.Add(this.gridControl3);
            this.xtraTabPage2.Controls.Add(this.simpleButton1);
            this.xtraTabPage2.Controls.Add(this.simpleButton2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.PageVisible = false;
            resources.ApplyResources(this.xtraTabPage2, "xtraTabPage2");
            // 
            // gridControl2
            // 
            this.gridControl2.DataSource = this.productBindingSource;
            this.gridControl2.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl2, "gridControl2");
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // productBindingSource
            // 
            this.productBindingSource.CurrentChanged += new System.EventHandler(this.productBindingSource_CurrentChanged);
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId,
            this.colProductName,
            this.colProductCategoryId,
            this.colProductUnit,
            this.colProductBarCode,
            this.colProductSpecification,
            this.colProductModel,
            this.colProductDescription,
            this.colProductPriceA,
            this.colProductPriceB,
            this.colProductPriceC,
            this.colProductRetailPrice,
            this.colProductCost0,
            this.colProductCost1});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId
            // 
            this.colProductId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId, "colProductId");
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.ReadOnly = true;
            this.colProductId.OptionsFilter.AllowFilter = false;
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductName, "colProductName");
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.OptionsFilter.AllowFilter = false;
            // 
            // colProductCategoryId
            // 
            this.colProductCategoryId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCategoryId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCategoryId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCategoryId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductCategoryId, "colProductCategoryId");
            this.colProductCategoryId.FieldName = "ProductCategory";
            this.colProductCategoryId.Name = "colProductCategoryId";
            this.colProductCategoryId.OptionsColumn.AllowEdit = false;
            this.colProductCategoryId.OptionsColumn.ReadOnly = true;
            this.colProductCategoryId.OptionsFilter.AllowFilter = false;
            // 
            // colProductUnit
            // 
            this.colProductUnit.AppearanceCell.Options.UseTextOptions = true;
            this.colProductUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductUnit, "colProductUnit");
            this.colProductUnit.FieldName = "ProductBaseUnit";
            this.colProductUnit.Name = "colProductUnit";
            this.colProductUnit.OptionsColumn.AllowEdit = false;
            this.colProductUnit.OptionsColumn.ReadOnly = true;
            this.colProductUnit.OptionsFilter.AllowFilter = false;
            // 
            // colProductBarCode
            // 
            this.colProductBarCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductBarCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductBarCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductBarCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductBarCode, "colProductBarCode");
            this.colProductBarCode.FieldName = "ProductBarCode";
            this.colProductBarCode.Name = "colProductBarCode";
            this.colProductBarCode.OptionsColumn.AllowEdit = false;
            this.colProductBarCode.OptionsColumn.ReadOnly = true;
            this.colProductBarCode.OptionsFilter.AllowFilter = false;
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
            this.colProductSpecification.OptionsColumn.ReadOnly = true;
            this.colProductSpecification.OptionsFilter.AllowFilter = false;
            // 
            // colProductModel
            // 
            this.colProductModel.AppearanceCell.Options.UseTextOptions = true;
            this.colProductModel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductModel.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductModel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductModel, "colProductModel");
            this.colProductModel.FieldName = "ProductModel";
            this.colProductModel.Name = "colProductModel";
            this.colProductModel.OptionsColumn.AllowEdit = false;
            this.colProductModel.OptionsColumn.ReadOnly = true;
            this.colProductModel.OptionsFilter.AllowFilter = false;
            // 
            // colProductDescription
            // 
            this.colProductDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colProductDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductDescription, "colProductDescription");
            this.colProductDescription.FieldName = "ProductDescription";
            this.colProductDescription.Name = "colProductDescription";
            this.colProductDescription.OptionsColumn.AllowEdit = false;
            this.colProductDescription.OptionsColumn.ReadOnly = true;
            this.colProductDescription.OptionsFilter.AllowFilter = false;
            // 
            // colProductPriceA
            // 
            resources.ApplyResources(this.colProductPriceA, "colProductPriceA");
            this.colProductPriceA.FieldName = "ProductPriceA";
            this.colProductPriceA.Name = "colProductPriceA";
            // 
            // colProductPriceB
            // 
            resources.ApplyResources(this.colProductPriceB, "colProductPriceB");
            this.colProductPriceB.FieldName = "ProductPriceB";
            this.colProductPriceB.Name = "colProductPriceB";
            // 
            // colProductPriceC
            // 
            resources.ApplyResources(this.colProductPriceC, "colProductPriceC");
            this.colProductPriceC.FieldName = "ProductPriceC";
            this.colProductPriceC.Name = "colProductPriceC";
            // 
            // colProductRetailPrice
            // 
            resources.ApplyResources(this.colProductRetailPrice, "colProductRetailPrice");
            this.colProductRetailPrice.FieldName = "ProductRetailPrice";
            this.colProductRetailPrice.Name = "colProductRetailPrice";
            // 
            // colProductCost0
            // 
            resources.ApplyResources(this.colProductCost0, "colProductCost0");
            this.colProductCost0.FieldName = "ProductCost0";
            this.colProductCost0.Name = "colProductCost0";
            // 
            // colProductCost1
            // 
            resources.ApplyResources(this.colProductCost1, "colProductCost1");
            this.colProductCost1.FieldName = "ProductStandardCost";
            this.colProductCost1.Name = "colProductCost1";
            // 
            // gridControl3
            // 
            this.gridControl3.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl3, "gridControl3");
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId1,
            this.colCompanyLevelId1,
            this.colProfitMargin});
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId1
            // 
            this.colProductId1.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId1, "colProductId1");
            this.colProductId1.FieldName = "ProductName";
            this.colProductId1.Name = "colProductId1";
            this.colProductId1.OptionsColumn.AllowEdit = false;
            this.colProductId1.OptionsColumn.ReadOnly = true;
            this.colProductId1.OptionsFilter.AllowFilter = false;
            // 
            // colCompanyLevelId1
            // 
            this.colCompanyLevelId1.AppearanceCell.Options.UseTextOptions = true;
            this.colCompanyLevelId1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCompanyLevelId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCompanyLevelId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colCompanyLevelId1, "colCompanyLevelId1");
            this.colCompanyLevelId1.FieldName = "CompanyLevelName";
            this.colCompanyLevelId1.Name = "colCompanyLevelId1";
            this.colCompanyLevelId1.OptionsColumn.AllowEdit = false;
            this.colCompanyLevelId1.OptionsColumn.AllowFocus = false;
            this.colCompanyLevelId1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colCompanyLevelId1.OptionsColumn.ReadOnly = true;
            this.colCompanyLevelId1.OptionsFilter.AllowFilter = false;
            // 
            // colProfitMargin
            // 
            this.colProfitMargin.AppearanceCell.Options.UseTextOptions = true;
            this.colProfitMargin.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProfitMargin.AppearanceHeader.Options.UseTextOptions = true;
            this.colProfitMargin.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProfitMargin, "colProfitMargin");
            this.colProfitMargin.FieldName = "SpecialProfitMarginValue";
            this.colProfitMargin.Name = "colProfitMargin";
            this.colProfitMargin.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProfitMargin.OptionsFilter.AllowFilter = false;
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            resources.ApplyResources(this.simpleButton2, "simpleButton2");
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
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
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 4;
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItem1
            // 
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyLevelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl3;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.BindingSource companyLevelBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyLevelName;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyLevelProfitMargin;
        private System.Windows.Forms.BindingSource productBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colProductBarCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn colProductModel;
        private DevExpress.XtraGrid.Columns.GridColumn colProductDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceA;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceB;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceC;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRetailPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCost0;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCost1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId1;
        private DevExpress.XtraGrid.Columns.GridColumn colCompanyLevelId1;
        private DevExpress.XtraGrid.Columns.GridColumn colProfitMargin;
    }
}