using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.Bank
{
    public class ChooseBank : IChoose
    {
        private Model.Bank ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            
            ChooseBankForm  f= new  ChooseBankForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Bank bank = f.SelectedItem as Model.Bank;
                item = new ChooseItem(bank,bank.BankId,bank.BankName );
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.BankManager bankmanager = new Book.BL.BankManager();
            Model.Bank bank = bankmanager.Get(item.ButtonText);
            if (bank != null)
            {
                item.EditValue = bank;
                item.LabelText = bank.BankName;
                item.ButtonText = bank.BankId;
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
                return EditValue == null ? string.Empty : (EditValue as Model.Bank)
                    .BankId;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Bank).BankName;
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
                ojb = (Model.Bank)value;
            }
        }

        #endregion
    }
}
