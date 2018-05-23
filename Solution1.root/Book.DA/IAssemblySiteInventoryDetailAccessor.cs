//------------------------------------------------------------------------------
//
// file name：IAssemblySiteInventoryDetailAccessor.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AssemblySiteInventoryDetail
    /// </summary>
    public partial interface IAssemblySiteInventoryDetailAccessor : IAccessor
    {
        void DeleteByHeaderId(string id);

        IList<Model.AssemblySiteInventoryDetail> SelectByHeaderId(string id);

        IList<Model.AssemblySiteInventoryDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, bool state);
    }
}
