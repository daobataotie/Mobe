using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.Customs
{
    public class ChooseCustoms : IChoose
    {
        private Model.Customer ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseCustomsForm f = new ChooseCustomsForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Customer customer = f.SelectedItem as Model.Customer;
                item = new ChooseItem(customer, customer.Id, customer.CustomerShortName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.CustomerManager manager = new Book.BL.CustomerManager();
            Model.Customer customer = manager.GetById(item.ButtonText);
            if (customer != null)
            {
                item.EditValue = customer;
                item.LabelText = customer.CustomerShortName;
                item.ButtonText = customer.Id;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Customer).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Customer).CustomerShortName;
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
                ojb = (Model.Customer)value;
            }
        }

        #endregion
    }
}
