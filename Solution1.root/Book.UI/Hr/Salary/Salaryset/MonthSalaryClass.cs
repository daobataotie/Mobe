using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book.UI.Hr.Salary.Salaryset
{
    public class MonthSalaryClass
    {
        ///<summary>
        ///Id
        ///</summary>
        private string _memployeeid;
        public string mEmployeeId
        {
            get { return this._memployeeid; }
            set { this._memployeeid = value; }
        }
        ///<summary>
        ///該月總例假數
        ///</summary>
        private double _mTotalHoliday;
        ///<summary>
        ///該月總例假數
        ///</summary>
        public double mTotalHoliday
        {
            get { return this._mTotalHoliday; }
            set { this._mTotalHoliday = value; }
        }
        ///<summary>
        /// 曠工總數
        ///</summary>
        private double _mAbsentCount;
        ///<summary>
        /// 曠工總數
        ///</summary>
        public double mAbsentCount
        {
            get { return this._mAbsentCount; }
            set { this._mAbsentCount = value; }
        }
        ///<summary>
        /// 請假總數
        ///</summary>
        private double _mLeaveCount;
        ///<summary>
        /// 請假總數
        ///</summary>
        public double mLeaveCount
        {
            get { return this._mLeaveCount; }
            set { this._mLeaveCount = value; }
        }
        ///<summary>
        /// 倒扣款假總數
        ///</summary>
        private double _mPunishLeaveCount;
        ///<summary>
        /// 倒扣款假總數
        ///</summary>
        public double mPunishLeaveCount
        {
            get { return this._mPunishLeaveCount; }
            set { this._mPunishLeaveCount = value; }
        }
        ///<summary>
        /// 离职日期
        ///</summary>
        private DateTime? _mLeaveDate;
        ///<summary>
        /// 离职日期
        ///</summary>
        public DateTime? mLeaveDate
        {
            get { return this._mLeaveDate; }
            set { this._mLeaveDate = value; }
        }
        ///<summary>
        /// 考勤日期
        ///</summary>
        private DateTime _mIdentifyDate;
        ///<summary>
        /// 考勤日期
        ///</summary>
        public DateTime mIdentifyDate
        {
            get { return this._mIdentifyDate; }
            set { this._mIdentifyDate = value; }
        }
        ///<summary>
        /// 部门名称
        ///</summary>
        private string _mDepartmetName;
        ///<summary>
        /// 部门名称
        ///</summary>
        public string mDepartmetName
        {
            get { return this._mDepartmetName; }
            set { this._mDepartmetName = value; }
        }
        ///<summary>
        /// 员工姓名
        ///</summary>
        private string _mEmployeeName;
        ///<summary>
        /// 员工姓名
        ///</summary>
        public string mEmployeeName
        {
            get { return this._mEmployeeName; }
            set { this._mEmployeeName = value; }
        }
        ///<summary>
        /// IDNo
        ///</summary>
        private string _mIDNo;
        ///<summary>
        /// IDNo
        ///</summary>
        public string mIDNo
        {
            get { return this._mIDNo; }
            set { this._mIDNo = value; }
        }
        ///<summary>
        /// 日薪
        ///</summary>
        private double _DailyPay;
        ///<summary>
        /// 日薪
        ///</summary>
        public double mDailyPay
        {
            get { return this._DailyPay; }
            set { this._DailyPay = value; }
        }
        ///<summary>
        ///月薪
        ///</summary>
        private double _mMonthlyPay;
        ///<summary>
        ///月薪
        ///</summary>
        public double mMonthlyPay
        {
            get { return this._mMonthlyPay; }
            set { this._mMonthlyPay = value; }
        }
        ///<summary>
        /// 底薪
        ///</summary>
        private double _mBasePay;
        ///<summary>
        /// 底薪
        ///</summary>
        public double mBasePay
        {
            get
            {
                double tb_1 = Math.Truncate(this._mBasePay * 10) % 10;
                if (tb_1 >= 5)
                {
                    return Math.Truncate(this._mBasePay) + 1;
                }
                else
                {
                    return Math.Truncate(this._mBasePay);
                }
            }
            set { this._mBasePay = value; }
        }
        ///<summary>
        /// 職場津貼
        ///</summary>
        private double _mFieldPay;
        ///<summary>
        /// 職場津貼
        ///</summary>
        public double mFieldPay
        {
            get
            {
                double tb_1 = Math.Truncate(this._mFieldPay * 10) % 10;
                if (tb_1 >= 5)
                {
                    return Math.Truncate(this._mFieldPay) + 1;
                }
                else
                {
                    return Math.Truncate(this._mFieldPay);
                }
            }
            set { this._mFieldPay = value; }
        }
        ///<summary>
        /// 薪資單總計
        ///</summary>          
        public double SubTotal
        {
            get { return XiaoJI + JiaBan; }
        }
        ///<summary>
        /// 餐費
        ///</summary>
        private double _mLunchFee;
        ///<summary>
        /// 餐費
        ///</summary>
        public double mLunchFee
        {
            get { return this._mLunchFee; }
            set { this._mLunchFee = value; }
        }
        ///<summary>
        ///保險費
        ///</summary>    
        private double _mInsurance;
        ///<summary>
        ///保險費
        ///</summary>    
        public double mInsurance
        {
            get { return this._mInsurance; }
            set { this._mInsurance = value; }

        }
        ///<summary>
        /// 借支
        ///</summary>
        private double _mLoanFee;
        ///<summary>
        /// 借支
        ///</summary>
        public double mLoanFee
        {
            get { return this._mLoanFee; }
            set { this._mLoanFee = value; }
        }
        ///<summary>
        ///所得稅
        ///</summary>    
        private double _mTax;
        ///<summary>
        ///所得稅
        ///</summary>    
        public double mTax
        {
            get { return this._mTax; }
            set { this._mTax = value; }

        }
        ///<summary>
        ///薪資單實領金額
        ///</summary>
        public double mSalaryTotal
        {
            get { return SubTotal - mLunchFee - mInsurance - mLoanFee - mTax; }
        }
        ///<summary>
        /// 責任津貼
        ///</summary>
        private double _mDutyPay;
        ///<summary>
        /// 責任津貼
        ///</summary>
        public double mDutyPay
        {
            get { return this._mDutyPay; }
            set { this._mDutyPay = value; }
        }
        private double _mPostPay;
        ///<summary>
        /// 職務津貼
        ///</summary>    
        public double mPostPay
        {
            get { return this._mPostPay; }
            set { this._mPostPay = value; }
        }
        private double _mAllAttendBonus;
        ///<summary>
        /// 全勤獎金
        ///</summary>
        public double mAllAttendBonus
        {
            get { return this._mAllAttendBonus; }
            set { this._mAllAttendBonus = value; }
        }
        /// <summary>
        /// 中夜班津貼
        /// </summary>
        private double _mSpecialBonus;
        /// <summary>
        /// 中夜班津貼
        /// </summary>
        public double mSpecialBonus
        {
            get { return this._mSpecialBonus; }
            set { this._mSpecialBonus = value; }
        }
        ///<summary>
        /// 工作獎金
        ///</summary>
        private double _mWorkBonus;
        ///<summary>
        /// 工作獎金
        ///</summary>
        public double mWorkBonus
        {
            get { return this._mWorkBonus; }
            set { this._mWorkBonus = value; }
        }
        ///<summary>
        /// 績效獎金
        ///</summary>    
        private double _mEffectBonus;
        ///<summary>
        /// 績效獎金
        ///</summary>    
        public double mEffectBonus
        {
            get { return this._mEffectBonus; }
            set { this._mEffectBonus = value; }
        }
        ///<summary>
        /// 作業技術獎金
        ///</summary>
        private double _mTechBonus;
        ///<summary>
        /// 作業技術獎金
        ///</summary>
        public double mTechBonus
        {
            get { return this._mTechBonus; }
            set { this._mTechBonus = value; }
        }
        ///<summary>
        /// 績效係數
        ///</summary>    
        private double _mEffectFactor;
        ///<summary>
        /// 績效係數
        ///</summary>    
        public double mEffectFactor
        {
            get { return this._mEffectFactor; }
            set { this._mEffectFactor = value; }
        }
        ///<summary>
        ///平日加班（時數）
        ///</summary>
        private double _mGeneralOverTime;
        ///<summary>
        ///平日加班（時數）
        ///</summary>
        public double mGeneralOverTime
        {
            get { return this._mGeneralOverTime; }
            set { this._mGeneralOverTime = value; }
        }
        ///<summary>
        /// 假日加班（時數）
        ///</summary>    
        private double _mHolidayOverTime;
        ///<summary>
        /// 假日加班（時數）
        ///</summary>    
        public double mHolidayOverTime
        {
            get { return this._mHolidayOverTime; }
            set { this._mHolidayOverTime = value; }
        }
        ///<summary>
        /// 平日加班費
        ///</summary>
        private double _mGeneralOverTimeFee;
        ///<summary>
        /// 平日加班費
        ///</summary>
        public double mGeneralOverTimeFee
        {
            get { return this._mGeneralOverTimeFee; }
            set { this._mGeneralOverTimeFee = value; }
        }
        ///<summary>
        /// 假日加班費
        ///</summary>    
        private double _mHolidayOverTimeFee;
        ///<summary>
        /// 假日加班費
        ///</summary>    
        public double mHolidayOverTimeFee
        {
            get { return this._mHolidayOverTimeFee; }
            set { this._mHolidayOverTimeFee = value; }
        }
        ///<summary>
        /// 加班費
        ///</summary>      
        public double OverTimeFee
        {
            get { return mGeneralOverTimeFee + mHolidayOverTimeFee; }

        }
        ///<summary>
        /// 加班津貼
        ///</summary>    
        private double _mOverTimeBonus;
        ///<summary>
        /// 加班津貼
        ///</summary>    
        public double mOverTimeBonus
        {
            get { return this._mOverTimeBonus; }
            set { this._mOverTimeBonus = value; }
        }
        ///<summary>
        /// 其它補款
        ///</summary>    
        private double _mOtherPay;
        ///<summary>
        /// 其它補款
        ///</summary>    
        public double mOtherPay
        {
            get { return this._mOtherPay; }
            set { this._mOtherPay = value; }
        }
        ///<summary>
        /// 其它扣款
        ///</summary>
        private double _mOtherPunish;
        ///<summary>
        /// 其它扣款
        ///</summary>
        public double mOtherPunish
        {
            get { return this._mOtherPunish; }
            set { this._mOtherPunish = value; }
        }
        ///<summary>
        /// 遲到扣款
        ///</summary>    
        private double _mLatePunish;
        ///<summary>
        /// 遲到扣款
        ///</summary>    
        public double mLatePunish
        {
            get { return this._mLatePunish; }
            set { this._mLatePunish = value; }
        }
        ///<summary>
        /// 遲到次數
        ///</summary>
        private double _mLateCount;
        ///<summary>
        /// 遲到次數
        ///</summary>
        public double mLateCount
        {
            get { return this._mLateCount; }
            set { this._mLateCount = value; }
        }
        ///<summary>
        /// 總遲到（分）
        ///</summary>    
        private double _mTotalLateInMinute;
        ///<summary>
        /// 總遲到（分）
        ///</summary>
        public double mTotalLateInMinute
        {
            get { return this._mTotalLateInMinute; }
            set { this._mTotalLateInMinute = value; }
        }
        ///<summary>
        /// 總遲到（小时）
        ///</summary>    
        private double _mTotalLateInHour;
        ///<summary>
        /// 總遲到（小时）
        ///</summary>    
        public double mTotalLateInHour
        {
            get { return this._mTotalLateInHour; }
            set { this._mTotalLateInHour = value; }
        }
        ///<summary>
        /// 獎金明細實領金額
        ///</summary>
        public double BonusTotal
        {
            get
            {
                double tb = mDutyPay + mSpecialBonus + mPostPay;
                double tb_1 = Math.Truncate(tb * 10) % 10;
                if (tb_1 >= 5)
                {
                    return Math.Truncate(tb) + 1;
                }
                else
                {
                    return Math.Truncate(tb);
                }
            }
        }
        ///<summary>
        /// 應發金額
        ///</summary>    
        private double _mShouldPay;

        ///<summary>
        /// 應發金額
        ///</summary>    
        public double mShouldPay
        {
            get { return mBasePay + mAllAttendBonus + mDutyPay + mPostPay + mFieldPay + _mGeneralOverTimeFee + mHolidayOverTimeFee + mOverTimeBonus + mSpecialBonus + _mEffectFactor + mAnnualHolidayFee + mOtherPay - mOtherPunish - mLatePunish; }
        }

        ///<summary>
        ///  應領金額
        ///</summary>    
        public double mActualPay
        {
            get { return mBasePay + mAllAttendBonus + mDutyPay + mPostPay + mFieldPay + _mGeneralOverTimeFee + mHolidayOverTimeFee + mOverTimeBonus + mSpecialBonus + _mEffectFactor + mAnnualHolidayFee + mOtherPay - mOtherPunish - mLatePunish - _mLunchFee - mInsurance - mLoanFee - mTax; }

        }
        ///<summary>
        ///  日基數
        ///</summary>    
        private double _mDaysFactor;
        ///<summary>
        ///  日基數
        ///</summary>    
        public double mDaysFactor
        {
            get { return this._mDaysFactor; }
            set { this._mDaysFactor = value; }
        }
        ///<summary>
        ///月基數
        ///</summary>    
        private double _mMonthFactor;
        ///<summary>
        ///月基數
        ///</summary>    
        public double mMonthFactor
        {
            get { return this._mMonthFactor; }
            set { this._mMonthFactor = value; }
        }
        ///<summary>
        ///  扣點數
        ///</summary>    
        private double _mPunishCount;
        ///<summary>
        ///  扣點數
        ///</summary>    
        public double mPunishCount
        {
            get { return this._mPunishCount; }
            set { this._mPunishCount = value; }
        }
        ///<summary>
        /// 遲到（分）
        ///</summary>    
        private double _mLateInMinute;
        ///<summary>
        /// 遲到（分）
        ///</summary>    
        public double mLateInMinute
        {
            get { return this._mLateInMinute; }
            set { this._mLateInMinute = value; }
        }
        ///<summary>
        ///   临时变量,统计符合请假的次数
        ///</summary>    
        private double _mCount;
        ///<summary>
        ///   临时变量,统计符合请假的次数
        ///</summary>    
        public double mCount
        {
            get { return this._mCount; }
            set { this._mCount = value; }
        }
        ///<summary>
        ///   備註
        ///</summary>    
        private string _mNote;
        ///<summary>
        ///   備註
        ///</summary>    
        public string mNote
        {
            get { return this._mNote; }
            set { this._mNote = value; }
        }
        ///<summary>
        /// 加班费
        ///</summary>    
        private double _mOverTimeFee;
        ///<summary>
        ///   加班费
        ///</summary>    
        public double mOverTimeFee
        {
            get { return this._mOverTimeFee; }
            set { this._mOverTimeFee = value; }
        }
        ///<summary>
        /// 總出勤記錄數
        ///</summary>    
        private double _mDutyDateCount;
        //// <summary>
        //// 總出勤記錄數
        //// </summary>
        public double mDutyDateCount
        {
            get { return this._mDutyDateCount; }
            set { this._mDutyDateCount = value; }
        }

        /// 2013年3月26日11:04:52 Creator:CN Desc: 改变薪资界面,增加 小计&加班
        /// <summary>
        /// 小计(月薪资报表) 此为{绩效系数以上项目统计:底薪+工作奖金(出勤奖金)+职场津贴(餐费津贴)+绩效系数+其它补款-其它扣款-迟到扣款}
        /// </summary>
        public double XiaoJI
        {
            get
            {
                double tb = mBasePay + mAllAttendBonus + mFieldPay + mEffectFactor + mAnnualHolidayFee + mOtherPay - mOtherPunish - mLatePunish;
                double tb_1 = Math.Truncate(tb * 10) % 10;
                if (tb_1 >= 5)
                {
                    return Math.Truncate(tb) + 1;
                }
                else
                {
                    return Math.Truncate(tb);
                }
            }
        }

        /// <summary>
        /// 小计(窗口显示专用) 此为{绩效系数以上项目统计:底薪+工作奖金(出勤奖金)+职场津贴(餐费津贴)+绩效系数}
        /// </summary>
        //public double XiaoJI_Frm
        //{
        //    get
        //    {
        //        double tb = mBasePay + mAllAttendBonus + mFieldPay + mEffectFactor + mAnnualHolidayFee;
        //        double tb_1 = Math.Truncate(tb * 10) % 10;
        //        if (tb_1 >= 5)
        //        {
        //            return Math.Truncate(tb) + 1;
        //        }
        //        else
        //        {
        //            return Math.Truncate(tb);
        //        }
        //    }
        //}

        /// <summary>
        /// 加班 : 
        /// 1-2 hour 加班系数 = 1.33  实薪*1.33
        /// 2-max    加班系数 = 1.66  实薪*1.66
        /// </summary>
        public double JiaBan
        {
            //get
            //{
            //    return mDailyPay / 8 * 1.33 * OverTimeCountSmall + mDailyPay / 8 * 1.66 * OverTimeCountBig;
            //}

            #region 2013年5月4日15:20:18 加班 = 平日加班费+假日加班费
            get
            {
                return this.mGeneralOverTimeFee + this.mHolidayOverTimeFee;
            }
        }

        /// <summary>
        /// 平日加班2小时以下（时数）
        /// </summary>
        public double GeneralOverTimeCountSmall { get; set; }

        /// <summary>
        /// 平日加班2小时之外（时数）
        /// </summary>
        public double GeneralOverTimeCountBig { get; set; }

        /// <summary>
        /// 假日加班2小时以下（时数）
        /// </summary>
        public double HolidayOverTimeCountSmall { get; set; }

        /// <summary>
        /// 假日加班2小时之外（时数）
        /// </summary>
        public double HolidayOverTimeCountBig { get; set; }

        ///<summary>
        ///年假(补休)天数
        ///</summary>    
        private double _mGivenDays;
        ///<summary>
        ///年假(补休)天数
        ///</summary>    
        public double mGivenDays
        {
            get { return this._mGivenDays; }
            set { this._mGivenDays = value; }
        }
        ///<summary>
        /// 年假補休
        ///</summary>
        private double _mAnnualHolidayFee;
        ///<summary>
        /// 年假補休
        ///</summary>
        public double mAnnualHolidayFee
        {
            get { return this._mAnnualHolidayFee; }
            set { this._mAnnualHolidayFee = value; }
        }
            #endregion
    }
}
