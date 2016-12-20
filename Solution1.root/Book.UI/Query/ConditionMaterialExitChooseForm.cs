using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾             完成时间:2009-4-27
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionMaterialExitChooseForm :ConditionChooseForm
    {
        //Q51  生產退料單
        private ConditionMaterialExit condition;
        public ConditionMaterialExitChooseForm()
        {
            InitializeComponent();
        }

        #region 重写父类方法
        public override Condition Condition
        {
            get
            {
                return this.condition;
            }
            set
            {
                this.condition = value as ConditionMaterialExit;
            }
        }
        protected override void OnOK()
        {
            if (this.condition == null)
                this.condition = new ConditionMaterialExit();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                this.condition.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.condition.StartDate = this.dateEdit1.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit2.DateTime, new DateTime()))
            {
                this.condition.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.condition.EndDate = this.dateEdit2.DateTime;
            }
            if (this.startWorkHouse.EditValue!=null)
            this.condition.WorkHouse =new BL.WorkHouseManager().Get(this.startWorkHouse.EditValue.ToString());

        }
        #endregion

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionMaterialExitChooseForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = new BL.WorkHouseManager().Select();
        }
    }
}