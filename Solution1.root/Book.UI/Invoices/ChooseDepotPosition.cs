using System;
using System.Collections.Generic;
using System.Text;

namespace Book.UI.Invoices
{
    class ChooseDepotPosition : IChoose
    {
        private Model.DepotPosition obj;

        private Model.Depot depot;

        public ChooseDepotPosition(Model.Depot depot) 
        {
            this.depot = depot;
        }

        #region IChoose Members

        public string ButtonText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.DepotPosition).Id;
            }
        }

        public string LableText
        {
            get
            {
                return EditValue == null ? string.Empty : (EditValue as Model.DepotPosition).DepotPositionName;
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
                obj = (Model.DepotPosition)value;
            }
        }

        public void MyClick(ref ChooseItem item)
        {
            ChooseDepotPositionForm f = new ChooseDepotPositionForm(this.depot);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.DepotPosition depotPosition = f.SelectedItem as Model.DepotPosition;
                item = new ChooseItem(depotPosition, depotPosition.Id, depotPosition.DepotPositionName);
            }
        }

        public void MyLeave(ref ChooseItem item)
        {
            BL.DepotPositionManager manager = new Book.BL.DepotPositionManager();
            Model.DepotPosition depotPosition = manager.GetById(item.ButtonText);
            if (depotPosition != null)
            {
                item.EditValue = depotPosition;
                item.LabelText = depotPosition.DepotPositionName;
                item.ButtonText = depotPosition.Id;
            }
            else
            {
                item.ErrorMessage = Properties.Resources.ChooseDepotError;
            }
        }
        #endregion
    }
}
