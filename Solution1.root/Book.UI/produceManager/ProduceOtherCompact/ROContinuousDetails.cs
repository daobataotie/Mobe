using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherCompact
{
    public partial class ROContinuousDetails : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        private BL.ProduceOtherCompactDetailManager produceOtherCompactdetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();

        public ROContinuousDetails()
        {
            InitializeComponent();

            //明细
            // this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_ProductUnit);
            this.xrTableCellOtherCompactSum.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_OtherCompactCount);
            // this.xrTableCellStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity,"{0:0.####}");
            //this.xrTableCell5.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableJiaoQi.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_JiaoQi, "{0:yyyy-MM-dd}");
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_Description);
            this.TCNextWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouseNext." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
        }

        private Model.ProduceOtherCompact _produceOtherCompact;

        public Model.ProduceOtherCompact ProduceOtherCompact
        {
            get { return _produceOtherCompact; }
            set { _produceOtherCompact = value; }
        }

        private void ROContinuousDetails_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.ProduceOtherCompact.Details;
        }
    }
}
