using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.UnitGroup
{
    public class ChooseUnitGroup : IChoose
    {
        private Model.UnitGroup obj;

        #region IChoose 成员

        private Model.UnitGroup units=new Book.Model.UnitGroup();
        public void MyClick(ref ChooseItem item)
        {
            ChooseUnitGroupForm f = new ChooseUnitGroupForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                units = f.SelectedItem as Model.UnitGroup;             
                item = new ChooseItem(units, units.UnitGroupId, units.UnitGroupName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.UnitGroupManager manager = new Book.BL.UnitGroupManager();
            Model.UnitGroup unit = manager.Get(units.UnitGroupId);
            if (unit != null)
            {
                item.EditValue = unit;
                item.LabelText = string.Empty;
                item.ButtonText = unit.UnitGroupName;
            }
            else
            {
                item.ErrorMessage = "单位组错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.UnitGroup).UnitGroupName;
            }
        }

        public string LableText
        {
            get
            {
                return string.Empty;
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
                obj = (Model.UnitGroup)value;
            }
        }

        #endregion
    }
}
