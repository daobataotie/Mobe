//------------------------------------------------------------------------------
//
// file name：IPCMouldOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCMouldOnlineCheckDetail
    /// </summary>
    public partial interface IPCMouldOnlineCheckDetailAccessor : IAccessor
    {
        void DeleteByHeaderId(string id);
        IList<Model.PCMouldOnlineCheckDetail> SelectByCondition(DateTime OnlineDateStart, DateTime OnlineDateEnd, DateTime CheckDateStart, DateTime CheckDateEnd, string productId, string invoiceCusId);
        IList<Model.PCMouldOnlineCheckDetail> SelectByInvoiceCusId(string invoiceCusId);
        IList<Model.PCMouldOnlineCheckDetail> SelectByHeaderId(string id);
    }
}
