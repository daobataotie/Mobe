using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.Workhouselog
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-11-15
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseWorkHouseFrom : BasicData.BaseChooseForm
    {
        public ChooseWorkHouseFrom()
        {
            InitializeComponent();
            this.manager = new BL.WorkHouseManager();
        }

        //重写
        protected override Book.UI.Settings.BasicData.BaseEditForm  GetEditForm()

        {
            return new UI.Settings.ProduceManager.WorkHouse.EditForm();
        }
    }
}