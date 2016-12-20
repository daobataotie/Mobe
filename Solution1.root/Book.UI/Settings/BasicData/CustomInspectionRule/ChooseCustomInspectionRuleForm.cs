using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.CustomInspectionRule
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 选择自定义抽检规则
   // 文 件 名：ChooseCustomInspectionRuleForm
   // 编 码 人: 马艳军                   完成时间:2009-10-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/

    public partial class ChooseCustomInspectionRuleForm:BaseChooseForm
    {
        #region 無慘構造函數
        public ChooseCustomInspectionRuleForm()
        {
            InitializeComponent();
            this.manager = new  BL.CustomInspectionRuleManager();
        }
        #endregion

        #region 重載父類的方法
        protected override UI.Settings.BasicData.BaseEditForm GetEditForm()
        {
            return new UI.Settings.BasicData.CustomInspectionRule.EditForm();
        }
        #endregion
    }
}
 