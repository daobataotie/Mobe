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

// �� �� ��: ��� ������             ���ʱ��:2009-5-27
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q04Form : BaseForm
    {
        protected BL.Query04Manager query04Manager = new Book.BL.Query04Manager();

        public Q04Form(Condition condition)
            : base(condition)
        {
            InitializeComponent();
        }


        //����
        private void Q04Form_Load(object sender, EventArgs e)
        {
            //this.invoiceXTBindingSource.DataSource = query04Manager.SelectDataTable(null, null);
        }

        #region ��д���෽��
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R04(this.invoiceXTBindingSource.DataSource as DataTable);
        }

        protected override void DoQuery()
        {
            ConditionC condition = this.condition as ConditionC;
            //this.invoiceXTBindingSource.DataSource = query04Manager.SelectDataTable(condition.Company, condition.ExpiringDate);
        }
        #endregion

        
        public static ConditionChooseForm GetConditionChooseForm()
        {
            return new ConditionCChooseForm();
        }
    }
}