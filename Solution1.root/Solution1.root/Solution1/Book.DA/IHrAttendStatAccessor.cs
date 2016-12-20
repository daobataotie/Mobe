//------------------------------------------------------------------------------
//
// file name：IHrAttendStatAccessor.cs
// author: mayanjun
// create date：2010-7-6 11:09:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.HrAttendStat
    /// </summary>
    public partial interface IHrAttendStatAccessor : IAccessor
    {
        Model.HrAttendStat SelectHrAttendStatByEmpidAndYearMonth(Model.Employee employee, int year, int month);
    }
}

