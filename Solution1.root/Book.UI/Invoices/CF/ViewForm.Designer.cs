namespace Book.UI.Invoices.CF
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
            this.textEditInvoiceId = new DevExpress.XtraEditors.TextEdit();
            this.dateEditInvoiceDate = new DevExpress.XtraEditors.DateEdit();
            this.buttonEditEmployee = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditDepot1 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEditDepot0 = new DevExpress.XtraEditors.ButtonEdit();
            this.gridControlOut = new DevExpress.XtraGrid.GridControl();
            this.bindingSourceOut = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewOut = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cloProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZZDetailQuantity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZZDetailNote1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControlIn = new DevExpress.XtraGrid.GridControl();
            this.bindingSourceIn = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewIn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZZDetailQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInvoiceZZDetailNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Controls.Add(this.textEditNote);
            this.layoutControl1.Controls.Add(this.textEditInvoiceId);
            this.layoutControl1.Controls.Add(this.dateEditInvoiceDate);
            this.layoutControl1.Controls.Add(this.buttonEditEmployee);
            this.layoutControl1.Controls.Add(this.buttonEditDepot1);
            this.layoutControl1.Controls.Add(this.buttonEditDepot0);
            this.layoutControl1.Controls.Add(this.gridControlOut);
            this.layoutControl1.Controls.Add(this.gridControlIn);
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
            this.textEditNote.Properties.ReadOnly = true;
            this.textEditNote.StyleController = this.layoutControl1;
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
            // dateEditInvoiceDate
            // 
            resources.ApplyResources(this.dateEditInvoiceDate, "dateEditInvoiceDate");
            this.dateEditInvoiceDate.Name = "dateEditInvoiceDate";
            this.dateEditInvoiceDate.Properties.AccessibleDescription = resources.GetString("dateEditInvoiceDate.Properties.AccessibleDescription");
            this.dateEditInvoiceDate.Properties.AccessibleName = resources.GetString("dateEditInvoiceDate.Properties.AccessibleName");
            this.dateEditInvoiceDate.Properties.AutoHeight = ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject1, "serializableAppearanceObject1");
            this.dateEditInvoiceDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons"))), resources.GetString("dateEditInvoiceDate.Properties.Buttons1"), ((int)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons2"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons3"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons4"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("dateEditInvoiceDate.Properties.Buttons8"), ((object)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons10"))), ((bool)(resources.GetObject("dateEditInvoiceDate.Properties.Buttons11"))))});
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
            // buttonEditEmployee
            // 
            resources.ApplyResources(this.buttonEditEmployee, "buttonEditEmployee");
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Properties.AccessibleDescription = resources.GetString("buttonEditEmployee.Properties.AccessibleDescription");
            this.buttonEditEmployee.Properties.AccessibleName = resources.GetString("buttonEditEmployee.Properties.AccessibleName");
            this.buttonEditEmployee.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditEmployee.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject2, "serializableAppearanceObject2");
            this.buttonEditEmployee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditEmployee.Properties.Buttons"))), resources.GetString("buttonEditEmployee.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditEmployee.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditEmployee.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditEmployee.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("buttonEditEmployee.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditEmployee.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditEmployee.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditEmployee.Properties.Buttons11"))))});
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
            // buttonEditDepot1
            // 
            resources.ApplyResources(this.buttonEditDepot1, "buttonEditDepot1");
            this.buttonEditDepot1.Name = "buttonEditDepot1";
            this.buttonEditDepot1.Properties.AccessibleDescription = resources.GetString("buttonEditDepot1.Properties.AccessibleDescription");
            this.buttonEditDepot1.Properties.AccessibleName = resources.GetString("buttonEditDepot1.Properties.AccessibleName");
            this.buttonEditDepot1.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditDepot1.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject3, "serializableAppearanceObject3");
            this.buttonEditDepot1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditDepot1.Properties.Buttons"))), resources.GetString("buttonEditDepot1.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditDepot1.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditDepot1.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditDepot1.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditDepot1.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditDepot1.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditDepot1.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("buttonEditDepot1.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditDepot1.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditDepot1.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditDepot1.Properties.Buttons11"))))});
            this.buttonEditDepot1.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("buttonEditDepot1.Properties.Mask.AutoComplete")));
            this.buttonEditDepot1.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("buttonEditDepot1.Properties.Mask.BeepOnError")));
            this.buttonEditDepot1.Properties.Mask.EditMask = resources.GetString("buttonEditDepot1.Properties.Mask.EditMask");
            this.buttonEditDepot1.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("buttonEditDepot1.Properties.Mask.IgnoreMaskBlank")));
            this.buttonEditDepot1.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("buttonEditDepot1.Properties.Mask.MaskType")));
            this.buttonEditDepot1.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("buttonEditDepot1.Properties.Mask.PlaceHolder")));
            this.buttonEditDepot1.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("buttonEditDepot1.Properties.Mask.SaveLiteral")));
            this.buttonEditDepot1.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("buttonEditDepot1.Properties.Mask.ShowPlaceHolders")));
            this.buttonEditDepot1.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("buttonEditDepot1.Properties.Mask.UseMaskAsDisplayFormat")));
            this.buttonEditDepot1.Properties.NullValuePrompt = resources.GetString("buttonEditDepot1.Properties.NullValuePrompt");
            this.buttonEditDepot1.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("buttonEditDepot1.Properties.NullValuePromptShowForEmptyValue")));
            this.buttonEditDepot1.Properties.ReadOnly = true;
            this.buttonEditDepot1.StyleController = this.layoutControl1;
            // 
            // buttonEditDepot0
            // 
            resources.ApplyResources(this.buttonEditDepot0, "buttonEditDepot0");
            this.buttonEditDepot0.Name = "buttonEditDepot0";
            this.buttonEditDepot0.Properties.AccessibleDescription = resources.GetString("buttonEditDepot0.Properties.AccessibleDescription");
            this.buttonEditDepot0.Properties.AccessibleName = resources.GetString("buttonEditDepot0.Properties.AccessibleName");
            this.buttonEditDepot0.Properties.AutoHeight = ((bool)(resources.GetObject("buttonEditDepot0.Properties.AutoHeight")));
            resources.ApplyResources(serializableAppearanceObject4, "serializableAppearanceObject4");
            this.buttonEditDepot0.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("buttonEditDepot0.Properties.Buttons"))), resources.GetString("buttonEditDepot0.Properties.Buttons1"), ((int)(resources.GetObject("buttonEditDepot0.Properties.Buttons2"))), ((bool)(resources.GetObject("buttonEditDepot0.Properties.Buttons3"))), ((bool)(resources.GetObject("buttonEditDepot0.Properties.Buttons4"))), ((bool)(resources.GetObject("buttonEditDepot0.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("buttonEditDepot0.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("buttonEditDepot0.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("buttonEditDepot0.Properties.Buttons8"), ((object)(resources.GetObject("buttonEditDepot0.Properties.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("buttonEditDepot0.Properties.Buttons10"))), ((bool)(resources.GetObject("buttonEditDepot0.Properties.Buttons11"))))});
            this.buttonEditDepot0.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("buttonEditDepot0.Properties.Mask.AutoComplete")));
            this.buttonEditDepot0.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("buttonEditDepot0.Properties.Mask.BeepOnError")));
            this.buttonEditDepot0.Properties.Mask.EditMask = resources.GetString("buttonEditDepot0.Properties.Mask.EditMask");
            this.buttonEditDepot0.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("buttonEditDepot0.Properties.Mask.IgnoreMaskBlank")));
            this.buttonEditDepot0.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("buttonEditDepot0.Properties.Mask.MaskType")));
            this.buttonEditDepot0.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("buttonEditDepot0.Properties.Mask.PlaceHolder")));
            this.buttonEditDepot0.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("buttonEditDepot0.Properties.Mask.SaveLiteral")));
            this.buttonEditDepot0.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("buttonEditDepot0.Properties.Mask.ShowPlaceHolders")));
            this.buttonEditDepot0.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("buttonEditDepot0.Properties.Mask.UseMaskAsDisplayFormat")));
            this.buttonEditDepot0.Properties.NullValuePrompt = resources.GetString("buttonEditDepot0.Properties.NullValuePrompt");
            this.buttonEditDepot0.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("buttonEditDepot0.Properties.NullValuePromptShowForEmptyValue")));
            this.buttonEditDepot0.Properties.ReadOnly = true;
            this.buttonEditDepot0.StyleController = this.layoutControl1;
            // 
            // gridControlOut
            // 
            resources.ApplyResources(this.gridControlOut, "gridControlOut");
            this.gridControlOut.DataSource = this.bindingSourceOut;
            this.gridControlOut.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlOut.EmbeddedNavigator.AccessibleDescription");
            this.gridControlOut.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlOut.EmbeddedNavigator.AccessibleName");
            this.gridControlOut.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlOut.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlOut.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlOut.EmbeddedNavigator.Anchor")));
            this.gridControlOut.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlOut.EmbeddedNavigator.BackgroundImage")));
            this.gridControlOut.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlOut.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlOut.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlOut.EmbeddedNavigator.ImeMode")));
            this.gridControlOut.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlOut.EmbeddedNavigator.TextLocation")));
            this.gridControlOut.EmbeddedNavigator.ToolTip = resources.GetString("gridControlOut.EmbeddedNavigator.ToolTip");
            this.gridControlOut.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlOut.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlOut.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlOut.EmbeddedNavigator.ToolTipTitle");
            this.gridControlOut.MainView = this.gridViewOut;
            this.gridControlOut.Name = "gridControlOut";
            this.gridControlOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOut});
            // 
            // gridViewOut
            // 
            resources.ApplyResources(this.gridViewOut, "gridViewOut");
            this.gridViewOut.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId1,
            this.cloProduct,
            this.colInvoiceZZDetailQuantity1,
            this.colInvoiceZZDetailNote1});
            this.gridViewOut.GridControl = this.gridControlOut;
            this.gridViewOut.Name = "gridViewOut";
            this.gridViewOut.OptionsBehavior.Editable = false;
            this.gridViewOut.OptionsCustomization.AllowFilter = false;
            this.gridViewOut.OptionsCustomization.AllowGroup = false;
            this.gridViewOut.OptionsView.ColumnAutoWidth = false;
            this.gridViewOut.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId1
            // 
            this.colProductId1.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductId1.AppearanceCell.GradientMode")));
            this.colProductId1.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colProductId1.AppearanceCell.Image")));
            this.colProductId1.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId1.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductId1.AppearanceHeader.GradientMode")));
            this.colProductId1.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colProductId1.AppearanceHeader.Image")));
            this.colProductId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId1, "colProductId1");
            this.colProductId1.FieldName = "ProductId";
            this.colProductId1.Name = "colProductId1";
            this.colProductId1.OptionsColumn.AllowEdit = false;
            this.colProductId1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductId1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colProductId1.OptionsColumn.ReadOnly = true;
            this.colProductId1.OptionsFilter.AllowFilter = false;
            // 
            // cloProduct
            // 
            this.cloProduct.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("cloProduct.AppearanceCell.GradientMode")));
            this.cloProduct.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("cloProduct.AppearanceCell.Image")));
            this.cloProduct.AppearanceCell.Options.UseTextOptions = true;
            this.cloProduct.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.cloProduct.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("cloProduct.AppearanceHeader.GradientMode")));
            this.cloProduct.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("cloProduct.AppearanceHeader.Image")));
            this.cloProduct.AppearanceHeader.Options.UseTextOptions = true;
            this.cloProduct.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.cloProduct, "cloProduct");
            this.cloProduct.FieldName = "Product";
            this.cloProduct.Name = "cloProduct";
            this.cloProduct.OptionsColumn.AllowEdit = false;
            this.cloProduct.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.cloProduct.OptionsColumn.ReadOnly = true;
            this.cloProduct.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceZZDetailQuantity1
            // 
            this.colInvoiceZZDetailQuantity1.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailQuantity1.AppearanceCell.GradientMode")));
            this.colInvoiceZZDetailQuantity1.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailQuantity1.AppearanceCell.Image")));
            this.colInvoiceZZDetailQuantity1.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceZZDetailQuantity1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceZZDetailQuantity1.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailQuantity1.AppearanceHeader.GradientMode")));
            this.colInvoiceZZDetailQuantity1.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailQuantity1.AppearanceHeader.Image")));
            this.colInvoiceZZDetailQuantity1.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceZZDetailQuantity1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceZZDetailQuantity1, "colInvoiceZZDetailQuantity1");
            this.colInvoiceZZDetailQuantity1.DisplayFormat.FormatString = "0";
            this.colInvoiceZZDetailQuantity1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceZZDetailQuantity1.FieldName = "InvoiceZZDetailQuantity";
            this.colInvoiceZZDetailQuantity1.Name = "colInvoiceZZDetailQuantity1";
            this.colInvoiceZZDetailQuantity1.OptionsColumn.AllowEdit = false;
            this.colInvoiceZZDetailQuantity1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceZZDetailQuantity1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceZZDetailQuantity1.OptionsColumn.ReadOnly = true;
            this.colInvoiceZZDetailQuantity1.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceZZDetailNote1
            // 
            this.colInvoiceZZDetailNote1.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailNote1.AppearanceCell.GradientMode")));
            this.colInvoiceZZDetailNote1.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailNote1.AppearanceCell.Image")));
            this.colInvoiceZZDetailNote1.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceZZDetailNote1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceZZDetailNote1.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailNote1.AppearanceHeader.GradientMode")));
            this.colInvoiceZZDetailNote1.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailNote1.AppearanceHeader.Image")));
            this.colInvoiceZZDetailNote1.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceZZDetailNote1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceZZDetailNote1, "colInvoiceZZDetailNote1");
            this.colInvoiceZZDetailNote1.FieldName = "InvoiceZZDetailNote";
            this.colInvoiceZZDetailNote1.Name = "colInvoiceZZDetailNote1";
            this.colInvoiceZZDetailNote1.OptionsColumn.AllowEdit = false;
            this.colInvoiceZZDetailNote1.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceZZDetailNote1.OptionsColumn.ReadOnly = true;
            this.colInvoiceZZDetailNote1.OptionsFilter.AllowFilter = false;
            // 
            // gridControlIn
            // 
            resources.ApplyResources(this.gridControlIn, "gridControlIn");
            this.gridControlIn.DataSource = this.bindingSourceIn;
            this.gridControlIn.EmbeddedNavigator.AccessibleDescription = resources.GetString("gridControlIn.EmbeddedNavigator.AccessibleDescription");
            this.gridControlIn.EmbeddedNavigator.AccessibleName = resources.GetString("gridControlIn.EmbeddedNavigator.AccessibleName");
            this.gridControlIn.EmbeddedNavigator.AllowHtmlTextInToolTip = ((DevExpress.Utils.DefaultBoolean)(resources.GetObject("gridControlIn.EmbeddedNavigator.AllowHtmlTextInToolTip")));
            this.gridControlIn.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControlIn.EmbeddedNavigator.Anchor")));
            this.gridControlIn.EmbeddedNavigator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gridControlIn.EmbeddedNavigator.BackgroundImage")));
            this.gridControlIn.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControlIn.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControlIn.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControlIn.EmbeddedNavigator.ImeMode")));
            this.gridControlIn.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControlIn.EmbeddedNavigator.TextLocation")));
            this.gridControlIn.EmbeddedNavigator.ToolTip = resources.GetString("gridControlIn.EmbeddedNavigator.ToolTip");
            this.gridControlIn.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControlIn.EmbeddedNavigator.ToolTipIconType")));
            this.gridControlIn.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControlIn.EmbeddedNavigator.ToolTipTitle");
            this.gridControlIn.MainView = this.gridViewIn;
            this.gridControlIn.Name = "gridControlIn";
            this.gridControlIn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewIn});
            // 
            // gridViewIn
            // 
            resources.ApplyResources(this.gridViewIn, "gridViewIn");
            this.gridViewIn.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colProductId,
            this.colProductName,
            this.colInvoiceZZDetailQuantity,
            this.colInvoiceZZDetailNote});
            this.gridViewIn.GridControl = this.gridControlIn;
            this.gridViewIn.Name = "gridViewIn";
            this.gridViewIn.OptionsBehavior.Editable = false;
            this.gridViewIn.OptionsCustomization.AllowFilter = false;
            this.gridViewIn.OptionsCustomization.AllowGroup = false;
            this.gridViewIn.OptionsView.ColumnAutoWidth = false;
            this.gridViewIn.OptionsView.ShowGroupPanel = false;
            // 
            // colProductId
            // 
            this.colProductId.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductId.AppearanceCell.GradientMode")));
            this.colProductId.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colProductId.AppearanceCell.Image")));
            this.colProductId.AppearanceCell.Options.UseTextOptions = true;
            this.colProductId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProductId.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductId.AppearanceHeader.GradientMode")));
            this.colProductId.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colProductId.AppearanceHeader.Image")));
            this.colProductId.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colProductId, "colProductId");
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colProductId.OptionsColumn.ReadOnly = true;
            this.colProductId.OptionsFilter.AllowFilter = false;
            // 
            // colProductName
            // 
            this.colProductName.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductName.AppearanceCell.GradientMode")));
            this.colProductName.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colProductName.AppearanceCell.Image")));
            this.colProductName.AppearanceCell.Options.UseTextOptions = true;
            this.colProductName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductName.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colProductName.AppearanceHeader.GradientMode")));
            this.colProductName.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colProductName.AppearanceHeader.Image")));
            this.colProductName.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colProductName, "colProductName");
            this.colProductName.FieldName = "Product";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colProductName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceZZDetailQuantity
            // 
            this.colInvoiceZZDetailQuantity.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailQuantity.AppearanceCell.GradientMode")));
            this.colInvoiceZZDetailQuantity.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailQuantity.AppearanceCell.Image")));
            this.colInvoiceZZDetailQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceZZDetailQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colInvoiceZZDetailQuantity.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailQuantity.AppearanceHeader.GradientMode")));
            this.colInvoiceZZDetailQuantity.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailQuantity.AppearanceHeader.Image")));
            this.colInvoiceZZDetailQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceZZDetailQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            resources.ApplyResources(this.colInvoiceZZDetailQuantity, "colInvoiceZZDetailQuantity");
            this.colInvoiceZZDetailQuantity.DisplayFormat.FormatString = "0";
            this.colInvoiceZZDetailQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceZZDetailQuantity.FieldName = "InvoiceZZDetailQuantity";
            this.colInvoiceZZDetailQuantity.Name = "colInvoiceZZDetailQuantity";
            this.colInvoiceZZDetailQuantity.OptionsColumn.AllowEdit = false;
            this.colInvoiceZZDetailQuantity.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceZZDetailQuantity.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colInvoiceZZDetailQuantity.OptionsColumn.ReadOnly = true;
            this.colInvoiceZZDetailQuantity.OptionsFilter.AllowFilter = false;
            // 
            // colInvoiceZZDetailNote
            // 
            this.colInvoiceZZDetailNote.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailNote.AppearanceCell.GradientMode")));
            this.colInvoiceZZDetailNote.AppearanceCell.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailNote.AppearanceCell.Image")));
            this.colInvoiceZZDetailNote.AppearanceCell.Options.UseTextOptions = true;
            this.colInvoiceZZDetailNote.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colInvoiceZZDetailNote.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colInvoiceZZDetailNote.AppearanceHeader.GradientMode")));
            this.colInvoiceZZDetailNote.AppearanceHeader.Image = ((System.Drawing.Image)(resources.GetObject("colInvoiceZZDetailNote.AppearanceHeader.Image")));
            this.colInvoiceZZDetailNote.AppearanceHeader.Options.UseTextOptions = true;
            this.colInvoiceZZDetailNote.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colInvoiceZZDetailNote, "colInvoiceZZDetailNote");
            this.colInvoiceZZDetailNote.FieldName = "InvoiceZZDetailNote";
            this.colInvoiceZZDetailNote.Name = "colInvoiceZZDetailNote";
            this.colInvoiceZZDetailNote.OptionsColumn.AllowEdit = false;
            this.colInvoiceZZDetailNote.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceZZDetailNote.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colInvoiceZZDetailNote.OptionsColumn.ReadOnly = true;
            this.colInvoiceZZDetailNote.OptionsFilter.AllowFilter = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem8,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(930, 447);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEditDepot0;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(312, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.textEditInvoiceId;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(312, 25);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlOut;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(910, 141);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.dateEditInvoiceDate;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(312, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(337, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.buttonEditDepot1;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(312, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(337, 25);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.textEditNote;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 343);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(910, 84);
            this.layoutControlItem8.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControlIn;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 191);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(910, 152);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.buttonEditEmployee;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(649, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(261, 25);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(76, 14);
            // 
            // emptySpaceItem3
            // 
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(649, 25);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(261, 25);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.textEditInvoiceId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditInvoiceDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditEmployee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditDepot0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlOut;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOut;
        private DevExpress.XtraGrid.GridControl gridControlIn;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewIn;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDepot1;
        private DevExpress.XtraEditors.ButtonEdit buttonEditDepot0;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textEditInvoiceId;
        private DevExpress.XtraEditors.DateEdit dateEditInvoiceDate;
        private DevExpress.XtraEditors.ButtonEdit buttonEditEmployee;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private System.Windows.Forms.BindingSource bindingSourceOut;
        private System.Windows.Forms.BindingSource bindingSourceIn;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZZDetailQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZZDetailNote;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZZDetailQuantity1;
        private DevExpress.XtraGrid.Columns.GridColumn colInvoiceZZDetailNote1;
        private DevExpress.XtraGrid.Columns.GridColumn cloProduct;
        private DevExpress.XtraEditors.MemoEdit textEditNote;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}