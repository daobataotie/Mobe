namespace Book.UI.Settings.BasicData.Products
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
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCategoryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductBarCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductSpecification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductPriceC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductRetailPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.bindingSourceProductCate = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProductCate)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            // 
            // gridControl1
            // 
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId,
            this.colProductName,
            this.colProductCategoryId,
            this.colProductBarCode,
            this.colProductSpecification,
            this.colProductDescription,
            this.colProductPriceA,
            this.colProductPriceB,
            this.colProductPriceC,
            this.colProductRetailPrice});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomUnboundColumnData += new DevExpress.XtraGrid.Views.Base.CustomColumnDataEventHandler(this.gridView1_CustomUnboundColumnData);
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
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
            this.colProductId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductCategoryId
            // 
            this.colProductCategoryId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductCategoryId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductCategoryId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductCategoryId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductCategoryId, "colProductCategoryId");
            this.colProductCategoryId.FieldName = " ";
            this.colProductCategoryId.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colProductCategoryId.Name = "colProductCategoryId";
            this.colProductCategoryId.OptionsColumn.AllowEdit = false;
            this.colProductCategoryId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.True;
            this.colProductCategoryId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Like;
            this.colProductCategoryId.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
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
            this.colProductName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
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
            this.colProductSpecification.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
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
            this.colProductDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductPriceA
            // 
            this.colProductPriceA.AppearanceCell.Options.UseTextOptions = true;
            this.colProductPriceA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProductPriceA.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductPriceA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProductPriceA, "colProductPriceA");
            this.colProductPriceA.FieldName = "ProductPriceA";
            this.colProductPriceA.Name = "colProductPriceA";
            this.colProductPriceA.OptionsColumn.AllowEdit = false;
            this.colProductPriceA.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductPriceB
            // 
            this.colProductPriceB.AppearanceCell.Options.UseTextOptions = true;
            this.colProductPriceB.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProductPriceB.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductPriceB.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProductPriceB, "colProductPriceB");
            this.colProductPriceB.FieldName = "ProductPriceB";
            this.colProductPriceB.Name = "colProductPriceB";
            this.colProductPriceB.OptionsColumn.AllowEdit = false;
            this.colProductPriceB.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductPriceC
            // 
            this.colProductPriceC.AppearanceCell.Options.UseTextOptions = true;
            this.colProductPriceC.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProductPriceC.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductPriceC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProductPriceC, "colProductPriceC");
            this.colProductPriceC.FieldName = "ProductPriceC";
            this.colProductPriceC.Name = "colProductPriceC";
            this.colProductPriceC.OptionsColumn.AllowEdit = false;
            this.colProductPriceC.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colProductRetailPrice
            // 
            this.colProductRetailPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colProductRetailPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colProductRetailPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductRetailPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colProductRetailPrice, "colProductRetailPrice");
            this.colProductRetailPrice.FieldName = "ProductRetailPrice";
            this.colProductRetailPrice.Name = "colProductRetailPrice";
            this.colProductRetailPrice.OptionsColumn.AllowEdit = false;
            this.colProductRetailPrice.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.DataSource = this.bindingSourceProductCate;
            resources.ApplyResources(this.listBoxControl1, "listBoxControl1");
            this.listBoxControl1.Name = "listBoxControl1";
            // 
            // bindingSourceProductCate
            // 
            this.bindingSourceProductCate.CurrentChanged += new System.EventHandler(this.bindingSourceProductCate_CurrentChanged);
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxControl1);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.LisFormt_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.listBoxControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceProductCate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCategoryId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductBarCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProductSpecification;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceA;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceB;
        private DevExpress.XtraGrid.Columns.GridColumn colProductPriceC;
        private DevExpress.XtraGrid.Columns.GridColumn colProductRetailPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colProductDescription;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private System.Windows.Forms.BindingSource bindingSourceProductCate;
    }
}