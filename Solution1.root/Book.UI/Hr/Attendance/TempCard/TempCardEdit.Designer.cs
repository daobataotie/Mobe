namespace Book.UI.Hr.Attendance.TempCard
{
    partial class TempCardEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TempCardEdit));
            this.TempCardSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.EmployeeSource = new System.Windows.Forms.BindingSource(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.EmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.dateEdit_DutyDate = new DevExpress.XtraEditors.DateEdit();
            this.textEdit_CardNo = new DevExpress.XtraEditors.TextEdit();
            this.newChooseEmployeeId = new Book.UI.Invoices.NewChooseContorl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempCardSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DutyDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DutyDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CardNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 18;
            this.barManager1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // imageCollection1
            // 
            resources.ApplyResources(this.imageCollection1, "imageCollection1");
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // bar1
            // 
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemFirst
            // 
            this.barButtonItemFirst.AccessibleDescription = null;
            this.barButtonItemFirst.AccessibleName = null;
            resources.ApplyResources(this.barButtonItemFirst, "barButtonItemFirst");
            this.barButtonItemFirst.Enabled = false;
            // 
            // barButtonItemPrint
            // 
            this.barButtonItemPrint.AccessibleDescription = null;
            this.barButtonItemPrint.AccessibleName = null;
            resources.ApplyResources(this.barButtonItemPrint, "barButtonItemPrint");
            this.barButtonItemPrint.Enabled = false;
            // 
            // TempCardSource1
            // 
            this.TempCardSource1.CurrentChanged += new System.EventHandler(this.TempCardSource1_CurrentChanged);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // layoutControl1
            // 
            this.layoutControl1.AccessibleDescription = null;
            this.layoutControl1.AccessibleName = null;
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.BackgroundImage = null;
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.dateEdit_DutyDate);
            this.layoutControl1.Controls.Add(this.textEdit_CardNo);
            this.layoutControl1.Controls.Add(this.newChooseEmployeeId);
            this.layoutControl1.Font = null;
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gridControl1
            // 
            this.gridControl1.AccessibleDescription = null;
            this.gridControl1.AccessibleName = null;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.BackgroundImage = null;
            this.gridControl1.DataSource = this.TempCardSource1;
            this.gridControl1.EmbeddedNavigator.AccessibleDescription = null;
            this.gridControl1.EmbeddedNavigator.AccessibleName = null;
            this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControl1.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControl1.EmbeddedNavigator.Anchor")));
            this.gridControl1.EmbeddedNavigator.BackgroundImage = null;
            this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControl1.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode")));
            this.gridControl1.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation")));
            this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
            this.gridControl1.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType")));
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
            this.gridControl1.Font = null;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.EmployeeName,
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFind.AlwaysVisible = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // EmployeeName
            // 
            resources.ApplyResources(this.EmployeeName, "EmployeeName");
            this.EmployeeName.FieldName = "Employee";
            this.EmployeeName.Name = "EmployeeName";
            this.EmployeeName.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "CardNo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "DutyDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AccessibleDescription = null;
            this.repositoryItemLookUpEdit1.AccessibleName = null;
            resources.ApplyResources(this.repositoryItemLookUpEdit1, "repositoryItemLookUpEdit1");
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemLookUpEdit1.Buttons"))))});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit1.Columns"), resources.GetString("repositoryItemLookUpEdit1.Columns1"), ((int)(resources.GetObject("repositoryItemLookUpEdit1.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("repositoryItemLookUpEdit1.Columns3"))), resources.GetString("repositoryItemLookUpEdit1.Columns4"), ((bool)(resources.GetObject("repositoryItemLookUpEdit1.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("repositoryItemLookUpEdit1.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("repositoryItemLookUpEdit1.Columns7"), ((int)(resources.GetObject("repositoryItemLookUpEdit1.Columns8"))), resources.GetString("repositoryItemLookUpEdit1.Columns9"))});
            this.repositoryItemLookUpEdit1.DataSource = this.EmployeeSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "EmployeeName";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemLookUpEdit1.ValueMember = "EmployeeId";
            // 
            // dateEdit_DutyDate
            // 
            resources.ApplyResources(this.dateEdit_DutyDate, "dateEdit_DutyDate");
            this.dateEdit_DutyDate.BackgroundImage = null;
            this.dateEdit_DutyDate.EditValue = null;
            this.dateEdit_DutyDate.Name = "dateEdit_DutyDate";
            this.dateEdit_DutyDate.Properties.AccessibleDescription = null;
            this.dateEdit_DutyDate.Properties.AccessibleName = null;
            this.dateEdit_DutyDate.Properties.AutoHeight = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.AutoHeight")));
            this.dateEdit_DutyDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEdit_DutyDate.Properties.Buttons"))))});
            this.dateEdit_DutyDate.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.AutoComplete")));
            this.dateEdit_DutyDate.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.BeepOnError")));
            this.dateEdit_DutyDate.Properties.Mask.EditMask = resources.GetString("dateEdit_DutyDate.Properties.Mask.EditMask");
            this.dateEdit_DutyDate.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.IgnoreMaskBlank")));
            this.dateEdit_DutyDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.MaskType")));
            this.dateEdit_DutyDate.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.PlaceHolder")));
            this.dateEdit_DutyDate.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.SaveLiteral")));
            this.dateEdit_DutyDate.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.ShowPlaceHolders")));
            this.dateEdit_DutyDate.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.Mask.UseMaskAsDisplayFormat")));
            this.dateEdit_DutyDate.Properties.NullValuePrompt = resources.GetString("dateEdit_DutyDate.Properties.NullValuePrompt");
            this.dateEdit_DutyDate.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.NullValuePromptShowForEmptyValue")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.AccessibleDescription = null;
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.AccessibleName = null;
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.AutoHeight = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.AutoHeight")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.AutoComplete")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.BeepOnError")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.EditMask = resources.GetString("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.EditMask");
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.MaskType")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.PlaceHolder")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.SaveLiteral")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat")));
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.NullValuePrompt = resources.GetString("dateEdit_DutyDate.Properties.VistaTimeProperties.NullValuePrompt");
            this.dateEdit_DutyDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEdit_DutyDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue" +
                    "")));
            this.dateEdit_DutyDate.StyleController = this.layoutControl1;
            // 
            // textEdit_CardNo
            // 
            resources.ApplyResources(this.textEdit_CardNo, "textEdit_CardNo");
            this.textEdit_CardNo.BackgroundImage = null;
            this.textEdit_CardNo.EditValue = null;
            this.textEdit_CardNo.Name = "textEdit_CardNo";
            this.textEdit_CardNo.Properties.AccessibleDescription = null;
            this.textEdit_CardNo.Properties.AccessibleName = null;
            this.textEdit_CardNo.Properties.AutoHeight = ((bool)(resources.GetObject("textEdit_CardNo.Properties.AutoHeight")));
            this.textEdit_CardNo.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEdit_CardNo.Properties.Mask.AutoComplete")));
            this.textEdit_CardNo.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEdit_CardNo.Properties.Mask.BeepOnError")));
            this.textEdit_CardNo.Properties.Mask.EditMask = resources.GetString("textEdit_CardNo.Properties.Mask.EditMask");
            this.textEdit_CardNo.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEdit_CardNo.Properties.Mask.IgnoreMaskBlank")));
            this.textEdit_CardNo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEdit_CardNo.Properties.Mask.MaskType")));
            this.textEdit_CardNo.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEdit_CardNo.Properties.Mask.PlaceHolder")));
            this.textEdit_CardNo.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEdit_CardNo.Properties.Mask.SaveLiteral")));
            this.textEdit_CardNo.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEdit_CardNo.Properties.Mask.ShowPlaceHolders")));
            this.textEdit_CardNo.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEdit_CardNo.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEdit_CardNo.Properties.NullValuePrompt = resources.GetString("textEdit_CardNo.Properties.NullValuePrompt");
            this.textEdit_CardNo.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("textEdit_CardNo.Properties.NullValuePromptShowForEmptyValue")));
            this.textEdit_CardNo.StyleController = this.layoutControl1;
            // 
            // newChooseEmployeeId
            // 
            this.newChooseEmployeeId.AccessibleDescription = null;
            this.newChooseEmployeeId.AccessibleName = null;
            resources.ApplyResources(this.newChooseEmployeeId, "newChooseEmployeeId");
            this.newChooseEmployeeId.BackgroundImage = null;
            this.newChooseEmployeeId.EditValue = null;
            this.newChooseEmployeeId.Font = null;
            this.newChooseEmployeeId.Name = "newChooseEmployeeId";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(729, 369);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(709, 324);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEdit_DutyDate;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(490, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(219, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(64, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit_CardNo;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(274, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(216, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(64, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.newChooseEmployeeId;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(274, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(64, 14);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.AccessibleDescription = null;
            this.barButtonItem1.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 13;
            this.barButtonItem1.ImageIndex = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.AccessibleDescription = null;
            this.barButtonItem2.AccessibleName = null;
            this.barButtonItem2.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            resources.ApplyResources(this.barButtonItem2, "barButtonItem2");
            this.barButtonItem2.DropDownControl = this.popupMenu1;
            this.barButtonItem2.Id = 14;
            this.barButtonItem2.ImageIndex = 8;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.Tag = "export";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.AccessibleDescription = null;
            this.barButtonItem3.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem3, "barButtonItem3");
            this.barButtonItem3.Id = 15;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.Tag = "pdf";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.AccessibleDescription = null;
            this.barButtonItem4.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem4, "barButtonItem4");
            this.barButtonItem4.Id = 16;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.Tag = "xls";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.AccessibleDescription = null;
            this.barButtonItem5.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
            this.barButtonItem5.Id = 17;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.Tag = "doc";
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // TempCardEdit
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Icon = null;
            this.Name = "TempCardEdit";
            this.Load += new System.EventHandler(this.TempCardEdit_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TempCardSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmployeeSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DutyDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_DutyDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CardNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.BindingSource EmployeeSource;
        private System.Windows.Forms.BindingSource TempCardSource1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn EmployeeName;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.DateEdit dateEdit_DutyDate;
        private DevExpress.XtraEditors.TextEdit textEdit_CardNo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Book.UI.Invoices.NewChooseContorl newChooseEmployeeId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}