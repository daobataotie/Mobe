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

    // 编 码 人: 裴盾              完成时间:2009-6-27
    // 修改原因：
    // 修 改 人:                          修改时间:
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/
    public partial class SupplierTransactionsRank : BaseReport
    {
        private BL.MiscDataManager miscDateManager = new Book.BL.MiscDataManager();

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public SupplierTransactionsRank(ConditionF condition)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = Properties.Resources.CSJYPH;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));
            this.xrLabelIdreanger.Text = string.Format("{0}-{1}", condition.StartId, condition.EndId);
            System.Data.DataTable list = this.miscDateManager.SelectForSupplierTransactionRank(condition.StartDate, condition.EndDate, condition.StartId, condition.EndId, condition.CategoryId);
            if (list == null || list.Rows.Count < 1)
                throw new Helper.InvalidValueException(Properties.Resources.Title_NoRecRecord);

            this.bindingSource1.DataSource = list;


            this.DataSource = this.bindingSource1.DataSource;

            this.TCChangShangbianHao.DataBindings.Add("Text", this.DataSource, "Id");
            this.TCgongsimingcheng.DataBindings.Add("Text", this.DataSource, "SupplierFullName");
            this.TCjinhuojine.DataBindings.Add("Text", this.DataSource, "JinHuoJinE", "{0:0}");
            this.TCno.DataBindings.Add("Text", this.DataSource, "SortId");
            this.TCshijinjine.DataBindings.Add("Text", this.DataSource, "ShiJinJinE", "{0:0}");
            this.TCtuihuojine.DataBindings.Add("Text", this.DataSource, "TuiHuoJinE", "{0:0}");
            this.TCzherangjine.DataBindings.Add("Text", this.DataSource, "ZheRangJinE", "{0:0}");
            this.TCJinHuoCS.DataBindings.Add("Text", this.DataSource, "TotalQuantity", "{0:0}");

            this.TCsumJHJE.Summary.FormatString = "{0:0}";
            this.TCsumJHJE.Summary.Func = SummaryFunc.Sum;
            this.TCsumJHJE.Summary.IgnoreNullValues = true;
            this.TCsumJHJE.Summary.Running = SummaryRunning.Report;
            this.TCsumJHJE.DataBindings.Add("Text", this.DataSource, "JinHuoJinE", "{0:0}");

            this.TCsumSJJE.Summary.FormatString = "{0:0}";
            this.TCsumSJJE.Summary.Func = SummaryFunc.Sum;
            this.TCsumSJJE.Summary.IgnoreNullValues = true;
            this.TCsumSJJE.Summary.Running = SummaryRunning.Report;
            this.TCsumSJJE.DataBindings.Add("Text", this.DataSource, "ShiJinJinE", "{0:0}");

            this.TCsumTHJE.Summary.FormatString = "{0:0}";
            this.TCsumTHJE.Summary.Func = SummaryFunc.Sum;
            this.TCsumTHJE.Summary.IgnoreNullValues = true;
            this.TCsumTHJE.Summary.Running = SummaryRunning.Report;
            this.TCsumTHJE.DataBindings.Add("Text", this.DataSource, "TuiHuoJinE", "{0:0}");

            this.TCsumZRJE.Summary.FormatString = "{0:0}";
            this.TCsumZRJE.Summary.Func = SummaryFunc.Sum;
            this.TCsumZRJE.Summary.IgnoreNullValues = true;
            this.TCsumZRJE.Summary.Running = SummaryRunning.Report;
            this.TCsumZRJE.DataBindings.Add("Text", this.DataSource, "ZheRangJinE", "{0:0}");

        }

    }
}
