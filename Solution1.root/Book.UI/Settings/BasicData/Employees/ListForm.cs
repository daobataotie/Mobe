using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Employees
{

    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-18
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();
        BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        Model.Department department = new Book.Model.Department();
        IList<string> pylist = new List<string>(); 
 
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.EmployeeManager();
        }

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.radioGroup1.SelectedIndex = 0;

            this.bindingSourceDepartment.DataSource = this.departmentManager.Select();
            this.bindingSource1.DataSource = this.employeeManager.Select();




        }

        #region Override

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm(this.department);
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.Employee> employees = this.bindingSource1.DataSource as IList<Model.Employee>;

            if (employees == null || employees.Count <= 0)
                return;

            switch (e.Column.Name)
            {
                case "gridColumn9":
                    e.DisplayText = employees[e.ListSourceRowIndex].GenderDesc;
                    break;
                case "gridColumn3":
                    e.DisplayText = employees[e.ListSourceRowIndex].BloodTypeDesc;
                    break;
            }
        }

        private void bindingSourceDepartment_CurrentChanged(object sender, EventArgs e)
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.department = this.bindingSourceDepartment.Current as Model.Department;
                this.bindingSource1.DataSource = this.employeeManager.Select(department);
            }
            if (this.radioGroup1.SelectedIndex == 1)
            {
                this.bindingSource1.DataSource =this.employeeManager.SelectbyPinYin( this.bindingSourceDepartment.Current.ToString()); 
            }
            this.gridControl1.RefreshDataSource();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.SelectedIndex)
            { 
                case 0:
                   
                    this.bindingSourceDepartment.DataSource = this.departmentManager.Select();
                    break;
                case 1:
                  
                   string[] py=new  string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};                   
                   for (int i = 0; i < py.Length; i++)
                       this.pylist.Add((py[i]));
                   this.bindingSourceDepartment.DataSource = pylist;

                    break;
            }

        }
    }
}