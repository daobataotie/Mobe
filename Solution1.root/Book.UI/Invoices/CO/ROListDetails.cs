using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.CO
{
    public partial class ROListDetails : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.InvoiceCOManager invoiceCGManager = new Book.BL.InvoiceCOManager();
        private BL.InvoiceCODetailManager invoiceCGDetailManager = new Book.BL.InvoiceCODetailManager();
        private BL.InvoiceXOManager invoiceXoManager = new BL.InvoiceXOManager();
        private Model.InvoiceCO invoice;
        int pp = 0;
        public ROListDetails()
        {
            InitializeComponent();

            //Ã÷Ï¸ÐÅÏ¢

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellProductGuige.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceProductUnit);
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_OrderQuantity, "{0:0.##}");
            this.xrTableCellUintPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailPrice, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGDJXiao.Value));
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailMoney, global::Helper.DateTimeParse.GetFormatA(BL.V.SetDataFormat.CGJEXiao.Value));
            //this.xrTableCellNetWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_GrossWeight, "{0:0}");
            //this.xrTableCellGrossWeight.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_NetWeight, "{0:0}");
            //this.xrTableCellBulk.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_Bulk, "{0:0}");
            this.xrRichTextProductDescribe.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrLabelDetailDesc.DataBindings.Add("Text", this.DataSource, Model.InvoiceCODetail.PRO_InvoiceCODetailNote);
            this.xrTableCellNextWorkHouse.DataBindings.Add("Text", this.DataSource, "NextWorkHouse." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrTableCellPiHao.DataBindings.Add("Text", this.DataSource, "Invoice." + Model.InvoiceCO.PRO_SupplierLotNumber);
        }

        private Model.InvoiceCO _mInvoiceCO;

        public Model.InvoiceCO MInvoiceCO
        {
            get { return _mInvoiceCO; }
            set { _mInvoiceCO = value; }
        }

        private void ROListDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this._mInvoiceCO.Details;
        }
    }
}
