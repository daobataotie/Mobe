using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs
{
    public partial class ChooseCustomerMarksForm : BaseChooseForm
    {
        public ChooseCustomerMarksForm()
        {
            InitializeComponent();
        }

        public ChooseCustomerMarksForm(IList<Invoices.XO.EditForm.marks> list)
            : this()
        {
            this.bindingSource1.DataSource = list;
        }

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm1 GetEditForm1()
        {
            return base.GetEditForm1();
        }
    }
}