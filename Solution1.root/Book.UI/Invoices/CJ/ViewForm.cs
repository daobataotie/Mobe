using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.CJ
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceCJManager invoiceCJManager = new Book.BL.InvoiceCJManager();
        protected BL.InvoiceCJDetailManager invoiceDetailManager = new Book.BL.InvoiceCJDetailManager();

        private Model.InvoiceCJ invoice;

        #region Constructors

        private ViewForm() 
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceCJManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceCJ invoicecj)
            : this()
        {
            if (invoicecj == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoicecj;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
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