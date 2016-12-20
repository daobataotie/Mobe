using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q55_1 : DevExpress.XtraReports.UI.XtraReport
    {

        private BL.ProduceOtherExitDetailManager produceOtherManger = new Book.BL.ProduceOtherExitDetailManager();
        private ConditionOtherExit _otherExit;
        public Q55_1(ConditionOtherExit condition)
        {
            InitializeComponent();
            this._otherExit = condition;
            this.xrTableProID.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);            
            this.xrTableProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableQuanTity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitDetail.PRO_ProduceQuantity);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitDetail.PRO_ProductUnit);
            this.xrTableXOid.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitDetail.PRO_InvoiceXOId);
            this.xrTableDepotPosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
        }

        private Model.ProduceOtherExitMaterial produceOtherExitMaterial;

        public Model.ProduceOtherExitMaterial ProduceOtherExitMaterial
        {
            get { return produceOtherExitMaterial; }
            set { produceOtherExitMaterial = value; }
        }

        private void Q55_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.bindingSource1.DataSource = this.produceOtherManger.SelectByProductAndMaterialId(this.produceOtherExitMaterial.ProduceOtherExitMaterialId, this._otherExit.ProductId1, this._otherExit.ProductId2);
        }
    }
}
