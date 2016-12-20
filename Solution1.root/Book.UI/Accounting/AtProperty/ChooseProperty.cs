using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;
namespace Book.UI.Accounting.AtProperty
{
    class ChooseProperty : Invoices.IChoose
    {
        /// <summary>
        /// 
        /// 物料类型
        /// </summary>
        private Model.AtProperty obj;

        #region IChoose 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void MyClick(ref ChooseItem item)
        {
            ChoosePropertyForm f = new ChoosePropertyForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.AtProperty AtProperty = f.SelectedItem as Model.AtProperty;
                item = new ChooseItem(AtProperty, AtProperty.Id, AtProperty.PropertyName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.AtPropertyManager manager = new Book.BL.AtPropertyManager();
            Model.AtProperty AtProperty = manager.GetById(item.ButtonText);
            if (AtProperty != null)
            {
                item.EditValue = AtProperty;
                item.LabelText = AtProperty.Id;
                item.ButtonText = AtProperty.PropertyName;
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
                return EditValue == null ? string.Empty : (EditValue as Model.AtProperty).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.AtProperty).PropertyName;
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
                obj = (Model.AtProperty)value;
            }
        }

        #endregion
    }
}
