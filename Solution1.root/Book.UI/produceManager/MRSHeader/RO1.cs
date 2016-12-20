using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.MRSHeader
{
    public partial class RO1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MRSHeaderManager mRSheaderManager = new Book.BL.MRSHeaderManager();
        private BL.MRSdetailsManager mRSdetailsManager = new Book.BL.MRSdetailsManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();
        private BL.MRSdetailsManager mrsDetailManager = new Book.BL.MRSdetailsManager();

        private Model.MRSHeader mRSheader;
        public RO1(string mRSheaderId)
        {
            InitializeComponent();
            this.mRSheader = this.mRSheaderManager.GetDetails(mRSheaderId);

            if (this.mRSheader == null)
                return;

            // this.mRSheader.Details = this.mRSdetailsManager.Select(this.mRSheader);          

            foreach (Model.MRSdetails m in this.mRSheader.Details)
            {
                if (m.ProductId != null)
                    m.SpotStock = mrsDetailManager.SumSpotStock(m.ProductId);
            }
            this.DataSource = this.mRSheader.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.MRSDetails;
            this.xrLabelDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //物料需求计划
            this.xrLabelMRSHeaderId.Text = this.mRSheader.MRSHeaderId;

            Model.MPSheader mps = mPSheaderManager.Get(this.mRSheader.MPSheaderId);
            if (mps != null)
            {
                Model.InvoiceXO xo = this.invoiceXOManager.Get(mps.InvoiceXOId);
                if (xo != null)
                {
                    this.xrLabelXOCusId.Text = xo.CustomerInvoiceXOId;
                    xrLabelXOJiaoQi.Text = xo.InvoiceYjrq.Value.ToString("yyyy-MM-dd");
                    this.xrLabelMRSCustomer.Text = xo.xocustomer.CustomerShortName;
                    this.xrLabelPiHao.Text = xo.CustomerLotNumber;
                }
            }


            this.xrLabelMRSHeadername.Text = this.mRSheader.MPSheaderId;
            this.xrLabelMRSstate.Text = this.mRSheader.MRSstate;
            if (this.mRSheader.MRSstartdate != null)
            {
                this.xrLabelMRSstartdate.Text = this.mRSheader.MRSstartdate.Value.ToString("yyyy-MM-dd");
                // this.xrLabelMRSenddate.Text = this.mRSheader.MPSheaderId.Value.ToString("yyyy-MM-dd");
            }
            if (this.mRSheader.Employee0 != null)
            {
                this.xrLabelEmp0.Text = this.mRSheader.Employee0.EmployeeName;
            }
            if (this.mRSheader.Employee1 != null)
            {
                this.xrLabelEmployee0.Text = this.mRSheader.Employee1.EmployeeName;
            }
            this.xrLabelMRSheaderDesc.Text = this.mRSheader.MRSheaderDesc;

            xrLabelTpye.Text = this.mRSheader.GetSourceType;





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
            this.TCStockNum.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_SpotStock);
            //this.xrLabelJiaoQi.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_JiaoHuoDate, "{0:yyyy-MM-dd}");
            //this.xrTableWorkHouseNext.DataBindings.Add("Text", this.DataSource, "WorkHouseNext." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.lblHandBookId.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_HandbookId);
            this.lblHandBookProductId.DataBindings.Add("Text", this.DataSource, Model.MRSdetails.PRO_HandbookProductId);
        }
    }
}
