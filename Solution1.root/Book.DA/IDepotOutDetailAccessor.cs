//------------------------------------------------------------------------------
//
// file name：IDepotOutDetailAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.DepotOutDetail
    /// </summary>
    public partial interface IDepotOutDetailAccessor : IAccessor
    {
        IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId);
        void Delete(Model.DepotOut depotOut);

        IList<Model.DepotOutDetail> SelectByCondition(DateTime startDate, DateTime endDate, string DepotOutIdStart, string DepotOutIdEnd, string depotStart, string depotEnd);

        DataTable SelectOutAndInDepot(DateTime startDate, DateTime endDate, string depotStart, string depotEnd, string productCategoryStart, string productCategoryEnd, string ProductIdStart, string ProductIdEnd);

        IList<Model.DepotOutDetail> SelectByDepotOutId(string id);

        IList<Model.DepotOutDetail> SelectByDateRange(DateTime startDate, DateTime endDate, string productid, string invoiceCusId ,string depotId);
    }
}

