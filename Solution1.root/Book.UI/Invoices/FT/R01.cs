using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Invoices.FT
{
    public partial class R01 : DevExpress.XtraReports.UI.XtraReport
    {
        protected BL.InvoiceFTManager invoiceManager = new Book.BL.InvoiceFTManager();

        private Model.InvoiceFT invoice;

        public R01(string invoiceId)
        {
            InitializeComponent();

            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                return;

        }

    }
}
