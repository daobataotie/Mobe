using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ��� ������             ���ʱ��:2009-5-26
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q03Form : BaseForm
    {
      

        public Q03Form()
        {
            InitializeComponent();
        }

        #region ��д���෽��
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R03();
        }

        protected override void DoQuery()
        {
            //this.companyBindingSource.DataSource = this.companyManager.Select();
        }
        #endregion

    }
}