namespace Book.UI.Settings.ProduceManager.MaterialType
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
            //this.installer11 = new Book.UI.Installer1();
            this.gridColumnLevel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDesc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnIsEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // gridControl1
            // 
            this.gridControl1.AccessibleDescription = null;
            this.gridControl1.AccessibleName = null;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.BackgroundImage = null;
            this.gridControl1.EmbeddedNavigator.AccessibleDescription = null;
            this.gridControl1.EmbeddedNavigator.AccessibleName = null;
            this.gridControl1.EmbeddedNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("gridControl1.EmbeddedNavigator.Anchor")));
            this.gridControl1.EmbeddedNavigator.BackgroundImage = null;
            this.gridControl1.EmbeddedNavigator.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("gridControl1.EmbeddedNavigator.BackgroundImageLayout")));
            this.gridControl1.EmbeddedNavigator.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("gridControl1.EmbeddedNavigator.ImeMode")));
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.EmbeddedNavigator.TextLocation = ((DevExpress.XtraEditors.NavigatorButtonsTextLocation)(resources.GetObject("gridControl1.EmbeddedNavigator.TextLocation")));
            this.gridControl1.EmbeddedNavigator.ToolTip = resources.GetString("gridControl1.EmbeddedNavigator.ToolTip");
            this.gridControl1.EmbeddedNavigator.ToolTipIconType = ((DevExpress.Utils.ToolTipIconType)(resources.GetObject("gridControl1.EmbeddedNavigator.ToolTipIconType")));
            this.gridControl1.EmbeddedNavigator.ToolTipTitle = resources.GetString("gridControl1.EmbeddedNavigator.ToolTipTitle");
            this.gridControl1.Font = null;
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemSpinEdit1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnLevel,
            this.gridColumnName,
            this.gridColumnDesc,
            this.gridColumnIsEnd});
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanging);
            // 
            // gridColumnLevel
            // 
            resources.ApplyResources(this.gridColumnLevel, "gridColumnLevel");
            this.gridColumnLevel.ColumnEdit = this.repositoryItemSpinEdit1;
            this.gridColumnLevel.FieldName = "MaterialLevel";
            this.gridColumnLevel.Name = "gridColumnLevel";
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AccessibleDescription = null;
            this.repositoryItemSpinEdit1.AccessibleName = null;
            resources.ApplyResources(this.repositoryItemSpinEdit1, "repositoryItemSpinEdit1");
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemSpinEdit1.Buttons"))), resources.GetString("repositoryItemSpinEdit1.Buttons1"), ((int)(resources.GetObject("repositoryItemSpinEdit1.Buttons2"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons3"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons4"))), ((bool)(resources.GetObject("repositoryItemSpinEdit1.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("repositoryItemSpinEdit1.Buttons6"))), null)});
            this.repositoryItemSpinEdit1.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("repositoryItemSpinEdit1.Mask.AutoComplete")));
            this.repositoryItemSpinEdit1.Mask.BeepOnError = ((bool)(resources.GetObject("repositoryItemSpinEdit1.Mask.BeepOnError")));
            this.repositoryItemSpinEdit1.Mask.EditMask = resources.GetString("repositoryItemSpinEdit1.Mask.EditMask");
            this.repositoryItemSpinEdit1.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("repositoryItemSpinEdit1.Mask.IgnoreMaskBlank")));
            this.repositoryItemSpinEdit1.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("repositoryItemSpinEdit1.Mask.MaskType")));
            this.repositoryItemSpinEdit1.Mask.PlaceHolder = ((char)(resources.GetObject("repositoryItemSpinEdit1.Mask.PlaceHolder")));
            this.repositoryItemSpinEdit1.Mask.SaveLiteral = ((bool)(resources.GetObject("repositoryItemSpinEdit1.Mask.SaveLiteral")));
            this.repositoryItemSpinEdit1.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("repositoryItemSpinEdit1.Mask.ShowPlaceHolders")));
            this.repositoryItemSpinEdit1.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("repositoryItemSpinEdit1.Mask.UseMaskAsDisplayFormat")));
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // gridColumnName
            // 
            resources.ApplyResources(this.gridColumnName, "gridColumnName");
            this.gridColumnName.FieldName = "MaterialTypeName";
            this.gridColumnName.Name = "gridColumnName";
            // 
            // gridColumnDesc
            // 
            resources.ApplyResources(this.gridColumnDesc, "gridColumnDesc");
            this.gridColumnDesc.FieldName = "MaterialTypeDescription";
            this.gridColumnDesc.Name = "gridColumnDesc";
            // 
            // gridColumnIsEnd
            // 
            resources.ApplyResources(this.gridColumnIsEnd, "gridColumnIsEnd");
            this.gridColumnIsEnd.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumnIsEnd.FieldName = "EndMaterialTypeLogo";
            this.gridColumnIsEnd.Name = "gridColumnIsEnd";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AccessibleDescription = null;
            this.repositoryItemCheckEdit1.AccessibleName = null;
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // EditForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Icon = null;
            this.Name = "EditForm";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

   
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLevel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDesc;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnIsEnd;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}