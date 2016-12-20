using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CG
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCGManager invoiceCGManager = new Book.BL.InvoiceCGManager();
        private BL.InvoiceCGDetailManager invoiceCGDetailManager = new Book.BL.InvoiceCGDetailManager();

        private Model.InvoiceCG invoice;

        public R01(string invoiceid)
        {
            InitializeComponent();

            this.invoice = this.invoiceCGManager.Get(invoiceid);

            if (this.invoice == null)
                return;

            this.invoice.Details = this.invoiceCGDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelData.Text = Properties.Resources.InvoiceCG;
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
            this.xrLabelYeWuName.Text = this.invoice.Employee0.EmployeeName;
            this.xrLabelNote.Text = this.invoice.InvoiceNote;
            this.xrBarCodeInvoiceId.Text = this.invoice.InvoiceId;
            if (this.invoice.Employee0 != null)
                this.xrLabelEmp0.Text += this.invoice.Employee0.ToString();
            if (this.invoice.Employee1 != null)
                this.xrLabelEmp1.Text += this.invoice.Employee1.ToString();
            this.xrLabelTaxCaluType.Text += this.invoice.TaxCaluTypeName;
            this.xrLabelTaxrate.Text += global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceTaxrate.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.xrLabelHeji.Text += global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceHeji.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.xrLabelTax.Text += global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceTax.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.xrLabelTatol.Text += global::Helper.DateTimeParse.GetSiSheWuRu(this.invoice.InvoiceTotal.Value, BL.V.SetDataFormat.CGZJXiao.Value).ToString();
            this.lbl_bibie.Text = this.invoice.AtCurrencyCategory == null ? "" : this.invoice.AtCurrencyCategory.ToString();
            //明细信息
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellProductQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailQuantity);

            this.xrTablePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrTableTaxPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTaxPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrTableHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.xrTableTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTax, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.xrTableTaxTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetailTaxMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.xrTableOrderQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.####}");
            this.xrTableNoCGQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceCODetail." + Model.InvoiceCODetail.PRO_NoArrivalQuantity, "{0:0.####}");
            this.xrTableInQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCGDetaiInQuantity, "{0:0.####}");
            this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_ProduceTransferQuantity, "{0:0.####}");
            this.xrTableZR.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceAllowance, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);
            this.xrCheckBoxZS.DataBindings.Add("Checked", this.DataSource, Model.InvoiceCGDetail.PRO_Donatetowards);
            this.xrTableCellCOId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceCOId);
            this.xrTableWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            //this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCGDetail.PRO_InvoiceProductUnit);            
            //this.TCISPass.DataBindings.Add("Text",this.DataSource,Model.InvoiceCGDetail.PRO_ISPass);
        }

    }
}
