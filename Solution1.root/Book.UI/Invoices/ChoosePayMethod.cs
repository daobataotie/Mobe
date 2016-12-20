using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public class ChoosePayMethod : IChoose
    {
        private Model.PayMethod obj;

        #region IChoose 成员

        public string ButtonText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.PayMethod).Id; }
        }

        public string LableText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.PayMethod).PayMethodName; }
        }

        public object EditValue
        {
            get
            {
                return obj;
            }
            set
            {
                obj = (Model.PayMethod)value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            ChoosePayMethodForm f = new ChoosePayMethodForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.PayMethod paymethod = f.SelectedItem as Model.PayMethod;
                item = new ChooseItem(paymethod, paymethod.Id, paymethod.PayMethodName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.PayMethodManager manager = new Book.BL.PayMethodManager();
            Model.PayMethod paymethod = manager.GetById(item.ButtonText);
            if (paymethod != null)
            {
                item.EditValue = paymethod;
                item.LabelText = paymethod.PayMethodName;
                item.ButtonText = paymethod.Id;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChoosePayMethodError;
            }
        }
        #endregion
    }
}