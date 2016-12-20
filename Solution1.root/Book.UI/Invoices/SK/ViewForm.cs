using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.SK
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.Invoice01Manager invoice01Manager = new Book.BL.Invoice01Manager();
        protected BL.InvoiceSKManager invoiceSKManager = new Book.BL.InvoiceSKManager();
        
        /// <summary>
        /// 被修改的单据
        /// </summary>        
        private Model.InvoiceSK invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceSKManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("invoiceid");
            }
        }

        public ViewForm(Model.InvoiceSK invoice)
            : this()
        {
            if (invoice == null)
            {
                throw new ArithmeticException("invoice");
            }
            this.invoice = invoice;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoice01Manager.Select("Sk_View", this.invoice.Customer.CustomerId, this.invoice.InvoiceId);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.buttonEditAccount.EditValue = this.invoice.Account;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditPayMethod.EditValue = this.invoice.PayMethod;
            this.textEditPay.EditValue = this.invoice.Customer == null ? decimal.Zero : this.invoice.Customer.CustomerReceivable;

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