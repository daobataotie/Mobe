using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.ProduceOtherCompact
{

    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        private BL.ProduceOtherCompactDetailManager produceOtherCompactdetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private Model.ProduceOtherCompact produceOtherCompact;
        public RO(string produceOtherCompactId)
        {
            InitializeComponent();
            this.produceOtherCompact = this.produceOtherCompactManager.Get(produceOtherCompactId);

            if (this.produceOtherCompact == null)
                return;

            this.produceOtherCompact.Details = this.produceOtherCompactdetailManager.Select(this.produceOtherCompact);

            this.DataSource = this.produceOtherCompact.Details;
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherCompactDetail;
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();

            //外发合同
            this.xrLabelProduceOtherCompactId.Text = this.produceOtherCompact.ProduceOtherCompactId;
            this.xrLabelProduceOtherCompactDate.Text = this.produceOtherCompact.ProduceOtherCompactDate.Value.ToString("yyyy-MM-dd");
            if (this.produceOtherCompact.Employee0 != null)
            {
                this.xrLabelEmployee0.Text = this.produceOtherCompact.Employee0.EmployeeName;
            }
            if (this.produceOtherCompact.Supplier != null)
            {
                this.xrLabelSupplierId.Text = this.produceOtherCompact.Supplier.SupplierFullName;
            }
            this.xrLabelProduceOtherCompactDesc.Text = this.produceOtherCompact.ProduceOtherCompactDesc;
            //if (global::Helper.DateTimeParse.DateTimeEquls(this.produceOtherCompact.JiaoHuoDate, global::Helper.DateTimeParse.NullDate) || this.produceOtherCompact.JiaoHuoDate==null)
            //{
            //    this.xrLabelJhDate.Text = string.Empty;
            //}
            //else
            //{
            //    this.xrLabelJhDate.Text = this.produceOtherCompact.JiaoHuoDate.Value.ToString("yyyy-MM-dd");
            //}

            if (!string.IsNullOrEmpty(this.produceOtherCompact.MRSHeaderId))
            {
                Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(this.produceOtherCompact.MRSHeaderId);
                if (mRSHeader != null)
                {
                    Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
                    if (mPSheader != null)
                    {
                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
                        if (invoiceXO != null)
                        {
                            this.xrLabelCustomerXoId.Text = invoiceXO.CustomerInvoiceXOId;
                            this.xrLabelCustomer.Text = invoiceXO.xocustomer == null ? null : invoiceXO.xocustomer.CustomerShortName;
                            this.xrLabelCheck.Text = invoiceXO.xocustomer == null ? null : invoiceXO.xocustomer.CheckedStandard;
                        }
                    }
                }
            }

            //明细
            // this.xrTableCellProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_ProductUnit);
            this.xrTableCellOtherCompactSum.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_OtherCompactCount);
            // this.xrTableCellStock.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_StocksQuantity,"{0:0.####}");
            //this.xrTableCell5.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableJiaoQi.DataBindings.Add("Text", this.DataSource, "ProduceOtherCompact.InvoiceXO." + Model.InvoiceXO.PRO_InvoiceYjrq, "{0:yyyy-MM-dd}");
            this.xrTableDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompactDetail.PRO_Description);
            this.TCNextWorkHouse.DataBindings.Add("Text", this.DataSource, "WorkHouseNext." + Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            this.xrRichText1.DataBindings.Add("Rtf", this.DataSource, "ProductDesc");
        }

    }
}
