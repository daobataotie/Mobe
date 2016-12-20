using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using Book.UI.Query;

namespace Book.UI.produceManager.MRSHeader
{
    public partial class RO1Details : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MRSHeaderManager mRSheaderManager = new Book.BL.MRSHeaderManager();
        private BL.MRSdetailsManager mRSdetailsManager = new Book.BL.MRSdetailsManager();
        private BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new Book.BL.MPSheaderManager();

        public RO1Details(ConditionMRS condition)
        {
            InitializeComponent();

            IList<Model.MRSHeader> list = this.mRSheaderManager.SelectbyCondition(condition.MrsStart, condition.MrsEnd, condition.CustomerStart, condition.CustomerEnd, condition.StartDate, condition.EndDate, condition.SourceType, condition.Id1, condition.Id2, condition.Cusxoid, condition.Product);
            if (list == null || list.Count == 0)
                return;
            //Model.InvoiceXO xo = null;
            //Model.MPSheader mpsH = null;
            //if (list == null || list.Count == 0)
            //    return;
            //foreach (Model.MRSHeader item in list)
            //{
            //    mpsH = mPSheaderManager.Get(item.MPSheaderId);
            //    if (mpsH != null)
            //    {
            //        xo = this.invoiceXOManager.Get(mpsH.InvoiceXOId);
            //        item.CustomerInvoiceXOId = xo == null ? "" : xo.CustomerInvoiceXOId;
            //        item.CustomerId = xo == null ? "" : xo.xocustomer.CustomerShortName;
            //        item.YjrqDate = xo == null ? null : xo.InvoiceYjrq;
            //        item.PiHao = xo == null ? "" : xo.CustomerLotNumber;
            //    }
            //}
            this.DataSource = list;
            band();
        }

        public RO1Details(IList<Model.MRSHeader> mrsHeaderList)
        {
            InitializeComponent();

            if (mrsHeaderList == null || mrsHeaderList.Count == 0)
                return;
            this.DataSource = mrsHeaderList;
            band();
        }

        private void band()
        {
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.MRSDetails;
            this.xrLabelDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //物料需求计划
            this.xrLabelMRSHeaderId.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSHeaderId);
            this.xrLabelXOCusId.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PROPERTY_CustomerInvoiceXOId);
            this.xrLabelXOJiaoQi.DataBindings.Add("Text",this.DataSource, "YjrqDate", "{0:yyyy-MM-dd}");
            this.xrLabelMRSCustomer.DataBindings.Add("Text", this.DataSource, "CustomerShortName");
            this.xrLabelPiHao.DataBindings.Add("Text", this.DataSource, "PiHao");
            this.xrLabelMPSHeader.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MPSheaderId);
            this.xrLabelMRSstate.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSstate);
            this.xrLabelMRSstartdate.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSstartdate, "{0:yyyy-MM-dd}");
            this.xrLabelEmployee0.DataBindings.Add("Text", this.DataSource, "Employee1Name");
            this.xrLabelEmp0.DataBindings.Add("Text", this.DataSource, "Employee0Name") ;
            this.xrLabelMRSheaderDesc.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PRO_MRSheaderDesc);
            this.xrLabelTpye.DataBindings.Add("Text", this.DataSource, Model.MRSHeader.PROPERTY_GETSOURCETYPE);
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.MRSHeader.PRO_MRSHeaderId));
            this.xrSubreport1.ReportSource = new RO1Details1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO1Details1 detail = this.xrSubreport1.ReportSource as RO1Details1;
            Model.MRSHeader mrsh = this.GetCurrentRow() as Model.MRSHeader;
            if (mrsh != null)
            {
                mrsh.Details = this.mRSdetailsManager.Select(mrsh);
                detail.MMRSHeader = mrsh;
            }
        }
    }
}
