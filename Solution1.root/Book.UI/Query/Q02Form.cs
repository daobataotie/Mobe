using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Invoices;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ��� ������             ���ʱ��:2009-5-25
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q02Form : BaseForm
    {
        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

        public Q02Form()
        {
            InitializeComponent();
        }

        #region ��д���෽��
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R02();
        }

        protected override void DoQuery()
        {
            this.stockBindingSource.DataSource = this.miscDataManager.SelectDataTable("Q02");
        }
        #endregion
    }
}