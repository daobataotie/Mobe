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
// Copyright (C) 2008 - 2010 咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 马艳军             完成时间:2009-11-11
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class WordBonusList : DevExpress.XtraEditors.XtraForm
    {

        private MonthSalaryClass _ms;



        public WordBonusList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 三个参数的构造函数
        /// </summary>
        /// <param name="Employees"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public WordBonusList(MonthSalaryClass ms)
            : this()
        {
            this._ms = ms;
        }

        /// <summary>
        /// 窗体加载时初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WordBonusList_Load(object sender, EventArgs e)
        {
            this.textEditDutyPay.EditValue = this._ms.mDutyPay; //责任津贴
            this.textEditPostPay.EditValue = this._ms.mPostPay;// 职务津贴
            this.textEditDailyPay.EditValue = this._ms.mDailyPay;  //日薪
            this.textEditDutyPay1.EditValue = this._ms.mDutyPay; //责任津贴
            this.textEditPostPay1.EditValue = this._ms.mPostPay;// 职务津贴
            this.textEditDayFactor.EditValue = this._ms.mDaysFactor; //日基数
            this.textEdittMonthFactor.EditValue = this._ms.mMonthFactor; //月基数
            this.textEditAttend.EditValue = this._ms.mAllAttendBonus; //全勤奖金
            this.textEditBusinessHourPay.EditValue = this._ms.mSpecialBonus;//班别津贴
            #region @old Code
            //decimal dayFactorSum = decimal.Zero;
            //decimal monthFactorSum = decimal.Zero;
            //decimal AllAttendBonus = decimal.Zero;
            //decimal DutyDateCount = decimal.Zero;//总出勤数
            //decimal PunishLeaveSum = decimal.Zero;

            //monthFactorSum = this.attendManager.SelectDayMonthSum(this.years, this.months, this.Employee);
            //dayFactorSum = this.attendManager.SelectDayFactorSum(this.years, this.months, this.Employee);

            //this.textEditBusinessHourPay.EditValue = this.attendManager.SelectBusinessHourPaySum(this.years, this.months, this.Employee.EmployeeId);
            //DutyDateCount = this.attendManager.SelectDutyDateCount(this.years, this.months, this.Employee);


            //this.textEditDayFactor.EditValue = dayFactorSum;
            //this.textEdittMonthFactor.EditValue = monthFactorSum;

            //PunishLeaveSum = new Book.BL.LeaveManager().SelectPunishByMonthEmp(this.Employee, years, months);
            //if (DutyDateCount == DateTime.DaysInMonth(this.years, this.months))
            //{
            //    //有做滿一個月：日薪三天
            //    if (monthFactorSum == DateTime.DaysInMonth(this.years, this.months))
            //    {

            //        //沒缺刷卡記錄
            //        if (PunishLeaveSum == 0)
            //        {

            //            AllAttendBonus = this.Employee.DailyPay.Value * 3;
            //        }
            //        //請倒扣款假小於等於一天：日薪二天   
            //        else if (PunishLeaveSum <= 1)
            //        {
            //            AllAttendBonus = this.Employee.DailyPay.Value * 2;
            //        }
            //    }
            //    //有缺刷卡記錄                  
            //    else
            //    {
            //        AllAttendBonus = 0;
            //    }

            //}
            ////未滿一個月（因到職或離職）
            //else
            //{
            //    AllAttendBonus = 0;
            //}

            //this.textEditAttend.EditValue = AllAttendBonus;
            #endregion
        }
    }
}