using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtBankAccount
{
    public partial class ChooseAtBankAccountForm :  Settings.BasicData.BaseChooseForm
    {
        public ChooseAtBankAccountForm()
        {
            InitializeComponent();
            this.manager = new BL.AtBankAccountManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.AtBankAccount.EditForm();
        }
    }
}