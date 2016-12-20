using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    /// <summary>
    /// Invoice×Ó±¨±í
    /// </summary>
    public partial class ROInvoice1 : DevExpress.XtraReports.UI.XtraReport
    {
        public ROInvoice1()
        {
            InitializeComponent();
        }
        public ROInvoice1(IList<Model.InvoicePackingDetail> details)
            : this()
        {
            this.DataSource = details;
            this.TC_Description.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TC_Quantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PackingNum);
            this.TC_UnitPrice.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_UnitPrice, "{0:f2}");
            this.TC_Amount.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_Amount, "{0:f2}");
            this.TCQuantityUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_ProductUnit);
            this.TCQuantityTotalUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_ProductUnit);
            this.TCPriceUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PriceUnit);
            this.TCAmountUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PriceUnit);
            this.TCAmountTotalUnit.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PriceUnit);

            this.TC_TotalQuantity.Summary.FormatString = "{0:f2}";
            this.TC_TotalQuantity.Summary.Func = SummaryFunc.Sum;
            this.TC_TotalQuantity.Summary.IgnoreNullValues = true;
            this.TC_TotalQuantity.Summary.Running = SummaryRunning.Report;
            this.TC_TotalQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_PackingNum, "{0:f2}");

            this.TC_TotalAmount.Summary.FormatString = "{0:f2}";
            this.TC_TotalAmount.Summary.Func = SummaryFunc.Sum;
            this.TC_TotalAmount.Summary.IgnoreNullValues = true;
            this.TC_TotalAmount.Summary.Running = SummaryRunning.Report;
            this.TC_TotalAmount.DataBindings.Add("Text", this.DataSource, Model.InvoicePackingDetail.PRO_Amount, "{0:f2}");
        }
    }
}
