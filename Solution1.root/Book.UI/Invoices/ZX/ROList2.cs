using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Invoices.ZX
{
    public partial class ROList2 : DevExpress.XtraReports.UI.XtraReport
    {
        public ROList2()
        {
            InitializeComponent();
        }

        public ROList2(IList<Model.CustomerMarks> list)
            : this()
        {
            this.DataSource = list;
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, Model.CustomerMarks.PRO_CustomerMarksName);
        }
    }
}
