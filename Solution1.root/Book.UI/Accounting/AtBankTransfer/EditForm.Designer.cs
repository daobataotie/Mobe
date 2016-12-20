namespace Book.UI.Accounting.AtBankTransfer
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
            this.spinEditWithMoney = new DevExpress.XtraEditors.SpinEdit();
            this.newChooseContorlIntoBankId = new Book.UI.Invoices.NewChooseContorl();
            this.newChooseContorlWithBankId = new Book.UI.Invoices.NewChooseContorl();
            this.textEditTransferId = new DevExpress.XtraEditors.TextEdit();
            this.dateEditTransferDate = new DevExpress.XtraEditors.DateEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditWithMoney.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTransferId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTransferDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTransferDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
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
            this.layoutControl1.Controls.Add(this.spinEditWithMoney);
            this.layoutControl1.Controls.Add(this.newChooseContorlIntoBankId);
            this.layoutControl1.Controls.Add(this.newChooseContorlWithBankId);
            this.layoutControl1.Controls.Add(this.textEditTransferId);
            this.layoutControl1.Controls.Add(this.dateEditTransferDate);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // spinEditWithMoney
            // 
            resources.ApplyResources(this.spinEditWithMoney, "spinEditWithMoney");
            this.spinEditWithMoney.Name = "spinEditWithMoney";
            this.spinEditWithMoney.Properties.AccessibleDescription = resources.GetString("spinEditWithMoney.Properties.AccessibleDescription");
            this.spinEditWithMoney.Properties.AccessibleName = resources.GetString("spinEditWithMoney.Properties.AccessibleName");
            this.spinEditWithMoney.Properties.AutoHeight = ((bool)(resources.GetObject("spinEditWithMoney.Properties.AutoHeight")));
            this.spinEditWithMoney.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditWithMoney.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("spinEditWithMoney.Properties.Mask.AutoComplete")));
            this.spinEditWithMoney.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("spinEditWithMoney.Properties.Mask.BeepOnError")));
            this.spinEditWithMoney.Properties.Mask.EditMask = resources.GetString("spinEditWithMoney.Properties.Mask.EditMask");
            this.spinEditWithMoney.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("spinEditWithMoney.Properties.Mask.IgnoreMaskBlank")));
            this.spinEditWithMoney.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("spinEditWithMoney.Properties.Mask.MaskType")));
            this.spinEditWithMoney.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("spinEditWithMoney.Properties.Mask.PlaceHolder")));
            this.spinEditWithMoney.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("spinEditWithMoney.Properties.Mask.SaveLiteral")));
            this.spinEditWithMoney.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("spinEditWithMoney.Properties.Mask.ShowPlaceHolders")));
            this.spinEditWithMoney.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("spinEditWithMoney.Properties.Mask.UseMaskAsDisplayFormat")));
            this.spinEditWithMoney.Properties.NullValuePrompt = resources.GetString("spinEditWithMoney.Properties.NullValuePrompt");
            this.spinEditWithMoney.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("spinEditWithMoney.Properties.NullValuePromptShowForEmptyValue")));
            this.spinEditWithMoney.StyleController = this.layoutControl1;
            // 
            // newChooseContorlIntoBankId
            // 
            resources.ApplyResources(this.newChooseContorlIntoBankId, "newChooseContorlIntoBankId");
            this.newChooseContorlIntoBankId.EditValue = null;
            this.newChooseContorlIntoBankId.Name = "newChooseContorlIntoBankId";
            // 
            // newChooseContorlWithBankId
            // 
            resources.ApplyResources(this.newChooseContorlWithBankId, "newChooseContorlWithBankId");
            this.newChooseContorlWithBankId.EditValue = null;
            this.newChooseContorlWithBankId.Name = "newChooseContorlWithBankId";
            // 
            // textEditTransferId
            // 
            resources.ApplyResources(this.textEditTransferId, "textEditTransferId");
            this.textEditTransferId.Name = "textEditTransferId";
            this.textEditTransferId.Properties.AccessibleDescription = resources.GetString("textEditTransferId.Properties.AccessibleDescription");
            this.textEditTransferId.Properties.AccessibleName = resources.GetString("textEditTransferId.Properties.AccessibleName");
            this.textEditTransferId.Properties.AutoHeight = ((bool)(resources.GetObject("textEditTransferId.Properties.AutoHeight")));
            this.textEditTransferId.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("textEditTransferId.Properties.Mask.AutoComplete")));
            this.textEditTransferId.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("textEditTransferId.Properties.Mask.BeepOnError")));
            this.textEditTransferId.Properties.Mask.EditMask = resources.GetString("textEditTransferId.Properties.Mask.EditMask");
            this.textEditTransferId.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("textEditTransferId.Properties.Mask.IgnoreMaskBlank")));
            this.textEditTransferId.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("textEditTransferId.Properties.Mask.MaskType")));
            this.textEditTransferId.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("textEditTransferId.Properties.Mask.PlaceHolder")));
            this.textEditTransferId.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("textEditTransferId.Properties.Mask.SaveLiteral")));
            this.textEditTransferId.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("textEditTransferId.Properties.Mask.ShowPlaceHolders")));
            this.textEditTransferId.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("textEditTransferId.Properties.Mask.UseMaskAsDisplayFormat")));
            this.textEditTransferId.Properties.NullValuePrompt = resources.GetString("textEditTransferId.Properties.NullValuePrompt");
            this.textEditTransferId.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("textEditTransferId.Properties.NullValuePromptShowForEmptyValue")));
            this.textEditTransferId.StyleController = this.layoutControl1;
            // 
            // dateEditTransferDate
            // 
            resources.ApplyResources(this.dateEditTransferDate, "dateEditTransferDate");
            this.dateEditTransferDate.Name = "dateEditTransferDate";
            this.dateEditTransferDate.Properties.AccessibleDescription = resources.GetString("dateEditTransferDate.Properties.AccessibleDescription");
            this.dateEditTransferDate.Properties.AccessibleName = resources.GetString("dateEditTransferDate.Properties.AccessibleName");
            this.dateEditTransferDate.Properties.AutoHeight = ((bool)(resources.GetObject("dateEditTransferDate.Properties.AutoHeight")));
            this.dateEditTransferDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEditTransferDate.Properties.Buttons"))))});
            this.dateEditTransferDate.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditTransferDate.Properties.Mask.AutoComplete")));
            this.dateEditTransferDate.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditTransferDate.Properties.Mask.BeepOnError")));
            this.dateEditTransferDate.Properties.Mask.EditMask = resources.GetString("dateEditTransferDate.Properties.Mask.EditMask");
            this.dateEditTransferDate.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditTransferDate.Properties.Mask.IgnoreMaskBlank")));
            this.dateEditTransferDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditTransferDate.Properties.Mask.MaskType")));
            this.dateEditTransferDate.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditTransferDate.Properties.Mask.PlaceHolder")));
            this.dateEditTransferDate.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditTransferDate.Properties.Mask.SaveLiteral")));
            this.dateEditTransferDate.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditTransferDate.Properties.Mask.ShowPlaceHolders")));
            this.dateEditTransferDate.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditTransferDate.Properties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditTransferDate.Properties.NullValuePrompt = resources.GetString("dateEditTransferDate.Properties.NullValuePrompt");
            this.dateEditTransferDate.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditTransferDate.Properties.NullValuePromptShowForEmptyValue")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.AccessibleDescription = resources.GetString("dateEditTransferDate.Properties.VistaTimeProperties.AccessibleDescription");
            this.dateEditTransferDate.Properties.VistaTimeProperties.AccessibleName = resources.GetString("dateEditTransferDate.Properties.VistaTimeProperties.AccessibleName");
            this.dateEditTransferDate.Properties.VistaTimeProperties.AutoHeight = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.AutoHeight")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.AutoComplete")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.BeepOnError = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.BeepOnError")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.EditMask = resources.GetString("dateEditTransferDate.Properties.VistaTimeProperties.Mask.EditMask");
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.MaskType")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.PlaceHolder = ((char)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.PlaceHolder")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.SaveLiteral = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.SaveLiteral")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.ShowPlaceHolders")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat")));
            this.dateEditTransferDate.Properties.VistaTimeProperties.NullValuePrompt = resources.GetString("dateEditTransferDate.Properties.VistaTimeProperties.NullValuePrompt");
            this.dateEditTransferDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("dateEditTransferDate.Properties.VistaTimeProperties.NullValuePromptShowForEmptyVa" +
                    "lue")));
            this.dateEditTransferDate.StyleController = this.layoutControl1;
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
            this.gridColumn1.FieldName = "TransferDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            resources.ApplyResources(this.gridColumn2, "gridColumn2");
            this.gridColumn2.FieldName = "Id";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn3
            // 
            resources.ApplyResources(this.gridColumn3, "gridColumn3");
            this.gridColumn3.FieldName = "WithBank";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.FieldName = "WithBank";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.DisplayFormat.FormatString = "0";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "WithMoney";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
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
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(667, 345);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(647, 249);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateEditTransferDate;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(324, 25);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditTransferId;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(324, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(323, 25);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.newChooseContorlWithBankId;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 25);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(324, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.newChooseContorlIntoBankId;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(324, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(323, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(48, 14);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.spinEditWithMoney;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(324, 25);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(48, 14);
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(324, 51);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(323, 25);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // EditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "EditForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.Controls.SetChildIndex(this.layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditWithMoney.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTransferId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTransferDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEditTransferDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.DateEdit dateEditTransferDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Book.UI.Invoices.NewChooseContorl newChooseContorlIntoBankId;
        private Book.UI.Invoices.NewChooseContorl newChooseContorlWithBankId;
        private DevExpress.XtraEditors.TextEdit textEditTransferId;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SpinEdit spinEditWithMoney;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}