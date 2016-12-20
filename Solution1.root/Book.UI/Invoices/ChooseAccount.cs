using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public class ChooseAccount : IChoose
    {
        private Model.Account obj;

        #region IChoose 成员

        public string ButtonText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.Account).Id; }
        }

        public string LableText
        {
            get { return EditValue == null ? string.Empty : (EditValue as Model.Account).AccountName; }
        }

        public object EditValue
        {
            get
            {
                return obj;
            }
            set
            {
                obj = (Model.Account)value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            ChooseAccountForm f = new ChooseAccountForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Account account = f.SelectedItem as Model.Account;
                item = new ChooseItem(account, account.Id, account.AccountName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AccountManager manager = new Book.BL.AccountManager();
            Model.Account account = manager.GetById(item.ButtonText);
            if (account != null)
            {
                item.EditValue = account;
                item.LabelText = account.AccountName;
                item.ButtonText = account.Id;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChooseAccountError;
            }
        }
        #endregion
    }
}