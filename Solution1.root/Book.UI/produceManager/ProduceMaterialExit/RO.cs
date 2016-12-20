using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceMaterialExit
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceMaterialExitManager produceMaterialExitManager = new Book.BL.ProduceMaterialExitManager();
        private BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        private Model.ProduceMaterialExit produceMaterialExit;
        public RO(string produceMaterialExitId)
        {
            InitializeComponent();
            this.produceMaterialExit = this.produceMaterialExitManager.Get(produceMaterialExitId);

            if (this.produceMaterialExit == null)
                return;

            this.produceMaterialExit.Detail = this.produceMaterialExitDetailManager.Select(this.produceMaterialExit);

            this.DataSource = this.produceMaterialExit.Detail;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceMaterialExitDetail;
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();
            //生產退料
            this.xrLabelPronoteHeader.Text = this.produceMaterialExit.PronoteHeaderID;
            this.xrLabelDepot.Text = this.produceMaterialExit.Depot == null ? string.Empty : this.produceMaterialExit.Depot.ToString();
            this.lblSupplier.Text = this.produceMaterialExit.Supplier == null ? string.Empty : this.produceMaterialExit.Supplier.ToString();
            this.xrLabelProduceExitMaterialId.Text = this.produceMaterialExit.ProduceMaterialExitId;
            this.xrLabelProduceExitMaterialDate.Text = this.produceMaterialExit.ProduceExitMaterialDate.Value.ToString("yyyy-MM-dd");
            //客戶訂單編號
            this.xrLabelInvoiceCusXoId.Text = this.produceMaterialExit.CustomerInvoiceXOId;
            if (this.produceMaterialExit.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceMaterialExit.Employee0.EmployeeName;
            }
            if (this.produceMaterialExit.WorkHouse != null)
            {
                this.xrLabelDepartment.Text = this.produceMaterialExit.WorkHouse.Workhousename;
            }
            this.xrLabelProduceExitMaterialDesc.Text = this.produceMaterialExit.ProduceExitMaterialDesc;
            if (!string.IsNullOrEmpty(this.produceMaterialExit.PronoteHeaderID))
            {
                Model.PronoteHeader mPH = new BL.PronoteHeaderManager().Get(this.produceMaterialExit.PronoteHeaderID);
                if (mPH != null)
                {
                    this.lblZhuJianProductName.Text = mPH.Product.ToString();
                }
            }

            //明细
            this.xrTableCell1ProductId.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_Inumber);
            this.xrTableproductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrDepotPosi.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProductUnit);
            this.xrTableProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProduceQuantity);
            this.xrTableGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrProductioinBatch.DataBindings.Add("Text", this.DataSource, Model.ProduceMaterialExitDetail.PRO_ProductioinBatch);
        }

    }
}
