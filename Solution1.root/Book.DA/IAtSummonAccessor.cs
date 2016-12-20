//------------------------------------------------------------------------------
//
// file name：IAtSummonAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtSummon
    /// </summary>
    public partial interface IAtSummonAccessor : IAccessor
    {
        IList<Book.Model.AtSummon> SelectByDateRage(DateTime startdate, DateTime enddate);

        IList<Model.AtSummon> SelectByCondition(DateTime startDate, DateTime endDate, string startId, string endId);
    }
}

