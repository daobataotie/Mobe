using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class ROSupplierProduct : DevExpress.XtraReports.UI.XtraReport
    {
        public ROSupplierProduct()
        {
            InitializeComponent();
        }

        public ROSupplierProduct(Model.Supplier sup, DataTable dt)
            : this()
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            this.DataSource = dt;

            //Controls
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lblSupplier.Text += "  " + sup.ToString();

            //Bind
            this.TCProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCPrice.DataBindings.Add("Text", this.DataSource, "Price");
        }

    }
}
