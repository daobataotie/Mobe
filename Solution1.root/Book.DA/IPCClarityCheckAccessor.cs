//------------------------------------------------------------------------------
//
// file name：IPCClarityCheckAccessor.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCClarityCheck
    /// </summary>
    public partial interface IPCClarityCheckAccessor : IAccessor
    {
        IList<Book.Model.PCClarityCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate);
    }
}

