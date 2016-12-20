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

// 编 码 人: 裴盾              完成时间:2009-4-18
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class ConditionGChooseForm : ConditionAChooseForm
    {
        //员工管理
        private BL.EmployeeManager emplpoyeeManager = new Book.BL.EmployeeManager();

        private ConditionG condition;

        public ConditionGChooseForm()
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
                this.condition = value as ConditionG;
            }
        }
        protected override void OnOK()
        {
            if (condition == null)
                condition = new ConditionG();
            condition.StartDate = this.dateEditStartDate.DateTime;
            condition.EndDate = this.dateEditEndDate.DateTime;
            condition.EmployeeStartId = this.comboBoxEditEmployeeStartId.Text.Split(new char[] { ' ' })[0];
            condition.EmployeeEndId = this.comboBoxEditEmployeeEndId.Text.Split(new char[] { ' ' })[0];

        }
        #endregion


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConditionGChooseForm_Load(object sender, EventArgs e)
        {
            System.Collections.Generic.IList<Model.Employee> list = this.emplpoyeeManager.Select();
            foreach (Model.Employee emp in list)    
            {
                this.comboBoxEditEmployeeEndId.Properties.Items.Add(string.Format("{0} {1}", emp.EmployeeId, emp.EmployeeName));
                this.comboBoxEditEmployeeStartId.Properties.Items.Add(string.Format("{0} {1}", emp.EmployeeId, emp.EmployeeName));
            }
        }
 
    }
}