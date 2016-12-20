using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.Employees
{
    public class ChooseEmployee : IChoose
    {

        private string _roleId;
        private Model.Employee ojb;         

        public ChooseEmployee() 
        {
            _roleId = EmployeeParameters.BUSINESS;
        }

        /// <summary>
        /// 必须是 EmployeeParameters类中的某个值
        /// </summary>
        /// <param name="roleId"></param>
        public ChooseEmployee(string roleId)
            : this()
        {
            _roleId = roleId;
        }

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm(_roleId);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                Model.Employee emp = f.SelectedItem as Model.Employee;
                item = new ChooseItem(emp, emp.IDNo, emp.EmployeeName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.EmployeeManager manager = new Book.BL.EmployeeManager();
            Model.Employee emp = manager.GetbyIdNo(item.ButtonText);
            if (emp != null)
            {
                item.EditValue = emp;
                item.LabelText = emp.EmployeeName;
                item.ButtonText = emp.IDNo;
            }
            else 
            {
                item.ErrorMessage = Properties.Resources.ChooseEmployeeError;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Employee).IDNo;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Employee).EmployeeName;
            }
        }

        public object EditValue
        {
            get
            {
                return ojb;
            }
            set
            {
                ojb = (Model.Employee)value;
            }
        }

        #endregion
    }

    /// <summary>
    /// 角色类别，将会根据不同的选择，列出不同角色的员工
    /// </summary>
    public class EmployeeParameters 
    {
        /// <summary>
        /// 所有
        /// </summary>
        public const string ALL = "000"; 
        /// <summary>
        /// 管理员
        /// </summary>
        public const string SYSTEMMANAGER = "001";
        /// <summary>
        /// 会计
        /// </summary>
        public const string ACCOUNTANT = "002";
        /// <summary>
        /// 品管
        /// </summary>
        public const string PRODUCTMANAGER = "003";
        /// <summary>
        /// 业务
        /// </summary>
        public const string BUSINESS = "004";
        /// <summary>
        /// 设计
        /// </summary>
        public const string DESIGN = "005";
        /// <summary>
        /// 库管
        /// </summary>
        public const string DEPOTMANAGER = "006";        
    }
}
