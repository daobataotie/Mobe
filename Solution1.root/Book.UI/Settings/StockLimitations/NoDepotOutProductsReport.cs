using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Settings.StockLimitations
{
    public partial class NoDepotOutProductsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public NoDepotOutProductsReport(DataTable dt, double year)
        {
            InitializeComponent();

            this.DataSource = dt;

            this.xrLabel1.Text = BL.Settings.CompanyChineseName;
            this.xrLabel2.Text = year.ToString() + "年內未出倉商品";

            this.TCProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "CustomerProductName");
            this.TCLastDepotoutDate.DataBindings.Add("Text", this.DataSource, "LastDepotoutDate");
            this.TCStockQuantity.DataBindings.Add("Text", this.DataSource, "StocksQuantity");
            this.TCCustomer.DataBindings.Add("Text", this.DataSource, "CustomerShortName");
        }

    }
}
