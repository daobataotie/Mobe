using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseSupplier: Settings.BasicData.BaseChooseForm
    {
        #region Construcotrs

        public ChooseSupplier()
        {
            InitializeComponent();
            this.manager = new BL.SupplierManager();            
        }

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Supplier.EditForm();
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = ((BL.SupplierManager)this.manager).Select();
        }

    }
}