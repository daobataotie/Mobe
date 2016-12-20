using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q54_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private Model.ProduceOtherMaterial produceOtherMaterial;

        public Model.ProduceOtherMaterial ProduceOtherMaterial
        {
            get { return produceOtherMaterial; }
            set { produceOtherMaterial = value; }
        }

        public Q54_1()
        {
            InitializeComponent();

            this.xrTableProID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            //this.xrTableDate.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterial." + Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialDate, "{0:yyyy-MM-dd}");
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_OtherMaterialQuantity);
            //this.xrTableMaterialId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProduceOtherCompactMaterialId);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProductUnit);
            this.xrTableXOid.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialdetails.PRO_InvoiceXOId);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity);
            this.xrTablePiHao.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_PiHao);
            this.xrTableYiLing.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_OtherMaterialALLUserQuantity);
     
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSourceDetail.DataSource = this.produceOtherMaterial.Details;
        }

    }
}
