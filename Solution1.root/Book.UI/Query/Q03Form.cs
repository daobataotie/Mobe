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

// 编 码 人: 裴盾 够波涛             完成时间:2009-5-26
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class Q03Form : BaseForm
    {
      

        public Q03Form()
        {
            InitializeComponent();
        }

        #region 重写父类方法
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