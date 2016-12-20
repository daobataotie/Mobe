using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Settings.BasicData.CustomerCategory
{
     public   class ChooseCustomerCategory:IChoose
    {
        private Model.CustomerCategory obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseCustomerCategoryForm  f = new ChooseCustomerCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.CustomerCategory customerCategory = f.SelectedItem as Model.CustomerCategory;
                item = new ChooseItem(customerCategory, customerCategory.Id,customerCategory.CustomerCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.CustomerCategoryManager manager = new Book.BL.CustomerCategoryManager();
            Model.CustomerCategory customerCategory = manager.GetById(item.ButtonText);
            if (customerCategory != null)
            {
                item.EditValue = customerCategory;
                item.LabelText = customerCategory.CustomerCategoryName;
                item.ButtonText = customerCategory.Id;
            }
            else
            {
                item.ErrorMessage = "客户分类错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomerCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomerCategory).CustomerCategoryName;
            }
        }

        public object EditValue
        {
            get
            {
                return obj;
            }
            set
            {
                obj = (Model.CustomerCategory)value;
            }
        }

        #endregion
    }
}
