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
    public partial class Q30 : BaseReport
    {
        private BL.InvoiceDetail01Manager detailManager;


        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        /// <param name="conditioon"></param>
        public Q30(ConditionF conditioon)
        {
            InitializeComponent();

       
                this.detailManager = new Book.BL.InvoiceDetail01Manager();

                this.xrLabelReportName.Text = Properties.Resources.YSZKTJB;
                this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, conditioon.StartDate.ToString("yyyy-MM-dd"), conditioon.EndDate.ToString("yyyy-MM-dd"));

                System.Collections.Generic.IList<Model.InvoiceDetail01> list = this.detailManager.Select1(conditioon.StartDate, conditioon.EndDate, conditioon.StartId, conditioon.EndId, Helper.CompanyKind.Customer);

                if (list == null || list.Count<=0)
                    throw new Helper.InvalidValueException();

                this.bindingSource1.DataSource = list;

                this.xrTableCellCustomId.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_COMPANYID);
                this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICETAX, "{0:0}");
                this.xrTableCellZre.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEZRE, "{0:0}");
                this.xrTableCellZS.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEZSE, "{0:0}");
                //this.xrTableCustomName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1, "{0:0}");
                this.xrTableCellYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_YISHOU, "{0:0}");
                this.xrTableCellYingShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEOWED, "{0:0}");

                this.xrTableCellTotalTax.Summary.FormatString = "{0:0}";
                this.xrTableCellTotalYingShou.Summary.FormatString = "{0:0}";
                this.xrTableCellTotalYiShou.Summary.FormatString = "{0:0}";
                this.xrTableCellTotalZse.Summary.FormatString = "{0:0}";
                this.xrTableCellTotalZre.Summary.FormatString = "{0:0}";

                this.xrTableCellTotalTax.Summary.Func = SummaryFunc.Sum;
                this.xrTableCellTotalYingShou.Summary.Func = SummaryFunc.Sum;
                this.xrTableCellTotalYiShou.Summary.Func = SummaryFunc.Sum;
                this.xrTableCellTotalZse.Summary.Func = SummaryFunc.Sum;
                this.xrTableCellTotalZre.Summary.Func = SummaryFunc.Sum;

                this.xrTableCellTotalTax.Summary.IgnoreNullValues = true;
                this.xrTableCellTotalYingShou.Summary.IgnoreNullValues = true;
                this.xrTableCellTotalYiShou.Summary.IgnoreNullValues = true;
                this.xrTableCellTotalZse.Summary.IgnoreNullValues = true;
                this.xrTableCellTotalZre.Summary.IgnoreNullValues = true;

                this.xrTableCellTotalTax.Summary.Running = SummaryRunning.Report;
                this.xrTableCellTotalYingShou.Summary.Running = SummaryRunning.Report;
                this.xrTableCellTotalYiShou.Summary.Running = SummaryRunning.Report;
                this.xrTableCellTotalZse.Summary.Running = SummaryRunning.Report;
                this.xrTableCellTotalZre.Summary.Running = SummaryRunning.Report;

                this.xrTableCellTotalTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICETAX, "{0:0}");
                this.xrTableCellTotalYingShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEOWED, "{0:0}");
                this.xrTableCellTotalYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_YISHOU, "{0:0}");
                this.xrTableCellTotalZse.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEZSE, "{0:0}");
                this.xrTableCellTotalZre.DataBindings.Add("Text", this.DataSource, Model.InvoiceDetail01.PROPERTY_INVOICEZRE, "{0:0}");
            

        }

    }
}
