using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.Invoices.XJ
{
    public partial class RoSubBJ_Pack : DevExpress.XtraReports.UI.XtraReport
    {
        public Model.InvoiceXJ _InvoiceXJ { get; set; }

        public RoSubBJ_Pack()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(RoSubCB_Pack_BeforePrint);
            if (this._InvoiceXJ != null)
                this.TCGuanXiao_Pack.Text += this._InvoiceXJ.GuanXiaoPack.ToString();

            //Binding
            this.TCProductName.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_ProductName);
            this.TCInvoiceXJPackageDetailsQuantity.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_InvoiceXJPackageDetailsQuantity, "{0:0.###}");
            this.TCInvoiceXJPackageDetailsType.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_InvoiceXJPackageDetailsType);
            this.TCPackagePrice.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_PackagePrice, "{0:0.###}");
            this.TCProcessCategory.DataBindings.Add("Text", this.DataSource, "ProcessCategory." + Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
            this.TCSupplier.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.RTPackageDesc.DataBindings.Add("Rtf", this.DataSource, Model.InvoiceXJPackageDetails.PRO_Description);


            this.TCInvoiceXJPackageDetailsQuantityTotal.Summary.FormatString = "{0:0.####}";
            this.TCInvoiceXJPackageDetailsQuantityTotal.Summary.Func = SummaryFunc.Sum;
            this.TCInvoiceXJPackageDetailsQuantityTotal.Summary.IgnoreNullValues = true;
            this.TCInvoiceXJPackageDetailsQuantityTotal.Summary.Running = SummaryRunning.Report;
            this.TCInvoiceXJPackageDetailsQuantityTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_InvoiceXJPackageDetailsQuantity, "{0:0}");

            this.TCPackagePriceTotal.Summary.FormatString = "{0:0.####}";
            this.TCPackagePriceTotal.Summary.Func = SummaryFunc.Sum;
            this.TCPackagePriceTotal.Summary.IgnoreNullValues = true;
            this.TCPackagePriceTotal.Summary.Running = SummaryRunning.Report;
            this.TCPackagePriceTotal.DataBindings.Add("Text", this.DataSource, Model.InvoiceXJPackageDetails.PRO_PackagePrice, "{0:0}");
        }

        private void RoSubCB_Pack_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = (from item in this._InvoiceXJ.DetailPackage
                               where item.IsChecked == true
                               orderby item.Inumber ascending
                               select item).ToList<Model.InvoiceXJPackageDetails>();
            this.TCGuanXiao_Pack.Text += this._InvoiceXJ.GuanXiaoPack.HasValue ? this._InvoiceXJ.GuanXiaoPack.ToString() : "";
        }
    }
}
