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

// 编 码 人:  够波涛             完成时间:2009-6-25
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q43 : BaseReport
    {
        private BL.MiscDataManager miscDateManager = new Book.BL.MiscDataManager();


        /// <summary>
        /// 构造函数。初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q43(ConditionG condition)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YWJYPH;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));            
            this.xrLabelIdreanger.Text = string.Format("{0}-{1}", condition.EmployeeStartId, condition.EmployeeEndId);

            System.Data.DataTable list = this.miscDateManager.Select1(condition.StartDate, condition.EndDate, condition.EmployeeStartId, condition.EmployeeEndId);
            if (list == null || list.Rows.Count <= 0)
                throw new Helper.InvalidValueException();

            this.bindingSource1.DataSource = list;

            this.xrTableCellEmployeeId.DataBindings.Add("Text", this.DataSource, "EmployeeId");
            this.xrTableCellEmployeeName.DataBindings.Add("Text", this.DataSource, "EmployeeName");
            this.xrTableCellInvoiceXSMoney.DataBindings.Add("Text", this.DataSource, "InvoiceXSMoney", "{0:0}");
            this.xrTableCellInvoiceXTMoney.DataBindings.Add("Text", this.DataSource, "InvoiceXTMoney", "{0:0}");
            this.xrTableCellInvoiceZongJi.DataBindings.Add("Text", this.DataSource, "InvoiceZongJi", "{0:0}");
            this.xrTableCellInvoiceZRE.DataBindings.Add("Text", this.DataSource, "InvoiceXSZRE", "{0:0}");

            this.xrTableCellTotalInvoiceXSMoney.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalInvoiceXTMoney.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalInvoiceZongJi.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalInvoiceZRE.Summary.FormatString = "{0:0}";

            this.xrTableCellTotalInvoiceXSMoney.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalInvoiceXTMoney.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalInvoiceZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalInvoiceZRE.Summary.Func = SummaryFunc.Sum;

            this.xrTableCellTotalInvoiceXSMoney.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalInvoiceXTMoney.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalInvoiceZongJi.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalInvoiceZRE.Summary.IgnoreNullValues = true;

            this.xrTableCellTotalInvoiceXSMoney.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalInvoiceXTMoney.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalInvoiceZongJi.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalInvoiceZRE.Summary.Running = SummaryRunning.Report;

            this.xrTableCellTotalInvoiceXSMoney.DataBindings.Add("Text", this.DataSource, "InvoiceXSMoney", "{0:0}");
            this.xrTableCellTotalInvoiceXTMoney.DataBindings.Add("Text", this.DataSource, "InvoiceXTMoney", "{0:0}");
            this.xrTableCellTotalInvoiceZongJi.DataBindings.Add("Text", this.DataSource, "InvoiceZongJi", "{0:0}");
            this.xrTableCellTotalInvoiceZRE.DataBindings.Add("Text", this.DataSource, "InvoiceXSZRE", "{0:0}");
        }
    }
}