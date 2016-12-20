using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.TradeCategory
{
    public class ChooseTradeCategory : IChoose
    {
        private Model.TradeCategory obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseTradeCategoryForm f = new ChooseTradeCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.TradeCategory tradeCategory = f.SelectedItem as Model.TradeCategory;
                item = new ChooseItem(tradeCategory, tradeCategory.Id, tradeCategory.TradeCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.TradeCategoryManager manager = new Book.BL.TradeCategoryManager();
            Model.TradeCategory tradeCategory = manager.GetById(item.ButtonText);
            if (tradeCategory != null)
            {
                item.EditValue = tradeCategory;
                item.LabelText = tradeCategory.TradeCategoryName;
                item.ButtonText = tradeCategory.Id;
            }
            else
            {
                item.ErrorMessage = "行业类别错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.TradeCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.TradeCategory).TradeCategoryName;
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
                obj = (Model.TradeCategory)value;
            }
        }

        #endregion
    }
}
