using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.QO
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceQOManager invoiceQOManager = new Book.BL.InvoiceQOManager();
        protected BL.InvoiceQODetailManager invoiceDetailManager = new Book.BL.InvoiceQODetailManager();

        private Model.InvoiceQO invoice;        
    
        #region Constuctors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceid):this()
        {
            this.invoice = invoiceQOManager.Get(invoiceid);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceQO invoiceQO):this()
        {
            if (invoiceQO == null)
                throw new ArithmeticException("invoiceQO");
            this.invoice = invoiceQO;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditAccount.EditValue = this.invoice.Account;
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