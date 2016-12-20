using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    public class ChooseDepot : IChoose
    {
        private Model.Depot obj;

        #region IChoose 成员

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Depot).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.Depot).DepotName;
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
                obj = (Model.Depot)value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            ChooseDepotForm f = new ChooseDepotForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.Depot depot = f.SelectedItem as Model.Depot;
                item = new ChooseItem(depot, depot.Id, depot.DepotName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.DepotManager manager = new Book.BL.DepotManager();
            Model.Depot depot = manager.GetById(item.ButtonText);
            if (depot != null)
            {
                item.EditValue = depot;
                item.LabelText = depot.DepotName;
                item.ButtonText = depot.Id;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChooseDepotError;
            }
        }

        #endregion
    }
}
