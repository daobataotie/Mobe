using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.Options
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BaseOptionsPage : DevExpress.XtraEditors.XtraUserControl
    {
        public BaseOptionsPage()
        {
            InitializeComponent();
        }

        public virtual void DoSave()
        {

        }

        public virtual void DoLoad()
        {

        }
    }
}
