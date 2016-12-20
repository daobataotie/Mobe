//------------------------------------------------------------------------------
//
// file name：IDepotInDetailAccessor.cs
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
    /// Interface of data accessor of dbo.DepotInDetail
    /// </summary>
    public partial interface IDepotInDetailAccessor : IAccessor
    {
        IList<Model.DepotInDetail> GetDetailByDepotInId(string depotInId);
        void Delete(Model.DepotIn depotIn);

        IList<Model.DepotInDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string InDepotIdStart, string InDepotIdEnd, string DepotIdStart, string DepotIdEnd, Model.Supplier SupplierStart, Model.Supplier SupplierEnd);
    }
}

