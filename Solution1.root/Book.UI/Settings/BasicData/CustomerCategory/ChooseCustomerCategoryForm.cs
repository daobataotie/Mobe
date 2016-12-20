using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.CustomerCategory
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 選擇客戶分類
   // 文 件 名：ChooseCustomerCategoryForm
   // 编 码 人: 马艳军                   完成时间:2009-10-08
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseCustomerCategoryForm:BaseChooseForm
    {
        #region myj---無慘構造函數
        public ChooseCustomerCategoryForm()
        {
            InitializeComponent();
            this.manager = new BL.CustomerCategoryManager();
        }
        #endregion 

        #region myj---重載父類的GetEditForm 方法
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
        #endregion
    }
}