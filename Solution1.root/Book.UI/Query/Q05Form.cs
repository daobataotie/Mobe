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
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾              完成时间:2009-5-27
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q05Form : BaseForm
    {
        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();
        protected BL.InvoiceXSDetailManager XsDetailManager = new Book.BL.InvoiceXSDetailManager();

        public Q05Form(Condition condition)
            : base(condition)
        {
            InitializeComponent();
        }

        #region 重写父类方法
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R05(this.bindingSource1.DataSource as  IList<Model.InvoiceXSDetail>);
        }

        protected override void DoQuery()
        {
            ConditionB condition = this.condition as ConditionB;
            //this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable(condition.Date1, condition.Date2, condition.Company, condition.Employee, condition.Depot, global::Helper.InvoiceStatus.Normal, "Q05");
            this.bindingSource1.DataSource = XsDetailManager.Select(condition.Date1, condition.Date2,condition.Employee,condition.Customer,condition.Depot);
        }
        #endregion

        public static ConditionChooseForm GetConditionChooseForm()
        {
            return new ConditionBChooseForm(global::Helper.CompanyKind.Customer);
        }
    }
}