using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.MaterialType
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军            完成时间:2009-10-27
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseMaterialTypeForm :Settings.BasicData.BaseChooseForm
    {
        public ChooseMaterialTypeForm()
        {
            InitializeComponent();
            this.manager = new BL.MaterialTypeManager();
        }

        //重写方法
        protected override Book.UI.Settings.BasicData.BaseEditForm1  GetEditForm1()

        {
            return new EditForm();
        }


        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == this.gridColumn4.Name)
            { 
             
                //if(e.DisplayText=="0") 
                //e.DisplayText=Properties.Resources.No;
            
            }
        }       

       
    }
}