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

// 编 码 人:  够波涛             完成时间:2009-6-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q23 : BaseReport
    {
        protected BL.InvoiceXTManager invoiceManager = new Book.BL.InvoiceXTManager();
        protected BL.InvoiceXTDetailManager detailManager = new Book.BL.InvoiceXTDetailManager();

        /// <summary>
        /// 构造，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q23(ConditionA condition)
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;
            InitializeComponent();
            System.Collections.Generic.IList<Model.InvoiceXT> list = this.invoiceManager.Select(start, end);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;
            this.xrLabelReportName.Text = Properties.Resources.XSXTDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);

            this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEZONGJI, "{0:0}");
            this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICETAX, "{0:0}");
            this.xrLabelZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEZSE, "{0:0}");


            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEHEJI, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICETAX, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEZONGJI, "{0:0}");

            this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXT.PROPERTY_INVOICEZSE, "{0:0}");



            this.xrSubreport1.ReportSource = new Q23_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q23_1 subReport = this.xrSubreport1.ReportSource as Q23_1;
            subReport.Invoice = this.GetCurrentRow() as Model.InvoiceXT;
        }
    }
}
