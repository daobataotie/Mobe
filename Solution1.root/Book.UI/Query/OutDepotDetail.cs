using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    public partial class OutDepotDetail : DevExpress.XtraReports.UI.XtraReport
    {
        BL.DepotOutDetailManager manager = new Book.BL.DepotOutDetailManager();
        public OutDepotDetail()
        {
            InitializeComponent();
        }
        public OutDepotDetail(OutDepot condition)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.OutDepotDetail;
            this.lblDateRange.Text = "日期區間：" + condition.StartDate.ToString("yyyy-MM-dd") + "-" + condition.EndDate.ToString("yyyy-MM-dd");
            this.lblPrintDate.Text = "列印日期：" + DateTime.Now.ToString("yyyy-MM-dd");

            IList<Model.DepotOutDetail> details = this.manager.SelectByCondition(condition.StartDate, condition.EndDate, condition.OutDepotIdStart, condition.OutDepotIdEnd, condition.DepotStart, condition.DepotEnd);
            if (details == null || details.Count == 0)
                throw new Helper.InvalidValueException("無記錄");
            this.DataSource = details;

            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_Date, "{0:yyyy-MM-dd}");
            this.xrTableCellOutDepotId.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutId);
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "DepotPosition." + "Depot." + Model.Depot.PRO_DepotName);
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_ProductUnit);
            this.xrTableCellHuoWei.DataBindings.Add("Text", this.DataSource, "DepotPosition." + Model.DepotPosition.PROPERTY_ID);
            this.xrTableCellOutNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutDetailQuantity);
            this.xrTableCellHuoWeiNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_CurrentDepotQuantity);
            this.xrTableCellDepotNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_CurrentStockQuantity);

            this.lblTotalNum.Summary.FormatString = "{0:0}";
            this.lblTotalNum.Summary.Func = SummaryFunc.Sum;
            this.lblTotalNum.Summary.IgnoreNullValues = true;
            this.lblTotalNum.Summary.Running = SummaryRunning.Report;
            this.lblTotalNum.DataBindings.Add("Text", this.DataSource, Model.DepotOutDetail.PRO_DepotOutDetailQuantity);
        }
    }
}
