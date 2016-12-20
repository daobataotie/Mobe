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
   // 文 件 名：OnListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-23
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class OnListForm : Settings.BasicData.BaseListForm
    {
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        BL.DepartmentManager deptnamanager = new Book.BL.DepartmentManager();
        public OnListForm()
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

        protected override void RefreshData()
        {
            this.bindingSource1.DataSource = this.employeeManager.SelectOnActive();
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
                    case "gridColumn3":
                        e.DisplayText = employees[e.ListSourceRowIndex].GenderDesc;
                        break;
                    case "gridColumn5":
                        e.DisplayText = employees[e.ListSourceRowIndex].BloodTypeDesc;
                        break;
                }
            
        }
        public override void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditForm._employee = this.bindingSource1.Current as Model.Employee;
            this.DialogResult = DialogResult.OK;
        }

        private void OnListForm_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = deptnamanager.Select();
        }
    }
}