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

// �� �� ��: ���           ���ʱ��:2009-6-28
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q45 : BaseReport
    {
        public Q45()
        {
            InitializeComponent();
            this.xrLabelDateRange.Text = string.Format(Properties.Resources.EndDate, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

    }
}
