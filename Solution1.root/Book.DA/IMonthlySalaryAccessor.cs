//------------------------------------------------------------------------------
//
// file name：IMonthlySalaryAccessor.cs
// author: peidun
// create date：2010-3-24 11:21:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MonthlySalary
    /// </summary>
    public partial interface IMonthlySalaryAccessor : IAccessor
    {
        Model.MonthlySalary getMonthsalarybyempid(string employeeid);
        void UpdateDataSet(Model.MonthlySalaryStruct monthlySalaryStruct);
        Model.MonthlySalary GetByeEmpIdMonth(Model.Employee employee, int year, int month);    
        DataSet GetMonthlySummaryByMonth(int years, int months);
        DataSet getMonthlySummaryFee(string employeeid, DateTime date, int years, int months);
        DataSet getAttendInfoList(string employeeid, int years, int months);
        DataSet getMonthlySalary(string employeeid, DateTime date);
        //更新基础设置
        int UpMonthSalFromClockFrm(Model.Employee emp, DateTime UpdateTime);
        DateTime getMaxIdentifyDateMonth();
    }
}



