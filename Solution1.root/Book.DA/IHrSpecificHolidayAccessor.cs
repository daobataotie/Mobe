//------------------------------------------------------------------------------
//
// file name：IHrSpecificHolidayAccessor.cs
// author: mayanjun
// create date：2010-5-28 14:21:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.HrSpecificHoliday
    /// </summary>
    public partial interface IHrSpecificHolidayAccessor : IAccessor
    {
        System.Data.DataSet SelectSpecificHolidayInfo();
        void SaveSpecificHolidayInfo(System.Data.DataTable table);
        bool IsExistForHolidayDate(System.Data.DataRow dr);
    }
}

