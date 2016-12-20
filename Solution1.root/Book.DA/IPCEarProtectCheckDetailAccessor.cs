//------------------------------------------------------------------------------
//
// file name：IPCEarProtectCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarProtectCheckDetail
    /// </summary>
    public partial interface IPCEarProtectCheckDetailAccessor : IAccessor
    {
        IList<Model.PCEarProtectCheckDetail> SelectByPCEarProtectCheckId(string PCEarProtectCheckId);
        void DeleteByPCEarProtectCheckId(string PCEarProtectCheckId);
    }
}

