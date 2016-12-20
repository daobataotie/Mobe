//------------------------------------------------------------------------------
//
// file name：IPCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-15 14:09:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCImpactCheckDetail
    /// </summary>
    public partial interface IPCImpactCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.PCImpactCheckDetail> Select(string PCImpactCheckId);
        void DeleteByPCImpactCheckId(string PCImpactCheckId);
    }
}

