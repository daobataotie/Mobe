using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZG
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.InvoiceZG invoiceZG)
            : this()
        {
            if (invoiceZG != null)
            {
                this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
                this.lblReportName.Text = Properties.Resources.InvoiceZG;
                this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

                this.lblInvoiceNO.Text = invoiceZG.InvoiceZGId;
                this.lblInvoiceDate.Text = invoiceZG.InvoiceZGDate != null ? invoiceZG.InvoiceZGDate.Value.ToString("yyyy-MM-dd") : null;
                this.lblInvoiceOF.Text = invoiceZG.XOCustomer.ToString();
                this.lblInvoiceTO_1.Text = invoiceZG.XOCustomer.CustomerFullName;
                this.lblInvoiceTO_2.Text = invoiceZG.XOCustomer.CustomerAddress;
                this.lblShippedBy.Text = invoiceZG.Shipped.CompanyName;
                this.lblPerSS.Text = invoiceZG.PerSS;
                this.lblS_O.Text = invoiceZG.SorO;
                this.lblShippedON_About.Text = invoiceZG.ShippedOnAbout;
                this.lblArrivel_ONAbout.Text = invoiceZG.ArrivelOnAbout;
                this.lblInvoice_AddFrom.Text = invoiceZG.AddressFrom;
                this.lblInvoice_AddTo.Text = invoiceZG.AddressTo;
            }
        }
    }
}
