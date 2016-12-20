using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.MouldCategory
{
    public partial class BarCode : DevExpress.XtraReports.UI.XtraReport
    {
        //private string _barcode;
        public BarCode()
        {
            InitializeComponent();
        }
        public BarCode(string code)
            : this()
        {
            this.xrBarCode1.Text = code;
        }
    }
}
