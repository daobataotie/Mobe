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

// �� �� ��: ���              ���ʱ��:2009-6-18
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q33 : BaseReport
    {
        //private BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        private ConditionF condition;


        //����
        public Q33(ConditionF condition)
        {
            InitializeComponent();
            this.condition = condition;
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.DateRange,condition.StartDate.ToString("yyyy-MM-dd"),condition.EndDate.ToString("yyyy-MM-dd"));
            this.xrLabelReportName.Text = Properties.Resources.SKCXRBB;


        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Q33_1 reportProducts = this.xrSubreport1.ReportSource as Q33_1;
            //reportProducts.Company = this.GetCurrentRow() as Model.Company;
            //reportProducts.Condition = this.condition;
        }
    }
}
