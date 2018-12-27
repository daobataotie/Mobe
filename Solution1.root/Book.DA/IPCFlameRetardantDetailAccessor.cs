//------------------------------------------------------------------------------
//
// file name：IPCFlameRetardantDetailAccessor.cs
// author: mayanjun
// create date：2018/12/27 13:17:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCFlameRetardantDetail
    /// </summary>
    public partial interface IPCFlameRetardantDetailAccessor : IAccessor
    {
        IList<Model.PCFlameRetardantDetail> SelectByPrimaryId(string primaryId);

        void DeleteByPrimaryId(string primaryid);

        IList<Model.PCFlameRetardantDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, string cusXOId);
    }
}
