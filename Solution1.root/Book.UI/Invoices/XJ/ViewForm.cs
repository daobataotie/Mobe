using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XJ
{
    public partial class ViewForm : BaseViewForm
    {

        protected BL.InvoiceXJManager invoiceXJManager = new Book.BL.InvoiceXJManager();
        protected BL.InvoiceXJDetailManager invoiceDetailManager = new Book.BL.InvoiceXJDetailManager();

        private Model.InvoiceXJ invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceXJManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceXJ invoicecj)
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
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
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