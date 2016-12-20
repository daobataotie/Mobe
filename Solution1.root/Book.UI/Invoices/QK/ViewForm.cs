using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Invoices.QK
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceQKManager invoiceManager = new Book.BL.InvoiceQKManager();
        protected Model.InvoiceQK invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(Model.InvoiceQK invoice)
            : this()
        {
            this.invoice = invoice;
        }

        public ViewForm(string invoiceid)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceid);
        }


        #endregion

        #region FormLoad

        private void ViewForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.Text = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            
            this.textEditNote.Text = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.calcEditMoney0.EditValue = this.invoice.InvoiceTotal0;
            this.calcEditMoney1.EditValue = this.invoice.InvoiceTotal1;
;

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