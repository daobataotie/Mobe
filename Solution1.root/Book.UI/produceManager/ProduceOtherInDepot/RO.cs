using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherInDepot
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherInDepotManager ProduceOtherInDepotManager = new Book.BL.ProduceOtherInDepotManager();

        private BL.ProduceOtherInDepotDetailManager ProduceOtherInDepotDetailManager = new Book.BL.ProduceOtherInDepotDetailManager();

        private Model.ProduceOtherInDepot produceOtherInDepot;

        public RO(string produceOtherInDepotId)
        {
            InitializeComponent();
            this.produceOtherInDepot = this.ProduceOtherInDepotManager.Get(produceOtherInDepotId);

            if (this.produceOtherInDepot == null)
                return;

            this.produceOtherInDepot.Details = this.ProduceOtherInDepotDetailManager.Select(this.produceOtherInDepot);

            this.DataSource = this.produceOtherInDepot.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherInDepot;


            //外發入庫
            this.xrLabelProduceOtherInDepotId.Text = this.produceOtherInDepot.ProduceOtherInDepotId;
            this.xrLabelProduceOtherInDepotDate.Text = this.produceOtherInDepot.ProduceOtherInDepotDate.Value.ToString("yyyy-MM-dd");
            if (this.produceOtherInDepot.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherInDepot.Employee0.EmployeeName;
            }
            if (this.produceOtherInDepot.Employee1 != null)
            {
                this.xrLabelEmployee1.Text = this.produceOtherInDepot.Employee1.EmployeeName;
            }
            if (this.produceOtherInDepot.WorkHouse != null)
            {
                this.xrLabelDepartment.Text = this.produceOtherInDepot.WorkHouse.Workhousename;
            }
            this.xrLabelProduceOtherInDepotDesc.Text = this.produceOtherInDepot.ProduceOtherInDepotDesc;
            if (this.produceOtherInDepot.Depot != null)
                this.xrLabelDepot.Text = this.produceOtherInDepot.Depot.ToString();
            this.xrLabelSupplier.Text = this.produceOtherInDepot.Supplier.ToString();
            this.xrLabelOtherCompact.Text = this.produceOtherInDepot.ProduceOtherCompactId;
            this.xrLabelInsertTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.xrBarCode1.Text = this.produceOtherInDepot.ProduceOtherInDepotId;
            //明细
            //this.xrTableCell1ProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            //if (produceOtherInDepot.WorkHouse.Workhousename != null)
            //{
            //    this.xrTableCellDepartment.DataBindings.Add("Text", this.DataSource, "ProduceOtherInDepot.WorkHouse.Workhousename");
            //}
            //   this.xrTableDate.DataBindings.Add("Text", this.DataSource, "ProduceOtherInDepot." + Model.ProduceOtherInDepot.PRO_ProduceOtherInDepotDate, "{0:yyyy-MM-dd}");
            this.xrTableProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceQuantity);
            this.xrTableTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceTransferQuantity);
            this.InDepotQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceInDepotQuantity);
            this.xrTableUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProductUnit);
            //  this.xrTableProductGuige.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProductGuige);
            // this.xrTableProduceOtherInDepotId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_ProduceOtherInDepotId);
            this.xrTableCellDepotPosition.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDescription");
            //原本带委外合同单号，现带委外入库客户订单号
            this.TCProduceOtherCompactId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherInDepotDetail.PRO_InvoiceCusId);
        }
    }
}
