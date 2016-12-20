using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.Customs
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶一覽設置
   // 文 件 名：ListForm
   // 编 码 人: 马艳军                   完成时间:2009-10-12
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : Form
    {
        #region 变量对象定义
        private BL.CustomerManager _customerManager = new Book.BL.CustomerManager();
        #endregion

        #region 構造函數
        public ListForm()
        {
            InitializeComponent();
        }
        #endregion 

        private void ListForm_Load(object sender, EventArgs e)
        {
            this.bindingSourcecustomer.DataSource = _customerManager.Select();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            EditForm.cstomer = bindingSourcecustomer.Current as Model.Customer;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}