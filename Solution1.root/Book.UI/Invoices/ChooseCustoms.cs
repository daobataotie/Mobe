using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseCustoms : Settings.BasicData.BaseChooseForm
    {
        #region Construcotrs

        public ChooseCustoms()
        {
            InitializeComponent();
            this.manager = new BL.CustomerManager();
        }  

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.Customs.EditForm();
        }

        protected override void LoadData()
        {
            this.bindingSource1.DataSource = ((BL.CustomerManager)this.manager).Select();
        }

    }
}