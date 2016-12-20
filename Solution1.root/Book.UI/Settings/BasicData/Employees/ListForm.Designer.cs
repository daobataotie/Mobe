namespace Book.UI.Settings.BasicData.Employees
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListForm));
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridColumnEmployeeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.bindingSourceDepartment = new System.Windows.Forms.BindingSource(this.components);
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnEmployeeId,
            this.gridColumnEmployeeName,
            this.gridColumn9,
            this.gridColumn4,
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn7});
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // gridControl1
            // 
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.EmbeddedNavigator.Name = "";
            gridLevelNode2.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // gridColumnEmployeeId
            // 
            this.gridColumnEmployeeId.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEmployeeId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gridColumnEmployeeId.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEmployeeId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.gridColumnEmployeeId, "gridColumnEmployeeId");
            this.gridColumnEmployeeId.FieldName = "IDNo";
            this.gridColumnEmployeeId.Name = "gridColumnEmployeeId";
            this.gridColumnEmployeeId.OptionsColumn.AllowEdit = false;
            this.gridColumnEmployeeId.OptionsFilter.AllowFilter = false;
            // 
            // gridColumnEmployeeName
            // 
            this.gridColumnEmployeeName.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumnEmployeeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnEmployeeName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnEmployeeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.gridColumnEmployeeName, "gridColumnEmployeeName");
            this.gridColumnEmployeeName.FieldName = "EmployeeName";
            this.gridColumnEmployeeName.Name = "gridColumnEmployeeName";
            this.gridColumnEmployeeName.OptionsColumn.AllowEdit = false;
            this.gridColumnEmployeeName.OptionsFilter.AllowFilter = false;
            // 
            // gridColumn4
            // 
            resources.ApplyResources(this.gridColumn4, "gridColumn4");
            this.gridColumn4.DisplayFormat.FormatString = "d";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "EmployeeBirthday";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.DisplayFormat.FormatString = "d";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn5.FieldName = "EmployeeJoinDate";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.FieldName = "ContactAddress";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // gridColumn8
            // 
            resources.ApplyResources(this.gridColumn8, "gridColumn8");
            this.gridColumn8.FieldName = "ContactPhone";
            this.gridColumn8.Name = "gridColumn8";
            // 
            // gridColumn9
            // 
            resources.ApplyResources(this.gridColumn9, "gridColumn9");
            this.gridColumn9.FieldName = "EmployeeGender";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn1
            // 
            resources.ApplyResources(this.gridColumn1, "gridColumn1");
            this.gridColumn1.FieldName = "Cellphone";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.DataSource = this.bindingSourceDepartment;
            resources.ApplyResources(this.listBoxControl1, "listBoxControl1");
            this.listBoxControl1.Name = "listBoxControl1";
            // 
            // bindingSourceDepartment
            // 
            this.bindingSourceDepartment.CurrentChanged += new System.EventHandler(this.bindingSourceDepartment_CurrentChanged);
            // 
            // radioGroup1
            // 
            resources.ApplyResources(this.radioGroup1, "radioGroup1");
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Columns = 2;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(resources.GetString("radioGroup1.Properties.Items"), resources.GetString("radioGroup1.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(resources.GetString("radioGroup1.Properties.Items2"), resources.GetString("radioGroup1.Properties.Items3"))});
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // ListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.listBoxControl1);
            this.Name = "ListForm";
            this.Load += new System.EventHandler(this.ListForm_Load);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            this.Controls.SetChildIndex(this.listBoxControl1, 0);
            this.Controls.SetChildIndex(this.radioGroup1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEmployeeId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private System.Windows.Forms.BindingSource bindingSourceDepartment;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
    }
}