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
    public partial class XSForm : DevExpress.XtraEditors.XtraForm
    {      
        public XSForm()
        {
            InitializeComponent();            
        }
        public XSForm(Model.Customer customer)
            : this()
        {   
        
        }
        public XSForm(IList<Model.InvoiceXO> invoiceXOs)
            : this()
        {
            this.bindingSource1.DataSource = invoiceXOs;
        }
        private Model.InvoiceXO _invoice;

        public Model.InvoiceXO Invoice
        {
            get
            {
                return _invoice;
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
           
            _invoice = this.bindingSource1.Current as Model.InvoiceXO;
            XSDetailForm f = new XSDetailForm(_invoice);
            if (_invoice != null)
            {
                if (f.ShowDialog() == DialogResult.OK) 
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.RequireDataForCJ, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceXO> details = this.bindingSource1.DataSource as IList<Model.InvoiceXO>;
            if (details == null || details.Count < 1) return;
            Model.InvoiceXO detail = details[e.ListSourceRowIndex] as Model.InvoiceXO;
            if (e.Column.Name == "gridXSCustomer")
            {
                if (detail == null) return;

                if (detail.xocustomer!=null)
                    e.DisplayText = string.IsNullOrEmpty(detail.xocustomer.CustomerShortName) ? detail.Customer.CustomerShortName : detail.xocustomer.CustomerShortName;
            
            
            }
        }
    }
}