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

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.DepotOutDetail
    /// </summary>
    public partial interface IDepotOutDetailAccessor : IAccessor
    {
        IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId);
        void Delete(Model.DepotOut depotOut);
    }
}

