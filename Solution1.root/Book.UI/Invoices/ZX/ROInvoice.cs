using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    public partial class ROInvoice : DevExpress.XtraReports.UI.XtraReport
    {
        public ROInvoice()
        {
            InitializeComponent();
        }

        public ROInvoice(Model.InvoicePacking model)
            : this()
        {
            //this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            //this.lblReportName.Text = Properties.Resources.InvoicePacking;
            //this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            //this.lblInvoicePackingId.Text = model.InvoicePackingId;
            this.lbl_NO.Text = model.InvoiceNO;
            this.lblInvoiceOF.Text = model.InvoiceOf;
            this.lblInvoicePackingDate.Text = model.InvoicePackingDate == null ? null : model.InvoicePackingDate.Value.ToString("yyyy-MM-dd");
            this.lbl_For.Text = model.Messrs;
            this.lblShippedBy.Text = model.ShippedBy == null ? null : model.ShippedBy.CompanyName;
            this.lblCONSIGNEE.Text = model.CONSIGNEE == null ? null : model.CONSIGNEE.CustomerShortName;
            this.lblADDRESS1.Text = model.ADDRESS1;
            this.lblADDRESS2.Text = model.ADDRESS2;
            this.lbl_Sailing.Text = model.Sailing;

            //this.TCMark.DataBindings.Add("Rtf",model.Marks, Model.CustomerMarks.PRO_CustomerMarksName);

            //因为有子报表该页面不能给Datasource赋值，否则出现循环列印多次现象，故此传值给子报表
            this.xrSubreport1.ReportSource = new ROInvoice1(model.Details);
            this.xrSubreport2.ReportSource = new ROInvoice2(model.Marks);
        }
    }
}
