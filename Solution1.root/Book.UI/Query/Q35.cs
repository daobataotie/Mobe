using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-6-20
     * 
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q35 : BaseReport
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

        //构造函数,初始化
        public Q35(ConditionK condition)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YSZKZLFXB;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.EndDate, condition.EndDate.ToString("yyyy-MM-dd"));


            System.Data.DataTable dt = miscDataManager.SelectDataTable(condition.EndDate);

            if (dt == null || dt.Rows.Count <= 0)
                throw new Helper.InvalidValueException();

            this.bindingSource1.DataSource = dt;

            this.xrTableCellAllMoney.DataBindings.Add("Text", this.DataSource, "allMoney", "{0:0}");
            this.xrTableCellCompanyId.DataBindings.Add("Text", this.DataSource, "CompanyId");
            this.xrTableCellCompanyName.DataBindings.Add("Text", this.DataSource, "CompanyName1");
            this.xrTableCellCompanyR0.DataBindings.Add("Text", this.DataSource, "CompanyR0", "{0:0}");
            this.xrTableCellGreatTenFifteenDays.DataBindings.Add("Text", this.DataSource, "greatTenFifteenDays", "{0:0}");
            this.xrTableCellNintyDats.DataBindings.Add("Text", this.DataSource, "nintyDays", "{0:0}");
            this.xrTableCellSixtyDays.DataBindings.Add("Text", this.DataSource, "sixtyDays", "{0:0}");
            this.xrTableCellTenFifteenDays.DataBindings.Add("Text", this.DataSource, "tenFifteenDays", "{0:0}");
            this.xrTableCellTenTwelveDays.DataBindings.Add("Text", this.DataSource, "tenTwelveDays", "{0:0}");
            this.xrTableCellThirtyDays.DataBindings.Add("Text", this.DataSource, "thirtyDays", "{0:0}");


            this.xrTableCellTotalAllMoney.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalCompanyR0.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalGreatTenFifteenDays.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalNintyDays.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalSixtyDays.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalTenFifteenDays.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalTenTwelveDays.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalThirtyDays.Summary.FormatString = "{0:0}";



            this.xrTableCellTotalAllMoney.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalCompanyR0.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalGreatTenFifteenDays.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalNintyDays.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalSixtyDays.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalTenFifteenDays.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalTenTwelveDays.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalThirtyDays.Summary.Func = SummaryFunc.Sum;



            this.xrTableCellTotalAllMoney.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalCompanyR0.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalGreatTenFifteenDays.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalNintyDays.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalSixtyDays.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalTenFifteenDays.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalTenTwelveDays.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalThirtyDays.Summary.IgnoreNullValues = true;


            this.xrTableCellTotalAllMoney.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalCompanyR0.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalGreatTenFifteenDays.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalNintyDays.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalSixtyDays.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalTenFifteenDays.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalTenTwelveDays.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalThirtyDays.Summary.Running = SummaryRunning.Report;





            this.xrTableCellTotalAllMoney.DataBindings.Add("Text", this.DataSource, "allMoney", "{0:0}");
            this.xrTableCellTotalCompanyR0.DataBindings.Add("Text", this.DataSource, "CompanyR0", "{0:0}");
            this.xrTableCellTotalGreatTenFifteenDays.DataBindings.Add("Text", this.DataSource, "greatTenFifteenDays", "{0:0}");
            this.xrTableCellTotalNintyDays.DataBindings.Add("Text", this.DataSource, "nintyDays", "{0:0}");
            this.xrTableCellTotalSixtyDays.DataBindings.Add("Text", this.DataSource, "sixtyDays", "{0:0}");
            this.xrTableCellTotalTenFifteenDays.DataBindings.Add("Text", this.DataSource, "tenFifteenDays", "{0:0}");
            this.xrTableCellTotalTenTwelveDays.DataBindings.Add("Text", this.DataSource, "tenTwelveDays", "{0:0}");
            this.xrTableCellTotalThirtyDays.DataBindings.Add("Text", this.DataSource, "thirtyDays", "{0:0}");
        }
    }
}