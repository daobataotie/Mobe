using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.JC
{

    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceJCDetailManager invoiceDetailManager = new Book.BL.InvoiceJCDetailManager();
        protected BL.InvoiceJCManager invoiceJCManager = new Book.BL.InvoiceJCManager();

        private Model.InvoiceJC invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceJCManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceJC initInvoiceJC)
            : this()
        {
            if (initInvoiceJC == null)
                throw new ArithmeticException("initInvoiceJC");
            this.invoice = initInvoiceJC;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.textEditInvoiceSendAddress.EditValue = this.invoice.InvoiceSendAddress;
            //this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;

            this.bindingSource1.DataSource = this.invoice.Details;
        }

        #endregion


        #region Overloaded

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
            //return new R01(this.invoice.InvoiceId);
        }

        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.InvoiceJCDetail> invoiceCgDetails = this.bindingSource1.DataSource as IList<Model.InvoiceJCDetail>;

            if (invoiceCgDetails == null || invoiceCgDetails.Count <= 0)
                return;

            switch (e.Column.Name)
            {
                case "gridColumn4":
                    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProductSpecification;
                    break;
                case "gridColumn5":
                    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProductSpecification;
                    break;
            }
        }
    }
}