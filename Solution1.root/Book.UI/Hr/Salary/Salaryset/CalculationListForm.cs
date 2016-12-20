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

// 编 码 人: 马艳军              完成时间:2009-10-25
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class CalculationListForm : DevExpress.XtraEditors.XtraForm
    {
        private BL.HrDailyEmployeeAttendInfoManager _hrDailyManger = new BL.HrDailyEmployeeAttendInfoManager();

        private MonthSalaryClass _ms;

        public CalculationListForm()
        {
            InitializeComponent();
        }

        public CalculationListForm(MonthSalaryClass ms)
            : this()
        {
            this._ms = ms;
        }

        private void CalculationListForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            InitDailyInfo();
            InitControlValue();
        }

        //根据当前员工和日期进行查询
        protected void InitDailyInfo()
        {
            DataSet _dailyInfo = this._hrDailyManger.SelectDailyInfoByEmployee(this._ms.mEmployeeId, this._ms.mIdentifyDate, string.Empty);
            this.bindingSourceDailyInfo.DataSource = _dailyInfo.Tables[0];
        }

        //为各个控件赋值
        protected void InitControlValue()
        {
            this.txtLateCount.Text = this._ms.mLateCount.ToString();                //迟到次数
            this.txtLateMinute.Text = this._ms.mLateInMinute.ToString();            //迟到总时间(分)
            this.txtBusinessPay.EditValue = this._ms.mSpecialBonus;      //班别津贴
            this.txtDayCount.Text = this._ms.mDaysFactor.ToString();                //日基数
            this.txtMonthCount.Text = this._ms.mMonthFactor.ToString();             //月基数
            this.txtSpecialHoliday.EditValue = this._ms.mTotalHoliday;   //该月列假总数
            this.txtDaySalary.EditValue = this._ms.mDailyPay;            //日薪
            this.txtMonthSalary.EditValue = this._ms.mMonthlyPay;        //月薪
            this.txtLeaveCount.EditValue = this._ms.mLeaveCount;         //该月请假总数
            this.txtTruancy.Text = this._ms.mAbsentCount.ToString();                //旷职总数
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "LateInMinute":
                case "BusinessHours":
                case "DayFactor":
                case "MonthFactor":
                case "gridColumn1":
                    try
                    {
                        if (int.Parse(e.DisplayText) == 0)
                            e.DisplayText = string.Empty;
                        break;
                    }
                    catch
                    {
                        break;
                    }
            }
        }
    }
}