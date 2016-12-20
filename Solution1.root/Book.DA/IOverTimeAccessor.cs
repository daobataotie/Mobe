//------------------------------------------------------------------------------
//
// file name：IOverTimeAccessor.cs
// author: peidun
// create date：2010-3-20 11:59:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.OverTime
    /// </summary>
    public partial interface IOverTimeAccessor : IAccessor
    {
        IList<Model.OverTime> GetOverTimebyempiddate(string empid, DateTime starttime, DateTime endtime);
        System.Data.DataSet SelectOverTimeInfoByEmployeeId(string employeeId, DateTime dueDate);
        IList<Model.OverTime> SelectByEmployeeAndMonth(Model.Employee employee, int year, int month);
        DataSet selectOverTimebyDate(string IDNo, int year, int month);
        Book.Model.OverTime GetLastForEmployeeYearMonth(string EmpID, int year, int month);
        Book.Model.OverTime GetNextForEmployeeYearMonth(string EmpID, DateTime date, int year, int month);
        Book.Model.OverTime GetPrevForEmployeeYearMonth(string EmpID, DateTime date, int year, int month);
        DataSet selectOverTime();
        IList<Model.OverTime> SelectOverTimeList(DateTime startdate, DateTime enddate);
        bool JudgeRepeater(string empids, int Empcount, DateTime JudgeDate);

    }
}

