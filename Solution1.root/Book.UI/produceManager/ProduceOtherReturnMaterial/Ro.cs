using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherReturnMaterial
{
    public partial class Ro : DevExpress.XtraReports.UI.XtraReport
    {

        #region
        BL.ProduceOtherReturnMaterialManager produceOtherReturnMaterialManager = new BL.ProduceOtherReturnMaterialManager();
        BL.ProduceOtherReturnDetailManager produceOtherReturnDetailManager = new BL.ProduceOtherReturnDetailManager();
        #endregion

        public Ro(Model.ProduceOtherReturnMaterial produceOtherReturnMaterial)
        {
            InitializeComponent();

            this.xrLabelTitle.Text = BL.Settings.CompanyChineseName;
            this.xrLabelName.Text = "Î¯ÍâÍËØ›†Î";// Properties.Resources.ProduceOtherReturnMaterial;

            this.xrLabelProduceOtherReturnMaterialId.Text = produceOtherReturnMaterial.ProduceOtherReturnMaterialId;
            this.xrLabelPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.xrLabelProduceOtherReturnMaterialDate.Text = produceOtherReturnMaterial.ProduceOtherReturnMaterialDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelProduceOtherReturnMaterialDesc.Text = produceOtherReturnMaterial.ProduceOtherReturnMaterialDesc;
            this.xrLabelEmployee0Id.Text = produceOtherReturnMaterial.Employee0 == null ? "" : produceOtherReturnMaterial.Employee0.ToString();
            //this.xrLabelEmployee1Id.Text = produceOtherReturnMaterial.Employee1 == null ? "" : produceOtherReturnMaterial.Employee1.ToString();
            //this.xrLabelEmployee2Id.Text = produceOtherReturnMaterial.Employee2 == null ? "" : produceOtherReturnMaterial.Employee2.ToString();
            this.xrLabelSupplier.Text = produceOtherReturnMaterial.Supplier == null ? "" : produceOtherReturnMaterial.Supplier.ToString();

            this.DataSource = this.produceOtherReturnDetailManager.Select(produceOtherReturnMaterial);

            this.xrTableCusProName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableProductUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherReturnDetail.PRO_ProductUnit);
            this.xrTableOtherCom.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherReturnDetail.PRO_ProduceOtherCompactId);
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherReturnDetail.PRO_ProduceOtherReturnDetailDesc);
            this.xrTableSum.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherReturnDetail.PRO_Quantity);
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherReturnDetail.PRO_ProduceOtherReturnDetailDesc);
        }

    }
}
