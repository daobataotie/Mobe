using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.PT
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoicePTManager invoicePTManager = new Book.BL.InvoicePTManager();
        protected BL.InvoicePTDetailManager invoiceDetailManager = new Book.BL.InvoicePTDetailManager();
        /// <summary>
        /// 被编辑的单据
        /// </summary>
        protected Book.Model.InvoicePT invoice = null;

        #region Constructors

        private ViewForm()
        {
            InitializeComponent();
        }


        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoicePTManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoicePT initInvoicePt)
            : this()
        {
            if (initInvoicePt == null)
                throw new ArithmeticException("InvoicePT");
            this.invoice = initInvoicePt;
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
            //this.buttonEditDepot1.EditValue = this.invoice.Depot1;
            //this.buttonEditDepot0.EditValue = this.invoice.Depot0;

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