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
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾 够波涛             完成时间:2009-5-27
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q04Form : BaseForm
    {
        protected BL.Query04Manager query04Manager = new Book.BL.Query04Manager();

        public Q04Form(Condition condition)
            : base(condition)
        {
            InitializeComponent();
        }


        //加载
        private void Q04Form_Load(object sender, EventArgs e)
        {
            //this.invoiceXTBindingSource.DataSource = query04Manager.SelectDataTable(null, null);
        }

        #region 重写父类方法
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