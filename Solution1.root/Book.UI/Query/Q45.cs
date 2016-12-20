using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾           完成时间:2009-6-28
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
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
