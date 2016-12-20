﻿//------------------------------------------------------------------------------
//
// file name：IAnnualHolidayAccessor.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AnnualHoliday
    /// </summary>
    public partial interface IAnnualHolidayAccessor : IAccessor
    {
        System.Data.DataSet  SelectAllAnnualInfo();
        void SaveAnnualInfo(System.Data.DataTable table, string years);
        //只比较年和月
        System.Data.DataSet SelectSingleAnnualInfo(DateTime HolidayDate);
        //比较年月日
        System.Data.DataSet SelectAnnualInfoByDueDate(DateTime dueDate);
        int SelectCountByMonth(int year, int month);
        System.Data.DataSet SelectAnnualInfoByYear(int year);
        bool ExistsAutoYear(string years);
    }
}

