using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.CurrencyCategory
{
    public partial class ChooseAtCurrencyCategoryForm : Settings.BasicData.BaseChooseForm
    {
        public ChooseAtCurrencyCategoryForm()
        {
            InitializeComponent();
            this.manager = new BL.AtCurrencyCategoryManager();
        }
        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.CurrencyCategory.CategoryEditForm();
        }
    }
}