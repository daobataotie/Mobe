using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.AreaCategory
{
    public class ChooseAreaCategory : IChoose
    {
        private Model.AreaCategory obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseAreaCategoryForm f = new ChooseAreaCategoryForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AreaCategory tradeCategory = f.SelectedItem as Model.AreaCategory;
                item = new ChooseItem(tradeCategory, tradeCategory.Id, tradeCategory.AreaCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AreaCategoryManager manager = new Book.BL.AreaCategoryManager();
            Model.AreaCategory tradeCategory = manager.GetById(item.ButtonText);
            if (tradeCategory != null)
            {
                item.EditValue = tradeCategory;
                item.LabelText = tradeCategory.AreaCategoryName;
                item.ButtonText = tradeCategory.Id;
            }
            else
            {
                item.ErrorMessage = "地区类别错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AreaCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AreaCategory).AreaCategoryName;
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
                obj = (Model.AreaCategory)value;
            }
        }

        #endregion
    }
}
