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

// 编 码 人:  够波涛             完成时间:2009-6-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q33_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

  
        private ConditionF condition;

        public ConditionF Condition
        {
            get { return condition; }
            set { condition = value; }
        }


        //无参构造，初始化
        public Q33_1()
        {
            InitializeComponent();

            this.xrTableCelInvoiceCZMoney.DataBindings.Add("Text", this.DataSource, Model.XR1.PROPERTY_XR1MONEY, "{0:0}");
            this.xrTableCelInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.xrTableCelInvoiceId.DataBindings.Add("Text", this.DataSource, "InvoiceXSId");

            this.xrTableCelTotalInvoiceCZMoney.Summary.FormatString = "{0:0}";

            this.xrTableCelTotalInvoiceCZMoney.Summary.Func = SummaryFunc.Sum;

            this.xrTableCelTotalInvoiceCZMoney.Summary.IgnoreNullValues = true;

            this.xrTableCelTotalInvoiceCZMoney.Summary.Running = SummaryRunning.Report;

            this.xrTableCelTotalInvoiceCZMoney.DataBindings.Add("Text", this.DataSource, Model.XR1.PROPERTY_XR1MONEY, "{0:0}");

        }

        private void Q33_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable(this.condition.StartDate, this.condition.EndDate, this.Company);
        }

    }
}
