using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AccountingCategory
{
    public partial class ChooseAccountingCategoryForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseAccountingCategoryForm()
        {
            InitializeComponent();
            this.manager = new BL.AtAccountingCategoryManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.AccountingCategory.EditForm();
        }
    }
}