using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.QualityTestPlan
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChooseQualityTestPlanForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-05
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseQualityTestPlanForm :BaseChooseForm 
    {
        public ChooseQualityTestPlanForm()
        {
            InitializeComponent();
            this.manager = new BL.QualityTestPlanManager(); 
        }
        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.QualityTestPlan.EditForm();
        }
    }
}
