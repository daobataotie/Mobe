//------------------------------------------------------------------------------
//
// file name：IPCFirstOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCFirstOnlineCheckDetail
    /// </summary>
    public partial interface IPCFirstOnlineCheckDetailAccessor : IAccessor
    {
        IList<Model.PCFirstOnlineCheckDetail> SelectByHeaderId(string pCFirstOnlineCheckId);

        void DelectByHeaderId(string pCFirstOnlineCheckId);

        IList<Model.PCFirstOnlineCheckDetail> SelectByCondition(DateTime startDate, DateTime endDate, string CustomerInvoiceXOId);
    }
}
