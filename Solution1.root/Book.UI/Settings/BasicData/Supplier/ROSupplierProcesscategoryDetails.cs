using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.BasicData.Supplier
{
    public partial class ROSupplierProcesscategoryDetails : DevExpress.XtraReports.UI.XtraReport
    {
        public ROSupplierProcesscategoryDetails()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(ROSupplierProcesscategoryDetails_BeforePrint);

            //Bind
            //this.TCPrice.DataBindings.Add("Text", this.DataSource, Model.SupplierProcesscategory.PRO_SupplierProcesscategoryPrice);
            this.TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
        }

        public IList<Model.SupplierProcesscategory> data { get; set; }
        
        private void ROSupplierProcesscategoryDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = data;
            this.lblProcess.Text += data[0].ProcessCategory.ToString();
        }

    }
}
