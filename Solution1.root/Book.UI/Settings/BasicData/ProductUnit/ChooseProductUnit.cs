using System;
using System.Collections.Generic;
using System.Text;
using Book.UI.Invoices;

namespace Book.UI.Settings.BasicData.ProductUnit
{
    public class ChooseProductUnit : IChoose
    {

        private Model.ProductUnit productUnit = new Book.Model.ProductUnit();
        private string unitGroupId;

        public string UnitGroupId
        {
            get { return unitGroupId; }
            set { unitGroupId = value; }
        }

        public ChooseProductUnit(string groupId)
            : this()
        {
            this.unitGroupId = groupId;
        }

        public ChooseProductUnit(Model.UnitGroup unitGroup)
            : this(unitGroup.UnitGroupId)
        {

        }

        public ChooseProductUnit() 
        {

        }
        private Model.ProductUnit obj;

        #region IChoose 成员

        public void MyClick(ref ChooseItem item)
        {
            ChooseProductUnitForm f = new ChooseProductUnitForm(unitGroupId);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                productUnit = f.SelectedItem as Model.ProductUnit;
                item = new ChooseItem(productUnit, productUnit.ProductUnitId, productUnit.CnName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
            Model.ProductUnit unit = manager.Get(productUnit.ProductUnitId);
            if (unit != null)
            {
                item.EditValue = unit;
                item.LabelText = string.Empty;
                item.ButtonText = unit.CnName;
            }
            else
            {
                item.ErrorMessage = "单位错误";
            }
        }

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.ProductUnit).CnName;
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
                obj = (Model.ProductUnit)value;
            }
        }

        #endregion
    }
}
