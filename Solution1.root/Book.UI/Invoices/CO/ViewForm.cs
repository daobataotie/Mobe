using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CO
{
    public partial class ViewForm : BaseViewForm
    {
        BL.InvoiceCOManager invoiceManager = new Book.BL.InvoiceCOManager();
        BL.InvoiceCODetailManager invoiceDetailManager = new Book.BL.InvoiceCODetailManager();
        
        private Model.InvoiceCO invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("");
            }
        }

        public ViewForm(Model.InvoiceCO invoiceco)
            : this()
        {
            this.invoice = invoiceco;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            this.bindingSource1.DataSource = this.invoice.Details;
        }

        #endregion


        #region Overloaded

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
        }

        #endregion

    }
}