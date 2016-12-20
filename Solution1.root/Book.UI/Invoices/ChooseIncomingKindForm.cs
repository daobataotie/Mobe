using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices
{
    public partial class ChooseIncomingKindForm : Settings.BasicData.BaseChooseForm
    {
        #region Construcotrs

        public ChooseIncomingKindForm()
        {
            InitializeComponent();
            this.manager = new Book.BL.IncomingKindManager();
        }

        #endregion

        protected override UI.Settings.BasicData.BaseEditForm1 GetEditForm1()
        {
            return new UI.Settings.BasicData.IncomingKinds.EditForm();
        }
    }
}