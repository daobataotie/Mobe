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

// 编 码 人: 裴盾             完成时间:2009-6-13
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q21 : BaseReport
    {
        private BL.InvoiceXSManager invoiceManager = new Book.BL.InvoiceXSManager();


        //构造
        public Q21(ConditionA condition)
        {
            DateTime start = condition.StartDate;
            DateTime end = condition.EndDate;

            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.CHZL;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, start.ToString("yyyy/MM/dd"), end.ToString("yyyy/MM/dd"));

            System.Collections.Generic.IList<Model.InvoiceXS> list = this.invoiceManager.Select1(start, end);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;

            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_KIND);
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEID);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);

            //this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrLabelZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");

            this.xrLabelTotalHeJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalHeJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalHeJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalHeJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");

            this.xrLabelTotalTax.Summary.FormatString = "{0:0}";
            this.xrLabelTotalTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalTax.Summary.IgnoreNullValues = true;
            this.xrLabelTotalTax.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");

            this.xrLabelTotalZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZongJi.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");

            this.xrLabelTotalZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTotalZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTotalZSE.Summary.Running = SummaryRunning.Report;
            //this.xrLabelTotalZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");

        }

        /// <summary>
        /// 报表打印前触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Model.InvoiceXS invoicexs = this.GetCurrentRow() as Model.InvoiceXS;
            switch (invoicexs.Kind)
            {
                case "销售":
                    Q21_1 q21_1_1 = new Q21_1();
                    this.xrSubreport1.ReportSource = q21_1_1;
                    q21_1_1.Invoice = invoicexs;
                    break;
                case "銷售":
                    Q21_1 q21_1_2 = new Q21_1();
                    this.xrSubreport1.ReportSource = q21_1_2;
                    q21_1_2.Invoice = invoicexs;
                    break;
                case "銷退":
                    Q21_2 q21_2_1 = new Q21_2();
                    this.xrSubreport1.ReportSource = q21_2_1;
                    q21_2_1.Invoice = invoicexs;
                    break;
                case "销退":
                    Q21_2 q21_2_2 = new Q21_2();
                    this.xrSubreport1.ReportSource = q21_2_2;
                    q21_2_2.Invoice = invoicexs;
                    break;
                default:
                    break;
            }
        }
    }
}
