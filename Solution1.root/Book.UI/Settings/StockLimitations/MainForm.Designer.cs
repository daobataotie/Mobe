namespace Book.UI.Settings.StockLimitations
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDepotName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProductUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProductSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnProductModel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnStockQuantityU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.gridColumnStockQuantityD = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.gridColumnProductDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
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
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemSave});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemSave)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemSave
            // 
            resources.ApplyResources(this.barButtonItemSave, "barButtonItemSave");
            this.barButtonItemSave.Id = 0;
            this.barButtonItemSave.Name = "barButtonItemSave";
            this.barButtonItemSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1,
            this.repositoryItemCalcEdit2,
            this.repositoryItemRichTextEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDepotName,
            this.gridColumnProductName,
            this.gridColumnProductUnit,
            this.gridColumnProductSpecification,
            this.gridColumnProductModel,
            this.gridColumnStockQuantityU,
            this.gridColumnStockQuantityD,
            this.gridColumnProductDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            // 
            // gridColumnDepotName
            // 
            this.gridColumnDepotName.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDepotName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnDepotName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDepotName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.gridColumnDepotName, "gridColumnDepotName");
            this.gridColumnDepotName.FieldName = "DepotName";
            this.gridColumnDepotName.Name = "gridColumnDepotName";
            this.gridColumnDepotName.OptionsColumn.AllowEdit = false;
            this.gridColumnDepotName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnDepotName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnDepotName.OptionsColumn.ReadOnly = true;
            this.gridColumnDepotName.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnProductName
            // 
            this.gridColumnProductName.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumnProductName, "gridColumnProductName");
            this.gridColumnProductName.FieldName = "ProductName";
            this.gridColumnProductName.Name = "gridColumnProductName";
            this.gridColumnProductName.OptionsColumn.AllowEdit = false;
            this.gridColumnProductName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnProductName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnProductName.OptionsColumn.ReadOnly = true;
            this.gridColumnProductName.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnProductUnit
            // 
            this.gridColumnProductUnit.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnProductUnit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnProductUnit.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnProductUnit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumnProductUnit, "gridColumnProductUnit");
            this.gridColumnProductUnit.FieldName = "CnName";
            this.gridColumnProductUnit.Name = "gridColumnProductUnit";
            this.gridColumnProductUnit.OptionsColumn.AllowEdit = false;
            this.gridColumnProductUnit.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnProductUnit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnProductUnit.OptionsColumn.ReadOnly = true;
            this.gridColumnProductUnit.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnProductSpecification
            // 
            this.gridColumnProductSpecification.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnProductSpecification.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnProductSpecification.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnProductSpecification.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumnProductSpecification, "gridColumnProductSpecification");
            this.gridColumnProductSpecification.FieldName = "ProductSpecification";
            this.gridColumnProductSpecification.Name = "gridColumnProductSpecification";
            this.gridColumnProductSpecification.OptionsColumn.AllowEdit = false;
            this.gridColumnProductSpecification.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnProductSpecification.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnProductSpecification.OptionsColumn.ReadOnly = true;
            this.gridColumnProductSpecification.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnProductModel
            // 
            this.gridColumnProductModel.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnProductModel.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnProductModel.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnProductModel.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumnProductModel, "gridColumnProductModel");
            this.gridColumnProductModel.FieldName = "ProductModel";
            this.gridColumnProductModel.Name = "gridColumnProductModel";
            this.gridColumnProductModel.OptionsColumn.AllowEdit = false;
            this.gridColumnProductModel.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnProductModel.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnProductModel.OptionsColumn.ReadOnly = true;
            this.gridColumnProductModel.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnStockQuantityU
            // 
            this.gridColumnStockQuantityU.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnStockQuantityU.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumnStockQuantityU.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnStockQuantityU.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.gridColumnStockQuantityU, "gridColumnStockQuantityU");
            this.gridColumnStockQuantityU.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumnStockQuantityU.FieldName = "StockQuantityU";
            this.gridColumnStockQuantityU.Name = "gridColumnStockQuantityU";
            this.gridColumnStockQuantityU.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnStockQuantityU.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnStockQuantityU.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemCalcEdit1
            // 
            resources.ApplyResources(this.repositoryItemCalcEdit1, "repositoryItemCalcEdit1");
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCalcEdit1.Buttons"))))});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // gridColumnStockQuantityD
            // 
            this.gridColumnStockQuantityD.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnStockQuantityD.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumnStockQuantityD.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnStockQuantityD.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.gridColumnStockQuantityD, "gridColumnStockQuantityD");
            this.gridColumnStockQuantityD.ColumnEdit = this.repositoryItemCalcEdit2;
            this.gridColumnStockQuantityD.FieldName = "StockQuantityD";
            this.gridColumnStockQuantityD.Name = "gridColumnStockQuantityD";
            this.gridColumnStockQuantityD.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnStockQuantityD.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnStockQuantityD.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemCalcEdit2
            // 
            resources.ApplyResources(this.repositoryItemCalcEdit2, "repositoryItemCalcEdit2");
            this.repositoryItemCalcEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemCalcEdit2.Buttons"))))});
            this.repositoryItemCalcEdit2.Name = "repositoryItemCalcEdit2";
            // 
            // gridColumnProductDescription
            // 
            resources.ApplyResources(this.gridColumnProductDescription, "gridColumnProductDescription");
            this.gridColumnProductDescription.ColumnEdit = this.repositoryItemRichTextEdit1;
            this.gridColumnProductDescription.FieldName = "ProductDescription";
            this.gridColumnProductDescription.Name = "gridColumnProductDescription";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSave;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProductName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProductUnit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProductSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProductModel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStockQuantityU;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStockQuantityD;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnProductDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
    }
}