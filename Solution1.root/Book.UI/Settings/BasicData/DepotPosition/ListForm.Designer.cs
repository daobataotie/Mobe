namespace Book.UI.Settings.BasicData.DepotPosition
{
    partial class ListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            this.gridColumnDepotPositionDepot = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepotPositionDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDepotPositionId = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
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
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDepotPositionDepot,
            this.gridColumnDepotPositionDescription,
            this.gridColumnDepotPositionId});
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // gridColumnDepotPositionDepot
            // 
            this.gridColumnDepotPositionDepot.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDepotPositionDepot.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnDepotPositionDepot.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDepotPositionDepot.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.gridColumnDepotPositionDepot, "gridColumnDepotPositionDepot");
            this.gridColumnDepotPositionDepot.FieldName = "Depot";
            this.gridColumnDepotPositionDepot.Name = "gridColumnDepotPositionDepot";
            this.gridColumnDepotPositionDepot.OptionsColumn.AllowEdit = false;
            this.gridColumnDepotPositionDepot.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDepotPositionDepot.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDepotPositionDepot.OptionsColumn.ReadOnly = true;
            this.gridColumnDepotPositionDepot.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnDepotPositionDescription
            // 
            this.gridColumnDepotPositionDescription.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDepotPositionDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnDepotPositionDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDepotPositionDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.gridColumnDepotPositionDescription, "gridColumnDepotPositionDescription");
            this.gridColumnDepotPositionDescription.FieldName = "DepotPositionDescription";
            this.gridColumnDepotPositionDescription.Name = "gridColumnDepotPositionDescription";
            this.gridColumnDepotPositionDescription.OptionsColumn.AllowEdit = false;
            this.gridColumnDepotPositionDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDepotPositionDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDepotPositionDescription.OptionsColumn.ReadOnly = true;
            this.gridColumnDepotPositionDescription.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnDepotPositionId
            // 
            this.gridColumnDepotPositionId.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnDepotPositionId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnDepotPositionId.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnDepotPositionId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.gridColumnDepotPositionId, "gridColumnDepotPositionId");
            this.gridColumnDepotPositionId.FieldName = "Id";
            this.gridColumnDepotPositionId.Name = "gridColumnDepotPositionId";
            this.gridColumnDepotPositionId.OptionsColumn.AllowEdit = false;
            this.gridColumnDepotPositionId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumnDepotPositionId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumnDepotPositionId.OptionsColumn.ReadOnly = true;
            this.gridColumnDepotPositionId.OptionsFilter.AllowFilter = false;
            // 
            // ListForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Icon = null;
            this.Name = "ListForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotPositionId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotPositionDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDepotPositionDepot;
    }
}