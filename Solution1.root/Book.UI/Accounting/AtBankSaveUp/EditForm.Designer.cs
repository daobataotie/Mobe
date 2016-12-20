namespace Book.UI.Accounting.AtBankSaveUp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.memoEditMark = new DevExpress.XtraEditors.MemoEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.newChooseContorlBankId = new Book.UI.Invoices.NewChooseContorl();
            this.spinEditSaveUpMoney = new DevExpress.XtraEditors.SpinEdit();
            this.textEditSaveUpId = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEditSaveUpCategory = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dateEditSaveUpdate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSaveUpMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSaveUpId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSaveUpCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSaveUpdate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSaveUpdate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            resources.ApplyResources(this.imageCollection1, "imageCollection1");
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // bar1
            // 
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemFirst
            // 
            resources.ApplyResources(this.barButtonItemFirst, "barButtonItemFirst");
            this.barButtonItemFirst.Enabled = false;
            // 
            // barButtonItemPrint
            // 
            resources.ApplyResources(this.barButtonItemPrint, "barButtonItemPrint");
            this.barButtonItemPrint.Enabled = false;
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Controls.Add(this.memoEditMark);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.newChooseContorlBankId);
            this.layoutControl1.Controls.Add(this.spinEditSaveUpMoney);
            this.layoutControl1.Controls.Add(this.textEditSaveUpId);
            this.layoutControl1.Controls.Add(this.comboBoxEditSaveUpCategory);
            this.layoutControl1.Controls.Add(this.dateEditSaveUpdate);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // memoEditMark
            // 
            resources.ApplyResources(this.memoEditMark, "memoEditMark");
            this.memoEditMark.Name = "memoEditMark";
            this.memoEditMark.Properties.AccessibleDescription = resources.GetString("memoEditMark.Properties.AccessibleDescription");
            this.memoEditMark.Properties.AccessibleName = resources.GetString("memoEditMark.Properties.AccessibleName");
            this.memoEditMark.Properties.NullValuePrompt = resources.GetString("memoEditMark.Properties.NullValuePrompt");
            this.memoEditMark.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("memoEditMark.Properties.NullValuePromptShowForEmptyValue")));
            this.memoEditMark.StyleController = this.layoutControl1;
            // 
            // gridControl1
            // 
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.DataSource = this.bindingSource1;
            this.gridControl1.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleDescription");
            this.gridControl1.EmbeddedNavigator.AccessibleName = resources.GetString("gridControl1.EmbeddedNavigator.AccessibleName");
            this.gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControl1.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControl1.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControl1.EmbeddedNavigator.Anchor")));
            this.gridControl1.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImage")));
            this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControl1.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode")));
            this.gridControl1.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation")));
            this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
            this.gridControl1.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType")));
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "SaveUpdate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "SaveUpCategory";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "Id";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.DisplayFormat.FormatString = "0";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "SaveUpMoney";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "Bank";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            // 
            // newChooseContorlBankId
            // 
            resources.ApplyResources(this.newChooseContorlBankId, "newChooseContorlBankId");
            this.newChooseContorlBankId.EditValue = null;
            this.newChooseContorlBankId.Name = "newChooseContorlBankId";
            // 
            // spinEditSaveUpMoney
            // 
            resources.ApplyResources(this.spinEditSaveUpMoney, "spinEditSaveUpMoney");
            this.spinEditSaveUpMoney.Name = "spinEditSaveUpMoney";
            this.spinEditSaveUpMoney.Properties.AccessibleDescription = resources.GetString("spinEditSaveUpMoney.Properties.AccessibleDescription");
            this.spinEditSaveUpMoney.Properties.AccessibleName = resources.GetString("spinEditSaveUpMoney.Properties.AccessibleName");
            this.spinEditSaveUpMoney.Properties.AutoHeight = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.AutoHeight")));
            this.spinEditSaveUpMoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditSaveUpMoney.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.AutoComplete")));
            this.spinEditSaveUpMoney.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.BeepOnError")));
            this.spinEditSaveUpMoney.Properties.Mask.EditMask = resources.GetString("spinEditSaveUpMoney.Properties.Mask.EditMask");
            this.spinEditSaveUpMoney.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.IgnoreMaskBlank")));
            this.spinEditSaveUpMoney.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.MaskType")));
            this.spinEditSaveUpMoney.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.PlaceHolder")));
            this.spinEditSaveUpMoney.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.SaveLiteral")));
            this.spinEditSaveUpMoney.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.ShowPlaceHolders")));
            this.spinEditSaveUpMoney.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.Mask.UseMaskAsDisplayFormat")));
            this.spinEditSaveUpMoney.Properties.NullValuePrompt = resources.GetString("spinEditSaveUpMoney.Properties.NullValuePrompt");
            this.spinEditSaveUpMoney.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("spinEditSaveUpMoney.Properties.NullValuePromptShowForEmptyValue")));
            this.spinEditSaveUpMoney.StyleController = this.layoutControl1;
            // 
            // textEditSaveUpId
            // 
            resources.ApplyResources(this.textEditSaveUpId, "textEditSaveUpId");
            this.textEditSaveUpId.Name = "textEditSaveUpId";
            this.textEditSaveUpId.Properties.AccessibleDescription = resources.GetString("textEditSaveUpId.Properties.AccessibleDescription");
            this.textEditSaveUpId.Properties.AccessibleName = resources.GetString("textEditSaveUpId.Properties.AccessibleName");
            this.textEditSaveUpId.Properties.AutoHeight = ((bool)(resources.GetObject("textEditSaveUpId.Properties.AutoHeight")));
            this.textEditSaveUpId.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditSaveUpId.Properties.Mask.AutoComplete")));
            this.textEditSaveUpId.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditSaveUpId.Properties.Mask.BeepOnError")));
            this.textEditSaveUpId.Properties.Mask.EditMask = resources.GetString("textEditSaveUpId.Properties.Mask.EditMask");
            this.textEditSaveUpId.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditSaveUpId.Properties.Mask.IgnoreMaskBlank")));
            this.textEditSaveUpId.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditSaveUpId.Properties.Mask.MaskType")));
            this.textEditSaveUpId.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditSaveUpId.Properties.Mask.PlaceHolder")));
            this.textEditSaveUpId.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditSaveUpId.Properties.Mask.SaveLiteral")));
            this.textEditSaveUpId.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditSaveUpId.Properties.Mask.ShowPlaceHolders")));
            this.textEditSaveUpId.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditSaveUpId.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEditSaveUpId.Properties.NullValuePrompt = resources.GetString("textEditSaveUpId.Properties.NullValuePrompt");
            this.textEditSaveUpId.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("textEditSaveUpId.Properties.NullValuePromptShowForEmptyValue")));
            this.textEditSaveUpId.StyleController = this.layoutControl1;
            // 
            // comboBoxEditSaveUpCategory
            // 
            resources.ApplyResources(this.comboBoxEditSaveUpCategory, "comboBoxEditSaveUpCategory");
            this.comboBoxEditSaveUpCategory.Name = "comboBoxEditSaveUpCategory";
            this.comboBoxEditSaveUpCategory.Properties.AccessibleDescription = resources.GetString("comboBoxEditSaveUpCategory.Properties.AccessibleDescription");
            this.comboBoxEditSaveUpCategory.Properties.AccessibleName = resources.GetString("comboBoxEditSaveUpCategory.Properties.AccessibleName");
            this.comboBoxEditSaveUpCategory.Properties.AutoHeight = ((bool)(resources.GetObject("comboBoxEditSaveUpCategory.Properties.AutoHeight")));
            this.comboBoxEditSaveUpCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("comboBoxEditSaveUpCategory.Properties.Buttons"))))});
            this.comboBoxEditSaveUpCategory.Properties.Items.AddRange(new object[] {
            resources.GetString("comboBoxEditSaveUpCategory.Properties.Items"),
            resources.GetString("comboBoxEditSaveUpCategory.Properties.Items1")});
            this.comboBoxEditSaveUpCategory.Properties.NullValuePrompt = resources.GetString("comboBoxEditSaveUpCategory.Properties.NullValuePrompt");
            this.comboBoxEditSaveUpCategory.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("comboBoxEditSaveUpCategory.Properties.NullValuePromptShowForEmptyValue")));
            this.comboBoxEditSaveUpCategory.StyleController = this.layoutControl1;
            // 
            // dateEditSaveUpdate
            // 
            resources.ApplyResources(this.dateEditSaveUpdate, "dateEditSaveUpdate");
            this.dateEditSaveUpdate.Name = "dateEditSaveUpdate";
            this.dateEditSaveUpdate.Properties.AccessibleDescription = resources.GetString("dateEditSaveUpdate.Properties.AccessibleDescription");
            this.dateEditSaveUpdate.Properties.AccessibleName = resources.GetString("dateEditSaveUpdate.Properties.AccessibleName");
            this.dateEditSaveUpdate.Properties.AutoHeight = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.AutoHeight")));
            this.dateEditSaveUpdate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditSaveUpdate.Properties.Buttons"))))});
            this.dateEditSaveUpdate.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.AutoComplete")));
            this.dateEditSaveUpdate.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.BeepOnError")));
            this.dateEditSaveUpdate.Properties.Mask.EditMask = resources.GetString("dateEditSaveUpdate.Properties.Mask.EditMask");
            this.dateEditSaveUpdate.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.IgnoreMaskBlank")));
            this.dateEditSaveUpdate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.MaskType")));
            this.dateEditSaveUpdate.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.PlaceHolder")));
            this.dateEditSaveUpdate.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.SaveLiteral")));
            this.dateEditSaveUpdate.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.ShowPlaceHolders")));
            this.dateEditSaveUpdate.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditSaveUpdate.Properties.NullValuePrompt = resources.GetString("dateEditSaveUpdate.Properties.NullValuePrompt");
            this.dateEditSaveUpdate.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.NullValuePromptShowForEmptyValue")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.AccessibleDescription = resources.GetString("dateEditSaveUpdate.Properties.VistaTimeProperties.AccessibleDescription");
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.AccessibleName = resources.GetString("dateEditSaveUpdate.Properties.VistaTimeProperties.AccessibleName");
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.AutoHeight = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.AutoHeight")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.AutoComplete")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.BeepOnError")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.EditMask = resources.GetString("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.EditMask");
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.MaskType")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.PlaceHolder")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.SaveLiteral")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.NullValuePrompt = resources.GetString("dateEditSaveUpdate.Properties.VistaTimeProperties.NullValuePrompt");
            this.dateEditSaveUpdate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditSaveUpdate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValu" +
                    "e")));
            this.dateEditSaveUpdate.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(708, 361);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEditSaveUpdate;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(343, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.comboBoxEditSaveUpCategory;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(343, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(345, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditSaveUpId;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(343, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.spinEditSaveUpMoney;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(343, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(345, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.newChooseContorlBankId;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(343, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 74);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(688, 267);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.memoEditMark;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(343, 50);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(345, 24);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(48, 14);
            // 
            // EditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "EditForm";
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditSaveUpMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSaveUpId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSaveUpCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSaveUpdate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditSaveUpdate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.MemoEdit memoEditMark;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private Book.UI.Invoices.NewChooseContorl newChooseContorlBankId;
        private DevExpress.XtraEditors.SpinEdit spinEditSaveUpMoney;
        private DevExpress.XtraEditors.TextEdit textEditSaveUpId;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSaveUpCategory;
        private DevExpress.XtraEditors.DateEdit dateEditSaveUpdate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
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
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}