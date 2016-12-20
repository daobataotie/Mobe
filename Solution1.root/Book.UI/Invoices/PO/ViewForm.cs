using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.PO
{
    public partial class ViewForm : BaseViewForm
    {
        protected BL.InvoicePODetailManager invoiceDetailManager = new Book.BL.InvoicePODetailManager();
        protected BL.InvoicePOManager invoicePOManager = new Book.BL.InvoicePOManager();

        private Model.InvoicePO invoice;

        #region Constructors

        private ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoicePOManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
        }

        public ViewForm(Model.InvoicePO initInvoicePO)
            : this()
        {
            if (initInvoicePO == null)
                throw new ArithmeticException("initInvoicePO");
            this.invoice = initInvoicePO;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;            
            
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.buttonEditDepartment.EditValue = this.invoice.Department;

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

            IList<Model.InvoicePODetail> invoiceCgDetails = this.bindingSource1.DataSource as IList<Model.InvoicePODetail>;

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