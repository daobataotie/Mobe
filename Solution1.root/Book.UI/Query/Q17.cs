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

// 编 码 人: 裴盾             完成时间:2009-6-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q17 : BaseReport
    {
        protected BL.InvoiceCTManager invoiceManager = new Book.BL.InvoiceCTManager();
        protected BL.InvoiceCTDetailManager detailManager = new Book.BL.InvoiceCTDetailManager();

        //一参构造
        public Q17(ConditionA condition)
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.CGCTDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));

            System.Collections.Generic.IList<Model.InvoiceCT> list = this.invoiceManager.Select(start, end);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            this.xrTableCellSupplierName.DataBindings.Add("Text", this.DataSource, "Supplier." + Model.Supplier.PROPERTY_SUPPLIERSHORTNAME);
            this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEZONGJI, "{0:0}");
            this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICETAX, "{0:0}");
            this.xrLabelZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEZSE, "{0:0}");

            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;            
            this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEHEJI, "{0:0}");
            
            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICETAX, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEZONGJI, "{0:0}");

            this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceCT.PROPERTY_INVOICEZSE, "{0:0}");

            this.xrSubreport1.ReportSource = new Q17_1();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q17_1 subReport = this.xrSubreport1.ReportSource as Q17_1;
            subReport.Invoice = this.GetCurrentRow() as Model.InvoiceCT;
        }
    }
}
