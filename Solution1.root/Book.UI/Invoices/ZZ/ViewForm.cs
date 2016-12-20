using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZZ
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceZZDetailManager invoiceDetailManager = new Book.BL.InvoiceZZDetailManager();
        protected BL.InvoiceZZManager invoiceZZManager = new Book.BL.InvoiceZZManager();

        protected Model.InvoiceZZ invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceZZManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("invoiceid");
            }
        }
        public ViewForm(Model.InvoiceZZ initZZ)
            : this()
        {
            if (initZZ == null)
            {
                throw new ArithmeticException("invoiceid");
            }
            this.invoice = initZZ;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.DetailsIn = invoiceDetailManager.Select("I", this.invoice);
            this.invoice.DetailsOut = invoiceDetailManager.Select("O", this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.bindingSourceIn.DataSource = this.invoice.DetailsIn;
            this.bindingSourceOut.DataSource = this.invoice.DetailsOut;
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