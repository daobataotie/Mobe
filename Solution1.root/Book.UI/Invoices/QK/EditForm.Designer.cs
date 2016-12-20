namespace Book.UI.Invoices.QK
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.buttonEditCompany = new Book.UI.Invoices.NewChooseContorl();
            this.buttonEditEmployee = new Book.UI.Invoices.NewChooseContorl();
            this.textEditNote = new DevExpress.XtraEditors.MemoEdit();
            this.dateEditInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.calcEditMoney0 = new DevExpress.XtraEditors.CalcEdit();
            this.calcEditMoney1 = new DevExpress.XtraEditors.CalcEdit();
            this.textEditInvoiceId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditMoney0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditMoney1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.buttonEditCompany);
            this.dataLayoutControl1.Controls.Add(this.buttonEditEmployee);
            this.dataLayoutControl1.Controls.Add(this.textEditNote);
            this.dataLayoutControl1.Controls.Add(this.dateEditInvoiceDate);
            this.dataLayoutControl1.Controls.Add(this.calcEditMoney0);
            this.dataLayoutControl1.Controls.Add(this.calcEditMoney1);
            this.dataLayoutControl1.Controls.Add(this.textEditInvoiceId);
            resources.ApplyResources(this.dataLayoutControl1, "dataLayoutControl1");
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            // 
            // buttonEditCompany
            // 
            this.buttonEditCompany.ButtonReadOnly = false;
            this.buttonEditCompany.EditValue = null;
            resources.ApplyResources(this.buttonEditCompany, "buttonEditCompany");
            this.buttonEditCompany.Name = "buttonEditCompany";
            this.buttonEditCompany.ShowButton = true;
            // 
            // buttonEditEmployee
            // 
            this.buttonEditEmployee.ButtonReadOnly = false;
            this.buttonEditEmployee.EditValue = null;
            resources.ApplyResources(this.buttonEditEmployee, "buttonEditEmployee");
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.ShowButton = true;
            // 
            // textEditNote
            // 
            resources.ApplyResources(this.textEditNote, "textEditNote");
            this.textEditNote.Name = "textEditNote";
            this.textEditNote.StyleController = this.dataLayoutControl1;
            // 
            // dateEditInvoiceDate
            // 
            resources.ApplyResources(this.dateEditInvoiceDate, "dateEditInvoiceDate");
            this.dateEditInvoiceDate.EnterMoveNextControl = true;
            this.dateEditInvoiceDate.Name = "dateEditInvoiceDate";
            this.dateEditInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons"))))});
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditInvoiceDate.StyleController = this.dataLayoutControl1;
            this.dateEditInvoiceDate.Leave += new System.EventHandler(this.dateEditInvoiceDate_Leave);
            // 
            // calcEditMoney0
            // 
            this.calcEditMoney0.EnterMoveNextControl = true;
            resources.ApplyResources(this.calcEditMoney0, "calcEditMoney0");
            this.calcEditMoney0.Name = "calcEditMoney0";
            this.calcEditMoney0.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("calcEditMoney0.Properties.Buttons"))), resources.GetString("calcEditMoney0.Properties.Buttons1"), ((int)(resources.GetObject("calcEditMoney0.Properties.Buttons2"))), ((bool)(resources.GetObject("calcEditMoney0.Properties.Buttons3"))), ((bool)(resources.GetObject("calcEditMoney0.Properties.Buttons4"))), ((bool)(resources.GetObject("calcEditMoney0.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("calcEditMoney0.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("calcEditMoney0.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("calcEditMoney0.Properties.Buttons8"), ((object)(resources.GetObject("calcEditMoney0.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("calcEditMoney0.Properties.Buttons10"))), ((bool)(resources.GetObject("calcEditMoney0.Properties.Buttons11"))))});
            this.calcEditMoney0.Properties.DisplayFormat.FormatString = "0";
            this.calcEditMoney0.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcEditMoney0.Properties.ReadOnly = true;
            this.calcEditMoney0.StyleController = this.dataLayoutControl1;
            // 
            // calcEditMoney1
            // 
            this.calcEditMoney1.EnterMoveNextControl = true;
            resources.ApplyResources(this.calcEditMoney1, "calcEditMoney1");
            this.calcEditMoney1.Name = "calcEditMoney1";
            this.calcEditMoney1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("calcEditMoney1.Properties.Buttons"))), resources.GetString("calcEditMoney1.Properties.Buttons1"), ((int)(resources.GetObject("calcEditMoney1.Properties.Buttons2"))), ((bool)(resources.GetObject("calcEditMoney1.Properties.Buttons3"))), ((bool)(resources.GetObject("calcEditMoney1.Properties.Buttons4"))), ((bool)(resources.GetObject("calcEditMoney1.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("calcEditMoney1.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("calcEditMoney1.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("calcEditMoney1.Properties.Buttons8"), ((object)(resources.GetObject("calcEditMoney1.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("calcEditMoney1.Properties.Buttons10"))), ((bool)(resources.GetObject("calcEditMoney1.Properties.Buttons11"))))});
            this.calcEditMoney1.Properties.DisplayFormat.FormatString = "0";
            this.calcEditMoney1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.calcEditMoney1.StyleController = this.dataLayoutControl1;
            // 
            // textEditInvoiceId
            // 
            this.textEditInvoiceId.EnterMoveNextControl = true;
            resources.ApplyResources(this.textEditInvoiceId, "textEditInvoiceId");
            this.textEditInvoiceId.Name = "textEditInvoiceId";
            this.textEditInvoiceId.StyleController = this.dataLayoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.layoutControlItem3,
            this.layoutControlItem8,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(905, 379);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.calcEditMoney0;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(289, 25);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(309, 25);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem7.AppearanceItemCaption.Font")));
            this.layoutControlItem7.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem7.Control = this.calcEditMoney1;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(598, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(287, 25);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditNote;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(885, 309);
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem5.AppearanceItemCaption.Font")));
            this.layoutControlItem5.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem5.Control = this.dateEditInvoiceDate;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(290, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem1.AppearanceItemCaption.Font")));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.textEditInvoiceId;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(290, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(309, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font")));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.buttonEditEmployee;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(599, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(286, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(56, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem6.AppearanceItemCaption.Font")));
            this.layoutControlItem6.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem6.Control = this.buttonEditCompany;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(289, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(56, 14);
            // 
            // EditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataLayoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Controls.SetChildIndex(this.dataLayoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditMoney0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditMoney1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.TextEdit textEditInvoiceId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CalcEdit calcEditMoney0;
        private DevExpress.XtraEditors.CalcEdit calcEditMoney1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DateEdit dateEditInvoiceDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.MemoEdit textEditNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private NewChooseContorl buttonEditEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private NewChooseContorl buttonEditCompany;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}