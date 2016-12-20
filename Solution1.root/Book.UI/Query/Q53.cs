using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class Q53 : DevExpress.XtraReports.UI.XtraReport
    {
        BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        BL.ProduceOtherCompactDetailManager detailManager = new Book.BL.ProduceOtherCompactDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private Model.ProduceOtherCompact produceOtherCompact;
        private ConditionOtherCompact conditionCom;
        public Q53(ConditionOtherCompact condition)
        {
            InitializeComponent();
            this.conditionCom = condition;
            IList<Model.ProduceOtherCompact> list = this.produceOtherCompactManager.Select(condition.ProduceOtherCompactId1, condition.ProduceOtherCompactId2, condition.StartDate, condition.EndDate, condition.SupplierId1, condition.SupplierId2, condition.ProductId1, condition.ProductId2);

            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException("無記錄");

            //foreach (Model.ProduceOtherCompact item in list)
            //{
            //    if (!string.IsNullOrEmpty(item.MRSHeaderId))
            //    {
            //        Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(item.MRSHeaderId);
            //        if (mRSHeader != null)
            //        {
            //            Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
            //            if (mPSheader != null)
            //            {
            //                item.InvoiceXOId = this.invoiceXOManager.Get(mPSheader.InvoiceXOId) == null ? "" : this.invoiceXOManager.Get(mPSheader.InvoiceXOId).CustomerInvoiceXOId;
            //            }
            //        }
            //    }

            //}

            this.DataSource = list;
            //CompanyInfo
            this.ReportName.Text = BL.Settings.CompanyChineseName;
            this.ReportTitle.Text = Properties.Resources.ProduceOtherCompactDetail;
            if (!global::Helper.DateTimeParse.DateTimeEquls(condition.StartDate, global::Helper.DateTimeParse.NullDate))
                this.xrLabelDateRange.Text += "自 " + condition.StartDate.ToString("yyyy-MM-dd");
            this.xrLabelDateRange.Text += "至 " + condition.EndDate.ToString("yyyy-MM-dd");
            this.xrLabelDates.Text += System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            //外发合同
            this.xrLabelProduceOtherCompactId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactId);
            this.xrLabelWorkHouse.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_NextWorkHouseName);
            this.xrLabelProduceOtherCompactDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactDate, "{0:yyyy-MM-dd}");
            this.xrLabelEmployee0.DataBindings.Add("Text", "Employee0." + this.DataSource, Model.ProduceOtherCompact.PRO_EmployeeName0);
            this.xrLabelSupplierId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_SupplierName);
            this.xrLabelProduceOtherCompactDesc.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_ProduceOtherCompactDesc);
            this.xrLabelJhDate.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_JiaoHuoDate, "{0:yyyy-MM-dd}");
            this.xrLabelCustomerXoId.DataBindings.Add("Text", this.DataSource, Model.ProduceOtherCompact.PRO_RPCustomerInvoiceXOId);

            this.xrSubreport1.ReportSource = new Q53_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q53_1 compact = this.xrSubreport1.ReportSource as Q53_1;
            Model.ProduceOtherCompact temp = this.GetCurrentRow() as Model.ProduceOtherCompact;
            if (temp != null)
            {
                temp.Details = this.detailManager.Select(temp.ProduceOtherCompactId, this.conditionCom.ProductId1, this.conditionCom.ProductId2);
                compact.ProduceOtherCompact = temp;
            }
        }
    }
}
