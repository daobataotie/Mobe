//------------------------------------------------------------------------------
//
// file name：IAssemblySiteDifferenceDetaiAccessor.cs
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
    /// Interface of data accessor of dbo.AssemblySiteDifferenceDetai
    /// </summary>
    public partial interface IAssemblySiteDifferenceDetaiAccessor : IAccessor
    {
        void DeleteByHeaderId(string id);

        IList<Model.AssemblySiteDifferenceDetai> SelectByHeaderId(string id);

        IList<Model.AssemblySiteDifferenceDetai> SelectByDateRage(DateTime startDate, DateTime endDate, string productid);
    }
}
