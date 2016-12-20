//------------------------------------------------------------------------------
//
// file name：HrAttendStatManager.cs
// author: mayanjun
// create date：2010-7-6 11:09:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.HrAttendStat.
    /// </summary>
    public partial class HrAttendStatManager
    {
		
		/// <summary>
		/// Delete HrAttendStat by primary key.
		/// </summary>
		public void Delete(string hrAttendStatId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(hrAttendStatId);
		}

		/// <summary>
		/// Insert a HrAttendStat.
		/// </summary>
        public void Insert(Model.HrAttendStat hrAttendStat)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(hrAttendStat);
        }
		
		/// <summary>
		/// Update a HrAttendStat.
		/// </summary>
        public void Update(Model.HrAttendStat hrAttendStat)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(hrAttendStat);
        }

        public Model.HrAttendStat SelectHrAttendStatByEmpidAndYearMonth(Model.Employee employee, int year, int month)
        {
            return accessor.SelectHrAttendStatByEmpidAndYearMonth(employee, year, month);
        }
    }
}

