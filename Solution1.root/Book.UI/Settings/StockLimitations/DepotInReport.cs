using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Settings.StockLimitations
{
    public partial class DepotInReport : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.DepotInManager DepotInManager = new Book.BL.DepotInManager();
        private BL.DepotInDetailManager DepotInDetailManager = new Book.BL.DepotInDetailManager();

        private Model.DepotIn DepotIn;
        public DepotInReport(string DepotInId)
        {  

            InitializeComponent();

            this.DepotIn = this.DepotInManager.Get(DepotInId);

            if (this.DepotIn == null)
                return;

            this.DepotIn.Details = this.DepotInDetailManager.GetDetailByDepotInId(this.DepotIn.DepotInId);

            this.DataSource = this.DepotIn.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text =Properties.Resources.DepotIn;
            this.xrLabelPrintDate.Text = "Ñu±íÈÕÆÚ£º        " + DateTime.Now.ToShortDateString();



            this.xrLabelDepotInId.Text = this.DepotIn.DepotInId;
            this.xrLabelDepotInDate.Text = this.DepotIn.DepotInDate.Value.ToString("yyyy-MM-dd");
            if (this.DepotIn.Employee != null)
            {
                this.xrLabelEmployeeId.Text = this.DepotIn.Employee.EmployeeName;
            }
            if (this.DepotIn.Employee0 != null)
            {
                this.xrLabelEmployee1Id.Text = this.DepotIn.Employee0.EmployeeName;
            }
            if (DepotIn.Depot != null)
            {
                this.xrLabelProduceInDepotId.Text = DepotIn.Depot.DepotName;
            }
            if (DepotIn.Supplier != null)
            {
                this.xrLabelSupplierId.Text = DepotIn.Supplier.SupplierShortName;
            }

            this.xrLabelDescription.Text = DepotIn.Description;
            this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, Model.DepotInDetail.PRO_Inumber);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //this.xrTableCellXH.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableCellCount.DataBindings.Add("Text", this.DataSource, Model.DepotInDetail.PRO_DepotInQuantity);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.DepotInDetail.PRO_ProductUnit);
            this.xrTableCellPronoteHeaderId.DataBindings.Add("Text", this.DataSource, Model.DepotInDetail.PRO_PronoteHeaderId);
            // this.xrTableCellCustomXOId.DataBindings.Add("Text", this.DataSource, Model.DepotInDetail);
            this.xrTableCellDepotId.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
        }

    }
}
