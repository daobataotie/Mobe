using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.HZ
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceHZDetailManager invoiceDetailManager = new Book.BL.InvoiceHZDetailManager();
        protected BL.InvoiceHZManager invoiceHZManager = new Book.BL.InvoiceHZManager();

        private Model.InvoiceHZ invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceHZManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoiceHZ initInvoiceHZ)
            : this()
        {
            if (initInvoiceHZ == null)
                throw new ArithmeticException("initInvoiceHZ");
            this.invoice = initInvoiceHZ;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {

            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
                        
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;

            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            this.bindingSource1.DataSource = this.invoice.Details;
        }

        private void update()
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;
            
                        
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditDepot.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditCompany.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceHZDetailPrice.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceHZDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceHZDetailNote.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
            
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