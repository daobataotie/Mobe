using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.PackageType
{
    public class ChoosePackageType : IChoose
    {
        private Model.PackageType ojb;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChoosePackageTypeForm f = new ChoosePackageTypeForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.PackageType packageType = f.SelectedItem as Model.PackageType;
                item = new ChooseItem(packageType, packageType.Id, packageType.PackagePypeName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.PackageTypeManager manager = new Book.BL.PackageTypeManager();
            Model.PackageType packageType = manager.GetById(item.ButtonText);
            if (packageType != null)
            {
                item.EditValue = packageType;
                item.LabelText = packageType.PackagePypeName;
                item.ButtonText = packageType.Id;
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.PackageType).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.PackageType).PackagePypeName;
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
                ojb = (Model.PackageType)value;
            }
        }

        #endregion
    }
}
