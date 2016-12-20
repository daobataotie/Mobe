//------------------------------------------------------------------------------
//
// file name：IANSIPCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-23 09:50:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ANSIPCImpactCheckDetail
    /// </summary>
    public partial interface IANSIPCImpactCheckDetailAccessor : IAccessor
    {
        IList<Model.ANSIPCImpactCheckDetail> Select(string aNSIPCImpactCheckID);
        void DeleteByANSIPCICId(string ANSIPCICId);
    }
}

