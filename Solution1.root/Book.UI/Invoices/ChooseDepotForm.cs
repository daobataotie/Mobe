using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseDepotForm : Settings.BasicData.BaseChooseForm
    {
        #region Construcotrs

        public ChooseDepotForm()
        {
            InitializeComponent();
            this.manager = new BL.DepotManager();
        }

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Depots.AddDepotpositionForm();
        }
    }
}