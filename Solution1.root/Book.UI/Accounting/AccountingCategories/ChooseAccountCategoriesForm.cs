using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AccountingCategories
{
    public partial class ChooseAccountCategoriesForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseAccountCategoriesForm()
        {
            InitializeComponent();
            this.manager = new BL.AtAccountingCategoriesManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.AccountingCategories.ListForm();
        }
    }
}