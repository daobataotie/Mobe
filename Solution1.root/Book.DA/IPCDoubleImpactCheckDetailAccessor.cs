//------------------------------------------------------------------------------
//
// file name：IPCDoubleImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-24 17:38:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCDoubleImpactCheckDetail
    /// </summary>
    public partial interface IPCDoubleImpactCheckDetailAccessor : IAccessor
    {
        void DeleteByPCDoubleImpactCheckId(string PCDoubleImpactCheckId);
        IList<Model.PCDoubleImpactCheckDetail> Select(string PCDoubleImpactCheckId);
    }
}

