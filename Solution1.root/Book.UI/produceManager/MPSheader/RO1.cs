using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.MPSheader
{
    public partial class RO1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();
        private BL.MPSdetailsManager mPSdetailsManager = new Book.BL.MPSdetailsManager();

        private Model.MPSheader mPSheader;

        public RO1(string mPSheaderId)
        {
            InitializeComponent();

            this.mPSheader = this.mPSheaderManager.Get(mPSheaderId);

            if (this.mPSheader == null)
                return;

            this.mPSheader.Details = this.mPSdetailsManager.Select(this.mPSheader);

            this.DataSource = this.mPSheader.Details;

            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.MPSdetails;
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();
            //计划信息
            this.xrLabelMPSheaderId.Text=this.mPSheader.Id;
            //this.xrLabelMPSheaderName.Text=this.mPSheader.MPSheaderName;
            this.xrLabelMPSStartDate.Text = this.mPSheader.MPSStartDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelMPSEndDate.Text = this.mPSheader.MPSEndDate.Value.ToString("yyyy-MM-dd");
            this.xrLabelZhiJian.Text=this.mPSheader.MPSheaderState;
            Model.InvoiceXO InvoiceXO=new BL.InvoiceXOManager().Get(this.mPSheader.InvoiceXOId);
            if (InvoiceXO != null)
            {
            this.xrLabelCustomer.Text = InvoiceXO.Customer.CustomerShortName;
            this.xrLabelZhiJian.Text = InvoiceXO.xocustomer.CustomerShortName;
            this.xrLabelCusXOId.Text = InvoiceXO.CustomerInvoiceXOId;
            this.xrLabelZhiJian.Text = InvoiceXO.xocustomer.CheckedStandard;
            
            }
           // this.xrLabelMPSheaderDesc.Text=this.mPSheader.MPSheaderDesc;
            if (this.mPSheader.Employee0!=null)
            {
                this.xrLabelEmployee0.Text = this.mPSheader.Employee0.EmployeeName;
            }
            if (this.mPSheader.Employee1 != null)
            {
                this.xrLabelEmployee1.Text = this.mPSheader.Employee1.EmployeeName;
            }
        
                //明细信息
            this.xrTableCell3.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_Inumber);
           // this.xrTableProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            this.xrTableProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableMPSdetailssum.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_MPSdetailssum);
            this.xrTableProductUnit.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_ProductUnit);
           // this.xrTableCellProductSpecification.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            this.xrTableMPSdetailsdes.DataBindings.Add("Text", this.DataSource, Model.MPSdetails.PRO_MPSdetailsdes);
            this.xrTableCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);
            this.xrTableGuiGe.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductSpecification);
            //this.xrTableCellCusPro.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_CustomerProductName);            
            
        }

    }
}
