using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q50_1 : DevExpress.XtraReports.UI.XtraReport
    {
        public Q50_1()
        {
            InitializeComponent();

            //this.xrTableProID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            //this.xrTableDate.DataBindings.Add("Text", this.DataSource, "ProduceMaterial." + Model.ProduceMaterial.PRO_ProduceMaterialDate, "{0:yyyy-MM-dd}");
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_ProductName);
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Materialprocessum);
            //this.xrTableMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_ProduceMaterialID);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_ProductUnit);
            //this.xrTableXOid.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_InvoiceXOId);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_ProductStock, "{0:0.####}");
            this.Pihao.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Pihao);
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_CustomerProductName);
            this.xrTableQuanTityed.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_Materialprocesedsum, "{0:0.####}");
        }

        private Model.ProduceMaterial _produceMaterial;

        public Model.ProduceMaterial ProduceMaterial
        {
            get { return _produceMaterial; }
            set { _produceMaterial = value; }
        }

        private void Q50_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.ProduceMaterial.Details;
        }

    }
}
