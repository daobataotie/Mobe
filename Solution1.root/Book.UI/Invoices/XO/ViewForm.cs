using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XO
{
    public partial class ViewForm : BaseViewForm
    {
        BL.InvoiceXOManager invoiceManager = new Book.BL.InvoiceXOManager();
        BL.InvoiceXODetailManager invoiceDetailManager = new Book.BL.InvoiceXODetailManager();

        private Model.InvoiceXO invoice;

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

        public ViewForm(Model.InvoiceXO invoicexo)
            : this()
        {
            this.invoice = invoicexo;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {

            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice,false);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
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