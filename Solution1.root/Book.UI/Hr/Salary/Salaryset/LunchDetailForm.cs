using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Book.UI.Hr.Salary.Salaryset
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军              完成时间:2009-11-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class LunchDetailForm : DevExpress.XtraEditors.XtraForm
    {

        //餐费管理
        BL.LunchDetailManager lunchManager = new Book.BL.LunchDetailManager();

        private MonthSalaryClass _ms;

        public LunchDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 三个参数的构造函数
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public LunchDetailForm(MonthSalaryClass ms)
            : this()
        {
            this._ms = ms;
        }

        /// <summary>
        /// 加载指定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LunchDetailForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Top -= 80;
            this.bindingSource1.DataSource = this.lunchManager.selectByempAndDate(this._ms.mEmployeeId, this._ms.mIdentifyDate.Date.Year, this._ms.mIdentifyDate.Month);
        }
    }
}