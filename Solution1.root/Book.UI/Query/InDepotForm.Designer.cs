namespace Book.UI.Query
{
    partial class InDepotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InDepotForm));
            this.bindingSourceDepot = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.nccCustomerEnd = new Book.UI.Invoices.NewChooseContorl();
            this.nccCustomerStart = new Book.UI.Invoices.NewChooseContorl();
            this.lookUpEditDepotEnd = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditDepotStart = new DevExpress.XtraEditors.LookUpEdit();
            this.txt_InDepotIdEnd = new DevExpress.XtraEditors.TextEdit();
            this.txt_InDepotIdStart = new DevExpress.XtraEditors.TextEdit();
            this.date_End = new DevExpress.XtraEditors.DateEdit();
            this.date_Start = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepotEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepotStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InDepotIdEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InDepotIdStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButtonOK
            // 
            resources.ApplyResources(this.simpleButtonOK, "simpleButtonOK");
            // 
            // simpleButtonCancel
            // 
            resources.ApplyResources(this.simpleButtonCancel, "simpleButtonCancel");
            this.simpleButtonCancel.Click += new System.EventHandler(this.simpleButtonCancel_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.nccCustomerEnd);
            this.layoutControl1.Controls.Add(this.nccCustomerStart);
            this.layoutControl1.Controls.Add(this.lookUpEditDepotEnd);
            this.layoutControl1.Controls.Add(this.lookUpEditDepotStart);
            this.layoutControl1.Controls.Add(this.txt_InDepotIdEnd);
            this.layoutControl1.Controls.Add(this.txt_InDepotIdStart);
            this.layoutControl1.Controls.Add(this.date_End);
            this.layoutControl1.Controls.Add(this.date_Start);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // nccCustomerEnd
            // 
            this.nccCustomerEnd.EditValue = null;
            resources.ApplyResources(this.nccCustomerEnd, "nccCustomerEnd");
            this.nccCustomerEnd.Name = "nccCustomerEnd";
            // 
            // nccCustomerStart
            // 
            this.nccCustomerStart.EditValue = null;
            resources.ApplyResources(this.nccCustomerStart, "nccCustomerStart");
            this.nccCustomerStart.Name = "nccCustomerStart";
            // 
            // lookUpEditDepotEnd
            // 
            resources.ApplyResources(this.lookUpEditDepotEnd, "lookUpEditDepotEnd");
            this.lookUpEditDepotEnd.Name = "lookUpEditDepotEnd";
            this.lookUpEditDepotEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lookUpEditDepotEnd.Properties.Buttons"))))});
            this.lookUpEditDepotEnd.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepotEnd.Properties.Columns"), resources.GetString("lookUpEditDepotEnd.Properties.Columns1")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepotEnd.Properties.Columns2"), resources.GetString("lookUpEditDepotEnd.Properties.Columns3"))});
            this.lookUpEditDepotEnd.Properties.DataSource = this.bindingSourceDepot;
            this.lookUpEditDepotEnd.Properties.DisplayMember = "DepotName";
            this.lookUpEditDepotEnd.Properties.NullText = resources.GetString("lookUpEditDepotEnd.Properties.NullText");
            this.lookUpEditDepotEnd.Properties.ValueMember = "DepotId";
            this.lookUpEditDepotEnd.StyleController = this.layoutControl1;
            // 
            // lookUpEditDepotStart
            // 
            resources.ApplyResources(this.lookUpEditDepotStart, "lookUpEditDepotStart");
            this.lookUpEditDepotStart.Name = "lookUpEditDepotStart";
            this.lookUpEditDepotStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lookUpEditDepotStart.Properties.Buttons"))))});
            this.lookUpEditDepotStart.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepotStart.Properties.Columns"), resources.GetString("lookUpEditDepotStart.Properties.Columns1")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lookUpEditDepotStart.Properties.Columns2"), resources.GetString("lookUpEditDepotStart.Properties.Columns3"))});
            this.lookUpEditDepotStart.Properties.DataSource = this.bindingSourceDepot;
            this.lookUpEditDepotStart.Properties.DisplayMember = "DepotName";
            this.lookUpEditDepotStart.Properties.NullText = resources.GetString("lookUpEditDepotStart.Properties.NullText");
            this.lookUpEditDepotStart.Properties.ValueMember = "DepotId";
            this.lookUpEditDepotStart.StyleController = this.layoutControl1;
            // 
            // txt_InDepotIdEnd
            // 
            resources.ApplyResources(this.txt_InDepotIdEnd, "txt_InDepotIdEnd");
            this.txt_InDepotIdEnd.Name = "txt_InDepotIdEnd";
            this.txt_InDepotIdEnd.StyleController = this.layoutControl1;
            // 
            // txt_InDepotIdStart
            // 
            resources.ApplyResources(this.txt_InDepotIdStart, "txt_InDepotIdStart");
            this.txt_InDepotIdStart.Name = "txt_InDepotIdStart";
            this.txt_InDepotIdStart.StyleController = this.layoutControl1;
            // 
            // date_End
            // 
            this.date_End.EditValue = null;
            resources.ApplyResources(this.date_End, "date_End");
            this.date_End.Name = "date_End";
            this.date_End.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("date_End.Properties.Buttons"))))});
            this.date_End.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_End.StyleController = this.layoutControl1;
            // 
            // date_Start
            // 
            this.date_Start.EditValue = null;
            resources.ApplyResources(this.date_Start, "date_Start");
            this.date_Start.Name = "date_Start";
            this.date_Start.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("date_Start.Properties.Buttons"))))});
            this.date_Start.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.date_Start.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(527, 167);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.date_Start;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(92, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.date_End;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(254, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(253, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(92, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 99);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(507, 48);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txt_InDepotIdStart;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(92, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt_InDepotIdEnd;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(254, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(253, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(92, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lookUpEditDepotStart;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(92, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.lookUpEditDepotEnd;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(254, 50);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(253, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(92, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.nccCustomerStart;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 75);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(253, 24);
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            this.layoutControlItem7.TextToControlDistance = 5;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.nccCustomerEnd;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(253, 75);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem8.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(50, 20);
            this.layoutControlItem8.TextToControlDistance = 5;
            // 
            // InDepotForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "InDepotForm";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            this.Controls.SetChildIndex(this.simpleButtonCancel, 0);
            this.Controls.SetChildIndex(this.simpleButtonOK, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepotEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditDepotStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InDepotIdEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_InDepotIdStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_End.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.date_Start.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourceDepot;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txt_InDepotIdEnd;
        private DevExpress.XtraEditors.TextEdit txt_InDepotIdStart;
        private DevExpress.XtraEditors.DateEdit date_End;
        private DevExpress.XtraEditors.DateEdit date_Start;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Book.UI.Invoices.NewChooseContorl nccCustomerEnd;
        private Book.UI.Invoices.NewChooseContorl nccCustomerStart;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepotEnd;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditDepotStart;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}