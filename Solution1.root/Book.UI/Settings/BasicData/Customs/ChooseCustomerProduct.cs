using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.Customs
{
    public class ChooseCustomerProduct : IChoose
    {
        private Model.CustomerProducts ojb;
        private Model.Customer _customer;

     
        public ChooseCustomerProduct(Model.Customer customer) 
        {
            this._customer = customer;
        }

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseCustomerProductForm f = new ChooseCustomerProductForm(_customer);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.CustomerProducts customer = f.SelectedItem as Model.CustomerProducts;
                item = new ChooseItem(customer, customer.CustomerProductId, customer.CustomerProductName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.CustomerProductsManager manager = new Book.BL.CustomerProductsManager();
            Model.CustomerProducts customer = manager.GetById(item.ButtonText);
            if (customer != null)
            {
                item.EditValue = customer;
                item.LabelText = customer.CustomerProductName;
                item.ButtonText = customer.CustomerProductId;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomerProducts).CustomerProductId;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.CustomerProducts).CustomerProductName;
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
                ojb = (Model.CustomerProducts)value;
            }
        }

        #endregion
    }
}
