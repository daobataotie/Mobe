using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherExitMaterial
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherExitMaterialManager produceOtherExitMaterialManager = new Book.BL.ProduceOtherExitMaterialManager();
        private BL.ProduceOtherExitDetailManager produceOtherExitDetailManager = new Book.BL.ProduceOtherExitDetailManager();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        private Model.ProduceOtherExitMaterial produceOtherExitMaterial;
        public RO(string produceOtherExitMaterialId)
        {
            InitializeComponent();
            this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(produceOtherExitMaterialId);

            if (this.produceOtherExitMaterial == null)
                return;

            this.produceOtherExitMaterial.Details = this.produceOtherExitDetailManager.Select(this.produceOtherExitMaterial);

            this.DataSource = this.produceOtherExitMaterial.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherExitDetail;
            this.xrLabelPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //外包退料
            this.xrLabelProduceOtherExitMaterialId.Text = this.produceOtherExitMaterial.ProduceOtherExitMaterialId;
            this.xrLabelProduceOtherExitMaterialDate.Text = this.produceOtherExitMaterial.ProduceOtherExitMaterialDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelCompact.Text = this.produceOtherExitMaterial.ProduceOtherCompactId;
            if (this.produceOtherExitMaterial.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherExitMaterial.Employee0.EmployeeName;
            }
            if (this.produceOtherExitMaterial.Supplier != null)
            {
                this.xrLabelDepartment.Text = this.produceOtherExitMaterial.Supplier.ToString();
            }
            this.xrLabelProduceOtherMaterialDesc.Text = this.produceOtherExitMaterial.ProduceOtherExitMaterialDesc;
            //if (this.produceOtherExitMaterial.ProduceOtherCompactId != null)
            //{
            //    Model.ProduceOtherCompact produceOtherCompact = produceOtherCompactManager.Get(this.produceOtherExitMaterial.ProduceOtherCompactId);
            //    if (produceOtherCompact != null)
            //        this.xrLabelCusXOId.Text = produceOtherCompact.CustomerInvoiceXOId;
            //}
            //明细        
            this.xrLabelCusXOId.Text = this.produceOtherExitMaterial.InvoiceCusId;
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellOtherMaterialQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitDetail.PRO_ProduceQuantity);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherExitDetail.PRO_ProductUnit);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);

        }

    }
}
