//------------------------------------------------------------------------------
//
// file name：IDepotInAccessor.cs
// author: mayanjun
// create date：2010-10-25 16:14:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.DepotIn
    /// </summary>
    public partial interface IDepotInAccessor : IAccessor
    {
        IList<Model.DepotIn> SelectByDateRange(DateTime startdate, DateTime enddate);

        IList<Model.DepotIn> SelectByDateAndOther(DateTime startDate, DateTime endDate, Model.Product product, string depotInId, Model.Employee employee, Model.Employee employee0, string depotId, Model.Supplier supplier);
    }
}

