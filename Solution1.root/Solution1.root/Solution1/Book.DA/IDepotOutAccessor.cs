//------------------------------------------------------------------------------
//
// file name：IDepotOutAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.DepotOut
    /// </summary>
    public partial interface IDepotOutAccessor : IAccessor
    {
        IList<Model.DepotOut> SelectByDateRange(DateTime startdate, DateTime enddate);
    }
}

