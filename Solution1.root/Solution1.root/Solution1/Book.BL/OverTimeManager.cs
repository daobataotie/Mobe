//------------------------------------------------------------------------------
//
// file name：OverTimeManager.cs
// author: peidun
// create date：2010-3-20 11:59:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.OverTime.
    /// </summary>
    public partial class OverTimeManager:BaseManager
    {
		
		/// <summary>
		/// Delete OverTime by primary key.
		/// </summary>
		public void Delete(Model.OverTime  overtime)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(overtime.OverTimeId);
		}

		/// <summary>
		/// Insert a OverTime.
		/// </summary>
        public void Insert(Model.OverTime overTime)
        {
			//
			// todo:add other logic here
			//
            Validate(overTime);
            overTime.InsertTime = DateTime.Now;
            accessor.Insert(overTime);

        }
		
		/// <summary>
		/// Update a OverTime.
		/// </summary>
        public void Update(Model.OverTime overTime)
        {
			//
			// todo: add other logic here.
			//
            Validate(overTime);
            overTime.UpdateTime = DateTime.Now;
            accessor.Update(overTime);
        }
        private void Validate(Model.OverTime overtime)
        {
            if (string.IsNullOrEmpty(overtime.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.OverTime.PROPERTY_EMPLOYEEID);
            }
        }

        public IList<Model.OverTime> GetOverTimebyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.GetOverTimebyempiddate(empid, starttime, endtime);
        }
        public System.Data.DataSet SelectOverTimeInfoByEmployeeId(string employeeId, DateTime dueDate)
        {
            return accessor.SelectOverTimeInfoByEmployeeId(employeeId, dueDate);
        }
        public IList<Model.OverTime> SelectByEmployeeAndMonth(Model.Employee employee, int year, int month)
        {
            return accessor.SelectByEmployeeAndMonth(employee, year, month);        
    
        }


        //根据日期
        public System.Data.DataSet selectOverTimebyDate(string IDNo,int year, int month)
        {
            return accessor.selectOverTimebyDate(IDNo,year, month);
        }

        //查询所有
        public System.Data.DataSet selectOverTime()
        {
            return accessor.selectOverTime();
        }
    }
}

