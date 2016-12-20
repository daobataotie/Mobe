using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.QO
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceQOManager invoiceManager = new Book.BL.InvoiceQOManager();
        protected BL.InvoiceQODetailManager invoiceDetailManager = new Book.BL.InvoiceQODetailManager();

        private Model.InvoiceQO invoice;

        public R01(string invoiceId)
        { 
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                return;

            this.invoice.Details = this.invoiceDetailManager.Select(this.invoice);

            this.DataSource = this.invoice.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoAddress.Text = BL.Settings.CompanyAddress1;
            this.xrLabelCompanyInfoFAX.Text = BL.Settings.CompanyFax;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName + Properties.Resources.InvoiceQO;
            this.xrLabelCompanyInfoTelphone.Text = BL.Settings.CompanyPhone;

            //每页合计统计
            this.xrLabelPateTotal.Summary.Running = SummaryRunning.Page;
            this.xrLabelPateTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelPateTotal.Summary.FormatString = "{0:0}";
            this.xrLabelPateTotal.Summary.IgnoreNullValues = true;
            this.xrLabelPateTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceQODetail.PROPERTY_INVOICEQODETAILMONEY, "{0:0}");


            this.xrLabelAccount.Text = this.invoice.Account.AccountName;
            this.xrLabelInvoiceDate.Text = this.invoice.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelInvoiceId.Text = this.invoice.InvoiceId;
            this.xrLabelNote.Text = this.invoice.InvoiceNote;

            this.xrLabelTotal.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotal.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotal.Summary.FormatString = "{0:0}";
            this.xrLabelTotal.Summary.IgnoreNullValues = true;
            this.xrLabelTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceQODetail.PROPERTY_INVOICEQODETAILMONEY, "{0:0}");

            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, "OutgoingKind." + Model.OutgoingKind.PROPERTY_OUTGOINGKINDNAME);
            this.xrTableCellMoney.DataBindings.Add("Text", this.DataSource, Model.InvoiceQODetail.PROPERTY_INVOICEQODETAILMONEY, "{0:0}");
            this.xrTableCellNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceQODetail.PROPERTY_INVOICEQODETAILNOTE);

        }

    }
}
