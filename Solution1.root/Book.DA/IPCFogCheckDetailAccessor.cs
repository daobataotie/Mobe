//------------------------------------------------------------------------------
//
// file name：IPCFogCheckDetailAccessor.cs
// author: mayanjun
// create date：2012-3-17 09:38:33
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCFogCheckDetail
    /// </summary>
    public partial interface IPCFogCheckDetailAccessor : IAccessor
    {
        IList<Model.PCFogCheckDetail> Select(string pcfcid);

        void DeleteByHeaderId(string headerid);
    }
}

