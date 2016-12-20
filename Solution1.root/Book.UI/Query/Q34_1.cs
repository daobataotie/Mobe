using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ���              ���ʱ��:2009-6-19
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q34_1 : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
        private ConditionF condition;

        public ConditionF Condition
        {
            get { return condition; }
            set { condition = value; }
        }


        /// <summary>
        /// �޲ι��죬��ʼ��
        /// </summary>
        public Q34_1()
        {
            InitializeComponent();

            this.xrTableCelInvoiceCZMoney.DataBindings.Add("Text", this.DataSource, Model.XP1.PROPERTY_XP1MONEY, "{0:0}");
            this.xrTableCelInvoiceDate.DataBindings.Add("Text", this.DataSource, "InvoiceDate", "{0:yyyy-MM-dd}");
            this.xrTableCelInvoiceId.DataBindings.Add("Text", this.DataSource, "InvoiceCGId");

            this.xrTableCelTotalInvoiceCZMoney.Summary.FormatString = "{0:0}";

            this.xrTableCelTotalInvoiceCZMoney.Summary.Func = SummaryFunc.Sum;

            this.xrTableCelTotalInvoiceCZMoney.Summary.IgnoreNullValues = true;

            this.xrTableCelTotalInvoiceCZMoney.Summary.Running = SummaryRunning.Report;

            this.xrTableCelTotalInvoiceCZMoney.DataBindings.Add("Text", this.DataSource, Model.XP1.PROPERTY_XP1MONEY, "{0:0}");

        }

        private void Q33_1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable(this.condition.StartDate, this.condition.EndDate, this.Company);
        }

    }
}
