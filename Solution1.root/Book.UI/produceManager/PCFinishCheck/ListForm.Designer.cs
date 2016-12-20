namespace Book.UI.produceManager.PCFinishCheck
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.barBtn_Search = new DevExpress.XtraBars.BarButtonItem();
            this.PCFinishCheckID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCFinishCheckDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WorkHouse = new DevExpress.XtraGrid.Columns.GridColumn();
            this.InvoiceCusXOId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Product = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCFinishCheckCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PCFinishCheckInCoiunt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Employee0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Employee1 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.PCFinishCheckID,
            this.PCFinishCheckDate,
            this.WorkHouse,
            this.InvoiceCusXOId,
            this.Product,
            this.PCFinishCheckCount,
            this.PCFinishCheckInCoiunt,
            this.Employee0,
            this.Employee1});
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
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
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
            // PCFinishCheckID
            // 
            resources.ApplyResources(this.PCFinishCheckID, "PCFinishCheckID");
            this.PCFinishCheckID.FieldName = "PCFinishCheckID";
            this.PCFinishCheckID.Name = "PCFinishCheckID";
            this.PCFinishCheckID.OptionsColumn.AllowEdit = false;
            // 
            // PCFinishCheckDate
            // 
            resources.ApplyResources(this.PCFinishCheckDate, "PCFinishCheckDate");
            this.PCFinishCheckDate.FieldName = "PCFinishCheckDate";
            this.PCFinishCheckDate.Name = "PCFinishCheckDate";
            this.PCFinishCheckDate.OptionsColumn.AllowEdit = false;
            // 
            // WorkHouse
            // 
            resources.ApplyResources(this.WorkHouse, "WorkHouse");
            this.WorkHouse.FieldName = "WorkHouse";
            this.WorkHouse.Name = "WorkHouse";
            this.WorkHouse.OptionsColumn.AllowEdit = false;
            // 
            // InvoiceCusXOId
            // 
            resources.ApplyResources(this.InvoiceCusXOId, "InvoiceCusXOId");
            this.InvoiceCusXOId.FieldName = "InvoiceCusXOId";
            this.InvoiceCusXOId.Name = "InvoiceCusXOId";
            this.InvoiceCusXOId.OptionsColumn.AllowEdit = false;
            // 
            // Product
            // 
            resources.ApplyResources(this.Product, "Product");
            this.Product.FieldName = "Product";
            this.Product.Name = "Product";
            this.Product.OptionsColumn.AllowEdit = false;
            // 
            // PCFinishCheckCount
            // 
            resources.ApplyResources(this.PCFinishCheckCount, "PCFinishCheckCount");
            this.PCFinishCheckCount.FieldName = "PCFinishCheckCount";
            this.PCFinishCheckCount.Name = "PCFinishCheckCount";
            this.PCFinishCheckCount.OptionsColumn.AllowEdit = false;
            // 
            // PCFinishCheckInCoiunt
            // 
            resources.ApplyResources(this.PCFinishCheckInCoiunt, "PCFinishCheckInCoiunt");
            this.PCFinishCheckInCoiunt.FieldName = "PCFinishCheckInCoiunt";
            this.PCFinishCheckInCoiunt.Name = "PCFinishCheckInCoiunt";
            this.PCFinishCheckInCoiunt.OptionsColumn.AllowEdit = false;
            // 
            // Employee0
            // 
            resources.ApplyResources(this.Employee0, "Employee0");
            this.Employee0.FieldName = "Employee0";
            this.Employee0.Name = "Employee0";
            this.Employee0.OptionsColumn.AllowEdit = false;
            // 
            // Employee1
            // 
            resources.ApplyResources(this.Employee1, "Employee1");
            this.Employee1.FieldName = "Employee1";
            this.Employee1.Name = "Employee1";
            this.Employee1.OptionsColumn.AllowEdit = false;
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
        private DevExpress.XtraGrid.Columns.GridColumn PCFinishCheckID;
        private DevExpress.XtraGrid.Columns.GridColumn PCFinishCheckDate;
        private DevExpress.XtraGrid.Columns.GridColumn WorkHouse;
        private DevExpress.XtraGrid.Columns.GridColumn InvoiceCusXOId;
        private DevExpress.XtraGrid.Columns.GridColumn Product;
        private DevExpress.XtraGrid.Columns.GridColumn PCFinishCheckCount;
        private DevExpress.XtraGrid.Columns.GridColumn PCFinishCheckInCoiunt;
        private DevExpress.XtraGrid.Columns.GridColumn Employee0;
        private DevExpress.XtraGrid.Columns.GridColumn Employee1;
    }
}
