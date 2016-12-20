//------------------------------------------------------------------------------
//
// file name：IHrDailyEmployeeAttendInfoAccessor.cs
// author: mayanjun
// create date：2010-5-19 11:29:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.HrDailyEmployeeAttendInfo
    /// </summary>
    public partial interface IHrDailyEmployeeAttendInfoAccessor : IAccessor
    {
        System.Data.DataSet  SelectDailyInfoByEmployee(string employeeId,DateTime  dutyDate,string state);
        //System.Data.DataSet SelectLateInMinute(string hrId,DateTime dutyDate);
        //void UpdateActualCheckIn(string hrId);
        System.Data.DataSet SelectLateInfo(string EmployeeId,DateTime ClockDate);
        void InsertLateInfo(string id, string EmployeeId, DateTime ClockDate, int LateInMinute);
        System.Data.DataSet SelectHrInfoByStateAndDate(DateTime DutyDate);
        System.Data.DataSet SelectHrInfoById(string HrDailyEmployeeAttendInfoId);
        System.Data.DataSet SelectHrInfoByEmployeeIdAndDueDate(string employeeId,DateTime dueDate);
        void DeleteLateInfoByEmployeeIdAndDate(string EmployeeId, DateTime ClockDate);
        int SelectBusinessHourPaySum(int years, int months, string employeeid);
        decimal SelectDayFactorSum(int years, int months, Model.Employee employee);
        decimal SelectDayMonthSum(int years, int months, Model.Employee employee);
        int SelectDutyDateCount(int years, int months, Model.Employee employee);
        int SelectAbsentCount(int years, int months, Model.Employee employee);
        DataSet SelectByEmpMonth(Model.Employee employee, DateTime date);
        DataSet SelectByEmpMonth(DateTime date);
        DataSet SelectBeginAndEndTime(Model.Employee employee, DateTime date);
        DataSet GetemployeeJoinDate(Model.Employee empoyee);
        IList<Model.HrDailyEmployeeAttendInfo> SelectByEmpMonth(Model.Employee employee, int year, int month);
      
        int SelectLateCount(int years, int months, Model.Employee employee);
        int SelectLateSum(int years, int months, Model.Employee employee);
        DateTime GetUpdateTime(DateTime dt, string employeeid);
        IList<Model.HrDailyEmployeeAttendInfo> SelectHrInfoByEmployeeIdAndDueDate1(string employeeId, DateTime dueDate);
    }
}

