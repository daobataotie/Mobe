//------------------------------------------------------------------------------
//
// file name：ILeaveAccessor.cs
// author: peidun
// create date：2010-3-16 16:05:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Leave
    /// </summary>
    public partial interface ILeaveAccessor : IAccessor
    {
         IList<Model.Leave> Getleavebyempiddate(string empid, DateTime starttime, DateTime endtime);
         System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId,DateTime leaveDate);
         decimal SelectCountByMonthEmp(Model.Employee employee, int year, int month);
         decimal SelectPunishByMonthEmp(Model.Employee employee, int year, int month);
         decimal SelectTotalHolidayMonthEmp(Model.Employee employee, int year, int month);
         int SelectSpecificHolidayMonthEmp(Model.Employee employee, int year, int month);
         IList<Model.Leave> Getleavebyempidmonth(string empid, int year, int month);
         System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, string year);
    }
}

