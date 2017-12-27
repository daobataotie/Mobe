namespace Book.UI.Settings.BasicData.Products
{
    partial class List1Form
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(List1Form));
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.MaxItemId = 14;
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn11,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Controls.SetChildIndex(this.gridControl1, 0);
            // 
            // gridControl1
            // 
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemRichTextEdit1});
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "ProductName";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "ProductCategoryName";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "ProductBarCode";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "ProductSpecification";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.ColumnEdit = this.repositoryItemRichTextEdit1;
            this.gridColumn6.FieldName = "ProductDescription";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.FieldName = "ProductPriceA";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            resources.ApplyResources(this.gridColumn8, "gridColumn8");
            this.gridColumn8.FieldName = "ProductPriceB";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            resources.ApplyResources(this.gridColumn9, "gridColumn9");
            this.gridColumn9.FieldName = "ProductPriceC";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            resources.ApplyResources(this.gridColumn10, "gridColumn10");
            this.gridColumn10.FieldName = "ProductRetailPrice";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            resources.ApplyResources(this.gridColumn11, "gridColumn11");
            this.gridColumn11.FieldName = "CustomerProductName";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn12
            // 
            resources.ApplyResources(this.gridColumn12, "gridColumn12");
            this.gridColumn12.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn12.FieldName = "IsCustomerProduct";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gridColumn13
            // 
            resources.ApplyResources(this.gridColumn13, "gridColumn13");
            this.gridColumn13.FieldName = "Customer";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn14
            // 
            resources.ApplyResources(this.gridColumn14, "gridColumn14");
            this.gridColumn14.FieldName = "SupplierFullName";
            this.gridColumn14.Name = "gridColumn14";
            // 
            // gridColumn15
            // 
            resources.ApplyResources(this.gridColumn15, "gridColumn15");
            this.gridColumn15.FieldName = "StocksQuantity";
            this.gridColumn15.Name = "gridColumn15";
            // 
            // gridColumn16
            // 
            resources.ApplyResources(this.gridColumn16, "gridColumn16");
            this.gridColumn16.FieldName = "ProductVersion";
            this.gridColumn16.Name = "gridColumn16";
            // 
            // gridColumn17
            // 
            resources.ApplyResources(this.gridColumn17, "gridColumn17");
            this.gridColumn17.FieldName = "Price";
            this.gridColumn17.Name = "gridColumn17";
            // 
            // List1Form
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "List1Form";
            this.Load += new System.EventHandler(this.List1Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
    }
}