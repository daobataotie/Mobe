using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.XT
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoiceXTManager invoiceXTManager = new Book.BL.InvoiceXTManager();
        protected BL.InvoiceXTDetailManager invoiceDetailManager = new Book.BL.InvoiceXTDetailManager();

        /// <summary>
        /// 被修改的单据
        /// </summary>
        protected Book.Model.InvoiceXT invoice = null;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceid)
            : this()
        {
            this.invoice = this.invoiceXTManager.Get(invoiceid);
            if (this.invoice == null)
                throw new ArgumentNullException();
        }
        public ViewForm(Model.InvoiceXT initInvoicexs)
            : this()
        {
            if (initInvoicexs == null)
                throw new ArgumentNullException();
            this.invoice = initInvoicexs;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;

            this.spinEditInvoiceTaxRate1.EditValue = this.invoice.InvoiceTaxRate == null ? 5 : this.invoice.InvoiceTaxRate;
            this.calcEditInvoiceTax1.EditValue = this.invoice.InvoiceTax == null ? 0 : this.invoice.InvoiceTax;
            this.calcEditInvoiceTotal0.EditValue = this.invoice.InvoiceZongJi == null ? 0 : this.invoice.InvoiceZongJi; ;
            this.calcEditInvoiceTotal1.EditValue = this.invoice.InvoiceHeJi == null ? 0 : this.invoice.InvoiceHeJi; ;
            this.calcEditInvoiceZSE.EditValue = this.invoice.InvoiceZSE == null ? 0 : this.invoice.InvoiceZSE; ;

            this.dateEditInvoicePayTimeLimit.DateTime = this.invoice.InvoicePayTimeLimit.Value;

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