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

// 编 码 人: 裴盾              完成时间:2009-6-10
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q27 : BaseReport
    {
        private BL.InvoiceXSManager invoiceManager = new Book.BL.InvoiceXSManager();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="condition"></param>
        public Q27(ConditionF condition)
        {
            InitializeComponent();

            this.xrLabelReportName.Text = Properties.Resources.YSZKJYB;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));

            System.Collections.Generic.IList<Model.InvoiceXS> list = this.invoiceManager.Select(condition.StartDate, condition.EndDate, condition.StartId, condition.EndId);

            if (list == null || list.Count <= 0)
            {
                throw new global::Helper.InvalidValueException();
            }

            this.bindingSource1.DataSource = list;

            this.xrLabelCompanyId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PRO_CustomerId);
            //this.xrLabelCompanyName.DataBindings.Add("Text", this.DataSource, "Company." + Model.Company.PROPERTY_COMPANYNAME1);


            this.xrTableCellKind.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_KIND);
            //this.xrTableCellFPHM.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEFPBH);
            this.xrTableCellInvoiceDate.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEDATE, "{0:yyyy-MM-dd}");
            //this.xrTableCellInvoiceHeJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            this.xrTableCellInvoiceId.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEID);
            //this.xrTableCellInvoiceZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrTableCellTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            //this.xrTableCellWeiFu.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEOWED, "{0:0}");
            this.xrTableCellYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");
            //this.xrTableCellZre.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZRE, "{0:0}");


            this.xrLabelBenQiHeji.Summary.FormatString = "{0:0}";
            this.xrLabelBenQiZongJi.Summary.FormatString = "{0:0}";
            this.xrLabelZRE.Summary.FormatString = "{0:0}";
            this.xrLabelZSE.Summary.FormatString = "{0:0}";
            this.xrLabelTax.Summary.FormatString = "{0:0}";
            this.xrLabelYiShou.Summary.FormatString = "{0:0}";

            this.xrLabelZSE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelZRE.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBenQiZongJi.Summary.Func = SummaryFunc.Sum;
            this.xrLabelBenQiHeji.Summary.Func = SummaryFunc.Sum;
            this.xrLabelTax.Summary.Func = SummaryFunc.Sum;
            this.xrLabelYiShou.Summary.Func = SummaryFunc.Sum;

            this.xrLabelBenQiHeji.Summary.IgnoreNullValues = true;
            this.xrLabelBenQiZongJi.Summary.IgnoreNullValues = true;
            this.xrLabelZRE.Summary.IgnoreNullValues = true;
            this.xrLabelZSE.Summary.IgnoreNullValues = true;
            this.xrLabelTax.Summary.IgnoreNullValues = true;
            this.xrLabelYiShou.Summary.IgnoreNullValues = true;

            this.xrLabelZSE.Summary.Running = SummaryRunning.Group;
            this.xrLabelZRE.Summary.Running = SummaryRunning.Group;
            this.xrLabelBenQiZongJi.Summary.Running = SummaryRunning.Group;
            this.xrLabelBenQiHeji.Summary.Running = SummaryRunning.Group;
            this.xrLabelTax.Summary.Running = SummaryRunning.Group;
            this.xrLabelYiShou.Summary.Running = SummaryRunning.Group;

            //this.xrLabelBenQiHeji.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEHEJI, "{0:0}");
            //this.xrLabelBenQiZongJi.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZONGJI, "{0:0}");
            //this.xrLabelZRE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZRE, "{0:0}");
            //this.xrLabelZSE.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICEZSE, "{0:0}");
            //this.xrLabelTax.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_INVOICETAX, "{0:0}");
            this.xrLabelYiShou.DataBindings.Add("Text", this.DataSource, Model.InvoiceXS.PROPERTY_YISHOU, "{0:0}");

            //this.GroupHeader1.GroupFields.Add(new GroupField("Company." + Model.Company.PROPERTY_COMPANYID));
        }
    }
}
