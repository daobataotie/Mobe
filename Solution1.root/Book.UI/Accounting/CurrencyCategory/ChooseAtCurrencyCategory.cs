using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.CurrencyCategory
{
    class ChooseAtCurrencyCategory : Invoices.IChoose
    { /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtCurrencyCategory obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChooseAtCurrencyCategoryForm f = new ChooseAtCurrencyCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtCurrencyCategory AtCurrencyCategory = f.SelectedItem as Model.AtCurrencyCategory;
                item = new ChooseItem(AtCurrencyCategory, AtCurrencyCategory.Id, AtCurrencyCategory.AtCurrencyName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtCurrencyCategoryManager manager = new Book.BL.AtCurrencyCategoryManager();
            Model.AtCurrencyCategory AtCurrencyCategory = manager.GetById(item.ButtonText);
            if (AtCurrencyCategory != null)
            {
                item.EditValue = AtCurrencyCategory;
                item.LabelText = AtCurrencyCategory.AtCurrencyName;
                item.ButtonText = AtCurrencyCategory.Id;
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
                return EditValue == null ? string.Empty : (EditValue as Model.AtCurrencyCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtCurrencyCategory).AtCurrencyName;
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
                obj = (Model.AtCurrencyCategory)value;
            }
        }

        #endregion
    }
}

