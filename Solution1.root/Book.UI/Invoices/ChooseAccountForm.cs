using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseAccountForm : UI.Settings.BasicData.BaseChooseForm
    {
        public ChooseAccountForm()
        {
            InitializeComponent();
            this.manager = new BL.AccountManager();
        }

        protected override UI.Settings.BasicData.BaseEditForm1 GetEditForm1()
        {
            return new UI.Settings.BasicData.Accounts.EditForm();
        }
    }
}