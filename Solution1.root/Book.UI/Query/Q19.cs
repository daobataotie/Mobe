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

// 编 码 人:  够波涛             完成时间:2009-6-9
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q19 : BaseReport
    {
        private BL.InvoiceCGManager invoiceManager = new Book.BL.InvoiceCGManager();


        //一参构造
        public Q19(ConditionA condition)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = Properties.Resources.JHJYB;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy/MM/dd"), condition.EndDate.ToString("yyyy/MM/dd"));

            System.Collections.Generic.IList<Model.InvoiceCG> list = this.invoiceManager.Select(condition.StartDate, condition.EndDate);
            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }
            this.bindingSource1.DataSource = list;

            this.xrTableCellKind.Text = Properties.Resources.CG;
            //this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrTableCellCompanyName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);
            //this.xrTableCellFpbh.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEFPBH);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.Invoice.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellInvoiceHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");

            this.xrLabelTax.Summary.FormatString = "{0:0}";
            this.xrLabelZongji.Summary.FormatString = "{0:0}";
            this.xrLabelHeJi.Summary.FormatString = "{0:0}";

            this.xrLabelTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelZongji.Summary.Func = SummaryFunc.Sum;
            this.xrLabelHeJi.Summary.Func = SummaryFunc.Sum;

            this.xrLabelTax.Summary.IgnoreNullValues = true;
            this.xrLabelZongji.Summary.IgnoreNullValues = true;
            this.xrLabelHeJi.Summary.IgnoreNullValues = true;

            this.xrLabelTax.Summary.Running = SummaryRunning.Report;
            this.xrLabelZongji.Summary.Running = SummaryRunning.Report;
            this.xrLabelHeJi.Summary.Running = SummaryRunning.Report;

            //this.xrLabelHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrLabelZongji.DataBindings.Add("Text", this.DataSource, Model.InvoiceCG.PROPERTY_INVOICEZONGJI, "{0:0}");

        }

    }
}
