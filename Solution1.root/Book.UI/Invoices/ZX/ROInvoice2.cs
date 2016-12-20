using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    public partial class ROInvoice2 : DevExpress.XtraReports.UI.XtraReport
    {
        public ROInvoice2()
        {
            InitializeComponent();
        }
        public ROInvoice2(IList<Model.CustomerMarks> list)
            : this()
        {
            this.DataSource = list;
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, Model.CustomerMarks.PRO_CustomerMarksName);
        }
    }
}
