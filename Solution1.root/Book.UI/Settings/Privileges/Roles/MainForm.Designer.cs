namespace Book.UI.Settings.Privileges.Roles
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItemInsert = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.roleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRoleId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItemInsert,
            this.barButtonItemUpdate,
            this.barButtonItemDelete,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 4;
            // 
            // bar1
            // 
            this.bar1.BarItemVertIndent = 4;
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemInsert),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemUpdate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // barButtonItemInsert
            // 
            this.barButtonItemInsert.AccessibleDescription = null;
            this.barButtonItemInsert.AccessibleName = null;
            resources.ApplyResources(this.barButtonItemInsert, "barButtonItemInsert");
            this.barButtonItemInsert.Id = 0;
            this.barButtonItemInsert.Name = "barButtonItemInsert";
            this.barButtonItemInsert.Tag = "insert";
            this.barButtonItemInsert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemInsert_ItemClick);
            // 
            // barButtonItemUpdate
            // 
            this.barButtonItemUpdate.AccessibleDescription = null;
            this.barButtonItemUpdate.AccessibleName = null;
            resources.ApplyResources(this.barButtonItemUpdate, "barButtonItemUpdate");
            this.barButtonItemUpdate.Id = 1;
            this.barButtonItemUpdate.Name = "barButtonItemUpdate";
            this.barButtonItemUpdate.Tag = "update";
            this.barButtonItemUpdate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemInsert_ItemClick);
            // 
            // barButtonItemDelete
            // 
            this.barButtonItemDelete.AccessibleDescription = null;
            this.barButtonItemDelete.AccessibleName = null;
            resources.ApplyResources(this.barButtonItemDelete, "barButtonItemDelete");
            this.barButtonItemDelete.Id = 2;
            this.barButtonItemDelete.Name = "barButtonItemDelete";
            this.barButtonItemDelete.Tag = "delete";
            this.barButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemInsert_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.AccessibleDescription = null;
            this.barButtonItem1.AccessibleName = null;
            resources.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Id = 3;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Tag = "setprivilege";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemInsert_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.AccessibleDescription = null;
            this.barDockControlTop.AccessibleName = null;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            this.barDockControlTop.Appearance.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("barDockControlTop.Appearance.GradientMode")));
            this.barDockControlTop.Appearance.Image = null;
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Font = null;
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.AccessibleDescription = null;
            this.barDockControlBottom.AccessibleName = null;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            this.barDockControlBottom.Appearance.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("barDockControlBottom.Appearance.GradientMode")));
            this.barDockControlBottom.Appearance.Image = null;
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Font = null;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.AccessibleDescription = null;
            this.barDockControlLeft.AccessibleName = null;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            this.barDockControlLeft.Appearance.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("barDockControlLeft.Appearance.GradientMode")));
            this.barDockControlLeft.Appearance.Image = null;
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Font = null;
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.AccessibleDescription = null;
            this.barDockControlRight.AccessibleName = null;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            this.barDockControlRight.Appearance.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("barDockControlRight.Appearance.GradientMode")));
            this.barDockControlRight.Appearance.Image = null;
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Font = null;
            // 
            // gridControl1
            // 
            this.gridControl1.AccessibleDescription = null;
            this.gridControl1.AccessibleName = null;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.BackgroundImage = null;
            this.gridControl1.DataSource = this.roleBindingSource;
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
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRoleId,
            this.colRoleName,
            this.colRoleDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colRoleId
            // 
            this.colRoleId.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleId.AppearanceCell.GradientMode")));
            this.colRoleId.AppearanceCell.Image = null;
            this.colRoleId.AppearanceCell.Options.UseTextOptions = true;
            this.colRoleId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colRoleId.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleId.AppearanceHeader.GradientMode")));
            this.colRoleId.AppearanceHeader.Image = null;
            this.colRoleId.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colRoleId, "colRoleId");
            this.colRoleId.FieldName = "Id";
            this.colRoleId.Name = "colRoleId";
            this.colRoleId.OptionsColumn.AllowEdit = false;
            this.colRoleId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colRoleId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colRoleId.OptionsFilter.AllowFilter = false;
            // 
            // colRoleName
            // 
            this.colRoleName.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleName.AppearanceCell.GradientMode")));
            this.colRoleName.AppearanceCell.Image = null;
            this.colRoleName.AppearanceCell.Options.UseTextOptions = true;
            this.colRoleName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleName.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleName.AppearanceHeader.GradientMode")));
            this.colRoleName.AppearanceHeader.Image = null;
            this.colRoleName.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.colRoleName, "colRoleName");
            this.colRoleName.FieldName = "RoleName";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.OptionsColumn.AllowEdit = false;
            this.colRoleName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colRoleName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colRoleName.OptionsFilter.AllowFilter = false;
            // 
            // colRoleDescription
            // 
            this.colRoleDescription.AppearanceCell.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleDescription.AppearanceCell.GradientMode")));
            this.colRoleDescription.AppearanceCell.Image = null;
            this.colRoleDescription.AppearanceCell.Options.UseTextOptions = true;
            this.colRoleDescription.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colRoleDescription.AppearanceHeader.GradientMode = ((System.Drawing.Drawing2D.LinearGradientMode)(resources.GetObject("colRoleDescription.AppearanceHeader.GradientMode")));
            this.colRoleDescription.AppearanceHeader.Image = null;
            this.colRoleDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.colRoleDescription, "colRoleDescription");
            this.colRoleDescription.FieldName = "RoleDescription";
            this.colRoleDescription.Name = "colRoleDescription";
            this.colRoleDescription.OptionsColumn.AllowEdit = false;
            this.colRoleDescription.OptionsFilter.AllowFilter = false;
            // 
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource roleBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleId;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleDescription;
        private DevExpress.XtraBars.BarButtonItem barButtonItemInsert;
        private DevExpress.XtraBars.BarButtonItem barButtonItemUpdate;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDelete;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}