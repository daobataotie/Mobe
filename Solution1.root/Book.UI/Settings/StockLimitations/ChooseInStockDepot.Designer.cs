namespace Book.UI.Settings.StockLimitations
{
    partial class ChooseInStockDepot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseInStockDepot));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Button_Sure = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditEndate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditStartDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumnDepotInId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepotInDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEmployeeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEmployee0Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepotId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnSupplierId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDepotInId,
            this.gridColumnDepotInDate,
            this.gridColumnEmployeeId,
            this.gridColumnEmployee0Id,
            this.gridColumnDepotId,
            this.gridColumnSupplierId,
            this.gridColumnDescription});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButtonCancel
            // 
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            // 
            // simpleButtonOK
            // 
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            // 
            // simpleButtonNew
            // 
            resources.ApplyResources(this.simpleButtonNew, "simpleButtonNew");
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.Button_Sure);
            this.layoutControl1.Controls.Add(this.dateEditEndate);
            this.layoutControl1.Controls.Add(this.dateEditStartDate);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // Button_Sure
            // 
            resources.ApplyResources(this.Button_Sure, "Button_Sure");
            this.Button_Sure.Name = "Button_Sure";
            this.Button_Sure.StyleController = this.layoutControl1;
            this.Button_Sure.Click += new System.EventHandler(this.Button_SearCh_Click);
            // 
            // dateEditEndate
            // 
            resources.ApplyResources(this.dateEditEndate, "dateEditEndate");
            this.dateEditEndate.Name = "dateEditEndate";
            this.dateEditEndate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditEndate.Properties.Buttons"))))});
            this.dateEditEndate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditEndate.StyleController = this.layoutControl1;
            // 
            // dateEditStartDate
            // 
            resources.ApplyResources(this.dateEditStartDate, "dateEditStartDate");
            this.dateEditStartDate.Name = "dateEditStartDate";
            this.dateEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditStartDate.Properties.Buttons"))))});
            this.dateEditStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStartDate.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(681, 48);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditStartDate;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(255, 28);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditEndate;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(255, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(240, 28);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Button_Sure;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(495, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(166, 28);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // gridColumnDepotInId
            // 
            resources.ApplyResources(this.gridColumnDepotInId, "gridColumnDepotInId");
            this.gridColumnDepotInId.FieldName = "DepotInId";
            this.gridColumnDepotInId.Name = "gridColumnDepotInId";
            this.gridColumnDepotInId.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDepotInDate
            // 
            resources.ApplyResources(this.gridColumnDepotInDate, "gridColumnDepotInDate");
            this.gridColumnDepotInDate.FieldName = "DepotInDate";
            this.gridColumnDepotInDate.Name = "gridColumnDepotInDate";
            this.gridColumnDepotInDate.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnEmployeeId
            // 
            resources.ApplyResources(this.gridColumnEmployeeId, "gridColumnEmployeeId");
            this.gridColumnEmployeeId.FieldName = "Employee";
            this.gridColumnEmployeeId.Name = "gridColumnEmployeeId";
            this.gridColumnEmployeeId.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnEmployee0Id
            // 
            resources.ApplyResources(this.gridColumnEmployee0Id, "gridColumnEmployee0Id");
            this.gridColumnEmployee0Id.FieldName = "Employee0";
            this.gridColumnEmployee0Id.Name = "gridColumnEmployee0Id";
            this.gridColumnEmployee0Id.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDepotId
            // 
            resources.ApplyResources(this.gridColumnDepotId, "gridColumnDepotId");
            this.gridColumnDepotId.FieldName = "Depot";
            this.gridColumnDepotId.Name = "gridColumnDepotId";
            this.gridColumnDepotId.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnSupplierId
            // 
            resources.ApplyResources(this.gridColumnSupplierId, "gridColumnSupplierId");
            this.gridColumnSupplierId.FieldName = "Supplier";
            this.gridColumnSupplierId.Name = "gridColumnSupplierId";
            this.gridColumnSupplierId.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDescription
            // 
            resources.ApplyResources(this.gridColumnDescription, "gridColumnDescription");
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.OptionsColumn.AllowEdit = false;
            // 
            // ChooseInStockDepot
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChooseInStockDepot";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            this.Controls.SetChildIndex(this.simpleButtonNew, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dateEditStartDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton Button_Sure;
        private DevExpress.XtraEditors.DateEdit dateEditEndate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotInId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotInDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEmployeeId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEmployee0Id;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSupplierId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
    }
}