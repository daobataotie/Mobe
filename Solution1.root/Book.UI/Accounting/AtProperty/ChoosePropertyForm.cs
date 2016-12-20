using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Accounting.AtProperty
{
    public partial class ChoosePropertyForm : Settings.BasicData.BaseChooseForm
    {
        public ChoosePropertyForm()
        {
            InitializeComponent();
            this.manager = new BL.AtPropertyManager();
        }
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Accounting.AtProperty.EditForm();
        }
    }
}