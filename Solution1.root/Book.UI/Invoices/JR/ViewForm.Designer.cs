namespace Book.UI.Invoices.JR
{
    partial class ViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditNote = new DevExpress.XtraEditors.MemoEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buttonEditDepot = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditCompany = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditEmployee = new DevExpress.XtraEditors.ButtonEdit();
            this.textEditInvoiceId = new DevExpress.XtraEditors.TextEdit();
            this.dateEditInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditNote);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.buttonEditDepot);
            this.layoutControl1.Controls.Add(this.buttonEditCompany);
            this.layoutControl1.Controls.Add(this.buttonEditEmployee);
            this.layoutControl1.Controls.Add(this.textEditInvoiceId);
            this.layoutControl1.Controls.Add(this.dateEditInvoiceDate);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(48, 299, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // textEditNote
            // 
            resources.ApplyResources(this.textEditNote, "textEditNote");
            this.textEditNote.Name = "textEditNote";
            this.textEditNote.Properties.ReadOnly = true;
            this.textEditNote.StyleController = this.layoutControl1;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1,
            this.repositoryItemComboBox1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
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
            this.gridColumn7});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowColumnResizing = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumn1.FieldName = "ProductId";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemSpinEdit1
            // 
            resources.ApplyResources(this.repositoryItemSpinEdit1, "repositoryItemSpinEdit1");
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemSpinEdit1.Buttons"))), resources.GetString("repositoryItemSpinEdit1.Buttons1"), ((int)(resources.GetObject("repositoryItemSpinEdit1.Buttons2"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons3"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons4"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("repositoryItemSpinEdit1.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("repositoryItemSpinEdit1.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("repositoryItemSpinEdit1.Buttons8"), ((object)(resources.GetObject("repositoryItemSpinEdit1.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("repositoryItemSpinEdit1.Buttons10"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons11"))))});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "Product";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn2.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn3.FieldName = "InvoiceProductUnit";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn3.OptionsFilter.AllowFilter = false;
            // 
            // repositoryItemComboBox1
            // 
            resources.ApplyResources(this.repositoryItemComboBox1, "repositoryItemComboBox1");
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemComboBox1.Buttons"))))});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "ProductSpecification";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "ProductModel";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn5.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "InvoiceJRDetailQuantity";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.FieldName = "InvoiceJRDetailNote";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            // 
            // buttonEditDepot
            // 
            resources.ApplyResources(this.buttonEditDepot, "buttonEditDepot");
            this.buttonEditDepot.Name = "buttonEditDepot";
            this.buttonEditDepot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditDepot.Properties.Buttons"))), resources.GetString("buttonEditDepot.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditDepot.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditDepot.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditDepot.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("buttonEditDepot.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditDepot.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditDepot.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons11"))))});
            this.buttonEditDepot.Properties.ReadOnly = true;
            this.buttonEditDepot.StyleController = this.layoutControl1;
            // 
            // buttonEditCompany
            // 
            resources.ApplyResources(this.buttonEditCompany, "buttonEditCompany");
            this.buttonEditCompany.Name = "buttonEditCompany";
            this.buttonEditCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditCompany.Properties.Buttons"))), resources.GetString("buttonEditCompany.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditCompany.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditCompany.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditCompany.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditCompany.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditCompany.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditCompany.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("buttonEditCompany.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditCompany.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditCompany.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditCompany.Properties.Buttons11"))))});
            this.buttonEditCompany.Properties.ReadOnly = true;
            this.buttonEditCompany.StyleController = this.layoutControl1;
            // 
            // buttonEditEmployee
            // 
            resources.ApplyResources(this.buttonEditEmployee, "buttonEditEmployee");
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditEmployee.Properties.Buttons"))), resources.GetString("buttonEditEmployee.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditEmployee.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditEmployee.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditEmployee.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("buttonEditEmployee.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditEmployee.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditEmployee.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons11"))))});
            this.buttonEditEmployee.Properties.ReadOnly = true;
            this.buttonEditEmployee.StyleController = this.layoutControl1;
            // 
            // textEditInvoiceId
            // 
            resources.ApplyResources(this.textEditInvoiceId, "textEditInvoiceId");
            this.textEditInvoiceId.Name = "textEditInvoiceId";
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.textEditInvoiceId.StyleController = this.layoutControl1;
            // 
            // dateEditInvoiceDate
            // 
            resources.ApplyResources(this.dateEditInvoiceDate, "dateEditInvoiceDate");
            this.dateEditInvoiceDate.Name = "dateEditInvoiceDate";
            this.dateEditInvoiceDate.Properties.ReadOnly = true;
            this.dateEditInvoiceDate.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(838, 406);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditInvoiceId;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(298, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(272, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditInvoiceDate;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(298, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.buttonEditCompany;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(298, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.buttonEditDepot;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(298, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(272, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(818, 276);
            this.layoutControlItem6.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.textEditNote;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 326);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(818, 60);
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEditEmployee;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(570, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(248, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(570, 25);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(248, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ViewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ViewForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.MemoEdit textEditNote;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDepot;
        private DevExpress.XtraEditors.ButtonEdit buttonEditCompany;
        private DevExpress.XtraEditors.ButtonEdit buttonEditEmployee;
        private DevExpress.XtraEditors.TextEdit textEditInvoiceId;
        private DevExpress.XtraEditors.DateEdit dateEditInvoiceDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    
    }
}