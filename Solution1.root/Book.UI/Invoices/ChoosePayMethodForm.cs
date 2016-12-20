using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChoosePayMethodForm :Settings.BasicData.BaseChooseForm
    {
       #region Construcotrs

        public ChoosePayMethodForm()
        {
            InitializeComponent();
            this.manager = new Book.BL.PayMethodManager();
        }

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.PayMethods.EditForm();
        }

    }
}