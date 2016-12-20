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

// 编 码 人:  够波涛             完成时间:2009-6-3
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q13Form : BaseForm
    {
        protected BL.MiscDataManager miscDataManager = new Book.BL.MiscDataManager();

        //构造
        public Q13Form(Condition condition)
            : base(condition)
        {
            InitializeComponent();
        }

        //重写
        protected override void DoQuery()
        {
            ConditionB condition = this.condition as ConditionB;
            //this.bindingSource1.DataSource = this.miscDataManager.SelectDataTable(condition.Date1, condition.Date2, condition.Company, condition.Employee, global::Helper.InvoiceStatus.Normal, "Q13");
        }

        public static ConditionChooseForm GetConditionChooseForm()
        {
            return new ConditionBChooseForm(global::Helper.CompanyKind.Supplier);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R13(this.bindingSource1.DataSource as System.Data.DataTable);
        }
    }
}