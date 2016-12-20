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
using Helper;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.HrDailyEmployeeAttendInfo.
    /// </summary>
    public partial class HrDailyEmployeeAttendInfoManager
    {

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
            return accessor.GetUpdateTime(dt,employeeid);
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
                                    if (Convert.ToDateTime(rows[Model.AnnualHoliday.PROPERTY_HOLIDAYDATE]) == item.DutyDate)
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
    }
}

