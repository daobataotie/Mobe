using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.Supplier
{
    public class ChooseSupplier : IChoose
    {

        private Model.Supplier obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseSupplierForm f = new ChooseSupplierForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Supplier supplier = f.SelectedItem as Model.Supplier;
                item = new ChooseItem(supplier, supplier.Id, supplier.SupplierShortName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.SupplierManager manager = new Book.BL.SupplierManager();
            Model.Supplier supplier = manager.GetById(item.ButtonText);
            if (supplier != null)
            {
                item.EditValue = supplier;
                item.LabelText = supplier.SupplierShortName;
                item.ButtonText = supplier.Id;
            }
            else
            {
                item.ErrorMessage = "供應商錯誤";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Supplier).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Supplier).SupplierShortName;
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
                obj = (Model.Supplier)value;
            }
        }

        #endregion
    }
}