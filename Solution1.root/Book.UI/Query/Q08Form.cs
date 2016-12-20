using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Invoices;

namespace Book.UI.Query
{
 /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  �����w�Yܛ�����޹�˾
//                     ������� �����ؾ�

// �� �� ��: ������             ���ʱ��:2009-5-27
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
// �޸�ԭ��
// �� �� ��:                          �޸�ʱ��:
//----------------------------------------------------------------*/
    public partial class Q08Form : BaseForm
    {
        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

        //����
        public Q08Form(Condition condition)
            : base(condition)
        {
            InitializeComponent();
        }

        #region ��д���෽��
        private void Q05Form_Load(object sender, EventArgs e)
        {
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R08(this.bindingSource1.DataSource as System.Data.DataTable);
        }

        protected override void DoQuery()
        {
            ConditionB condition = this.condition as ConditionB;
            //this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable(condition.Date1, condition.Date2, condition.Company, condition.Employee, condition.Depot, global::Helper.InvoiceStatus.Normal, "Q08");
        }
        #endregion

        public static ConditionChooseForm GetConditionChooseForm()
        {
            return new ConditionBChooseForm(global::Helper.CompanyKind.Supplier);
        }
    }
}