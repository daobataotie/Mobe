using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class ListForm : Settings.BasicData.BaseListForm
    {
        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.AcOtherShouldPaymentManager();
        }
        
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new AccountPayable.AcOtherShouldPayment.EditForm();
        }

        public Model.AcOtherShouldPayment SelectItem
        {
            get { return this.bindingSource1.Current as Model.AcOtherShouldPayment; }
        }
        
        protected override Book.UI.Settings.BasicData.BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }
    }
}