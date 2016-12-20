//------------------------------------------------------------------------------
//
// file name：IClockDataAccessor.cs
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
    /// Interface of data accessor of dbo.ClockData
    /// </summary>
    public partial interface IClockDataAccessor : IAccessor
    {
        //DataSet GetOntimeDetails(string employeeId, DateTime clockDate);
        //DataSet GetOnMonthDetails(string employeeId, DateTime clockDate);
        IList<Model.ClockData> getClockbyempid(string employeeId);


        Model.ClockData Getfirstclosck(string empid, DateTime starttime, DateTime endtime);
        Model.ClockData Getlastclosck(string empid, DateTime starttime, DateTime endtime);
        /// <summary>
        /// 员工某段时间的打卡记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        IList<Model.ClockData> getbydateandempid(string empid, DateTime starttime, DateTime endtime);
        /// <summary>
        /// 某段时间的所有员工打卡记录
        /// </summary>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        IList<Model.ClockData> getbydate(DateTime starttime, DateTime endtime);
        /// <summary>
        /// 获取所有的日期
        /// </summary>
        /// <returns></returns>
        DataSet SearchDistinctDate();
        /// <summary>
        /// 获取所有的员工
        /// </summary>
        /// <returns></returns>
        DataSet SearchDistinctEmployee();
        /// <summary>
        /// 根据员工编号获取打卡信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        DataSet SearchSingleClockByCondition(string employeeId,DateTime date,string clockType);
        DataSet SearchBusinessHoursInfoByEmployeeId(string EmployeeId);
        DataSet SearchOvertimeInfoByEmployeeId(string employeeId,DateTime date);
        DataSet SearchClockDataInfoByEmployeeId(string employeeId,DateTime clockDate);

        //最近三个月的所有打卡记录
        IList<Model.ClockData> selectClockTopTreeMonth();
    }
}

