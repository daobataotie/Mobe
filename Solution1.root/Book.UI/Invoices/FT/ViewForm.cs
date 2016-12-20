using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.FT
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceFTManager invoiceFTManager = new Book.BL.InvoiceFTManager();

        
        private Model.InvoiceFT invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceFTManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArgumentNullException("invoiceId");
        }

        public ViewForm(Model.InvoiceFT invoiceFT):this()
        {
            if(invoiceFT==null)
                throw new ArgumentNullException("invoiceFT");
            this.invoice = invoiceFT;
        }
        #endregion

        #region FormLoad

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditAccount1.EditValue = this.invoice.Account1;
            this.buttonEditAccount2.EditValue = this.invoice.Account2;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;            
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