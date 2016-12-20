using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.PT
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoicePTManager invoiceManager = new Book.BL.InvoicePTManager();
        protected BL.InvoicePTDetailManager invoiceDetailManager = new Book.BL.InvoicePTDetailManager();

        private Model.InvoicePT invoice;

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
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;
            this.xrLabel28.Text = Properties.Resources.InvoicePT;

            //每页合计统计
            this.xrLabelPateTotal.Summary.Running = SummaryRunning.Page;
            this.xrLabelPateTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelPateTotal.Summary.FormatString = "{0:0}";
            this.xrLabelPateTotal.Summary.IgnoreNullValues = true;
            this.xrLabelPateTotal.DataBindings.Add("Text", this.DataSource, Model.InvoicePTDetail.PROPERTY_INVOICEPTDETAILQUANTITY, "{0:0}");

            //单据信息
            //this.xrLabelDepotIn.Text = this.invoice.Depot1.DepotName;
            //this.xrLabelDepotOut.Text = this.invoice.Depot0.DepotName;
            this.xrLabelDepotIn.Text = this.invoice.DepotIn == null ? "" : this.invoice.DepotIn.DepotName;
            this.xrLabelDepotOut.Text = this.invoice.Depot == null ? "" : this.invoice.Depot.DepotName;
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            this.xrLabelTotal.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotal.Summary.FormatString = "{0:0}";
            this.xrLabelTotal.Summary.IgnoreNullValues = true;
            this.xrLabelTotal.DataBindings.Add("Text", this.DataSource, Model.InvoicePTDetail.PROPERTY_INVOICEPTDETAILQUANTITY, "{0:0}");

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePTDetail.PROPERTY_INVOICEPRODUCTUNIT);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePTDetail.PROPERTY_INVOICEPTDETAILQUANTITY);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoicePTDetail.PROPERTY_INVOICEPTDETAILNOTE, "{0:0}");
            this.xrRichTextDesc.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
        }
    }
}
