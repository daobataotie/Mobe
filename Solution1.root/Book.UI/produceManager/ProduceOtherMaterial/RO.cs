using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherMaterialManager produceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        private BL.ProduceOtherMaterialDetailManager ProduceOtherMaterialDetailManager = new Book.BL.ProduceOtherMaterialDetailManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private Model.ProduceOtherMaterial produceOtherMaterial;
        public RO(string produceOtherMaterialId)
        {
            InitializeComponent();
            this.produceOtherMaterial = this.produceOtherMaterialManager.GetDetails(produceOtherMaterialId);

            if (this.produceOtherMaterial == null)
                return;

         //   this.produceOtherMaterial.Details = this.ProduceOtherMaterialDetailManager.GetOrderById(this.produceOtherMaterial);

            this.DataSource = this.produceOtherMaterial.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherMaterialDetail;
            this.xrLabelDepot.Text = this.produceOtherMaterial.Depot == null ? "" : this.produceOtherMaterial.Depot.ToString();
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();

            xrLabelSup.Text = this.produceOtherMaterial.Supplier==null?null: this.produceOtherMaterial.Supplier.SupplierShortName;
            //外包領料
            this.xrLabelProduceOtherMaterialId.Text = this.produceOtherMaterial.ProduceOtherMaterialId;
            this.xrLabelProduceOtherMaterialDate.Text = this.produceOtherMaterial.ProduceOtherMaterialDate.Value.ToString("yyyy-MM-dd");
            if (this.produceOtherMaterial.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherMaterial.Employee0.EmployeeName;
            }
            if (this.produceOtherMaterial.Employee1 != null)
            {
                this.xrLabelEmployee1.Text = this.produceOtherMaterial.Employee1.EmployeeName;
            }
            if (this.produceOtherMaterial.WorkHouse != null)
            {
                this.xrLabelDepartment.Text = this.produceOtherMaterial.WorkHouse.Workhousename;
            }
            this.xrLabelOtherCam.Text = this.produceOtherMaterial.ProduceOtherCompactId;
            this.xrLabelProduceOtherMaterialDesc.Text = this.produceOtherMaterial.ProduceOtherMaterialDesc;

            //if (!string.IsNullOrEmpty(produceOtherMaterial.ProduceOtherCompactId))
            //{
            //    Model.ProduceOtherCompact OtherCompact = new BL.ProduceOtherCompactManager().Get(produceOtherMaterial.ProduceOtherCompactId);
            //    if (OtherCompact != null)
            //    {
            //        Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(OtherCompact.MRSHeaderId);
            //        if (mrsHeader != null)
            //        {
            //            Model.MPSheader mPSheader = this.mPSheaderManager.Get(mrsHeader.MPSheaderId);
            //            if (mPSheader != null)
            //            {
            //                Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
            //                this.xrLabelCustomerXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
            //            }
            //        }
            //    }
            //}
            this.xrLabelCustomerXOId.Text = this.produceOtherMaterial.InvoiceCusId;
            //明细
            //this.xrTableCell1ProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //if (produceOtherMaterial.WorkHouse!=null)
            //{
            //    if(produceOtherMaterial.WorkHouse.Workhousename != null)
            //    this.xrTableCellDepartment.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterialWorkHouse.Workhousename");
            //}
           // this.xrTableCellProduceOtherMaterialDate.DataBindings.Add("Text", this.DataSource, "ProduceOtherMaterial." + Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialDate, "{0:yyyy-MM-dd}");
            this.xrTableCellOtherMaterialQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_OtherMaterialQuantity);
            this.xrTableCellDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_Description);
            this.xrTableCell4.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProductUnit);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ParentProduct." + Model.Product.PRO_ProductDescription);
            this.xrTableCellParent.DataBindings.Add("Text", this.DataSource, "ParentProduct." + Model.Product.PRO_ProductName);
            this.xrTableInumber.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_Inumber);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherMaterialDetail.PRO_ProductStock);
            this.xrTableCellNum.DataBindings.Add("Text",this.DataSource,Model.ProduceOtherMaterialDetail.PRO_PiHao);
        }

    }
}
