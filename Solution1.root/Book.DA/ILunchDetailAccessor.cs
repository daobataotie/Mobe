//------------------------------------------------------------------------------
//
// file name：ILunchDetailAccessor.cs
// author: peidun
// create date：2010-3-26 11:08:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.LunchDetail
    /// </summary>
    public partial interface ILunchDetailAccessor : IAccessor
    {
        IList<Model.LunchDetail> GetLunchDetailbyempiddate(string empid, DateTime starttime, DateTime endtime);
        decimal SelectFeeSum(Model.Employee employee, int year, int month);

        DataSet GetEmployeeInfo(DateTime date);
        void UpdateLunchDetail(DataSet dataset, DateTime date);
        IList<Model.LunchDetail> selectByempAndDate(string EmpID, int year, int month);      
    }
}

