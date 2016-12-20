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

// 编 码 人: 裴盾              完成时间:2009-6-3
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q12Form : BaseForm
    {
        protected BL.AccountManager accountManager = new Book.BL.AccountManager();

        public Q12Form()
        {
            InitializeComponent();
        }

        #region 重写父类方法
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R12();
        }

        protected override void DoQuery()
        {
            this.accountBindingSource.DataSource = this.accountManager.Select();
        }
        #endregion
    }
}