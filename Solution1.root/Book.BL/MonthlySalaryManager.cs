//------------------------------------------------------------------------------
//
// file name：MonthlySalaryManager.cs
// author: peidun
// create date：2010-3-24 11:21:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MonthlySalary.
    /// </summary>
    public partial class MonthlySalaryManager
    {

        public Model.MonthlySalary getMonthsalarybyempid(string employeeid)
        {
            return accessor.getMonthsalarybyempid(employeeid);
        }


        /// <summary>
        /// Delete MonthlySalary by primary key.
        /// </summary>
        public void Delete(string monthlySalaryId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(monthlySalaryId);
        }

        /// <summary>
        /// Insert a MonthlySalary.
        /// </summary>
        public void Insert(Model.MonthlySalary monthlySalary)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(monthlySalary);
        }

        /// <summary>
        /// Update a MonthlySalary.
        /// </summary>
        public void Update(Model.MonthlySalary monthlySalary)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(monthlySalary);
        }
        public void UpdateDataSet(Model.MonthlySalaryStruct monthlySalaryStruct)
        {
            accessor.UpdateDataSet(monthlySalaryStruct);
        }
        public Model.MonthlySalary GetByeEmpIdMonth(Model.Employee employee, int year, int month)
        {
            return accessor.GetByeEmpIdMonth(employee, year, month);
        }   
        public DataSet GetMonthlySummaryByMonth(int years, int months)
        {
            return accessor.GetMonthlySummaryByMonth(years, months);
        }

        /// <summary>
        /// 取MonthlySalary
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="date"></param>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <returns></returns>
        public DataSet getMonthlySalary(string employeeid, DateTime date)
        {
            return accessor.getMonthlySalary(employeeid, date);

        }

        /// <summary>
        ///  取月考勤记录
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="date"></param>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <returns></returns>

        public DataSet getAttendInfoList(string employeeid, int years, int months)
        {
            return accessor.getAttendInfoList(employeeid, years, months);
        }
        /// <summary>
        /// 取考勤计算
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="date"></param>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <returns></returns>

        public DataSet getMonthlySummaryFee(string employeeid, DateTime date, int years, int months)
        {
            return accessor.getMonthlySummaryFee(employeeid, date, years, months);
        }
        //更新基础设置
        public int  UpMonthSalFromClockFrm(Model.Employee emp, DateTime UpdateTime)
        {
            return accessor.UpMonthSalFromClockFrm(emp, UpdateTime);
        }

        //返回最大月份
        public DateTime get_MaxIdentifyDateMonth()
        {
            return accessor.getMaxIdentifyDateMonth();
        }
    }
}

