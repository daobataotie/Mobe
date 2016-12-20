using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceInDepot
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceInDepotManager ProduceInDepotManager = new Book.BL.ProduceInDepotManager();
        private BL.ProduceInDepotDetailManager ProduceInDepotDetailManager = new Book.BL.ProduceInDepotDetailManager();

        private Model.ProduceInDepot produceInDepot;
        public RO(string produceInDepotId)
        {
            InitializeComponent();
            this.produceInDepot = this.ProduceInDepotManager.Get(produceInDepotId);

            if (this.produceInDepot == null)
                return;

            this.produceInDepot.Details = this.ProduceInDepotDetailManager.Select(this.produceInDepot);

            this.DataSource = this.produceInDepot.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceInDepot;
            this.xrBarCodeId.Text = this.produceInDepot.ProduceInDepotId;
            this.xrLabelPrinDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //成品入庫
            this.xrLabelProduceInDepotId.Text = this.produceInDepot.ProduceInDepotId;
            this.xrLabelProduceOtherInDepotDate.Text = this.produceInDepot.ProduceInDepotDate.Value.ToString("yyyy-MM-dd");
            if (this.produceInDepot.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceInDepot.Employee0.EmployeeName;
            }
            //if (this.produceInDepot.Employee1 != null)
            //{
            //    this.xrLabelEmployee1.Text = this.produceInDepot.Employee1.EmployeeName;
            //}
            if (this.produceInDepot.WorkHouse != null)
            {
                this.xrLabelDepartment.Text = this.produceInDepot.WorkHouse.Workhousename;
            }
            this.xrLabelProduceOtherInDepotDesc.Text = this.produceInDepot.ProduceInDepotDesc;

            //明细
            this.xrTableProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            if (produceInDepot.WorkHouse.Workhousename != null)
            {
                this.xrTableDepartment.DataBindings.Add("Text", this.DataSource, "ProduceInDepot.WorkHouse.Workhousename");
            }
            this.xrTableProduceInDepotDate.DataBindings.Add("Text", this.DataSource, "ProduceInDepot." + Model.ProduceInDepot.PRO_ProduceInDepotDate, "{0:yyyy-MM-dd}");
            this.xrTableProduceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceQuantity);
            this.xrTableProductGuige.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProductGuige);
            this.xrTableProducePrice.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProducePrice);
            this.xrTableProduceMoney.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceMoney);
            this.xrTableCellCustomerProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            //this.xrTableCellProductProce.DataBindings.Add("Text", this.DataSource, "ProductProce." + Model.Product.PRO_ProductName);
            this.xrTableCellProduceTransferQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProduceTransferQuantity);
            this.xrTableCellPronoteHeaderId.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderId);
            this.xrTableCellHeJiProceduresSum.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_HeJiProceduresSum);
            this.xrTableCellHeJiCheckOutSum.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_HeJiCheckOutSum);
            this.xrTableCellBusinessHoursType.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_BusinessHoursType);
            this.xrTableCellRejectionRate.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_RejectionRate);
            this.xrTableCellZaZhi.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ZaZhi);
            this.xrTableCellHeJiProceduresSum.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_ProceduresSum);
            this.xrTableCellCheckOutSum.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_CheckOutSum);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrTableCellInvoiceQuantity.DataBindings.Add("Text", this.DataSource, Model.ProduceInDepotDetail.PRO_PronoteHeaderSum);
        }

    }
}
