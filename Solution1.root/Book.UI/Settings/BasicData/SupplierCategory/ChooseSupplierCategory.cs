using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.SupplierCategory
{
    public class ChooseSupplierCategory : IChoose
    {
        private Model.SupplierCategory obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseSupplierCategoryForm f = new ChooseSupplierCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.SupplierCategory supplierCategory = f.SelectedItem as Model.SupplierCategory;
                item = new ChooseItem(supplierCategory, supplierCategory.Id, supplierCategory.SupplierCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.SupplierCategoryManager manager = new Book.BL.SupplierCategoryManager();
            Model.SupplierCategory supplierCategory = manager.GetById(item.ButtonText);
            if (supplierCategory != null)
            {
                item.EditValue = supplierCategory;
                item.LabelText = supplierCategory.SupplierCategoryName;
                item.ButtonText = supplierCategory.Id;
            }
            else
            {
                item.ErrorMessage = "供应商类别错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.SupplierCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.SupplierCategory).SupplierCategoryName;
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
                obj = (Model.SupplierCategory)value;
            }
        }

        #endregion
    }
}
