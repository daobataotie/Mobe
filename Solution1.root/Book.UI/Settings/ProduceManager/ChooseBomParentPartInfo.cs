using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.ProduceManager
{
    public class ChooseBomParentPartInfo : IChoose
    {
        private Model.BomParentPartInfo obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseBomParentPartInfoForm f = new ChooseBomParentPartInfoForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.BomParentPartInfo BomParentPartInfo = f.SelectedItem as Model.BomParentPartInfo;
                item = new ChooseItem(BomParentPartInfo, BomParentPartInfo.Id, BomParentPartInfo.Product.ProductName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.BomParentPartInfoManager manager = new Book.BL.BomParentPartInfoManager();
            Model.BomParentPartInfo BomParentPartInfo = manager.GetById(item.ButtonText);
            if (BomParentPartInfo != null)
            {
                item.EditValue = BomParentPartInfo;
                item.LabelText = BomParentPartInfo.Product.ProductName;
                item.ButtonText = BomParentPartInfo.Id;
            }
            else
            {
                item.ErrorMessage = "BOM单錯誤";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.BomParentPartInfo).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.BomParentPartInfo).Product.ProductName;
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
                obj = (Model.BomParentPartInfo)value;
            }
        }

        #endregion
    }
}
