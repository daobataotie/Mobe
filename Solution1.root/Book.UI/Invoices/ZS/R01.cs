using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZS
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZSManager invoiceManager = new Book.BL.InvoiceZSManager();
        protected BL.InvoiceZSDetailManager invoiceDetailManager = new Book.BL.InvoiceZSDetailManager();

        private Model.InvoiceZS invoice;

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
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName + Properties.Resources.InvoiceZS;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;

            //每页合计统计
            this.xrLabelPageTotal.Summary.Running = SummaryRunning.Page;
            this.xrLabelPageTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelPageTotal.Summary.FormatString = "{0:0}";
            this.xrLabelPageTotal.Summary.IgnoreNullValues = true;
            this.xrLabelPageTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILMONEY, "{0:0}");

            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Customer.CustomerShortName;
            this.xrLabelCustomFax.Text = this.invoice.Customer.CustomerFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Customer.CustomerPhone) ? this.invoice.Customer.CustomerPhone1 : this.invoice.Customer.CustomerPhone;
            this.xrLabelTongYiNo.Text = this.invoice.Customer.CustomerNumber;
            this.xrLabelSongHuoAddress.Text = this.invoice.Customer.CustomerJinChuAddress;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;            
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            this.xrLabelReportTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelReportTotal.Summary.IgnoreNullValues = true;
            this.xrLabelReportTotal.Summary.Running = SummaryRunning.Report;
            this.xrLabelReportTotal.Summary.FormatString = "{0:0}";
            this.xrLabelReportTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILMONEY, "{0:0}");

            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEPRODUCTUNIT);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILQUANTITY);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILPRICE, "{0:0}");
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILMONEY, "{0:0}");
            this.xrTableDetailNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZSDetail.PROPERTY_INVOICEZSDETAILNOTE);
        }

    }
}
