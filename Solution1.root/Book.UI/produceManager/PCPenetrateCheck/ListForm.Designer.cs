namespace Book.UI.produceManager.PCPenetrateCheck
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.barBtn_Search = new DevExpress.XtraBars.BarButtonItem();
            this.colPCPenetrateCheckId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPCPenetrateCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceCusXOId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceXOQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPCPenetrateCheckQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colIsPassing = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colPCPenetrateCheckRightCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPCPenetrateCheckCenterCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPCPenetrateCheckLeftCount = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barBtn_Search});
            this.barManager1.MaxItemId = 14;
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barBtn_Search, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPCPenetrateCheckId,
            this.colPCPenetrateCheckDate,
            this.colInvoiceCusXOId,
            this.colProduct,
            this.colInvoiceXOQuantity,
            this.colPCPenetrateCheckQuantity,
            this.colPCPenetrateCheckLeftCount,
            this.colPCPenetrateCheckCenterCount,
            this.colPCPenetrateCheckRightCount,
            this.colIsPassing,
            this.colEmployee});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
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
            this.repositoryItemCheckEdit2});
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // barBtn_Search
            // 
            resources.ApplyResources(this.barBtn_Search, "barBtn_Search");
            this.barBtn_Search.Id = 13;
            this.barBtn_Search.ImageIndex = 3;
            this.barBtn_Search.Name = "barBtn_Search";
            this.barBtn_Search.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtn_Search_ItemClick);
            // 
            // colPCPenetrateCheckId
            // 
            resources.ApplyResources(this.colPCPenetrateCheckId, "colPCPenetrateCheckId");
            this.colPCPenetrateCheckId.FieldName = "PCPenetrateCheckId";
            this.colPCPenetrateCheckId.Name = "colPCPenetrateCheckId";
            this.colPCPenetrateCheckId.OptionsColumn.AllowEdit = false;
            // 
            // colPCPenetrateCheckDate
            // 
            resources.ApplyResources(this.colPCPenetrateCheckDate, "colPCPenetrateCheckDate");
            this.colPCPenetrateCheckDate.FieldName = "PCPenetrateCheckDate";
            this.colPCPenetrateCheckDate.Name = "colPCPenetrateCheckDate";
            this.colPCPenetrateCheckDate.OptionsColumn.AllowEdit = false;
            // 
            // colInvoiceCusXOId
            // 
            resources.ApplyResources(this.colInvoiceCusXOId, "colInvoiceCusXOId");
            this.colInvoiceCusXOId.FieldName = "InvoiceCusXOId";
            this.colInvoiceCusXOId.Name = "colInvoiceCusXOId";
            this.colInvoiceCusXOId.OptionsColumn.AllowEdit = false;
            // 
            // colProduct
            // 
            resources.ApplyResources(this.colProduct, "colProduct");
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.OptionsColumn.AllowEdit = false;
            // 
            // colInvoiceXOQuantity
            // 
            resources.ApplyResources(this.colInvoiceXOQuantity, "colInvoiceXOQuantity");
            this.colInvoiceXOQuantity.FieldName = "InvoiceXOQuantity";
            this.colInvoiceXOQuantity.Name = "colInvoiceXOQuantity";
            this.colInvoiceXOQuantity.OptionsColumn.AllowEdit = false;
            // 
            // colPCPenetrateCheckQuantity
            // 
            resources.ApplyResources(this.colPCPenetrateCheckQuantity, "colPCPenetrateCheckQuantity");
            this.colPCPenetrateCheckQuantity.FieldName = "PCPenetrateCheckQuantity";
            this.colPCPenetrateCheckQuantity.Name = "colPCPenetrateCheckQuantity";
            this.colPCPenetrateCheckQuantity.OptionsColumn.AllowEdit = false;
            // 
            // colEmployee
            // 
            resources.ApplyResources(this.colEmployee, "colEmployee");
            this.colEmployee.FieldName = "Employee";
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemCheckEdit2
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit2, "repositoryItemCheckEdit2");
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colIsPassing
            // 
            resources.ApplyResources(this.colIsPassing, "colIsPassing");
            this.colIsPassing.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colIsPassing.FieldName = "IsPassing";
            this.colIsPassing.Name = "colIsPassing";
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // colPCPenetrateCheckRightCount
            // 
            resources.ApplyResources(this.colPCPenetrateCheckRightCount, "colPCPenetrateCheckRightCount");
            this.colPCPenetrateCheckRightCount.FieldName = "PCPenetrateCheckRightCount";
            this.colPCPenetrateCheckRightCount.Name = "colPCPenetrateCheckRightCount";
            // 
            // colPCPenetrateCheckCenterCount
            // 
            resources.ApplyResources(this.colPCPenetrateCheckCenterCount, "colPCPenetrateCheckCenterCount");
            this.colPCPenetrateCheckCenterCount.FieldName = "PCPenetrateCheckCenterCount";
            this.colPCPenetrateCheckCenterCount.Name = "colPCPenetrateCheckCenterCount";
            // 
            // colPCPenetrateCheckLeftCount
            // 
            resources.ApplyResources(this.colPCPenetrateCheckLeftCount, "colPCPenetrateCheckLeftCount");
            this.colPCPenetrateCheckLeftCount.FieldName = "PCPenetrateCheckLeftCount";
            this.colPCPenetrateCheckLeftCount.Name = "colPCPenetrateCheckLeftCount";
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "ListForm";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barBtn_Search;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckId;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceCusXOId;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceXOQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn colIsPassing;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckLeftCount;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckCenterCount;
        private DevExpress.XtraGrid.Columns.GridColumn colPCPenetrateCheckRightCount;
    }
}
