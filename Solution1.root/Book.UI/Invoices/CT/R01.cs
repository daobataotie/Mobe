using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CT
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCTManager InvoiceCTManager = new Book.BL.InvoiceCTManager();
        private BL.InvoiceCTDetailManager InvoiceCTDetailManager = new Book.BL.InvoiceCTDetailManager();

        private Model.InvoiceCT invoice;

        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.InvoiceCTManager.Get(invoiceid);

            if (this.invoice == null)
                throw new ArgumentException();

            this.invoice.Details = this.InvoiceCTDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCompanyInfoFAX.Text = BL.Settings.CompanyFax;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName ;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;
            this.xrLabelData.Text =Properties.Resources.InvoiceCT;
            this.Print.Text += DateTime.Now.ToShortDateString();
            //每页合计统计
            //this.xrLabelPateTotal.Summary.Running = SummaryRunning.Page;
            //this.xrLabelPateTotal.Summary.Func = SummaryFunc.Sum;
            //this.xrLabelPateTotal.Summary.FormatString = "{0:0}";
            //this.xrLabelPateTotal.Summary.IgnoreNullValues = true;
            //this.xrLabelPateTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PROPERTY_INVOICECTDETAILMONEY0, "{0:0}");

            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Supplier.SupplierShortName;
            this.xrLabelLianluoName.Text = this.invoice.Supplier.SupplierManager;
            this.xrLabelCustomFax.Text = this.invoice.Supplier.SupplierFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Supplier.SupplierPhone1) ? this.invoice.Supplier.SupplierPhone2 : this.invoice.Supplier.SupplierPhone1;
            this.xrLabelTongYiNo.Text = this.invoice.Supplier.SupplierNumber;
            this.xrLabelSongHuoAddress.Text = this.invoice.Supplier.CompanyAddress;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelInvoiceFphm.Text = this.invoice.InvoiceFpbh;

            this.xrLabelTax.Text = this.invoice.InvoiceTax.Value.ToString("0");
            this.xrLabelZre.Text = this.invoice.InvoiceZRE.Value.ToString("0");
            this.xrLabelZse.Text = this.invoice.InvoiceZSE.Value.ToString("0");
            this.xrLabelOwced.Text = this.invoice.InvoiceOwed.Value.ToString("0");
            this.xrLabelYifu.Text = ((decimal)(this.invoice.InvoiceZongJi.Value - this.invoice.InvoiceOwed.Value)).ToString("0");
            this.xrLabelTotal1.Text = this.invoice.InvoiceZongJi.Value.ToString("0");
            this.xrLabelheji.Text = this.invoice.InvoiceHeJi.Value.ToString("0");
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
           // this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailQuantity);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrCheckBoxIsZs.DataBindings.Add("CheckState", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailZS);
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCTDetail.PRO_InvoiceCTDetailMoney0, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
        }

    }
}
