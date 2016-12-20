using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.CO
{
    public partial class ChooseInvoiceForm : DevExpress.XtraEditors.XtraForm 
    {
        protected BL.InvoiceCOManager invoiceManager = new Book.BL.InvoiceCOManager();

        public ChooseInvoiceForm()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void ChooseInvoiceForm_Load(object sender, EventArgs e)
        {
            DateTime datetime2 = DateTime.Today.AddDays(1);
            DateTime datetime1 = datetime2.AddMonths(-1);

            this.invoiceCOBindingSource.DataSource = invoiceManager.Select(datetime1, datetime2);
        }

        public Model.InvoiceCO SelectedItem
        {
            get
            {
                return this.invoiceCOBindingSource.Current as Model.InvoiceCO;
            }
        }
    }
}