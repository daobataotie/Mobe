using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZX
{
    public partial class ROPackingList : DevExpress.XtraReports.UI.XtraReport
    {
        public ROPackingList()
        {
            InitializeComponent();
        }


        public ROPackingList(Model.InvoicePacking model)
            : this()
        {
            this.lbl_NO.Text = model.InvoiceNO;
            this.lblInvoiceOF.Text = model.InvoiceOf;
            this.lblInvoicePackingDate.Text = model.InvoicePackingDate == null ? null : model.InvoicePackingDate.Value.ToString("yyyy-MM-dd");
            this.lbl_For.Text = model.Messrs;
            this.lblShippedBy.Text = model.ShippedBy == null ? null : model.ShippedBy.CompanyName;
            this.lblCONSIGNEE.Text = model.CONSIGNEE == null ? null : model.CONSIGNEE.CustomerShortName;
            this.lblADDRESS1.Text = model.ADDRESS1;
            this.lblADDRESS2.Text = model.ADDRESS2;
            this.lbl_Sailing.Text = model.Sailing;

            this.xrSubreport1.ReportSource = new ROList1(model.Details);
            this.xrSubreport2.ReportSource = new ROList2(model.Marks);
        }
    }
}
