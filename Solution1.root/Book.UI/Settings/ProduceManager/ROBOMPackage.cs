using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.Settings.ProduceManager
{
    public partial class ROBOMPackage : DevExpress.XtraReports.UI.XtraReport
    {
        public ROBOMPackage()
        {
            InitializeComponent();
        }

        public ROBOMPackage(IList<Model.BomPackageDetails> list)
            : this()
        {
            this.DataSource = list;

            this.TCInumber.DataBindings.Add("Text", this.DataSource, Model.BomPackageDetails.PRO_Inumber);
            this.TCproductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.TCproductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCUnit.DataBindings.Add("Text", this.DataSource, Model.BomPackageDetails.PRO_PackageUnit);
            this.TCUseQuantity.DataBindings.Add("Text", this.DataSource, Model.BomPackageDetails.PRO_UseQuantity);
            this.TCQuantity.DataBindings.Add("Text", this.DataSource, Model.BomPackageDetails.PRO_Quantity, "{0:0.####}");
            this.TCSunhaolv.DataBindings.Add("Text", this.DataSource, Model.BomPackageDetails.PRO_ConsumeRate);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
        }
    }
}
