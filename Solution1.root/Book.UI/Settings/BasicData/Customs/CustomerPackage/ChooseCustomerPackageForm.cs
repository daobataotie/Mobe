using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Customs.CustomerPackage
{
    public partial class ChooseCustomerPackageForm : BaseChooseForm
    {
        BL.CustomerPackageManager customerPackageManager = new Book.BL.CustomerPackageManager();
        private Model.Customer _customer;
        public ChooseCustomerPackageForm()
        {
            InitializeComponent();
            this.manager = new BL.CustomerPackageManager();
        }
        public ChooseCustomerPackageForm(Model.Customer customer):this()
        {
            this._customer = customer;        
        }

        private void ChooseCustomerPackageForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.customerPackageManager.Select(this._customer);
        }
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
    }
}