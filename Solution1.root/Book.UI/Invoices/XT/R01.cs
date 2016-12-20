using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XT
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXTManager InvoiceXTManager = new Book.BL.InvoiceXTManager();
        private BL.InvoiceXTDetailManager InvoiceXTDetailManager = new Book.BL.InvoiceXTDetailManager();

        private Model.InvoiceXT invoice;

        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.InvoiceXTManager.Get(invoiceid);

            if (this.invoice == null)
                return;

            this.invoice.Details = this.InvoiceXTDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCompanyInfoFAX.Text = BL.Settings.CompanyFax;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;

            this.xrLabelData.Text = Properties.Resources.InvoiceXT;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            //每页合计统计
            this.xrLabelPateTotal.Summary.Running = SummaryRunning.Page;
            this.xrLabelPateTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelPateTotal.Summary.FormatString = global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value);
            this.xrLabelPateTotal.Summary.IgnoreNullValues = true;
            this.xrLabelPateTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney0, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSZJXiao.Value));

            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Customer.CustomerShortName;
            this.xrLabelCustomFax.Text = this.invoice.Customer.CustomerFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Customer.CustomerPhone) ? this.invoice.Customer.CustomerPhone1 : this.invoice.Customer.CustomerPhone;
            this.xrLabelTongYiNo.Text = this.invoice.Customer.CustomerNumber;
            this.xrLabelSongHuoAddress.Text = this.invoice.Customer.CustomerJinChuAddress;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd hh:mm:ss");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelInvoiceFphm.Text = this.invoice.InvoiceFpbh;

            this.xrLabelTax.Text = this.invoice.InvoiceTax.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelZre.Text = this.invoice.InvoiceZRE.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelZse.Text = this.invoice.InvoiceZSE.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelOwced.Text = this.invoice.InvoiceOwed.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelYifu.Text = ((decimal)(this.invoice.InvoiceZongJi.Value - this.invoice.InvoiceOwed.Value)).ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelTotal1.Text = this.invoice.InvoiceZongJi.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelheji.Text = this.invoice.InvoiceHeJi.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailQuantity);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailPrice, "{0:0.###}");
            this.xrCheckBoxIsZs.DataBindings.Add("CheckState", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailZS);
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXTDetail.PRO_InvoiceXTDetailMoney1, "{0:0.###}");
        }

    }
}
