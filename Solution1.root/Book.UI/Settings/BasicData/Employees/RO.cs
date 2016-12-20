using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Book.BL;

namespace Book.UI.Settings.BasicData.Employees
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        private EmployeeManager employeemanager = new EmployeeManager();

        public RO(DateTime rzb, DateTime rzt, DateTime lzb, DateTime lzt, string t,int f)
            : this()
        {

            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            string selectInfo = string.Empty;
            if (t == "0")
            {
                selectInfo = rzb.ToShortDateString() + "至" + rzt.ToShortDateString() + "在職";
            }
            if (t == "1")
            {
                selectInfo = lzb.ToShortDateString() + "至" + lzt.ToShortDateString() + "離職";
            }
            if (t == "2")
            {
                selectInfo = lzb.ToShortDateString() + "至" + lzt.ToShortDateString() + "在職和" + lzb.ToShortDateString() + "至" + lzt.ToShortDateString() + "離職";
            }

            this.xrLabelDataName.Text = selectInfo + "員工信息";
            this.DataSource = employeemanager.selectEmployeeSearch(rzb, rzt, lzb, lzt, t,f);

            this.xrTableCellEmployeeIDNO.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_IDNO);
            this.xrTableCellEmployeeName.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrTableCellDeptName.DataBindings.Add("Text", this.DataSource, "Department." + Model.Department.PROPERTY_DEPARTMENTNAME);

            this.xrTableCellEmployeeBirthday.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_BIRTHDAY);
            this.xrTableCellSex.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_GENDERSEX);
            this.xrTableCellEmployeeBloodType.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_BLOODTYPEDESC);
            this.xrTableCellUrgentPhone.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_URGENTPHONE);
            this.xrTableCellEmployeeIdentityNO.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_EMPLOYEEIDENTITYNO);
            this.xrTableCellEmployeeMarried.DataBindings.Add("Text", this.DataSource, Model.Employee.PROPERTY_ISMARRIED);
        }
    }
}
