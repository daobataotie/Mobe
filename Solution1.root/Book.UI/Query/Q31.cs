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

// 编 码 人:  够波涛             完成时间:2009-6-17
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q31 : BaseReport
    {
        private BL.InvoiceXSManager inoviceManager = new Book.BL.InvoiceXSManager();


        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q31(ConditionH condition)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = Properties.Resources.YWYSDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));
            this.xrLabelEmployeeName.Text = string.Format(Properties.Resources.YeWuName, condition.Employee.EmployeeName);
            System.Collections.Generic.IList<Model.InvoiceXS> list = this.inoviceManager.Select(condition.StartDate, condition.EndDate, condition.Employee);
            if (list == null || list.Count <= 0)
                throw new global::Helper.InvalidValueException();
            this.bindingSource1.DataSource = list;
            this.GroupHeader1.GroupFields.Add(new GroupField(Model.InvoiceXS.PRO_CustomerId));

            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);
            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_KIND);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellInoiceHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrTableCellInvoiceTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrTableCellInvoiceZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            this.xrTableCellInvoiceYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");
            //this.xrTableCellInvoiceWeiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEOWED, "{0:0}");



            this.xrTableCellHeji.Summary.FormatString = "{0:0}";
            this.xrTableCellHeji.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellHeji.Summary.IgnoreNullValues = true;
            this.xrTableCellHeji.Summary.Running = SummaryRunning.Group;
            //this.xrTableCellHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");

            this.xrTableCellTax.Summary.FormatString = "{0:0}";
            this.xrTableCellTax.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTax.Summary.IgnoreNullValues = true;
            this.xrTableCellTax.Summary.Running = SummaryRunning.Group;
            //this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");

            this.xrTableCellZongji.Summary.FormatString = "{0:0}";
            this.xrTableCellZongji.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellZongji.Summary.IgnoreNullValues = true;
            this.xrTableCellZongji.Summary.Running = SummaryRunning.Group;
            //this.xrTableCellZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");

            this.xrTableCellYiShou.Summary.FormatString = "{0:0}";
            this.xrTableCellYiShou.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellYiShou.Summary.IgnoreNullValues = true;
            this.xrTableCellYiShou.Summary.Running = SummaryRunning.Group;
            this.xrTableCellYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");

            this.xrTableCellWeiShou.Summary.FormatString = "{0:0}";
            this.xrTableCellWeiShou.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellWeiShou.Summary.IgnoreNullValues = true;
            this.xrTableCellWeiShou.Summary.Running = SummaryRunning.Group;
            //this.xrTableCellWeiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEOWED, "{0:0}");

            this.xrTableCellTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalTax.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalWeiShou.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalYiShou.Summary.FormatString = "{0:0}";
            this.xrTableCellTotalZongJi.Summary.FormatString = "{0:0}";

            this.xrTableCellTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalWeiShou.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalYiShou.Summary.Func = SummaryFunc.Sum;
            this.xrTableCellTotalZongJi.Summary.Func = SummaryFunc.Sum;

            this.xrTableCellTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalTax.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalWeiShou.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalYiShou.Summary.IgnoreNullValues = true;
            this.xrTableCellTotalZongJi.Summary.IgnoreNullValues = true;

            this.xrTableCellTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalWeiShou.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalYiShou.Summary.Running = SummaryRunning.Report;
            this.xrTableCellTotalZongJi.Summary.Running = SummaryRunning.Report;

            //this.xrTableCellTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrTableCellTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrTableCellTotalWeiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEOWED, "{0:0}");
            this.xrTableCellTotalYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");
            //this.xrTableCellTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");

        }

    }
}
