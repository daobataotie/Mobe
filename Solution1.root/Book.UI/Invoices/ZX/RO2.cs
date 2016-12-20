using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    public partial class RO2 : DevExpress.XtraReports.UI.XtraReport
    {
        public RO2()
        {
            InitializeComponent();
        }

        public RO2(IList<Model.InvoicePacking> list)
            : this()
        {
            this.DataSource = list;

            xrTableCellNO.DataBindings.Add("Text", this.DataSource, Model.InvoicePacking.PRO_InvoiceNO);
            xrTableCellDATE.DataBindings.Add("Text", this.DataSource, Model.InvoicePacking.PRO_InvoicePackingDate, "{0:yyyy-MM-dd}");
            xrTableCellINVOICEOF.DataBindings.Add("Text", this.DataSource, Model.InvoicePacking.PRO_InvoiceOf);
            xrTableCellShippedBy.DataBindings.Add("Text", this.DataSource, "ShippedBy");
            xrTableCellConsignee.DataBindings.Add("Text", this.DataSource, "CONSIGNEE");
        }

    }
}
