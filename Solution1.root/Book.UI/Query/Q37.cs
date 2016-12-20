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

// 编 码 人: 裴盾             完成时间:2009-6-21
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q37 : BaseReport
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();


        /// <summary>
        /// 构造，初始化
        /// </summary>
        /// <param name="condition"></param>
        public Q37(ConditionJ condition)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = Properties.Resources.HPLRDetail;

            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));
            this.xrLabelProductIdRenage.Text = string.Format(Properties.Resources.ProductIdRange, condition.StartId, condition.EndId);

            System.Data.DataTable list = miscDataManager.SelectDataTable(condition.StartDate, condition.EndDate, condition.StartId, condition.EndId);

            if (list == null || list.Rows.Count <= 0)
                throw new Helper.InvalidValueException();

            this.bindingSource1.DataSource = list;

            this.xrTableCellAvgCost.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailCostPrice","{0:0}");
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, "Invoicedate", "{0:yyyy-MM-dd}");
            this.xrTableCellMaoLi.DataBindings.Add("Text",this.DataSource,"MaoLi","{0:0}");
            this.xrTableCellMaoLiLv.DataBindings.Add("Text",this.DataSource,"MaoLiLV","{0:P}");
            this.xrTableCellMoney.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailMoney1","{0:0}");
            this.xrTableCellProductId.DataBindings.Add("Text",this.DataSource,"ProductId");
            this.xrTableCellProductName.DataBindings.Add("Text",this.DataSource,"ProductName");
            this.xrTableCellProductPrice.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailPrice","{0:0}");
            this.xrTableCellProductUnit.DataBindings.Add("Text", this.DataSource, "MainUnit." + Model.ProductUnit.PROPERTY_CNNAME);
            this.xrTableCellQuantity.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailQuantity","{0:0}");

            this.xrLabelTotalMoney.Summary.FormatString = "{0:0}";
            this.xrLabelTotalQuantity.Summary.FormatString = "{0:0}";
            this.xrLabelTotalMaoLi.Summary.FormatString = "{0:0}";
            this.xrLabelInvoiceTotalMoney.Summary.FormatString = "{0:0}";
            this.xrLabelInvoiceTotalQuantity.Summary.FormatString = "{0:0}";
            this.xrLabelInvoiceTotalMaoLi.Summary.FormatString = "{0:0}";
            
            this.xrLabelTotalMoney.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalQuantity.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTotalMaoLi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelInvoiceTotalMoney.Summary.Func = SummaryFunc.Sum;
            this.xrLabelInvoiceTotalQuantity.Summary.Func = SummaryFunc.Sum;
            this.xrLabelInvoiceTotalMaoLi.Summary.Func = SummaryFunc.Sum;

            this.xrLabelTotalMoney.Summary.IgnoreNullValues = true;
            this.xrLabelTotalQuantity.Summary.IgnoreNullValues = true;
            this.xrLabelTotalMaoLi.Summary.IgnoreNullValues = true;
            this.xrLabelInvoiceTotalMoney.Summary.IgnoreNullValues = true;
            this.xrLabelInvoiceTotalQuantity.Summary.IgnoreNullValues = true;
            this.xrLabelInvoiceTotalMaoLi.Summary.IgnoreNullValues = true;

            this.xrLabelTotalMoney.Summary.Running = SummaryRunning.Group;
            this.xrLabelTotalQuantity.Summary.Running = SummaryRunning.Group;
            this.xrLabelTotalMaoLi.Summary.Running = SummaryRunning.Group;
            this.xrLabelInvoiceTotalMoney.Summary.Running = SummaryRunning.Report;
            this.xrLabelInvoiceTotalQuantity.Summary.Running = SummaryRunning.Report;
            this.xrLabelInvoiceTotalMaoLi.Summary.Running = SummaryRunning.Report;

            this.xrLabelTotalMoney.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailMoney1","{0:0}");
            this.xrLabelTotalQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXSDetailQuantity", "{0:0}");
            this.xrLabelTotalMaoLi.DataBindings.Add("Text",this.DataSource,"MaoLi","{0:0}");
            this.xrLabelInvoiceTotalMoney.DataBindings.Add("Text",this.DataSource,"InvoiceXSDetailMoney1","{0:0}");
            this.xrLabelInvoiceTotalQuantity.DataBindings.Add("Text", this.DataSource, "InvoiceXSDetailQuantity", "{0:0}");
            this.xrLabelInvoiceTotalMaoLi.DataBindings.Add("Text",this.DataSource,"MaoLi","{0:0}");

            this.GroupHeader1.GroupFields.Add(new GroupField("ProductId"));
        }
    }
}