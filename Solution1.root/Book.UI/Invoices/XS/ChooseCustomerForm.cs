using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XS
{
    public partial class ChooseCustomerForm : DevExpress.XtraEditors.XtraForm
    {
        public ChooseCustomerForm()
        {
            InitializeComponent();
            this.newChooseCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this._customer = this.newChooseCustomer.EditValue as Model.Customer;
            if (this._customer != null)
            {
                this.DialogResult = DialogResult.OK;                
            }
            else
            {
                MessageBox.Show(Properties.Resources.RequireDataForCompany, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }            

        }
        private Model.Customer _customer;
        public Model.Customer Customer
        {
            get { return _customer; }       
      
        }
        public XSForm GetXSForm()
        {
            return new XSForm(this._customer);
        }

        private void ChooseCustomerForm_Load(object sender, EventArgs e)
        {          
            
        }
    }
}