using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XS
{
    public partial class RO4 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceXSManager InvoiceXSManager = new Book.BL.InvoiceXSManager();
        private BL.InvoiceXSDetailManager InvoiceXSDetailManager = new Book.BL.InvoiceXSDetailManager();

        private Model.InvoiceXS invoice;
        public RO4(string invoiceid )
        {
            InitializeComponent();
            this.invoice = this.InvoiceXSManager.Get(invoiceid);
            if (invoice == null)
                return;
            this.invoice.Details = this.InvoiceXSDetailManager.Select(this.invoice);
            this.DataSource = this.invoice.Details;
            //CompanyInfo
            //  this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName + Properties.Resources.InvoiceXS;
            // this.xrLabelTel.Text = BL.Settings.CompanyPhone;
            // this.xrLabelFax.Text = BL.Settings.CompanyFax;
            //客户信息
            this.xrLabelxsid.Text = this.invoice.InvoiceId;
            this.xrLabelCustomName.Text = this.invoice.Customer.CustomerShortName;
            // this.xrLabelCustomFax.Text = this.invoice.Customer.CustomerFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Customer.CustomerPhone) ? this.invoice.Customer.CustomerPhone1 : this.invoice.Customer.CustomerPhone;
            this.xrLabelTongYiNo.Text = this.invoice.Customer.CustomerNumber;
            this.xrLabelSongHuoAddress.Text = this.invoice.Customer.CustomerJinChuAddress;
            this.xrLabelTAX.Text = Convert.ToInt32(this.invoice.InvoiceXO.InvoiceTax).ToString();
            this.xrLabelNote.Text = this.invoice.InvoiceNote;
            this.xrLabelNo.Text = Convert.ToInt32(this.invoice.InvoiceXO.InvoiceReceiveable).ToString();
            this.xrLabelYiShou.Text = (Convert.ToInt32(this.invoice.InvoiceXO.InvoiceTotal) - Convert.ToInt32(this.invoice.InvoiceXO.InvoiceReceiveable)).ToString();
            this.xrLabelCustomerInvoiceId.Text = this.invoice.InvoiceXO.CustomerInvoiceXOId;
            this.xrLabelZherang.Text = Convert.ToInt32(this.invoice.InvoiceXO.InvoiceDiscount).ToString();
            // this.xrLabelHeji.Text =Convert.ToInt32( this.invoice.InvoiceXO.InvoiceHeji).ToString();
            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            //this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelNote.Text = this.invoice.InvoiceNote;
            //明细信息
            ////this.xrTableCellCustomerProductId.DataBindings.Add("Text", this.DataSource, "PrimaryKey." + Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
            ////this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "PrimaryKey." + Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTNAME);

            this.xrTableCellCustomerProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);


            // this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "PrimaryKey.Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceProductUnit);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXSDetail.PRO_InvoiceXSDetailQuantity);
            //  this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "PrimaryKey.Product." + Model.Product.PRO_Id);
            this.xrTableCellPrice.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail.InvoiceXODetailPrice","{0:c0}");
            this.xrTablebeizhu.DataBindings.Add("Text", this.DataSource, "InvoiceXODetail.InvoiceXODetailNote");
            this.xrTableCellxiaoji.DataBindings.Add("Text", this.DataSource, "xiaoji");       

        }
        int a = 0;
        private void RO4_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
        {
            {
                a = a + Convert.ToInt32(string.IsNullOrEmpty(this.xrTable1.Rows[0].Cells[5].Text) ? "0" : this.xrTable1.Rows[0].Cells[5].Text);
            }
            this.xrLabelHeji.Text = a.ToString();
           this.xrLabelZongji.Text = (Convert.ToInt32(this.xrLabelHeji.Text) + Convert.ToInt32(this.xrLabelZherang.Text)).ToString();
        }

        private void RO4_AfterPrint(object sender, EventArgs e)
        {
             //this.xrLabelZongji.Text = (Convert.ToInt32(this.xrLabelHeji.Text) + Convert.ToInt32(this.xrLabelZherang.Text)).ToString();
           
        }



    }
}
