using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.AccountingCategory
{
    class ChooseAccountingCategory : Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtAccountingCategory obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseAccountingCategoryForm f = new ChooseAccountingCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtAccountingCategory AtAccountingCategory = f.SelectedItem as Model.AtAccountingCategory;
                item = new ChooseItem(AtAccountingCategory, AtAccountingCategory.Id, AtAccountingCategory.AccountingCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtAccountingCategoryManager manager = new Book.BL.AtAccountingCategoryManager();
            Model.AtAccountingCategory AtAccountingCategory = manager.GetById(item.ButtonText);
            if (AtAccountingCategory != null)
            {
                item.EditValue = AtAccountingCategory;
                item.LabelText = AtAccountingCategory.AccountingCategoryName;
                item.ButtonText = AtAccountingCategory.Id;
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
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountingCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtAccountingCategory).AccountingCategoryName;
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
                obj = (Model.AtAccountingCategory)value;
            }
        }

        #endregion
    }
}
