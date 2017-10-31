namespace Book.UI.produceManager.PCOtherCheck
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
            this.PCOtherCheckId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCOtherCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InvoiceCusXOId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Employee1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Employee0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Supplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCOtherCheckDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
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
            this.PCOtherCheckId,
            this.PCOtherCheckDate,
            this.InvoiceCusXOId,
            this.Employee1,
            this.Employee0,
            this.Supplier,
            this.PCOtherCheckDesc,
            this.gridColumn1,
            this.gridColumn2});
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
            // PCOtherCheckId
            // 
            resources.ApplyResources(this.PCOtherCheckId, "PCOtherCheckId");
            this.PCOtherCheckId.FieldName = "PCOtherCheckId";
            this.PCOtherCheckId.Name = "PCOtherCheckId";
            this.PCOtherCheckId.OptionsColumn.AllowEdit = false;
            // 
            // PCOtherCheckDate
            // 
            resources.ApplyResources(this.PCOtherCheckDate, "PCOtherCheckDate");
            this.PCOtherCheckDate.FieldName = "ProductName";
            this.PCOtherCheckDate.Name = "PCOtherCheckDate";
            this.PCOtherCheckDate.OptionsColumn.AllowEdit = false;
            // 
            // InvoiceCusXOId
            // 
            resources.ApplyResources(this.InvoiceCusXOId, "InvoiceCusXOId");
            this.InvoiceCusXOId.FieldName = "InvoiceCusXOId";
            this.InvoiceCusXOId.Name = "InvoiceCusXOId";
            this.InvoiceCusXOId.OptionsColumn.AllowEdit = false;
            // 
            // Employee1
            // 
            resources.ApplyResources(this.Employee1, "Employee1");
            this.Employee1.FieldName = "Employee1";
            this.Employee1.Name = "Employee1";
            this.Employee1.OptionsColumn.AllowEdit = false;
            // 
            // Employee0
            // 
            resources.ApplyResources(this.Employee0, "Employee0");
            this.Employee0.FieldName = "EmployeeName";
            this.Employee0.Name = "Employee0";
            this.Employee0.OptionsColumn.AllowEdit = false;
            // 
            // Supplier
            // 
            resources.ApplyResources(this.Supplier, "Supplier");
            this.Supplier.FieldName = "SupplierFullName";
            this.Supplier.Name = "Supplier";
            this.Supplier.OptionsColumn.AllowEdit = false;
            // 
            // PCOtherCheckDesc
            // 
            resources.ApplyResources(this.PCOtherCheckDesc, "PCOtherCheckDesc");
            this.PCOtherCheckDesc.FieldName = "PCOtherCheckDetailDesc1";
            this.PCOtherCheckDesc.Name = "PCOtherCheckDesc";
            this.PCOtherCheckDesc.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "EmployeeName1";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "PCOtherCheckDate";
            this.gridColumn2.Name = "gridColumn2";
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
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem barBtn_Search;
        private DevExpress.XtraGrid.Columns.GridColumn PCOtherCheckId;
        private DevExpress.XtraGrid.Columns.GridColumn PCOtherCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn InvoiceCusXOId;
        private DevExpress.XtraGrid.Columns.GridColumn Employee1;
        private DevExpress.XtraGrid.Columns.GridColumn Employee0;
        private DevExpress.XtraGrid.Columns.GridColumn Supplier;
        private DevExpress.XtraGrid.Columns.GridColumn PCOtherCheckDesc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
