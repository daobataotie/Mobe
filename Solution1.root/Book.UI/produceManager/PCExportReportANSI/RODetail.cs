using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class RODetail : DevExpress.XtraReports.UI.XtraReport
    {
        public RODetail()
        {
            InitializeComponent();
        }

        public RODetail(string text, Model.PCExportReportANSIDetail detail, IList<Model.PCExportReportANSIDetail> list)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = text + "†Î";
            this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lblInvoiceCusXOId.Text = detail.InvoiceCusXOId;
            this.lblProduct.Text = detail.Product == null ? null : detail.Product.ToString();
            this.lblCustomer.Text = detail.Customer == null ? null : detail.Customer.ToString();

            this.DataSource = list;

            TCTestType.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_Type);
            TCInvoiceId.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_PCExportReportANSIDetailId);
            TCTestDate.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_CheckDate, "{0:yyyy-MM-dd}");
            TCInvoiceNum.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_InvoiceQuantity);
            TCTestNum.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_MustCheckSum);
            TCGetNum.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_HasCheckSum);
            TCPassNum.DataBindings.Add("Text", DataSource, Model.PCExportReportANSIDetail.PRO_PassSum);
            TCUnPassNum.DataBindings.Add("Text", DataSource, "UnPassSum");
        }
    }
}
