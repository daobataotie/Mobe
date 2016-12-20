using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI
{
    public partial class ROStockPrompt : DevExpress.XtraReports.UI.XtraReport
    {
        public ROStockPrompt()
        {
            InitializeComponent();
        }

        public ROStockPrompt(IList<Model.Product> list)
            : this()
        {
            this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.DataSource = list;

            this.TCId.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_Id);
            this.TCProductName.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_ProductName);
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_CustomerProductName);
            this.TCSafeStock.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_SafeStock, "{0:0.##}");
            this.TCCurrentStock.DataBindings.Add("Text", this.DataSource, Model.Product.PRO_StocksQuantity, "{0:0.##}");
        }
    }
}
