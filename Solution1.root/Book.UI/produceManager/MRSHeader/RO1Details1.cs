using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.MRSHeader
{
    public partial class RO1Details1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MRSHeaderManager mRSheaderManager = new Book.BL.MRSHeaderManager();
        private BL.MRSdetailsManager mRSdetailsManager = new Book.BL.MRSdetailsManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();

        public RO1Details1()
        {
            InitializeComponent();

            //明细信息
            this.xrTableCell5.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_Inumber);
            // this.xrTableMRSdetailsId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableCellOnWay.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_OrderOnWayQuantity);
            // this.xrTableMRSstartdate.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PROPERTY_MRSSTARTDATE);
            this.xrTableMRSdetailssum.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailssum);
            this.xrTableMRSdetailsQuantity.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_MRSdetailsQuantity);
            // this.xrTableCellProductSpecification.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //  this.xrTableCustomer.DataBindings.Add("Text", this.DataSource, "Customer.CustomerShortName");
            this.xrTableProductUnit.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_ProductUnit);
            //this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "Product." + Model.Product.PRO_ProductDescription);
            this.xrTableStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity, "{0:0.####}");
            this.xrTableMaterialDistributioned.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProduceMaterialDistributioned, "{0:0.####}");
            //this.xrLabeOtherFenPei.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_OtherMaterialDistributioned, "{0:0.####}");
            //this.xrLabelJiaoQi.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_JiaoHuoDate, "{0:yyyy-MM-dd}");

            //this.xrTableWorkHouseNext.DataBindings.Add("Text", this.DataSource, "WorkHouseNext." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.TCStockNum.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_SpotStock);
            this.TCHandBookId.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_HandbookId);
            this.TCHandBookProductId.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_HandbookProductId);
        }

        private Model.MRSHeader _mMRSHeader;

        public Model.MRSHeader MMRSHeader
        {
            get { return _mMRSHeader; }
            set { _mMRSHeader = value; }
        }

        private void RO1Details1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = this.MMRSHeader.Details;
        }
    }
}
