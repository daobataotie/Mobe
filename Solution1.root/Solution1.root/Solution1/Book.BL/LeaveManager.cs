//------------------------------------------------------------------------------
//
// file name：LeaveManager.cs
// author: peidun
// create date：2010-3-16 16:05:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Leave.
    /// </summary>
    public partial class LeaveManager
    {
		
		/// <summary>
		/// Delete Leave by primary key.
		/// </summary>
		public void Delete(string leaveId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(leaveId);
		}
        public void Delete(Model.Leave Leave)
        {
            //
            // todo:add other logic here
            //
            this.Delete(Leave.LeaveId);
        }

		/// <summary>
		/// Insert a Leave.
		/// </summary>
        public void Insert(Model.Leave leave)
        {
			//
			// todo:add other logic here
			//
            Validate(leave);
            accessor.Insert(leave);
        }
		
		/// <summary>
		/// Update a Leave.
		/// </summary>
        public void Update(Model.Leave leave)
        {
			//
			// todo: add other logic here.
			//
            Validate(leave);
            accessor.Update(leave);
        }
        public IList<Model.Leave> Getleavebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
           return  accessor.Getleavebyempiddate(empid, starttime, endtime);
        }
        private void Validate(Model.Leave leave)
        {
            if (string.IsNullOrEmpty(leave.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.Leave.PROPERTY_EMPLOYEEID);
            }
        }
        public   System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, DateTime leaveDate)
        {
            return accessor.GetLeaveInfoByEmployeeId(employeeId, leaveDate);
        }

        public System.Data.DataSet GetLeaveInfoByEmployeeId(string employeeId, string year)
        {
            return accessor.GetLeaveInfoByEmployeeId(employeeId, year);
        }
        /// <summary>
        /// 月总请假总基数 包括无薪假，病假
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectCountByMonthEmp(Model.Employee employee, int year, int month)
        { 
             return accessor.SelectCountByMonthEmp( employee, year,  month);
        }
        /// <summary>
        /// 倒扣款总基数和
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectPunishByMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectPunishByMonthEmp(employee, year, month);
        }
        /// <summary>
        /// 出勤记录中  月实际假日总数 不包括劳动，清明等
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public decimal SelectTotalHolidayMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectTotalHolidayMonthEmp(employee, year, month);
        }
        /// <summary>
        /// 出勤月记录中 假日(劳动 清明 等)总数,不包含周日等年假
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public int SelectSpecificHolidayMonthEmp(Model.Employee employee, int year, int month)
        {
            return accessor.SelectSpecificHolidayMonthEmp(employee, year, month);        
        }


        public IList<Model.Leave> Getleavebyempidmonth(string empid, int year, int month)
        {
            return accessor.Getleavebyempidmonth(empid, year, month);
        }
    }
}

