using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CJ
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCJManager invoiceCGManager = new Book.BL.InvoiceCJManager();
        private BL.InvoiceCJDetailManager invoiceCGDetailManager = new Book.BL.InvoiceCJDetailManager();

        private Model.InvoiceCJ invoice;

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
            this.xrLabelData.Text = Properties.Resources.InvoiceCJ;
            this.xrLabelPrintDate.Text += DateTime.Now.ToShortDateString();
            //客户信息
            this.xrLabelCustomName.Text = this.invoice.Supplier.SupplierShortName;
            this.xrLabelLianluoName.Text = this.invoice.Supplier.SupplierManager;
            this.xrLabelCustomFax.Text = this.invoice.Supplier.SupplierFax;
            this.xrLabelCustomTel.Text = string.IsNullOrEmpty(this.invoice.Supplier.SupplierPhone1) ? this.invoice.Supplier.SupplierPhone2 : this.invoice.Supplier.SupplierPhone1;
            this.xrLabelTongYiNo.Text = this.invoice.Supplier.SupplierNumber;

            //单据信息
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName + this.invoice.Employee0Id;
            this.xrLabelTotal1.Text = global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceTotal.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJDetail.PRO_InvoiceCJDetailQuantity);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJDetail.PRO_InvoiceProductUnit);
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJDetail.PRO_InvoiceCJDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCJDetail.PRO_InvoiceCJDetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
        }

    }
}
