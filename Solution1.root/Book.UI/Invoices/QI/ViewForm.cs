using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.QI
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceQIManager invoiceQIManager = new Book.BL.InvoiceQIManager();
        protected BL.InvoiceQIDetailManager invoiceDetailManager = new Book.BL.InvoiceQIDetailManager();

        private Model.InvoiceQI invoice;  

        #region Constructors

        private ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceQIManager.Get(invoiceId);
            if (this.invoice == null) 
            {
                throw new ArithmeticException("invoiceId");
            }
        }
        public ViewForm(Model.InvoiceQI invoiceQi)
            : this()
        {
            if (invoiceQi == null) 
            {
                throw new ArithmeticException("invoiceQi");
            }
            this.invoice = invoiceQi;
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