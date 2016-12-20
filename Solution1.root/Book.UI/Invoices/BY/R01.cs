using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.BY
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceBYManager invoiceManager = new Book.BL.InvoiceBYManager();
        protected BL.InvoiceBYDetailManager invoiceDetailManager = new Book.BL.InvoiceBYDetailManager();

        private Model.InvoiceBY invoice;

        public R01(string invoiceId)
        {
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                return;

            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCompanyInfoFAX.Text = BL.Settings.CompanyFax;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName + Properties.Resources.InvoiceBY;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;

            //每页合计统计
            this.xrLabelPateTotal.Summary.Running = SummaryRunning.Page;
            this.xrLabelPateTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelPateTotal.Summary.FormatString = "{0:0}";
            this.xrLabelPateTotal.Summary.IgnoreNullValues = true;
            this.xrLabelPateTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceBYDetail.PROPERTY_INVOICEBYDETAILQUANTITY, "{0:0}");

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelNote.Text = this.invoice.InvoiceNote;
            //this.xrLabelDepot.Text = this.invoice.Depot.DepotName;

            this.xrLabelTotal1.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotal1.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotal1.Summary.IgnoreNullValues = true;
            this.xrLabelTotal1.DataBindings.Add("Text", this.DataSource, Model.InvoiceBYDetail.PROPERTY_INVOICEBYDETAILQUANTITY, "{0:0}");

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceBYDetail.PROPERTY_INVOICEPRODUCTUNIT);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceBYDetail.PROPERTY_INVOICEBYDETAILQUANTITY, "{0:0}");
            this.xrTableCellProductNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceBYDetail.PROPERTY_INVOICEBYDETAILNOTE);

        }

    }
}
