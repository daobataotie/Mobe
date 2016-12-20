namespace Book.UI.Invoices.XJ
{
    partial class ListConditionSearch
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btn_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.btn_ok = new DevExpress.XtraEditors.SimpleButton();
            this.lookUpEdit_Company = new DevExpress.XtraEditors.LookUpEdit();
            this.bindingSourceCompany = new System.Windows.Forms.BindingSource(this.components);
            this.txtInvoiceXJId = new DevExpress.XtraEditors.TextEdit();
            this.nccCustomer = new Book.UI.Invoices.NewChooseContorl();
            this.btnEdit_Product = new DevExpress.XtraEditors.ButtonEdit();
            this.DateEditEnd = new DevExpress.XtraEditors.DateEdit();
            this.DateEditStart = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Company.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceXJId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit_Product.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btn_cancel);
            this.layoutControl1.Controls.Add(this.btn_ok);
            this.layoutControl1.Controls.Add(this.lookUpEdit_Company);
            this.layoutControl1.Controls.Add(this.txtInvoiceXJId);
            this.layoutControl1.Controls.Add(this.nccCustomer);
            this.layoutControl1.Controls.Add(this.btnEdit_Product);
            this.layoutControl1.Controls.Add(this.DateEditEnd);
            this.layoutControl1.Controls.Add(this.DateEditStart);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(519, 175);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(388, 146);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(124, 22);
            this.btn_cancel.StyleController = this.layoutControl1;
            this.btn_cancel.TabIndex = 16;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(261, 146);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(123, 22);
            this.btn_ok.StyleController = this.layoutControl1;
            this.btn_ok.TabIndex = 15;
            this.btn_ok.Text = "确认";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lookUpEdit_Company
            // 
            this.lookUpEdit_Company.Location = new System.Drawing.Point(341, 57);
            this.lookUpEdit_Company.Name = "lookUpEdit_Company";
            this.lookUpEdit_Company.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_Company.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CompanyName", 120, "公司名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CompanyId", "编号", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.lookUpEdit_Company.Properties.DataSource = this.bindingSourceCompany;
            this.lookUpEdit_Company.Properties.DisplayMember = "CompanyName";
            this.lookUpEdit_Company.Properties.NullText = "";
            this.lookUpEdit_Company.Properties.ValueMember = "CompanyId";
            this.lookUpEdit_Company.Size = new System.Drawing.Size(171, 21);
            this.lookUpEdit_Company.StyleController = this.layoutControl1;
            this.lookUpEdit_Company.TabIndex = 14;
            // 
            // txtInvoiceXJId
            // 
            this.txtInvoiceXJId.Location = new System.Drawing.Point(87, 7);
            this.txtInvoiceXJId.Name = "txtInvoiceXJId";
            this.txtInvoiceXJId.Size = new System.Drawing.Size(170, 21);
            this.txtInvoiceXJId.StyleController = this.layoutControl1;
            this.txtInvoiceXJId.TabIndex = 8;
            // 
            // nccCustomer
            // 
            this.nccCustomer.EditValue = null;
            this.nccCustomer.Location = new System.Drawing.Point(341, 7);
            this.nccCustomer.Name = "nccCustomer";
            this.nccCustomer.Size = new System.Drawing.Size(171, 21);
            this.nccCustomer.TabIndex = 7;
            // 
            // btnEdit_Product
            // 
            this.btnEdit_Product.Location = new System.Drawing.Point(87, 57);
            this.btnEdit_Product.Name = "btnEdit_Product";
            this.btnEdit_Product.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEdit_Product.Size = new System.Drawing.Size(170, 21);
            this.btnEdit_Product.StyleController = this.layoutControl1;
            this.btnEdit_Product.TabIndex = 6;
            this.btnEdit_Product.Click += new System.EventHandler(this.btnEdit_Product_Click);
            // 
            // DateEditEnd
            // 
            this.DateEditEnd.EditValue = null;
            this.DateEditEnd.Location = new System.Drawing.Point(341, 32);
            this.DateEditEnd.Name = "DateEditEnd";
            this.DateEditEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEditEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEditEnd.Size = new System.Drawing.Size(171, 21);
            this.DateEditEnd.StyleController = this.layoutControl1;
            this.DateEditEnd.TabIndex = 5;
            // 
            // DateEditStart
            // 
            this.DateEditStart.EditValue = null;
            this.DateEditStart.Location = new System.Drawing.Point(87, 32);
            this.DateEditStart.Name = "DateEditStart";
            this.DateEditStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEditStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEditStart.Size = new System.Drawing.Size(170, 21);
            this.DateEditStart.StyleController = this.layoutControl1;
            this.DateEditStart.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(519, 175);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 75);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(509, 64);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.DateEditStart;
            this.layoutControlItem1.CustomizationFormText = "单据日期 从：";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem1.Text = "单据日期 从：";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.DateEditEnd;
            this.layoutControlItem2.CustomizationFormText = "至：";
            this.layoutControlItem2.Location = new System.Drawing.Point(254, 25);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(255, 25);
            this.layoutControlItem2.Text = "至：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnEdit_Product;
            this.layoutControlItem3.CustomizationFormText = "报价商品：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem3.Text = "报价商品：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtInvoiceXJId;
            this.layoutControlItem5.CustomizationFormText = "单据编号：";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem5.Text = "单据编号：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.nccCustomer;
            this.layoutControlItem4.CustomizationFormText = "客户：";
            this.layoutControlItem4.Location = new System.Drawing.Point(254, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(255, 25);
            this.layoutControlItem4.Text = "客户：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lookUpEdit_Company;
            this.layoutControlItem6.CustomizationFormText = "报价公司：";
            this.layoutControlItem6.Location = new System.Drawing.Point(254, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(255, 25);
            this.layoutControlItem6.Text = "报价公司：";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btn_ok;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(254, 139);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(127, 26);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btn_cancel;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(381, 139);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(128, 26);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 139);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(254, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ListConditionSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 175);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListConditionSearch";
            this.Text = "条件查询";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Company.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceXJId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit_Product.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEditStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.DateEdit DateEditEnd;
        private DevExpress.XtraEditors.DateEdit DateEditStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ButtonEdit btnEdit_Product;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private NewChooseContorl nccCustomer;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtInvoiceXJId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_Company;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private System.Windows.Forms.BindingSource bindingSourceCompany;
        private DevExpress.XtraEditors.SimpleButton btn_cancel;
        private DevExpress.XtraEditors.SimpleButton btn_ok;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}