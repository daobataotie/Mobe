using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices
{
    public partial class ChooseDepotPositionForm : Settings.BasicData.BaseChooseForm
    {
        private Model.Depot _depot;
        public ChooseDepotPositionForm(Model.Depot depot)
        {
            InitializeComponent();
            this.manager = new BL.DepotPositionManager();
            this._depot= depot;
        }

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Depots.AddDepotpositionForm();
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = (this.manager as BL.DepotPositionManager).Select(this._depot);
        }
    }
}