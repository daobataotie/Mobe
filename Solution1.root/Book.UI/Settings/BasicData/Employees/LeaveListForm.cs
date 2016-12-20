using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Employees
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：LeaveListForm
   // 编 码 人: 茍波濤                    完成时间:2009-10-23
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class LeaveListForm : BaseListForm
    {
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.DepartmentManager deptmanager = new Book.BL.DepartmentManager();
        public LeaveListForm()
        {
            InitializeComponent();
            this.barManager1.Bars[0].Visible = false;
        }
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.Employee> employees = this.bindingSource1.DataSource as IList<Model.Employee>;

            if (employees == null || employees.Count <= 0)
                return;

            switch (e.Column.Name)
            {
                //case "gridColumn3":
                //    e.DisplayText = employees[e.ListSourceRowIndex].GenderDesc;
                //break;
                case "gridColumn5":
                    e.DisplayText = employees[e.ListSourceRowIndex].BloodTypeDesc;
                    break;
            }

        }

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = this.employeeManager.SelectLeaveActive();
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditForm._employee = this.bindingSource1.Current as Model.Employee;
            this.DialogResult = DialogResult.OK;
        }

        private void LeaveListForm_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = deptmanager.Select();
            repositoryItemLookUpEdit2.DataSource = MakeParentTable();
        }
        private DataTable MakeParentTable()
        {

            DataTable table = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Id";
            column.Caption = "Id";
            column.ReadOnly = true;
            column.Unique = true;
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "GenderDesc";
            column.AutoIncrement = false;
            column.Caption = "GenderDesc";
            column.ReadOnly = false;
            column.Unique = false;
            table.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = table.Columns["id"];
            table.PrimaryKey = PrimaryKeyColumns;

            DataRow row = table.NewRow();
            row["id"] = "1001";
            row["GenderDesc"] = "男";
            table.Rows.Add(row);
            DataRow row1 = table.NewRow();
            row1["id"] = "1002";
            row1["GenderDesc"] = "女";
            table.Rows.Add(row1);
            return table;
        }

    }
}
