//------------------------------------------------------------------------------
//
// file name：HrDailyEmployeeAttendInfoManager.cs
// author: mayanjun
// create date：2010-5-19 11:29:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using Helper;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.HrDailyEmployeeAttendInfo.
    /// </summary>
    public partial class HrDailyEmployeeAttendInfoManager
    {
        /// <summary>
        /// 打卡記錄管理實例
        /// </summary>
        BL.ClockDataManager clockdata = new Book.BL.ClockDataManager();

        /// <summary>
        /// Delete HrDailyEmployeeAttendInfo by primary key.
        /// </summary>
        public void Delete(string hrDailyEmployeeAttendInfoId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(hrDailyEmployeeAttendInfoId);
        }

        /// <summary>
        /// Insert a HrDailyEmployeeAttendInfo.
        /// </summary>
        public void Insert(Model.HrDailyEmployeeAttendInfo hrDailyEmployeeAttendInfo)
        {
            //
            // todo:add other logic here
            //
            hrDailyEmployeeAttendInfo.HrDailyEmployeeAttendInfoId = Guid.NewGuid().ToString();
            accessor.Insert(hrDailyEmployeeAttendInfo);
        }

        /// <summary>
        /// Update a HrDailyEmployeeAttendInfo.
        /// </summary>
        public void Update(Model.HrDailyEmployeeAttendInfo hrDailyEmployeeAttendInfo)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(hrDailyEmployeeAttendInfo);
        }

        public System.Data.DataSet SelectDailyInfoByEmployee(string employeeId, DateTime dutyDate, string state)
        {
            return accessor.SelectDailyInfoByEmployee(employeeId, dutyDate, state);
        }

        public System.Data.DataSet SelectDailyInfoByEmployeeForDoubleDate(string employeeId, DateTime StartDate, DateTime EndDate, string state)
        {
            return accessor.SelectDailyInfoByEmployeeForDoubleDate(employeeId, StartDate, EndDate, state);
        }

        //public System.Data.DataSet SelectLateInMinute(string employeeId, DateTime dutyDate)
        //{
        //    return accessor.SelectLateInMinute(employeeId, dutyDate);
        //}
        //public void UpdateActualCheckIn(string hrId)
        //{
        //    accessor.UpdateActualCheckIn(hrId);
        //}

        public System.Data.DataSet SelectLateInfo(string EmployeeId, DateTime ClockDate)
        {
            return accessor.SelectLateInfo(EmployeeId, ClockDate);
        }

        public void InsertLateInfo(string id, string EmployeeId, DateTime ClockDate, int LateInMinute)
        {
            accessor.InsertLateInfo(id, EmployeeId, ClockDate, LateInMinute);
        }

        public System.Data.DataSet SelectHrInfoByStateAndDate(DateTime DutyDate)
        {
            return accessor.SelectHrInfoByStateAndDate(DutyDate);
        }

        public System.Data.DataSet SelectHrInfoById(string HrDailyEmployeeAttendInfoId)
        {
            return accessor.SelectHrInfoById(HrDailyEmployeeAttendInfoId);
        }

        public void DeleteLateInfoByEmployeeIdAndDate(string EmployeeId, DateTime ClockDate)
        {
            accessor.DeleteLateInfoByEmployeeIdAndDate(EmployeeId, ClockDate);
        }

        /// <summary>
        /// 班别津贴
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public int SelectBusinessHourPaySum(int years, int months, string employeeid)
        {
            return accessor.SelectBusinessHourPaySum(years, months, employeeid);
        }

        /// <summary>
        /// 查询总日基数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public decimal SelectDayFactorSum(int years, int months, Model.Employee employee)
        {
            return accessor.SelectDayFactorSum(years, months, employee);
        }

        /// <summary>
        /// 总月基数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employeeid"></param>
        /// <returns></returns>
        public decimal SelectDayMonthSum(int years, int months, Model.Employee employee)
        {
            return accessor.SelectDayMonthSum(years, months, employee);
        }

        /// <summary>
        /// 根据员工编号和日期来查询薪资记录是否有重复的
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public System.Data.DataSet SelectHrInfoByEmployeeIdAndDueDate(string employeeId, DateTime dueDate)
        {
            return accessor.SelectHrInfoByEmployeeIdAndDueDate(employeeId, dueDate);
        }

        /// <summary>
        /// 月总出勤数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectDutyDateCount(int years, int months, Model.Employee employee)
        {
            return accessor.SelectDutyDateCount(years, months, employee);
        }

        /// <summary>
        /// 总旷职数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectAbsentCount(int years, int months, Model.Employee employee)
        {
            return accessor.SelectAbsentCount(years, months, employee);
        }

        /// <summary>
        /// 月出勤信息
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<Model.HrDailyEmployeeAttendInfo> SelectByEmpMonth(Model.Employee employee, int year, int month)
        {
            return accessor.SelectByEmpMonth(employee, year, month);
        }

        public DataSet SelectByEmpMonth(Model.Employee employee, DateTime date)
        {
            return accessor.SelectByEmpMonth(employee, date);
        }

        public DataSet GetemployeeJoinDate(Model.Employee empoyee)
        {
            return accessor.GetemployeeJoinDate(empoyee);
        }

        public DataSet SelectBeginAndEndTime(Model.Employee employee, DateTime date)
        {
            return accessor.SelectBeginAndEndTime(employee, date);
        }

        /// <summary>
        /// 
        /// 月迟到次数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectLateCount(int years, int months, Model.Employee employee)
        {
            return accessor.SelectLateCount(years, months, employee);
        }

        /// <summary>
        /// 月总迟到分数
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int SelectLateSum(int years, int months, Model.Employee employee)
        {
            return accessor.SelectLateSum(years, months, employee);
        }

        public DataSet SelectByEmpMonth(DateTime date)
        {
            return accessor.SelectByEmpMonth(date);
        }

        public DateTime GetUpdateTime(DateTime dt, string employeeid)
        {
            return accessor.GetUpdateTime(dt, employeeid);
        }

        public IList<Model.HrDailyEmployeeAttendInfo> SelectHrInfoByEmployeeIdAndDueDate1(string employeeId, DateTime dueDate)
        {
            return accessor.SelectHrInfoByEmployeeIdAndDueDate1(employeeId, dueDate);
        }

        AnnualHolidayManager annualholidayManager = new AnnualHolidayManager();

        public void UpdateDailyInfo(Model.HrDailyEmployeeAttendInfo dailyInfo, IList<Model.HrDailyEmployeeAttendInfo> dailyInfolist)
        {
            if (dailyInfo.ShouldCheckIn == null || dailyInfo.ShouldCheckOut == null)
            {
                foreach (Model.HrDailyEmployeeAttendInfo item in dailyInfolist)
                {
                    item.OverTimeOff = dailyInfo.ActualCheckOut == null ? null : dailyInfo.ActualCheckOut;
                    item.OverTimeON = dailyInfo.ActualCheckIn == null ? null : dailyInfo.ActualCheckIn;
                    this.Update(item);
                }
            }
            else
            {
                foreach (Model.HrDailyEmployeeAttendInfo item in dailyInfolist)
                {
                    DataSet annualHolidayData = annualholidayManager.SelectSingleAnnualInfo(Convert.ToDateTime(item.DutyDate));
                    dailyInfo.HrDailyEmployeeAttendInfoId = item.HrDailyEmployeeAttendInfoId;
                    if (item.Note != null)
                    {
                        if (item.Note == "缺刷卡資料")
                        {
                            this.Update(dailyInfo);
                        }
                        else
                        {
                            foreach (DataRow rows in annualHolidayData.Tables[0].Rows)
                            {
                                if (Convert.ToDateTime(rows[Model.AnnualHoliday.PRO_HolidayDate]) == item.DutyDate)
                                {
                                    item.OverTimeOff = dailyInfo.ActualCheckOut == null ? null : dailyInfo.ActualCheckOut;
                                    item.OverTimeON = dailyInfo.ActualCheckIn == null ? null : dailyInfo.ActualCheckIn;
                                    this.Update(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        dailyInfo.OverTimeOff = item.OverTimeOff;
                        dailyInfo.OverTimeON = item.OverTimeON;
                        this.Update(dailyInfo);
                    }
                }

            }
        }

        /// <summary>
        /// 重新保存
        /// </summary>
        public int ReattenSave(Model.HrDailyEmployeeAttendInfo _hrEA)
        {
            return accessor.ReattenSave(_hrEA);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="date"></param>
        /// <param name="emp"></param>
        public void ReCheck(DateTime date, Model.Employee emp)
        {
            DateTime limitDate;
            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToShortDateString() + " 12:50"))
            {
                limitDate = DateTime.Parse(DateTime.Now.AddDays(-1).ToShortDateString());
            }
            else
            {
                limitDate = DateTime.Parse(DateTime.Now.AddDays(-2).ToShortDateString());
            }
            //已經自動考勤過了，所以必需再重新考勤
            if (limitDate >= date)
            {
                this.Reatten_Controller(date.Date, emp);
            }
        }

        /// <summary>
        /// 重新考勤处理
        /// </summary>
        /// <param name="checkdate">日期</param>
        /// <param name="emp">员工</param>
        public Model.HrDailyEmployeeAttendInfo Reatten_Controller(DateTime checkdate, Model.Employee emp)
        {
            Model.BusinessHours mBusinessHour = new Book.Model.BusinessHours(); //--當日應上的班別
            DateTime? mShouldCheckIn = null;            //--應上班時間
            DateTime? mShouldCheckOut = null;           //--應下班時間
            DateTime? mActualCheckIn = null;     //--實際上班時間
            DateTime? mActualCheckOut = null;    //--實際下班時間
            int mLateInMinute = 0;                   //--遲到（分）
            Double? mDayFactor = 0;                  //--日基數
            int? mMonthFactor = 0;                //--月基數
            Double? mSpecialBonus = 0;               //--班别津贴
            bool mIsNormal = true;                      //--是否正常出勤
            string mNote = string.Empty;         //--備註
            string mCardNo = string.Empty;                      //--卡号

            //取得班別表及班別津貼集合
            IList<Model.BusinessHours> mBusinessHourCollection = new BL.BusinessHoursManager().Select();
            //取得年度假日列表
            IList<Model.AnnualHoliday> holidayList = new BL.AnnualHolidayManager().SelectAnnualInfoByYear_list(checkdate.Year);
            //檢查考勤日是否為假日
            Model.AnnualHoliday holiday = null;
            //取得考情日上班部门,确定员工是否休假

            bool h_bool = holidayList.Any(pd => (pd.HolidayDate.Value.ToShortDateString() == checkdate.ToShortDateString()));
            if (h_bool)
            {
                holiday = holidayList.Where(pd => (pd.HolidayDate.Value.ToShortDateString() == checkdate.ToShortDateString())).First();
                if (!string.IsNullOrEmpty(holiday.Departs))
                {
                    if (!holiday.Departs.Contains(emp.DepartmentId))
                    {
                        holiday = null;
                    }
                }
            }

            //取得假別列表
            IList<Model.LeaveType> mleaveTypeList = new BL.LeaveTypeManager().Select();
            //請假資訊
            //Model.Leave mLeave = new BL.LeaveManager().GetEmployeeLeavebyDate(emp.EmployeeId, checkdate);
            IList<Model.Leave> mLeaves = new BL.LeaveManager().GetEmployeeLeavebyDate(emp.EmployeeId, checkdate);

            //若该请假不参与考勤
            if (mLeaves != null && mLeaves.Count > 0)
            {
                foreach (Model.Leave l in mLeaves)
                    if (!l.LeaveType.doAttendance.Value)
                        return null;
                //if (!mLeave.LeaveType.doAttendance.Value)
                //    return null;
            }
            //彈性排班資訊
            Model.Flextime mFlexTime = new BL.FlextimeManager().getbyempiddate(emp.EmployeeId, checkdate);
            if (mFlexTime == null)
            {   //沒有彈性排班，則取原訂班別
                mBusinessHour = emp.BusinessHours;
            }
            else
            {
                //有彈性排班，則排定之訂班別
                mBusinessHour = mFlexTime.BusinessHours;
            }
            //____查詢是否有換臨時卡
            Model.TempCard mTempCard = new BL.TempCardManager().Selectbyemployeedate(emp.EmployeeId, checkdate, checkdate);
            if (mTempCard == null)
            {
                mCardNo = emp.CardNo;
            }
            else
            {
                mCardNo = mTempCard.CardNo;
            }
            if (holiday == null)
            {
                //----------------------------------------------
                //非假日：查詢是否有請假及彈性排班，
                //並查出該員當日應上的班別
                //----------------------------------------------
                //先找出該員正常應上下班之時間
                if (mBusinessHour.Fromtime.Value.Hour == 0)
                {
                    mShouldCheckIn = DateTime.Parse(checkdate.AddDays(1).ToShortDateString() + " " + "00:" + mBusinessHour.Fromtime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                else
                {
                    mShouldCheckIn = DateTime.Parse(checkdate.ToShortDateString() + " " + mBusinessHour.Fromtime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.Fromtime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                if (mBusinessHour.Fromtime.Value.Hour > mBusinessHour.ToTime.Value.Hour || mBusinessHour.Fromtime.Value.Hour == 0)        //上班時段有跨日
                {
                    mShouldCheckOut = DateTime.Parse(checkdate.AddDays(1).ToShortDateString() + " " + mBusinessHour.ToTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                else        //沒有跨日
                {
                    if (mBusinessHour.ToTime.Value.Hour == 0)
                    {
                        mShouldCheckOut = DateTime.Parse(checkdate.AddDays(1).ToShortDateString() + " " + "00:" + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        mShouldCheckOut = DateTime.Parse(checkdate.ToShortDateString() + " " + mBusinessHour.ToTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                    }
                }
                //是否有請假
                if (mLeaves != null && mLeaves.Count > 0)
                {
                    if (mLeaves.Count == 1)     //本日只有一条请假记录
                    {
                        Model.LeaveType mLeaveType = mLeaves[0].LeaveType;
                        switch (mLeaves[0].LeaveRange.Value)
                        {
                            case 0:     //請整日
                                mShouldCheckIn = null;
                                mShouldCheckOut = null;
                                if (mLeaveType.PayRate.Value == 0.5)
                                    mDayFactor = 0.5;
                                else
                                    mDayFactor = Convert.ToInt32(mLeaveType.PayRate);
                                mMonthFactor = 1;
                                mIsNormal = true;
                                mNote = mLeaves[0].LeaveType.ToString() + "(整日)";
                                break;
                            case 1:     //請上半日
                                mNote = mLeaves[0].LeaveType.ToString() + "(上半日)";
                                mShouldCheckIn = mShouldCheckOut.Value.AddHours(-4); //以應下班時間減四小時為應上班時間
                                if (mLeaveType.PayRate.Value == 0.5)
                                    mDayFactor = 0.8;

                                break;
                            case 2:     //請下半日
                                mNote = mLeaves[0].LeaveType.ToString() + "(下半日)";
                                mShouldCheckOut = mShouldCheckIn.Value.AddHours(4); //以應上班時間加四小時為應下班時間
                                if (mLeaveType.PayRate.Value == 0.5)
                                    mDayFactor = 0.8;
                                break;
                        }
                    }
                    else    //上下半天都有请假记录
                    {
                        mShouldCheckIn = null;
                        mShouldCheckOut = null;
                        mIsNormal = true;
                        string _aNote = string.Empty;
                        double _aDayFactor = 0;
                        mMonthFactor = 1;
                        foreach (Model.Leave l in mLeaves)
                        {
                            _aDayFactor += l.LeaveType.PayRate.Value / 2;
                            if (l.LeaveRange == 1)
                                _aNote += l.LeaveType.ToString() + "(上半日)";
                            else
                                _aNote += l.LeaveType.ToString() + "(下半日)";
                        }
                        mDayFactor = _aDayFactor;
                        mNote = _aNote;
                    }
                }

                //找出實際上下班時間

                if (mShouldCheckIn != null)
                {
                    mActualCheckIn = clockdata.GetAnyInOut(mCardNo, mShouldCheckIn.Value.AddHours(-8), mShouldCheckIn.Value.AddHours(3), "ASC");
                }
                if (mShouldCheckOut != null)
                {
                    mActualCheckOut = clockdata.GetAnyInOut(mCardNo, mShouldCheckOut.Value.AddHours(-1), mShouldCheckOut.Value.AddHours(9), "ASC");
                }
                //判斷出勤記錄
                //___找不到上下班打卡資料
                if (mActualCheckIn == null || mActualCheckOut == null)
                {   //有請假
                    if (mLeaves != null && mLeaves.Count > 0)
                    {
                        if (mLeaves.Count == 1)
                        {   //只對請半天的做處理
                            if (mLeaves[0].LeaveRange != 0)
                            {
                                mIsNormal = false;
                                mNote = mNote + ";卻刷卡資料";
                            }
                        }
                    }
                    else //沒請假
                    {
                        mIsNormal = false;
                        mNote = "卻刷卡資料";
                    }
                }
                //___有上下班打卡資料
                else
                {
                    if (mLeaves != null && mLeaves.Count > 0)
                    {   //有請假
                        Model.LeaveType mLeaveType = mLeaves[0].LeaveType;
                        if (mLeaves[0].LeaveRange != 0)
                        {   //只對請半天的做處理
                            if (mLeaveType.PayRate.Value == 0.5)
                                mDayFactor = 0.75;
                            else
                                mDayFactor = 0.5 + 0.5 * mLeaveType.PayRate;
                            mMonthFactor = 1;
                            mSpecialBonus = Convert.ToDouble(mBusinessHour.SpecialPay) * 0.5;
                            mIsNormal = true;
                        }
                    }
                    else
                    {   //沒請假
                        mDayFactor = 1;
                        mMonthFactor = 1;
                        mSpecialBonus = Convert.ToDouble(mBusinessHour.SpecialPay);
                        mIsNormal = true;
                    }
                    if (mActualCheckIn > mShouldCheckIn)
                    {
                        TimeSpan ts = TimeSpan.Parse((mActualCheckIn - mShouldCheckIn).ToString());
                        mLateInMinute = ts.Hours * 60 + ts.Minutes;
                        if (mLateInMinute > 0)
                        {
                            if (mLateInMinute > 10)
                            {   //遲到超過10分鐘，顯示異常（讓員工有申訴機會）
                                mIsNormal = false;
                                mNote = "遲到";
                            }
                        }
                    }
                    else
                    {
                        mLateInMinute = 0;
                    }
                    //早退超過5分鐘，顯示異常
                    if (mActualCheckOut < mShouldCheckOut)
                    {
                        TimeSpan ts = TimeSpan.Parse((mShouldCheckOut - mActualCheckOut).ToString());
                        int mLeaveEarly = ts.Hours * 60 + ts.Minutes;
                        if (mLeaveEarly > 5)
                        {
                            mIsNormal = false;
                            if (mNote != "")
                            {
                                mNote = mNote + ";";
                            }
                            mNote = mNote + "早退";
                        }
                    }
                }
            }
            else
            {   //假日
                mDayFactor = 1;
                mMonthFactor = 1;
                mLateInMinute = 0;
                mIsNormal = true;
                mNote = holiday.HolidayName;
                //找出是否有假日出勤記錄
                //___依正常排班表來判
                DateTime mayBeCheckIn = global::Helper.DateTimeParse.NullDate;
                DateTime mayBeCheckOut = global::Helper.DateTimeParse.NullDate;
                //先找出該員可能上下班之時間
                if (mBusinessHour.Fromtime.Value.Hour == 0)
                {
                    mayBeCheckIn = DateTime.Parse(checkdate.AddDays(1).ToString("yyyy-MM-dd") + " " + "00:" + mBusinessHour.Fromtime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                else
                {
                    mayBeCheckIn = DateTime.Parse(checkdate.ToString("yyyy-MM-dd") + " " + mBusinessHour.Fromtime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.Fromtime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                if (mBusinessHour.Fromtime.Value.Hour > mBusinessHour.ToTime.Value.Hour)
                {   //上班時段有跨日
                    mayBeCheckOut = DateTime.Parse(checkdate.AddDays(1).ToString("yyyy-MM-dd") + " " + mBusinessHour.ToTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                }
                else
                {  //上班時段有沒有跨日
                    if (mBusinessHour.ToTime.Value.Hour == 0)
                    {
                        mayBeCheckOut = DateTime.Parse(checkdate.AddDays(1).ToString("yyyy-MM-dd") + " " + "00:" + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        mayBeCheckOut = DateTime.Parse(checkdate.ToString("yyyy-MM-dd") + " " + mBusinessHour.ToTime.Value.Hour.ToString().PadLeft(2, '0') + ":" + mBusinessHour.ToTime.Value.Minute.ToString().PadLeft(2, '0'));
                    }
                }
                //找出假日出勤時間
                mActualCheckIn = clockdata.GetAnyInOut(mCardNo, mayBeCheckIn.AddHours(-2), mayBeCheckOut.AddHours(4), "ASC");
                mActualCheckOut = clockdata.GetAnyInOut(mCardNo, mayBeCheckIn.AddHours(-2), mayBeCheckOut.AddHours(4), "DESC");
                //有正常刷卡假日加班
                if (mActualCheckIn != null && mActualCheckOut != null)
                {
                    //------------------------------
                    //檢查是否有上滿一整個班別
                    //若有,則給予班別津貼
                    //------------------------------
                    TimeSpan ts1 = TimeSpan.Parse((mActualCheckOut - mActualCheckIn).ToString());
                    TimeSpan ts2 = TimeSpan.Parse((mayBeCheckOut - mayBeCheckOut).ToString());
                    if (ts1.Minutes >= ts2.Minutes)
                    {
                        mSpecialBonus = Convert.ToDouble(mBusinessHour.SpecialPay);
                    }
                }
            }
            Model.HrDailyEmployeeAttendInfo _hrEA = new Book.Model.HrDailyEmployeeAttendInfo();
            _hrEA.HrDailyEmployeeAttendInfoId = Guid.NewGuid().ToString();
            _hrEA.DutyDate = checkdate;
            _hrEA.EmployeeId = emp.EmployeeId;
            _hrEA.ShouldCheckIn = mShouldCheckIn;
            _hrEA.ShouldCheckOut = mShouldCheckOut;
            _hrEA.ActualCheckIn = mActualCheckIn;
            _hrEA.ActualCheckOut = mActualCheckOut;
            _hrEA.LateInMinute = mLateInMinute;
            _hrEA.DayFactor = mDayFactor;
            _hrEA.MonthFactor = mMonthFactor;
            _hrEA.SpecialBonus = Convert.ToInt32(mSpecialBonus);
            _hrEA.IsNormal = mIsNormal;
            _hrEA.Note = mNote;
            _hrEA.MBusinessHours = mBusinessHour;
            _hrEA.MLeave = (mLeaves == null || mLeaves.Count == 0) ? null : mLeaves[0];

            ReattenSave(_hrEA);

            return _hrEA;
        }

        public int UpdateSave_AnormalySalaryEditForm(Model.HrDailyEmployeeAttendInfo _hrDaily)
        {
            return accessor.UpdateSave_AnormalySalaryEditForm(_hrDaily);
        }

        /// <summary>
        /// 處理關於更改員工離職日期調整
        /// </summary>
        public void DeleteForChangeLeaveDateEmpHrDay(Model.Employee employee)
        {
            accessor.DeleteForChangeLeaveDateEmpHrDay(employee);
        }
    }
}