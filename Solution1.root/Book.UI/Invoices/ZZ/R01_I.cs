using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZZ
{
    public partial class R01_I : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceZZDetailManager invoiceDetailManager = new Book.BL.InvoiceZZDetailManager();

        public R01_I(Model.InvoiceZZ invoice)
        {
            InitializeComponent();            
            
            invoice.DetailsIn = this.invoiceDetailManager.Select("I", invoice);
            this.DataSource = invoice.DetailsIn;

            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILQUANTITY);
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZZDetail.PROPERTY_INVOICEZZDETAILNOTE);

        }

    }
}
