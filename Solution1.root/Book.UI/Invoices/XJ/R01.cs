using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XJ
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXJManager invoiceCGManager = new Book.BL.InvoiceXJManager();
        private BL.InvoiceXJDetailManager invoiceCGDetailManager = new Book.BL.InvoiceXJDetailManager();
        private BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        private Model.InvoiceXJ invoice;


        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.invoiceCGManager.Get(invoiceid);

            if (this.invoice == null)
                return;

            this.invoice.Details = this.invoiceCGDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo 
     
                this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName ;
                this.xrLabelData.Text = Properties.Resources.InvoiceXJ;
                this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            
                Model.Company company = companyManager.Get(invoice.CompanyId);
                if(company!=null)
                {
                    if (company.CompanyName != BL.Settings.CompanyChineseName)
                    this.xrLabelCompanyInfoName.Text = company.CompanyName;
                    this.xrLabelData.Text = Properties.Resources.InvoiceXJ;


                }
               
            
            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Customer.CustomerShortName;
            this.xrLabelCustomFax.Text = this.invoice.Customer.CustomerFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Customer.CustomerPhone) ? this.invoice.Customer.CustomerPhone1 : this.invoice.Customer.CustomerPhone;
            this.xrLabelTongYiNo.Text = this.invoice.Customer.CustomerNumber;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelTotal1.Text = this.invoice.InvoiceTotal.Value.ToString(global::Helper.DateTimeParse.GetFormat(BL.V.SetDataFormat.XSZJXiao.Value));
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            ////明细信息
            //this.xrTableCellCustomerProductId.DataBindings.Add("Text", this.DataSource, "PrimaryKey." + Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
            //this.xrTableCellXingHao.DataBindings.Add("Text", this.DataSource, "PrimaryKey.Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PROPERTY_INVOICEPRODUCTUNIT);
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PROPERTY_INVOICEXJDETAILQUANTITY);
            //this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PROPERTY_INVOICEXJDETAILPRICE, "{0:c0}");
            //this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PROPERTY_INVOICEXJDETAILMONEY, "{0:c0}");
            //this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "PrimaryKey." + Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTNAME);

            //this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "PrimaryKey.Product." + Model.Product.PRO_Id);


            //明细信息
            this.xrTableCellCustomerProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_Inumber);
           // this.xrTableCellXingHao.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuantity);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSDJXiao.Value));
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.XSJEXiao.Value));
            this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);




        }

    }
}
