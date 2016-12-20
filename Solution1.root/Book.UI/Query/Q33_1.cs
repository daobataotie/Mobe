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

// �� �� ��:  ������             ���ʱ��:2009-6-18
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
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


        //�޲ι��죬��ʼ��
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
