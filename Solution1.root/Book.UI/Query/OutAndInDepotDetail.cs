using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.Query
{
    public partial class OutAndInDepotDetail : DevExpress.XtraReports.UI.XtraReport
    {
        BL.DepotOutDetailManager manager = new Book.BL.DepotOutDetailManager();
        public OutAndInDepotDetail()
        {
            InitializeComponent();
        }
        public OutAndInDepotDetail(OutAndInDepot condition)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.OutAndInDepot;
            this.lblDateRange.Text = "日期区间：" + condition.StartDate.ToString("yyyy-MM-dd") + "-" + condition.EndDate.ToString("yyyy-MM-dd");
            this.lblPrintDate.Text = "列印日期：" + DateTime.Now.ToString("yyyy-MM-dd");

            DataTable details = this.manager.SelectOutAndInDepot(condition.StartDate, condition.EndDate, condition.DepotStart, condition.DepotEnd, condition.ProduceCategoryStart, condition.ProductCategoryEnd, condition.ProductIdStart, condition.ProductIdEnd);
            if (details == null || details.Rows.Count == 0)
                throw new Helper.InvalidValueException("无记录");
            this.DataSource = details;

            this.xrTableCellDate.DataBindings.Add("Text", this.DataSource, "Date", "{0:yyyy-MM-dd}");
            this.xrTableCellOutDepotId.DataBindings.Add("Text", this.DataSource, "InvoiceId");
            this.xrTableCellDepot.DataBindings.Add("Text", this.DataSource, "DepotName");
            this.xrTableCellProductName.DataBindings.Add("Text", this.DataSource, "ProductName");
            this.xrTableCellUnit.DataBindings.Add("Text", this.DataSource, "ProductUnit");
            this.xrTableCellHuoWei.DataBindings.Add("Text", this.DataSource, "DepotPositionName");
            this.xrTableCellOutNum.DataBindings.Add("Text", this.DataSource, "Quantity", "{0:0.##}");

            this.lblTotalNum.Summary.FormatString = "{0:0.##}";
            this.lblTotalNum.Summary.Func = SummaryFunc.Sum;
            this.lblTotalNum.Summary.IgnoreNullValues = true;
            this.lblTotalNum.Summary.Running = SummaryRunning.Report;
            this.lblTotalNum.DataBindings.Add("Text", this.DataSource, "Quantity", "{0:0.##}");
        }
    }
}
