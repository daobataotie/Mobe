using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.FK
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.Invoice01Manager invoice01Manager = new Book.BL.Invoice01Manager();
        protected BL.InvoiceFKManager invoiceFKManager = new Book.BL.InvoiceFKManager();

        /// <summary>
        /// insert or update
        /// </summary>
        
        /// <summary>
        /// 被修改的单据
        /// </summary>        
        private Model.InvoiceFK invoice;
        
        

        #region Constructors

        private ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceFKManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("invoiceid");
            }
        }

        public ViewForm(Model.InvoiceFK invoiceFk)
            : this()
        {
            if (invoiceFk == null)
            {
                throw new ArithmeticException("invoiceFk");
            }
            this.invoice = invoiceFk;
        }
        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoice01Manager.Select("Fk_View", this.invoice.Customer.CustomerId, this.invoice.InvoiceId);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.buttonEditAccount.EditValue = this.invoice.Account;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditPayMethod.EditValue = this.invoice.PayMethod;

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