using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.BasicData.Bank
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 选择银行信息
   // 文 件 名：ChooseBankForm
   // 编 码 人: 马艳军                   完成时间:2009-09-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ChooseBankForm : BaseChooseForm
    { 
        //無慘構造函數          -----myj
        public ChooseBankForm()
        {
            InitializeComponent();
            this.manager = new BL.BankManager();
        }
        #region Construcotrs  
        protected override BaseEditForm GetEditForm()
        {
            return  new UI.Settings.BasicData.Bank.EditForm();
        }
        #endregion
    }
}