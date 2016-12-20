namespace Book.UI.Invoices.QO
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditNote = new DevExpress.XtraEditors.MemoEdit();
            this.calcEditTotal = new DevExpress.XtraEditors.CalcEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOutgoingKindId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceQODetailMoney = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceQODetailNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buttonEditAccount = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditEmployee = new DevExpress.XtraEditors.ButtonEdit();
            this.dateEditInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.textEditInvoiceId = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditAccount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Controls.Add(this.textEditNote);
            this.layoutControl1.Controls.Add(this.calcEditTotal);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.buttonEditAccount);
            this.layoutControl1.Controls.Add(this.buttonEditEmployee);
            this.layoutControl1.Controls.Add(this.dateEditInvoiceDate);
            this.layoutControl1.Controls.Add(this.textEditInvoiceId);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // textEditNote
            // 
            resources.ApplyResources(this.textEditNote, "textEditNote");
            this.textEditNote.Name = "textEditNote";
            this.textEditNote.Properties.AccessibleDescription = resources.GetString("textEditNote.Properties.AccessibleDescription");
            this.textEditNote.Properties.AccessibleName = resources.GetString("textEditNote.Properties.AccessibleName");
            this.textEditNote.Properties.NullValuePrompt = resources.GetString("textEditNote.Properties.NullValuePrompt");
            this.textEditNote.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("textEditNote.Properties.NullValuePromptShowForEmptyValue")));
            this.textEditNote.StyleController = this.layoutControl1;
            // 
            // calcEditTotal
            // 
            resources.ApplyResources(this.calcEditTotal, "calcEditTotal");
            this.calcEditTotal.Name = "calcEditTotal";
            this.calcEditTotal.Properties.AccessibleDescription = resources.GetString("calcEditTotal.Properties.AccessibleDescription");
            this.calcEditTotal.Properties.AccessibleName = resources.GetString("calcEditTotal.Properties.AccessibleName");
            this.calcEditTotal.Properties.AutoHeight = ((bool)(resources.GetObject("calcEditTotal.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject1, "serializableAppearanceObject1");
            this.calcEditTotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("calcEditTotal.Properties.Buttons"))), resources.GetString("calcEditTotal.Properties.Buttons1"), ((int)(resources.GetObject("calcEditTotal.Properties.Buttons2"))), ((bool)(resources.GetObject("calcEditTotal.Properties.Buttons3"))), ((bool)(resources.GetObject("calcEditTotal.Properties.Buttons4"))), ((bool)(resources.GetObject("calcEditTotal.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("calcEditTotal.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("calcEditTotal.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("calcEditTotal.Properties.Buttons8"), ((object)(resources.GetObject("calcEditTotal.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("calcEditTotal.Properties.Buttons10"))), ((bool)(resources.GetObject("calcEditTotal.Properties.Buttons11"))))});
            this.calcEditTotal.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("calcEditTotal.Properties.Mask.AutoComplete")));
            this.calcEditTotal.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("calcEditTotal.Properties.Mask.BeepOnError")));
            this.calcEditTotal.Properties.Mask.EditMask = resources.GetString("calcEditTotal.Properties.Mask.EditMask");
            this.calcEditTotal.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("calcEditTotal.Properties.Mask.IgnoreMaskBlank")));
            this.calcEditTotal.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("calcEditTotal.Properties.Mask.MaskType")));
            this.calcEditTotal.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("calcEditTotal.Properties.Mask.PlaceHolder")));
            this.calcEditTotal.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("calcEditTotal.Properties.Mask.SaveLiteral")));
            this.calcEditTotal.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("calcEditTotal.Properties.Mask.ShowPlaceHolders")));
            this.calcEditTotal.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("calcEditTotal.Properties.Mask.UseMaskAsDisplayFormat")));
            this.calcEditTotal.Properties.NullValuePrompt = resources.GetString("calcEditTotal.Properties.NullValuePrompt");
            this.calcEditTotal.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("calcEditTotal.Properties.NullValuePromptShowForEmptyValue")));
            this.calcEditTotal.Properties.ReadOnly = true;
            this.calcEditTotal.StyleController = this.layoutControl1;
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
            this.colOutgoingKindId,
            this.colInvoiceQODetailMoney,
            this.colInvoiceQODetailNote});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colOutgoingKindId
            // 
            this.colOutgoingKindId.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colOutgoingKindId.AppearanceCell.GradientMode")));
            this.colOutgoingKindId.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colOutgoingKindId.AppearanceCell.Image")));
            this.colOutgoingKindId.AppearanceCell.Options.UseTextOptions = true;
            this.colOutgoingKindId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colOutgoingKindId.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colOutgoingKindId.AppearanceHeader.GradientMode")));
            this.colOutgoingKindId.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colOutgoingKindId.AppearanceHeader.Image")));
            this.colOutgoingKindId.AppearanceHeader.Options.UseTextOptions = true;
            this.colOutgoingKindId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colOutgoingKindId, "colOutgoingKindId");
            this.colOutgoingKindId.FieldName = "OutgoingKind";
            this.colOutgoingKindId.Name = "colOutgoingKindId";
            this.colOutgoingKindId.OptionsColumn.AllowEdit = false;
            this.colOutgoingKindId.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceQODetailMoney
            // 
            this.colInvoiceQODetailMoney.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceQODetailMoney.AppearanceCell.GradientMode")));
            this.colInvoiceQODetailMoney.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceQODetailMoney.AppearanceCell.Image")));
            this.colInvoiceQODetailMoney.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceQODetailMoney.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceQODetailMoney.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceQODetailMoney.AppearanceHeader.GradientMode")));
            this.colInvoiceQODetailMoney.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceQODetailMoney.AppearanceHeader.Image")));
            this.colInvoiceQODetailMoney.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceQODetailMoney.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceQODetailMoney, "colInvoiceQODetailMoney");
            this.colInvoiceQODetailMoney.DisplayFormat.FormatString = "0";
            this.colInvoiceQODetailMoney.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceQODetailMoney.FieldName = "InvoiceQODetailMoney";
            this.colInvoiceQODetailMoney.Name = "colInvoiceQODetailMoney";
            this.colInvoiceQODetailMoney.OptionsColumn.AllowEdit = false;
            this.colInvoiceQODetailMoney.OptionsColumn.ReadOnly = true;
            // 
            // colInvoiceQODetailNote
            // 
            this.colInvoiceQODetailNote.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceQODetailNote.AppearanceHeader.GradientMode")));
            this.colInvoiceQODetailNote.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceQODetailNote.AppearanceHeader.Image")));
            this.colInvoiceQODetailNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceQODetailNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceQODetailNote, "colInvoiceQODetailNote");
            this.colInvoiceQODetailNote.FieldName = "InvoiceQODetailNote";
            this.colInvoiceQODetailNote.Name = "colInvoiceQODetailNote";
            this.colInvoiceQODetailNote.OptionsColumn.AllowEdit = false;
            this.colInvoiceQODetailNote.OptionsColumn.ReadOnly = true;
            // 
            // buttonEditAccount
            // 
            resources.ApplyResources(this.buttonEditAccount, "buttonEditAccount");
            this.buttonEditAccount.Name = "buttonEditAccount";
            this.buttonEditAccount.Properties.AccessibleDescription = resources.GetString("buttonEditAccount.Properties.AccessibleDescription");
            this.buttonEditAccount.Properties.AccessibleName = resources.GetString("buttonEditAccount.Properties.AccessibleName");
            this.buttonEditAccount.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditAccount.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject2, "serializableAppearanceObject2");
            this.buttonEditAccount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditAccount.Properties.Buttons"))), resources.GetString("buttonEditAccount.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditAccount.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditAccount.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditAccount.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditAccount.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditAccount.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditAccount.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("buttonEditAccount.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditAccount.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditAccount.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditAccount.Properties.Buttons11"))))});
            this.buttonEditAccount.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("buttonEditAccount.Properties.Mask.AutoComplete")));
            this.buttonEditAccount.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("buttonEditAccount.Properties.Mask.BeepOnError")));
            this.buttonEditAccount.Properties.Mask.EditMask = resources.GetString("buttonEditAccount.Properties.Mask.EditMask");
            this.buttonEditAccount.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("buttonEditAccount.Properties.Mask.IgnoreMaskBlank")));
            this.buttonEditAccount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("buttonEditAccount.Properties.Mask.MaskType")));
            this.buttonEditAccount.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("buttonEditAccount.Properties.Mask.PlaceHolder")));
            this.buttonEditAccount.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("buttonEditAccount.Properties.Mask.SaveLiteral")));
            this.buttonEditAccount.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("buttonEditAccount.Properties.Mask.ShowPlaceHolders")));
            this.buttonEditAccount.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("buttonEditAccount.Properties.Mask.UseMaskAsDisplayFormat")));
            this.buttonEditAccount.Properties.NullValuePrompt = resources.GetString("buttonEditAccount.Properties.NullValuePrompt");
            this.buttonEditAccount.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("buttonEditAccount.Properties.NullValuePromptShowForEmptyValue")));
            this.buttonEditAccount.Properties.ReadOnly = true;
            this.buttonEditAccount.StyleController = this.layoutControl1;
            // 
            // buttonEditEmployee
            // 
            resources.ApplyResources(this.buttonEditEmployee, "buttonEditEmployee");
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Properties.AccessibleDescription = resources.GetString("buttonEditEmployee.Properties.AccessibleDescription");
            this.buttonEditEmployee.Properties.AccessibleName = resources.GetString("buttonEditEmployee.Properties.AccessibleName");
            this.buttonEditEmployee.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditEmployee.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject3, "serializableAppearanceObject3");
            this.buttonEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditEmployee.Properties.Buttons"))), resources.GetString("buttonEditEmployee.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditEmployee.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditEmployee.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditEmployee.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("buttonEditEmployee.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditEmployee.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditEmployee.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons11"))))});
            this.buttonEditEmployee.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("buttonEditEmployee.Properties.Mask.AutoComplete")));
            this.buttonEditEmployee.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("buttonEditEmployee.Properties.Mask.BeepOnError")));
            this.buttonEditEmployee.Properties.Mask.EditMask = resources.GetString("buttonEditEmployee.Properties.Mask.EditMask");
            this.buttonEditEmployee.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("buttonEditEmployee.Properties.Mask.IgnoreMaskBlank")));
            this.buttonEditEmployee.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("buttonEditEmployee.Properties.Mask.MaskType")));
            this.buttonEditEmployee.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("buttonEditEmployee.Properties.Mask.PlaceHolder")));
            this.buttonEditEmployee.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("buttonEditEmployee.Properties.Mask.SaveLiteral")));
            this.buttonEditEmployee.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("buttonEditEmployee.Properties.Mask.ShowPlaceHolders")));
            this.buttonEditEmployee.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("buttonEditEmployee.Properties.Mask.UseMaskAsDisplayFormat")));
            this.buttonEditEmployee.Properties.NullValuePrompt = resources.GetString("buttonEditEmployee.Properties.NullValuePrompt");
            this.buttonEditEmployee.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("buttonEditEmployee.Properties.NullValuePromptShowForEmptyValue")));
            this.buttonEditEmployee.Properties.ReadOnly = true;
            this.buttonEditEmployee.StyleController = this.layoutControl1;
            // 
            // dateEditInvoiceDate
            // 
            resources.ApplyResources(this.dateEditInvoiceDate, "dateEditInvoiceDate");
            this.dateEditInvoiceDate.Name = "dateEditInvoiceDate";
            this.dateEditInvoiceDate.Properties.AccessibleDescription = resources.GetString("dateEditInvoiceDate.Properties.AccessibleDescription");
            this.dateEditInvoiceDate.Properties.AccessibleName = resources.GetString("dateEditInvoiceDate.Properties.AccessibleName");
            this.dateEditInvoiceDate.Properties.AutoHeight = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject4, "serializableAppearanceObject4");
            this.dateEditInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons"))), resources.GetString("dateEditInvoiceDate.Properties.Buttons1"), ((int)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons2"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons3"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons4"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("dateEditInvoiceDate.Properties.Buttons8"), ((object)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons10"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons11"))))});
            this.dateEditInvoiceDate.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.AutoComplete")));
            this.dateEditInvoiceDate.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.BeepOnError")));
            this.dateEditInvoiceDate.Properties.Mask.EditMask = resources.GetString("dateEditInvoiceDate.Properties.Mask.EditMask");
            this.dateEditInvoiceDate.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.IgnoreMaskBlank")));
            this.dateEditInvoiceDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.MaskType")));
            this.dateEditInvoiceDate.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.PlaceHolder")));
            this.dateEditInvoiceDate.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.SaveLiteral")));
            this.dateEditInvoiceDate.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.ShowPlaceHolders")));
            this.dateEditInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditInvoiceDate.Properties.NullValuePrompt = resources.GetString("dateEditInvoiceDate.Properties.NullValuePrompt");
            this.dateEditInvoiceDate.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.NullValuePromptShowForEmptyValue")));
            this.dateEditInvoiceDate.Properties.ReadOnly = true;
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.AccessibleDescription = resources.GetString("dateEditInvoiceDate.Properties.VistaTimeProperties.AccessibleDescription");
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.AccessibleName = resources.GetString("dateEditInvoiceDate.Properties.VistaTimeProperties.AccessibleName");
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.AutoHeight = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.AutoHeight")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.AutoComplete")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.BeepOnError")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.EditMask = resources.GetString("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.EditMask");
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.MaskType")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.PlaceHolder")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.SaveLiteral")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.NullValuePrompt = resources.GetString("dateEditInvoiceDate.Properties.VistaTimeProperties.NullValuePrompt");
            this.dateEditInvoiceDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyVal" +
                    "ue")));
            this.dateEditInvoiceDate.StyleController = this.layoutControl1;
            // 
            // textEditInvoiceId
            // 
            resources.ApplyResources(this.textEditInvoiceId, "textEditInvoiceId");
            this.textEditInvoiceId.Name = "textEditInvoiceId";
            this.textEditInvoiceId.Properties.AccessibleDescription = resources.GetString("textEditInvoiceId.Properties.AccessibleDescription");
            this.textEditInvoiceId.Properties.AccessibleName = resources.GetString("textEditInvoiceId.Properties.AccessibleName");
            this.textEditInvoiceId.Properties.AutoHeight = ((bool)(resources.GetObject("textEditInvoiceId.Properties.AutoHeight")));
            this.textEditInvoiceId.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditInvoiceId.Properties.Mask.AutoComplete")));
            this.textEditInvoiceId.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditInvoiceId.Properties.Mask.BeepOnError")));
            this.textEditInvoiceId.Properties.Mask.EditMask = resources.GetString("textEditInvoiceId.Properties.Mask.EditMask");
            this.textEditInvoiceId.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditInvoiceId.Properties.Mask.IgnoreMaskBlank")));
            this.textEditInvoiceId.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditInvoiceId.Properties.Mask.MaskType")));
            this.textEditInvoiceId.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditInvoiceId.Properties.Mask.PlaceHolder")));
            this.textEditInvoiceId.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditInvoiceId.Properties.Mask.SaveLiteral")));
            this.textEditInvoiceId.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditInvoiceId.Properties.Mask.ShowPlaceHolders")));
            this.textEditInvoiceId.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditInvoiceId.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEditInvoiceId.Properties.NullValuePrompt = resources.GetString("textEditInvoiceId.Properties.NullValuePrompt");
            this.textEditInvoiceId.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("textEditInvoiceId.Properties.NullValuePromptShowForEmptyValue")));
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.textEditInvoiceId.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem8,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(924, 516);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditInvoiceId;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(300, 25);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(904, 343);
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditInvoiceDate;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(623, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(281, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.calcEditTotal;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(300, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditNote;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 393);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(904, 103);
            this.layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.buttonEditAccount;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(300, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(323, 25);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(52, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEditEmployee;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(300, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(323, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(52, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(623, 25);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(281, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ViewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ViewForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEditTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditAccount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ButtonEdit buttonEditEmployee;
        private DevExpress.XtraEditors.DateEdit dateEditInvoiceDate;
        private DevExpress.XtraEditors.TextEdit textEditInvoiceId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.ButtonEdit buttonEditAccount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.CalcEdit calcEditTotal;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colOutgoingKindId;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceQODetailMoney;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceQODetailNote;
        private DevExpress.XtraEditors.MemoEdit textEditNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}