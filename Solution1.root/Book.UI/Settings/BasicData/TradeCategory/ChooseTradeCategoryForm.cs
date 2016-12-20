﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.TradeCategory
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ChooseTradeCategoryForm
   // 编 码 人: 茍波濤                   完成时间:2009-11-07
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseTradeCategoryForm : BaseChooseForm
    {
        public ChooseTradeCategoryForm()
        {
            InitializeComponent();
            this.manager = new BL.TradeCategoryManager();
        }

        #region gbt---重載基類方法
        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }
        #endregion

    }
}