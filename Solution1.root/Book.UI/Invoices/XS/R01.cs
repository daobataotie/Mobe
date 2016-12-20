using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XS
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXSManager InvoiceXSManager = new Book.BL.InvoiceXSManager();
        private BL.InvoiceXSDetailManager InvoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();

        private Model.InvoiceXS invoice;

        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.InvoiceXSManager.Get(invoiceid);
            if (invoice == null)
                return;

            this.invoice.Details = this.InvoiceXSDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelData.Text = Properties.Resources.InvoiceXS;
            this.xrLabelPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //客户信息
            this.xrLabelCustomer.Text = this.invoice.Customer.CustomerShortName;
            this.xrLabelCustomName.Text = this.invoice.XSCustomer == null ? "" : this.invoice.XSCustomer.CustomerShortName;
            this.xrLabelCustomFax.Text = this.invoice.XSCustomer == null ? "" : this.invoice.XSCustomer.CustomerFax;
            this.xrLabelCustomTel.Text = this.invoice.XSCustomer == null ? "" : string.IsNullOrEmpty(this.invoice.XSCustomer.CustomerPhone) ? this.invoice.Customer.CustomerPhone1 : this.invoice.Customer.CustomerPhone;
            this.xrLabelTongYiNo.Text = this.invoice.XSCustomer == null ? "" : this.invoice.XSCustomer.CustomerNumber;
            this.xrLabelSongHuoAddress.Text = this.invoice.XSCustomer == null ? "" : this.invoice.XSCustomer.CustomerJinChuAddress;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;

            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            this.xrLabelFreightCompany.Text = this.invoice.TransportCompany;
            this.xrLabelFreightWay.Text = this.invoice.ConveyanceMethod == null ? null : this.invoice.ConveyanceMethod.ToString();

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);

            this.TCHandBookId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_HandbookId);
            // this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxPrice, "{0:0.###}");
            this.TCHandBookProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_HandbookProductId);
            // this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTax, "{0:0.###}");
            // this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailTaxMoney, "{0:0.###}");
            // this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity, "{0:0.###}");
            //  this.xrTableNoXGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail." + Model.InvoiceXODetail.PRO_InvoiceXODetailQuantity0, "{0:0.###}");
            //  this.xrTableZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetaiInQuantity, "{0:0.###}");
            //this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.###}");
            // this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceAllowance, "{0:0.###}");
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            //this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceXSDetail.PRO_Donatetowards);
            //this.xrTableCellXOId.DataBindings.Add("Text", this.DataSource,"InvoiceXO."+ Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, "InvoiceXO." + Model.InvoiceXO.PRO_CustomerInvoiceXOId);
            this.xrLabelHeji.Text += this.invoice.InvoiceHeji == null ? "0" : this.invoice.InvoiceHeji.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelTax.Text += this.invoice.InvoiceTax == null ? "0" : this.invoice.InvoiceTax.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelTatol.Text += this.invoice.InvoiceTotal == null ? "0" : this.invoice.InvoiceTotal.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
        }
    }
}
