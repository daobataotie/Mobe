using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.AccountingCategories
{
    class ChooseAccountCategories : Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtAccountingCategories obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseAccountCategoriesForm f = new ChooseAccountCategoriesForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtAccountingCategories AtAccountingCategories = f.SelectedItem as Model.AtAccountingCategories;
                item = new ChooseItem(AtAccountingCategories, AtAccountingCategories.Id, AtAccountingCategories.AccountingCategoriesName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtAccountingCategoriesManager manager = new Book.BL.AtAccountingCategoriesManager();
            Model.AtAccountingCategories AtAccountingCategories = manager.GetById(item.ButtonText);
            if (AtAccountingCategories != null)
            {
                item.EditValue = AtAccountingCategories;
                item.LabelText = AtAccountingCategories.AccountingCategoriesName;
                item.ButtonText = AtAccountingCategories.Id;
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
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountingCategories).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountingCategories).AccountingCategoriesName;
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
                obj = (Model.AtAccountingCategories)value;
            }
        }

        #endregion
    }
}
