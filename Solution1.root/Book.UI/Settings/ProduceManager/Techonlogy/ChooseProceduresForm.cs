using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Settings.ProduceManager.Techonlogy
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾           完成时间:2009-11-1
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ChooseProceduresForm : Settings.BasicData.BaseChooseForm
    {
        protected BL.ProceduresManager proceduresManager = new Book.BL.ProceduresManager();

        //加载，初始化
        public ChooseProceduresForm()
        {
            InitializeComponent();
            this.manager = new BL.ProceduresManager();
        }

        #region 重写父类方法
        protected override void LoadData()
        {
            BL.WorkHouseManager managera = new BL.WorkHouseManager();
            this.bindingSourceWorkHouse.DataSource = managera.Select();
        }
   
        protected override Book.UI.Settings.BasicData.BaseEditForm  GetEditForm()

        {
            return new UI.Settings.ProduceManager.Techonlogy.ProceduresEditForm();
        }

        #endregion 


        /// <summary>
        /// 数据源改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bindingSourceWorkHouse_CurrentChanged_1(object sender, EventArgs e)
        {
            Model.WorkHouse workHouse = this.bindingSourceWorkHouse.Current as Model.WorkHouse;
            if (workHouse == null)
                return;
            this.bindingSource1.DataSource = this.proceduresManager.Select(workHouse.WorkHouseId);
        }
    }
}