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

// �� �� ��: ���              ���ʱ��:2009-6-16
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q25 : BaseReport
    {
        private BL.InvoiceZZManager invoiceManager = new Book.BL.InvoiceZZManager();

        /// <summary>
        /// һ�ι��죬��ʼ��
        /// </summary>
        /// <param name="condition"></param>
        public Q25(ConditionA condition)
        {
            InitializeComponent();
            this.xrLabelReportName.Text = Properties.Resources.ZZDetail;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange, condition.StartDate.ToString("yyyy-MM-dd"), condition.EndDate.ToString("yyyy-MM-dd"));
            
            System.Collections.Generic.IList<Model.InvoiceZZ> list = this.invoiceManager.Select(condition.StartDate,condition.EndDate);
            
            if (list == null || list.Count <= 0)
                throw new Helper.InvalidValueException();

            this.bindingSource1.DataSource = list;
            this.xrSubreport1.ReportSource = new Q25_1();
            this.xrSubreport2.ReportSource = new Q25_2();

        }

        //�����ӡǰ����
        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q25_1 reportProducts = this.xrSubreport1.ReportSource as Q25_1;
            reportProducts.Invoice = this.GetCurrentRow() as Model.InvoiceZZ;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Q25_2 reportProducts = this.xrSubreport2.ReportSource as Q25_2;
            reportProducts.Invoice = this.GetCurrentRow() as Model.InvoiceZZ;
        }
    }
}
