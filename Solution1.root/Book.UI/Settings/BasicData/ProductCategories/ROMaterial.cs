using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData.ProductCategories
{
    public partial class ROMaterial : DevExpress.XtraReports.UI.XtraReport
    {
        public ROMaterial(System.Collections.Generic.IList<Model.Material> materialList)
        {
            InitializeComponent();

            this.lblCompany.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "原料重量设置";
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.DataSource = materialList;

            this.TCName.DataBindings.Add("Text", this.DataSource, "Id");
            this.TCCategory.DataBindings.Add("Text", this.DataSource, "MaterialCategoryName");
            this.TCWeight.DataBindings.Add("Text", this.DataSource, "JWeight");
        }

    }
}
