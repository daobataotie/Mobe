namespace Book.UI.Settings.Options
{
    partial class Options01Page
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options01Page));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.settingBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSettingName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSettingCurrentValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSettingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSettingDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.settingBindingSource;
            this.gridControl1.EmbeddedNavigator.Name = "";
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSettingName,
            this.colSettingCurrentValue,
            this.colSettingId,
            this.colSettingDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            // 
            // colSettingName
            // 
            resources.ApplyResources(this.colSettingName, "colSettingName");
            this.colSettingName.FieldName = "SettingName";
            this.colSettingName.Name = "colSettingName";
            this.colSettingName.OptionsColumn.AllowEdit = false;
            this.colSettingName.OptionsColumn.AllowFocus = false;
            this.colSettingName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colSettingCurrentValue
            // 
            resources.ApplyResources(this.colSettingCurrentValue, "colSettingCurrentValue");
            this.colSettingCurrentValue.FieldName = "SettingCurrentValue";
            this.colSettingCurrentValue.Name = "colSettingCurrentValue";
            this.colSettingCurrentValue.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colSettingId
            // 
            resources.ApplyResources(this.colSettingId, "colSettingId");
            this.colSettingId.FieldName = "SettingId";
            this.colSettingId.Name = "colSettingId";
            this.colSettingId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // colSettingDescription
            // 
            resources.ApplyResources(this.colSettingDescription, "colSettingDescription");
            this.colSettingDescription.FieldName = "SettingDescription";
            this.colSettingDescription.Name = "colSettingDescription";
            this.colSettingDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // Options01Page
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.gridControl1);
            this.Name = "Options01Page";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource settingBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingId;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingCurrentValue;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
