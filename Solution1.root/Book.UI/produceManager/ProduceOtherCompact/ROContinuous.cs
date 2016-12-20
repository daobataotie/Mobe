using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Book.UI.Query;

namespace Book.UI.produceManager.ProduceOtherCompact
{

    public partial class ROContinuous : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new Book.BL.ProduceOtherCompactManager();
        private BL.ProduceOtherCompactDetailManager produceOtherCompactdetailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private ConditionOtherCompact _mCondition;

        public ROContinuous(IList<Model.ProduceOtherCompact> mList)
        {
            InitializeComponent();
            if (mList == null || mList.Count == 0)
                return;
            this.DataSource = mList;
            this.mBandData();
        }
        public ROContinuous(ConditionOtherCompact condition)
        {
            InitializeComponent();
            this._mCondition = condition;
            IList<Model.ProduceOtherCompact> list = this.produceOtherCompactManager.Select(condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.StartDate, condition.EndDate, condition.SupplierId1, condition.SupplierId2, condition.ProductId1, condition.ProductId2);
            if (list == null || list.Count == 0)
                return;
            this.DataSource = list;
            this.mBandData();

            #region Remarks
            //this.produceOtherCompact = this.produceOtherCompactManager.Get(produceOtherCompactId);
            //this.produceOtherCompact.Details = this.produceOtherCompactdetailManager.Select(this.produceOtherCompact);
            //foreach (Model.ProduceOtherCompactDetail detail in this.produceOtherCompact.Details)
            //{
            //    Model.MRSdetails mrsdetail = new BL.MRSdetailsManager().Get(detail.MRSdetailsId);
            //    foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
            //    {
            //        if (detail.ProductId == mrsdetail.MadeProductId)
            //            this.xrLabelInvoiceSum.Text = detail.InvoiceXODetailQuantity.Value.ToString("F0");
            //    }
            //}
            #endregion
        }

        private void mBandData()
        {
            //CompanyInfo
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = Properties.Resources.ProduceOtherCompactDetail;
            this.xrLabelDate.Text += DateTime.Now.ToShortDateString();
            //外发合同
            this.xrLabelProduceOtherCompactId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactId);
            this.xrLabelProduceOtherCompactDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactDate, "{0:yyyy-MM-dd}");
            this.xrLabelEmployee0.DataBindings.Add("Text", this.DataSource, "Employee0." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.xrLabelSupplierId.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERFULLNAME);
            this.xrLabelProduceOtherCompactDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactDesc);
            this.xrLabelCustomerXoId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_CustomerInvoiceXOId);
            this.xrLabelCustomer.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CustomerShortName);
            this.xrLabelCheck.DataBindings.Add("Text", this.DataSource, "Customer." + Model.Customer.PRO_CheckedStandard);
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.ProduceOtherCompact.PRO_ProduceOtherCompactId));
            this.xrSubreport1.ReportSource = new ROContinuousDetails();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ROContinuousDetails detail = this.xrSubreport1.ReportSource as ROContinuousDetails;
            Model.ProduceOtherCompact poc = this.GetCurrentRow() as Model.ProduceOtherCompact;
            if (poc != null)
            {
                poc.Details = this.produceOtherCompactdetailManager.Select(poc);
                detail.ProduceOtherCompact = poc;
            }
        }
    }
}
