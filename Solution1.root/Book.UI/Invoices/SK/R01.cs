using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.SK
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceSKManager invoiceManager = new Book.BL.InvoiceSKManager();

        private Model.InvoiceSK invoice;

        public R01(string invoiceId)
        {
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                throw new ArgumentException();            

            this.xrLabelAbstract.Text = this.invoice.InvoiceAbstract;
            this.xrLabelAccount.Text = this.invoice.Account.AccountName;
            this.xrLabelCompanyAddress.Text = "";
            this.xrLabelCompanyName.Text = this.invoice.Customer.CustomerFullName;
            this.xrLabelEmployee.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString();
            this.xrLabelInvoiceNote.Text = this.invoice.InvoiceNote;
            this.xrLabelPayMethod.Text = this.invoice.PayMethod.PayMethodName;
            this.xrLabelTotal.Text = this.invoice.InvoiceTotal.Value.ToString("0.00");            
        }

    }
}
