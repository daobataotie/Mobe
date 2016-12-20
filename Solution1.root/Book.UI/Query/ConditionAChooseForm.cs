using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Book.UI.Query
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  够波涛             完成时间:2009-4-6
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionAChooseForm : ConditionChooseForm
    {
        #region Data
        private ConditionA conditionA;

        #endregion

        public ConditionAChooseForm()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.ConditionAChooseForm_Load);
        }

        #region  初始化时间虚方法
        protected virtual void Init()
        {
            this.dateEditStartDate.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditEndDate.DateTime = DateTime.Now.Date;
        }
        private void ConditionAChooseForm_Load(object sender, EventArgs e)
        {
            Init();
        }
        #endregion

        /// <summary>
        /// 重写父类方法
        /// </summary>
        protected override void OnOK()
        {
            if (this.conditionA == null)
                this.conditionA = new ConditionA();

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditStartDate.DateTime, new DateTime()))
            {
                this.conditionA.StartDate = global::Helper.DateTimeParse.NullDate;
            }

            else
            {
                this.conditionA.StartDate = this.dateEditStartDate.DateTime;
            }


            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditEndDate.DateTime, new DateTime()))
            {
                this.conditionA.EndDate = global::Helper.DateTimeParse.EndDate;
            }

            else
            {
                this.conditionA.EndDate = this.dateEditEndDate.DateTime;
            }
            OnOK1();

        }

        protected virtual void OnOK1()
        {
           
        }

        public override Condition Condition
        {
            get
            {
                return this.conditionA;
            }
            set
            {
                this.conditionA = value as ConditionA;
            }
        }

        private void dateEditEndDate_EditValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}