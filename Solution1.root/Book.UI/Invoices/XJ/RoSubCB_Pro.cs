using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.XJ
{
    public partial class RoSubCB_Pro : DevExpress.XtraReports.UI.XtraReport
    {
        public Model.InvoiceXJ _InvoiceXJ { get; set; }

        public RoSubCB_Pro()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RoSubCB_Pro_BeforePrint);
            if (this._InvoiceXJ != null)
                this.TCInvoiceXJGuanXiaoPro.Text += this._InvoiceXJ.GuanXiaoPro.ToString();

            //Binding
            this.TCProductName.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_ProductName);
            this.TCInvoiceProductUnit.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceProductUnit);
            this.TCInvoiceXJDetailQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuantity, "{0:0.###}");
            this.TCInvoiceXJDetailMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailMoney, "{0:0.###}");
            this.TCInvoiceXJDetailMoney2.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailMoney2, "{0:0.###}");
            this.TCInvoiceXJDetailPrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailPrice, "{0:0.###}");
            this.TCInvoiceXJDetailQuote.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuote, "{0:0.###}");

            this.TCInvoiceXJDetailMoneyTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJDetailMoneyTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJDetailMoneyTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJDetailMoneyTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJDetailMoneyTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailMoney, "{0:0}");
            this.TCInvoiceXJDetailMoney2Total.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJDetailMoney2Total.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJDetailMoney2Total.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJDetailMoney2Total.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJDetailMoney2Total.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailMoney2, "{0:0}");

            this.TCInvoiceXJDetailQuantityTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJDetailQuantityTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJDetailQuantityTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJDetailQuantityTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJDetailQuantityTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuantity, "{0:0}");

            this.TCInvoiceXJDetailQuoteTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJDetailQuoteTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJDetailQuoteTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJDetailQuoteTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJDetailQuoteTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJDetail.PRO_InvoiceXJDetailQuote, "{0:0}");
        }

        private void RoSubCB_Pro_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this._InvoiceXJ.Details;
            this.TCInvoiceXJGuanXiaoPro.Text += this._InvoiceXJ.GuanXiaoPro.HasValue ? this._InvoiceXJ.GuanXiaoPro.ToString() : "";
        }
    }
}
