using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData.Products
{
    public partial class ROProductClassify : DevExpress.XtraReports.UI.XtraReport
    {
        public ROProductClassify(Model.ProductClassify productClassify)
        {
            InitializeComponent();

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "货品关键字分类";
            this.lblReportDate.Text = productClassify.ProductClassifyDate.Value.ToString("yyyy-MM-dd");

            this.DataSource = productClassify.Details;

            this.TCID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.TCName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
        }

    }
}
