//------------------------------------------------------------------------------
//
// file name：IPCClarityCheckDetailAccessor.cs
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
    /// Interface of data accessor of dbo.PCClarityCheckDetail
    /// </summary>
    public partial interface IPCClarityCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.PCClarityCheckDetail> SelectByPCClarityCheckId(string PCClarityCheckId);

        void DeleteByPCClarityCheckID(string PCCalrityCheckId);
    }
}

