namespace Book.UI.Settings.StockLimitations
{
    partial class ChooseOutStockDepot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseOutStockDepot));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_ExportExcel = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEditDepot = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSourceDepot = new System.Windows.Forms.BindingSource(this.components);
            this.Button_SearCh = new DevExpress.XtraEditors.SimpleButton();
            this.dateEditEndate = new DevExpress.XtraEditors.DateEdit();
            this.dateEditStartDate = new DevExpress.XtraEditors.DateEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.txt_InvoiceCusID = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridColumnDepotOutId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepot = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepotOutDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEmployee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InvoiceCusID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
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
            this.gridColumnDepotOutId,
            this.gridColumnDepot,
            this.gridColumnDepotOutDate,
            this.gridColumnEmployee,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn1,
            this.gridColumn2});
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
            this.layoutControl1.Controls.Add(this.btn_ExportExcel);
            this.layoutControl1.Controls.Add(this.lookUpEditDepot);
            this.layoutControl1.Controls.Add(this.Button_SearCh);
            this.layoutControl1.Controls.Add(this.dateEditEndate);
            this.layoutControl1.Controls.Add(this.dateEditStartDate);
            this.layoutControl1.Controls.Add(this.buttonEdit1);
            this.layoutControl1.Controls.Add(this.txt_InvoiceCusID);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btn_ExportExcel
            // 
            resources.ApplyResources(this.btn_ExportExcel, "btn_ExportExcel");
            this.btn_ExportExcel.Name = "btn_ExportExcel";
            this.btn_ExportExcel.StyleController = this.layoutControl1;
            this.btn_ExportExcel.Click += new System.EventHandler(this.btn_ExportExcel_Click);
            // 
            // lookUpEditDepot
            // 
            resources.ApplyResources(this.lookUpEditDepot, "lookUpEditDepot");
            this.lookUpEditDepot.Name = "lookUpEditDepot";
            this.lookUpEditDepot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lookUpEditDepot.Properties.Buttons"))))});
            this.lookUpEditDepot.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepot.Properties.Columns"), resources.GetString("lookUpEditDepot.Properties.Columns1")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepot.Properties.Columns2"), resources.GetString("lookUpEditDepot.Properties.Columns3"))});
            this.lookUpEditDepot.Properties.DataSource = this.bindingSourceDepot;
            this.lookUpEditDepot.Properties.DisplayMember = "DepotName";
            this.lookUpEditDepot.Properties.NullText = resources.GetString("lookUpEditDepot.Properties.NullText");
            this.lookUpEditDepot.Properties.ValueMember = "DepotId";
            this.lookUpEditDepot.StyleController = this.layoutControl1;
            // 
            // Button_SearCh
            // 
            resources.ApplyResources(this.Button_SearCh, "Button_SearCh");
            this.Button_SearCh.Name = "Button_SearCh";
            this.Button_SearCh.StyleController = this.layoutControl1;
            this.Button_SearCh.Click += new System.EventHandler(this.Button_SearCh_Click);
            // 
            // dateEditEndate
            // 
            this.dateEditEndate.EditValue = null;
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
            this.dateEditStartDate.EditValue = null;
            resources.ApplyResources(this.dateEditStartDate, "dateEditStartDate");
            this.dateEditStartDate.Name = "dateEditStartDate";
            this.dateEditStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditStartDate.Properties.Buttons"))))});
            this.dateEditStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditStartDate.StyleController = this.layoutControl1;
            // 
            // buttonEdit1
            // 
            resources.ApplyResources(this.buttonEdit1, "buttonEdit1");
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.StyleController = this.layoutControl1;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // txt_InvoiceCusID
            // 
            resources.ApplyResources(this.txt_InvoiceCusID, "txt_InvoiceCusID");
            this.txt_InvoiceCusID.Name = "txt_InvoiceCusID";
            this.txt_InvoiceCusID.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(874, 72);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditStartDate;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(184, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditEndate;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(184, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(188, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txt_InvoiceCusID;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(372, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(250, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lookUpEditDepot;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(622, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(232, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.buttonEdit1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(622, 27);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.Button_SearCh;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(622, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(116, 27);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_ExportExcel;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(738, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(116, 27);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // gridColumnDepotOutId
            // 
            resources.ApplyResources(this.gridColumnDepotOutId, "gridColumnDepotOutId");
            this.gridColumnDepotOutId.FieldName = "DepotOutId";
            this.gridColumnDepotOutId.Name = "gridColumnDepotOutId";
            this.gridColumnDepotOutId.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDepot
            // 
            resources.ApplyResources(this.gridColumnDepot, "gridColumnDepot");
            this.gridColumnDepot.FieldName = "Id";
            this.gridColumnDepot.Name = "gridColumnDepot";
            this.gridColumnDepot.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnDepotOutDate
            // 
            resources.ApplyResources(this.gridColumnDepotOutDate, "gridColumnDepotOutDate");
            this.gridColumnDepotOutDate.FieldName = "DepotOutDate";
            this.gridColumnDepotOutDate.Name = "gridColumnDepotOutDate";
            this.gridColumnDepotOutDate.OptionsColumn.AllowEdit = false;
            // 
            // gridColumnEmployee
            // 
            resources.ApplyResources(this.gridColumnEmployee, "gridColumnEmployee");
            this.gridColumnEmployee.FieldName = "EmployeeName";
            this.gridColumnEmployee.Name = "gridColumnEmployee";
            this.gridColumnEmployee.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "SourceType";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "InvioiceId";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "ProductName";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "DepotOutDetailQuantity";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // ChooseOutStockDepot
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ChooseOutStockDepot";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.simpleButtonNew, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditEndate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InvoiceCusID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton Button_SearCh;
        private DevExpress.XtraEditors.DateEdit dateEditEndate;
        private DevExpress.XtraEditors.DateEdit dateEditStartDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotOutId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepot;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotOutDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEmployee;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.TextEdit txt_InvoiceCusID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepot;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.BindingSource bindingSourceDepot;
        private DevExpress.XtraEditors.SimpleButton btn_ExportExcel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}