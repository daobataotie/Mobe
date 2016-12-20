using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Invoices.ZS
{
    public partial class ViewForm : BaseViewForm
    {
        BL.InvoiceZSManager invoiceZSManager = new Book.BL.InvoiceZSManager();
        BL.InvoiceZSDetailManager invoiceDetailManager = new Book.BL.InvoiceZSDetailManager();

        private Model.InvoiceZS invoice;

        #region Constructors

        public ViewForm()
        {
            InitializeComponent();
        }

        public ViewForm(Model.InvoiceZS invoicezs)
            : this()
        {
            this.invoice = invoicezs;
        }

        public ViewForm(string invoiceId)
            : this()
        {
            this.invoice = invoiceZSManager.Get(invoiceId);
            if (this.invoice == null)
            {
                throw new ArithmeticException("invoiceZSManager");
            }
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            this.bindingSource1.DataSource = this.invoice.Details;
        }

        private void update()
        {
            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;
            

            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditDepot.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditCompany.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceZSDetailPrice.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceZSDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceZSDetailNote.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
        }

        #endregion

        #region Overloaded

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
        }

        #endregion

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            switch (e.Column.Name)
            {
                case "gridColumn1":
                    IList<Model.InvoiceZSDetail> invoiceZsDetails = this.bindingSource1.DataSource as IList<Model.InvoiceZSDetail>;
                    if (invoiceZsDetails.Count > 0)
                    {
                        e.DisplayText = invoiceZsDetails[e.ListSourceRowIndex].Product.MainUnit.CnName;
                    }
                    break;

                default:
                    break;
            }
        }

    }
}