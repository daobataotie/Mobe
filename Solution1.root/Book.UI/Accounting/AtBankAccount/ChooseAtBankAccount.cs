using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.AtBankAccount
{
    class ChooseAtBankAccount : Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtBankAccount obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseAtBankAccountForm f = new ChooseAtBankAccountForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtBankAccount AtBankAccount = f.SelectedItem as Model.AtBankAccount;
                item = new ChooseItem(AtBankAccount, AtBankAccount.Id, AtBankAccount.BankAccountName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtBankAccountManager manager = new Book.BL.AtBankAccountManager();
            Model.AtBankAccount AtBankAccount = manager.GetById(item.ButtonText);
            if (AtBankAccount != null)
            {
                item.EditValue = AtBankAccount;
                item.LabelText = AtBankAccount.BankAccountName;
                item.ButtonText = AtBankAccount.Id;
            }
            else
            {
                item.ErrorMessage = "物料類型錯誤";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtBankAccount).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtBankAccount).BankAccountName;
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
                obj = (Model.AtBankAccount)value;
            }
        }

        #endregion
    }
}
