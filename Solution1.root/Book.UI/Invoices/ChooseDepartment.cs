using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public class ChooseDepartment : IChoose
    {
        private Model.Department obj;

        #region IChoose 成员

        public string ButtonText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.Department).Id; }
        }

        public string LableText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.Department).DepartmentName; }
        }

        public object EditValue
        {
            get
            {
                return obj;
            }
            set
            {
                obj = (Model.Department)value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            ChooseDepartmentForm f = new ChooseDepartmentForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Department department = f.SelectedItem as Model.Department;
                item = new ChooseItem(department, department.Id, department.DepartmentName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.DepartmentManager manager = new Book.BL.DepartmentManager();
            Model.Department department = manager.GetById(item.ButtonText);
            if (department != null)
            {
                item.EditValue = department;
                item.LabelText = department.DepartmentName;
                item.ButtonText = department.Id;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChooseDepartmentError;
            }
        }
        #endregion
    }
}
