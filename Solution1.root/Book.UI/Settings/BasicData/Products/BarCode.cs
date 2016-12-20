using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.BarCode;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class BarCode : DevExpress.XtraReports.UI.XtraReport
    {
        private string _barcode;
        public BarCode()
        {
            InitializeComponent();
        }

        public BarCode(string code)
            : this()
        {
            this._barcode = code;

            this.xrBarCode1.Text = this._barcode;
        }

    }
}
