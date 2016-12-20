using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Settings.BasicData.PayMethods
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：ListForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-28
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class ListForm : Settings.BasicData.BaseListForm
    {  

        public ListForm()
        {
            InitializeComponent();
            this.manager = new BL.PayMethodManager();
        }  

        #region Override

        protected override BaseEditForm GetEditForm()
        {
            return new EditForm();
        }

        protected override BaseEditForm GetEditForm(object[] args)
        {
            Type type = typeof(EditForm);
            return (EditForm)type.Assembly.CreateInstance(type.FullName, false, System.Reflection.BindingFlags.CreateInstance, null, args, null, null);
        }

        #endregion
    }
}