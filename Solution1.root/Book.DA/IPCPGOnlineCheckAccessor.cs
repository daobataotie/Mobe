//------------------------------------------------------------------------------
//
// file name：IPCPGOnlineCheckAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:19:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCPGOnlineCheck
    /// </summary>
    public partial interface IPCPGOnlineCheckAccessor : IAccessor
    {
        IList<Book.Model.PCPGOnlineCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId, string StartPronoteHeader, string EndPronoteHeader);
    }
}

