namespace Book.UI.Invoices.BY
{
    partial class ViewForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditNote = new DevExpress.XtraEditors.MemoEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceBYDetailQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceBYDetailNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buttonEditDepot = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditEmployee = new DevExpress.XtraEditors.ButtonEdit();
            this.dateEditInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.textEditInvoiceId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEditNote);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.buttonEditDepot);
            this.layoutControl1.Controls.Add(this.buttonEditEmployee);
            this.layoutControl1.Controls.Add(this.dateEditInvoiceDate);
            this.layoutControl1.Controls.Add(this.textEditInvoiceId);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(516, 418, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup2;
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
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId,
            this.colProduct,
            this.colInvoiceBYDetailQuantity,
            this.colInvoiceBYDetailNote});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId
            // 
            this.colProductId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductId, "colProductId");
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.ReadOnly = true;
            // 
            // colProduct
            // 
            this.colProduct.AppearanceCell.Options.UseTextOptions = true;
            this.colProduct.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProduct.AppearanceHeader.Options.UseTextOptions = true;
            this.colProduct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProduct, "colProduct");
            this.colProduct.FieldName = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.OptionsColumn.AllowEdit = false;
            this.colProduct.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceBYDetailQuantity
            // 
            this.colInvoiceBYDetailQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceBYDetailQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceBYDetailQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceBYDetailQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceBYDetailQuantity, "colInvoiceBYDetailQuantity");
            this.colInvoiceBYDetailQuantity.FieldName = "InvoiceBYDetailQuantity";
            this.colInvoiceBYDetailQuantity.Name = "colInvoiceBYDetailQuantity";
            this.colInvoiceBYDetailQuantity.OptionsColumn.AllowEdit = false;
            this.colInvoiceBYDetailQuantity.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceBYDetailNote
            // 
            this.colInvoiceBYDetailNote.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceBYDetailNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceBYDetailNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceBYDetailNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceBYDetailNote, "colInvoiceBYDetailNote");
            this.colInvoiceBYDetailNote.FieldName = "InvoiceBYDetailNote";
            this.colInvoiceBYDetailNote.Name = "colInvoiceBYDetailNote";
            this.colInvoiceBYDetailNote.OptionsColumn.AllowEdit = false;
            this.colInvoiceBYDetailNote.OptionsColumn.ReadOnly = true;
            // 
            // buttonEditDepot
            // 
            resources.ApplyResources(this.buttonEditDepot, "buttonEditDepot");
            this.buttonEditDepot.Name = "buttonEditDepot";
            this.buttonEditDepot.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditDepot.Properties.Buttons"))), resources.GetString("buttonEditDepot.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditDepot.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditDepot.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditDepot.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("buttonEditDepot.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditDepot.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditDepot.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditDepot.Properties.Buttons11"))))});
            this.buttonEditDepot.Properties.ReadOnly = true;
            this.buttonEditDepot.StyleController = this.layoutControl1;
            // 
            // buttonEditEmployee
            // 
            resources.ApplyResources(this.buttonEditEmployee, "buttonEditEmployee");
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditEmployee.Properties.Buttons"))), resources.GetString("buttonEditEmployee.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditEmployee.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditEmployee.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditEmployee.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("buttonEditEmployee.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditEmployee.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditEmployee.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons11"))))});
            this.buttonEditEmployee.Properties.ReadOnly = true;
            this.buttonEditEmployee.StyleController = this.layoutControl1;
            // 
            // dateEditInvoiceDate
            // 
            resources.ApplyResources(this.dateEditInvoiceDate, "dateEditInvoiceDate");
            this.dateEditInvoiceDate.Name = "dateEditInvoiceDate";
            this.dateEditInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons"))), resources.GetString("dateEditInvoiceDate.Properties.Buttons1"), ((int)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons2"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons3"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons4"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("dateEditInvoiceDate.Properties.Buttons8"), ((object)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons10"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons11"))))});
            this.dateEditInvoiceDate.Properties.ReadOnly = true;
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditInvoiceDate.StyleController = this.layoutControl1;
            // 
            // textEditInvoiceId
            // 
            resources.ApplyResources(this.textEditInvoiceId, "textEditInvoiceId");
            this.textEditInvoiceId.Name = "textEditInvoiceId";
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.textEditInvoiceId.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlItem4,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem5,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Size = new System.Drawing.Size(876, 393);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditInvoiceId;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(262, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(856, 252);
            this.layoutControlItem7.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dateEditInvoiceDate;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(262, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(299, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditNote;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 302);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(856, 71);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.buttonEditDepot;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(262, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.buttonEditEmployee;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(561, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(295, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(52, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(262, 25);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(594, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(292, 249);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // ViewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dateEditInvoiceDate;
        private DevExpress.XtraEditors.TextEdit textEditInvoiceId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDepot;
        private DevExpress.XtraEditors.ButtonEdit buttonEditEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceBYDetailQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceBYDetailNote;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.MemoEdit textEditNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}