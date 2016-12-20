namespace Book.UI.Hr.Attendance.Atten
{
    partial class AnormalySalaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnormalySalaryForm));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.sbtn_print = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DutyDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.Employee = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EmployeeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.ShouldCheckIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.ShouldCheckOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.ActualCheckIn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.ActualCheckOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTimeEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTimeEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.sbtn_print);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.dateEdit1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // sbtn_print
            // 
            resources.ApplyResources(this.sbtn_print, "sbtn_print");
            this.sbtn_print.Name = "sbtn_print";
            this.sbtn_print.StyleController = this.layoutControl1;
            this.sbtn_print.Click += new System.EventHandler(this.sbtn_print_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.bindingSource1;
            resources.ApplyResources(this.gridControl1, "gridControl1");
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.repositoryItemTimeEdit1,
            this.repositoryItemTimeEdit2,
            this.repositoryItemCheckEdit1,
            this.repositoryItemTimeEdit3,
            this.repositoryItemTimeEdit4,
            this.repositoryItemTimeEdit5,
            this.repositoryItemDateEdit1});
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DutyDate,
            this.Employee,
            this.EmployeeName,
            this.ShouldCheckIn,
            this.ShouldCheckOut,
            this.ActualCheckIn,
            this.ActualCheckOut,
            this.Note});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridView1_CustomColumnDisplayText);
            // 
            // DutyDate
            // 
            resources.ApplyResources(this.DutyDate, "DutyDate");
            this.DutyDate.ColumnEdit = this.repositoryItemDateEdit1;
            this.DutyDate.DisplayFormat.FormatString = "yyyy:MM:dd";
            this.DutyDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DutyDate.FieldName = "DutyDate";
            this.DutyDate.Name = "DutyDate";
            this.DutyDate.OptionsColumn.AllowEdit = false;
            this.DutyDate.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemDateEdit1
            // 
            resources.ApplyResources(this.repositoryItemDateEdit1, "repositoryItemDateEdit1");
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("repositoryItemDateEdit1.Buttons"))))});
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            this.repositoryItemDateEdit1.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // Employee
            // 
            resources.ApplyResources(this.Employee, "Employee");
            this.Employee.Name = "Employee";
            this.Employee.OptionsColumn.AllowEdit = false;
            this.Employee.OptionsColumn.ReadOnly = true;
            // 
            // EmployeeName
            // 
            resources.ApplyResources(this.EmployeeName, "EmployeeName");
            this.EmployeeName.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.EmployeeName.FieldName = "EmployeeName";
            this.EmployeeName.Name = "EmployeeName";
            // 
            // repositoryItemHyperLinkEdit1
            // 
            resources.ApplyResources(this.repositoryItemHyperLinkEdit1, "repositoryItemHyperLinkEdit1");
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            this.repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            // 
            // ShouldCheckIn
            // 
            resources.ApplyResources(this.ShouldCheckIn, "ShouldCheckIn");
            this.ShouldCheckIn.ColumnEdit = this.repositoryItemTimeEdit1;
            this.ShouldCheckIn.DisplayFormat.FormatString = "HH:mm";
            this.ShouldCheckIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ShouldCheckIn.FieldName = "ShouldCheckIn";
            this.ShouldCheckIn.Name = "ShouldCheckIn";
            this.ShouldCheckIn.OptionsColumn.AllowEdit = false;
            this.ShouldCheckIn.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemTimeEdit1
            // 
            resources.ApplyResources(this.repositoryItemTimeEdit1, "repositoryItemTimeEdit1");
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // ShouldCheckOut
            // 
            resources.ApplyResources(this.ShouldCheckOut, "ShouldCheckOut");
            this.ShouldCheckOut.ColumnEdit = this.repositoryItemTimeEdit2;
            this.ShouldCheckOut.DisplayFormat.FormatString = "HH:mm";
            this.ShouldCheckOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ShouldCheckOut.FieldName = "ShouldCheckOut";
            this.ShouldCheckOut.Name = "ShouldCheckOut";
            this.ShouldCheckOut.OptionsColumn.AllowEdit = false;
            this.ShouldCheckOut.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemTimeEdit2
            // 
            resources.ApplyResources(this.repositoryItemTimeEdit2, "repositoryItemTimeEdit2");
            this.repositoryItemTimeEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit2.Name = "repositoryItemTimeEdit2";
            // 
            // ActualCheckIn
            // 
            resources.ApplyResources(this.ActualCheckIn, "ActualCheckIn");
            this.ActualCheckIn.ColumnEdit = this.repositoryItemTimeEdit3;
            this.ActualCheckIn.DisplayFormat.FormatString = "HH:mm";
            this.ActualCheckIn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ActualCheckIn.FieldName = "ActualCheckIn";
            this.ActualCheckIn.Name = "ActualCheckIn";
            this.ActualCheckIn.OptionsColumn.AllowEdit = false;
            this.ActualCheckIn.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemTimeEdit3
            // 
            resources.ApplyResources(this.repositoryItemTimeEdit3, "repositoryItemTimeEdit3");
            this.repositoryItemTimeEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit3.Name = "repositoryItemTimeEdit3";
            // 
            // ActualCheckOut
            // 
            resources.ApplyResources(this.ActualCheckOut, "ActualCheckOut");
            this.ActualCheckOut.ColumnEdit = this.repositoryItemTimeEdit4;
            this.ActualCheckOut.DisplayFormat.FormatString = "HH:mm";
            this.ActualCheckOut.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ActualCheckOut.FieldName = "ActualCheckOut";
            this.ActualCheckOut.Name = "ActualCheckOut";
            this.ActualCheckOut.OptionsColumn.AllowEdit = false;
            this.ActualCheckOut.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemTimeEdit4
            // 
            resources.ApplyResources(this.repositoryItemTimeEdit4, "repositoryItemTimeEdit4");
            this.repositoryItemTimeEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit4.Name = "repositoryItemTimeEdit4";
            // 
            // Note
            // 
            resources.ApplyResources(this.Note, "Note");
            this.Note.FieldName = "Note";
            this.Note.Name = "Note";
            this.Note.OptionsColumn.AllowEdit = false;
            this.Note.OptionsColumn.ReadOnly = true;
            // 
            // repositoryItemCheckEdit1
            // 
            resources.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemTimeEdit5
            // 
            resources.ApplyResources(this.repositoryItemTimeEdit5, "repositoryItemTimeEdit5");
            this.repositoryItemTimeEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemTimeEdit5.Name = "repositoryItemTimeEdit5";
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            resources.ApplyResources(this.dateEdit1, "dateEdit1");
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEdit1.Properties.Buttons"))))});
            this.dateEdit1.Properties.NullDate = "";
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.StyleController = this.layoutControl1;
            this.dateEdit1.EditValueChanged += new System.EventHandler(this.dateEdit1_EditValueChanged);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(955, 440);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dateEdit1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(196, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(36, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControl1;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(935, 394);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(330, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(605, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.sbtn_print;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(196, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(134, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // AnormalySalaryForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "AnormalySalaryForm";
            this.Load += new System.EventHandler(this.AnormalySalaryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn DutyDate;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn Employee;
        private DevExpress.XtraGrid.Columns.GridColumn EmployeeName;
        private DevExpress.XtraGrid.Columns.GridColumn ShouldCheckIn;
        private DevExpress.XtraGrid.Columns.GridColumn ShouldCheckOut;
        private DevExpress.XtraGrid.Columns.GridColumn ActualCheckIn;
        private DevExpress.XtraGrid.Columns.GridColumn ActualCheckOut;
        private DevExpress.XtraGrid.Columns.GridColumn Note;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit5;
        private DevExpress.XtraEditors.SimpleButton sbtn_print;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}