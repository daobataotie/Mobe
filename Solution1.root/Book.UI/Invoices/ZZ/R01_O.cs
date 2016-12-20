using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZZ
{
    public partial class R01_O : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZZDetailManager invoiceDetailManager = new Book.BL.InvoiceZZDetailManager();

        public R01_O(Model.InvoiceZZ invoice)
        {
            InitializeComponent();

            invoice.DetailsOut = invoiceDetailManager.Select("O", invoice);
            
            this.DataSource = invoice.DetailsOut;

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource,  "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILNOTE);
        }

    }
}
