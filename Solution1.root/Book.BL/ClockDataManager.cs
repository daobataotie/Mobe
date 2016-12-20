//------------------------------------------------------------------------------
//
// file name：ClockDataManager.cs
// author: peidun
// create date：2010-3-20 11:59:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ClockData.
    /// </summary>
    public partial class ClockDataManager : BaseManager
    {
        /// <summary>
        /// Delete ClockData by primary key.
        /// </summary>
        public void Delete(Model.ClockData clockData)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(clockData.ClockDataId);
        }
        /// <summary>
        /// Insert a ClockData.
        /// </summary>
        public void Insert(Model.ClockData clockData)
        {
            //
            // todo:add other logic here
            //
            Validate(clockData);
            clockData.InsertTime = DateTime.Now;
            accessor.Insert(clockData);
        }
        /// <summary>
        /// Update a ClockData.
        /// </summary>
        public void Update(Model.ClockData clockData)
        {
            //
            // todo: add other logic here.
            //
            Validate(clockData);
            clockData.UpdateTime = DateTime.Now;
            accessor.Update(clockData);
        }
        private void Validate(Model.ClockData clockData)
        {
            if (string.IsNullOrEmpty(clockData.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.ClockData.PRO_EmployeeId);
            }
            if (clockData.Empclockdate == null)
            {
                throw new Helper.RequireValueException(Model.ClockData.PRO_Empclockdate);
            }
            if (clockData.Clocktime == null)
            {
                throw new Helper.RequireValueException(Model.ClockData.PRO_Clocktime);
            }
        }
        //public  DataSet GetOntimeDetails(string employeeId, DateTime clockDate)
        //{
        //    return accessor.GetOntimeDetails(employeeId, clockDate);
        //}
        //public DataSet GetOnMonthDetails(string employeeId, DateTime clockDate)
        //{
        //    return accessor.GetOnMonthDetails(employeeId, clockDate);
        //}
        /// <summary>
        /// 查询 员工 打卡记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IList<Model.ClockData> getClockbyempid(string employeeId)
        {
            return accessor.getClockbyempid(employeeId);
        }
        /// <summary>
        /// 员工在时间段内最早打卡记录
        /// </summary>
        /// <param name="empid">员工编号</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public Model.ClockData Getfirstclosck(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.Getfirstclosck(empid, starttime, endtime);
        }
        /// <summary>
        /// 员工在时间段内最晚打卡记录
        /// </summary>
        /// <param name="empid">员工编号</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public Model.ClockData Getlastclosck(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.Getlastclosck(empid, starttime, endtime);
        }
        /// <summary>
        /// 员工某段时间的打卡记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.ClockData> getbydateandempid(string empid, DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();
            parms.Add("empid", empid);
            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);

            return accessor.getbydateandempid(empid, starttime, endtime);
        }
        /// <summary>
        /// 某段时间的所有员工打卡记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public IList<Model.ClockData> getbydate(DateTime starttime, DateTime endtime)
        {
            Hashtable parms = new Hashtable();

            parms.Add("starttime", starttime);
            parms.Add("endtime", endtime);

            return accessor.getbydate(starttime, endtime);
        }
        /// <summary>
        /// 获取所有的日期
        /// </summary>
        /// <returns></returns>
        public DataSet SearchDistinctDate()
        {
            return accessor.SearchDistinctDate();
        }
        /// <summary>
        /// 获取所有的员工
        /// </summary>
        /// <returns></returns>
        public DataSet SearchDistinctEmployee()
        {
            return accessor.SearchDistinctEmployee();
        }
        /// <summary>
        /// 根据员工编号获取打卡信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet SearchSingleClockByCondition(string employeeId, DateTime date, string clockType)
        {
            return accessor.SearchSingleClockByCondition(employeeId, date, clockType);
        }
        /// <summary>
        /// 根据员工编号查询排班信息
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public DataSet SearchBusinessHoursInfoByEmployeeId(string EmployeeId)
        {
            return accessor.SearchBusinessHoursInfoByEmployeeId(EmployeeId);
        }
        /// <summary>
        /// 根据员工的编号和日期查询加班信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DataSet SearchOvertimeInfoByEmployeeId(string employeeId, DateTime date)
        {
            return accessor.SearchOvertimeInfoByEmployeeId(employeeId, date);
        }
        /// <summary>
        /// 根據打卡的卡号和日期查询打卡信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="clockDate"></param>
        /// <returns></returns>
        public DataSet SearchClockDataInfoByCarNoAndClockDate(string CarNo, DateTime clockDate)
        {
            return accessor.SearchClockDataInfoByCarNoAndClockDate(CarNo, clockDate);
        }
        /// <summary>
        /// 近三月的所有打卡记录
        /// </summary>
        /// <returns></returns>
        public IList<Model.ClockData> selectClockTopTreeMonth()
        {
            return accessor.selectClockTopTreeMonth();
        }
        /// <summary>
        /// 根据上传文件名初步判断欲上传文档内容是否已存在
        /// </summary>
        /// <returns></returns>
        public bool SearchDistinctFileName(string FileName)
        {
            return accessor.SearchDistinctFileName(FileName);
        }
        /// <summary>
        /// 获取实际上班,实际下班,假日上班,假日下班时间
        /// </summary>
        /// <param name="cardNo">打卡卡号</param>
        /// <param name="ChkDate">记录时间</param>
        /// <param name="ordertype">排序类型</param>
        /// <returns></returns>
        public DateTime? GetAnyInOut(string cardNo, DateTime MinDateTime, DateTime MaxDateTime, string ordertype)
        {
            return accessor.GetAnyInOut(cardNo, MinDateTime, MaxDateTime, ordertype);
        }

        /// <summary>
        /// 取考勤月雇员编号集合
        /// </summary>
        /// <param name="CheckDateTime"></param>
        /// <returns></returns>
        public IList<string> GetMakeSalaryList_DisEmpID(DateTime CheckDateTime)
        {
            return accessor.GetMakeSalaryList_DisEmpID(CheckDateTime);
        }

        public void DeleteByFileName(string FileName)
        {
            accessor.DeleteByFileName(FileName);
        }
    }
}

