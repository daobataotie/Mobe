using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.ZX
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.InvoiceZX invoiceZX)
            : this()
        {
            if (invoiceZX != null)
            {
                this.xrBarCode1.Text = invoiceZX.InvoiceZXId;
                this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
                this.lblROPacking.Text = Properties.Resources.XSPacking;
                this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

                this.lblPackingId.Text = invoiceZX.PackingId;
                this.lblPackingdate.Text = invoiceZX.InvoiceDate != null ? invoiceZX.InvoiceDate.Value.ToString("yyyy-MM-dd") : null;
                if (invoiceZX.Customer != null)
                    this.lblCustomer.Text = invoiceZX.Customer.ToString();
                this.lblLong.Text = invoiceZX.BLong.ToString();
                this.lblWidth.Text = invoiceZX.BWide.ToString();
                this.lblHeigth.Text = invoiceZX.BHigh.ToString();
                if (invoiceZX.AllWeight != null)
                    this.lblWeigth.Text = invoiceZX.AllWeight.ToString();
                if (invoiceZX.BWeight != null)
                    this.lblAllWeight.Text = invoiceZX.BWeight.ToString();
                this.lblNote.Text = invoiceZX.InvoiceNote;
                if (invoiceZX.Employee != null)
                    this.lblEmployee.Text = invoiceZX.Employee.ToString();

                this.DataSource = invoiceZX.Details;
                tcProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
                tcInvoiceXODetailId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXO.PRO_CustomerInvoiceXOId);
                tcNum.DataBindings.Add("Text", this.DataSource, Model.InvoiceZXDetail.PRO_ProductNum);
                tcNote.DataBindings.Add("Text", this.DataSource, Model.InvoiceZXDetail.PRO_InvoiceZXDetailNote);

            }
        }
    }
}
