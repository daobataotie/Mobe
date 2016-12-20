//------------------------------------------------------------------------------
//
// file name：IBGHandbookRangeAccessor.cs
// author: mayanjun
// create date：2013-4-17 15:13:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookRange
    /// </summary>
    public partial interface IBGHandbookRangeAccessor : IAccessor
    {
        IList<Model.BGHandbookRange> SelectByDate(DateTime startDate, DateTime endDate);
    }
}

