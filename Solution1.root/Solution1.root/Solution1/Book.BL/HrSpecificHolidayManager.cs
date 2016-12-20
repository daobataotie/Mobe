//------------------------------------------------------------------------------
//
// file name：HrSpecificHolidayManager.cs
// author: mayanjun
// create date：2010-5-28 14:21:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.HrSpecificHoliday.
    /// </summary>
    public partial class HrSpecificHolidayManager
    {
		
		/// <summary>
		/// Delete HrSpecificHoliday by primary key.
		/// </summary>
		public void Delete(string hrSpecificHoliday)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(hrSpecificHoliday);
		}

		/// <summary>
		/// Insert a HrSpecificHoliday.
		/// </summary>
        public void Insert(Model.HrSpecificHoliday hrSpecificHoliday)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(hrSpecificHoliday);
        }
		
		/// <summary>
		/// Update a HrSpecificHoliday.
		/// </summary>
        public void Update(Model.HrSpecificHoliday hrSpecificHoliday)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(hrSpecificHoliday);
        }
        public  System.Data.DataSet  SelectSpecificHolidayInfo()
        {
            return accessor.SelectSpecificHolidayInfo();
        }
        public void SaveSpecificHolidayInfo(System.Data.DataTable table)
        {
            accessor.SaveSpecificHolidayInfo(table);
        }
    }
}

