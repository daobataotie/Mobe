using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    public  class ChooseProductCategories:IChoose
    {

        private Model.ProductCategory ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseForm  f = new ChooseForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.ProductCategory productCaregory = f.SelectedItem as Model.ProductCategory;
                item = new ChooseItem(productCaregory, productCaregory.Id, productCaregory.ProductCategoryName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.ProductCategoryManager manager = new Book.BL.ProductCategoryManager();
            Model.ProductCategory productCaregory = manager.GetById(item.ButtonText);

            if (productCaregory != null)
            {
                item.EditValue = productCaregory;
                item.LabelText = productCaregory.ProductCategoryName;
                item.ButtonText = productCaregory.Id;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.ProductCategory).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.ProductCategory).ProductCategoryName;
            }
        }

        public object EditValue
        {
            get
            {
                return ojb;
            }
            set
            {
                ojb = (Model.ProductCategory)value;
            }
        }

        #endregion
    }
}
