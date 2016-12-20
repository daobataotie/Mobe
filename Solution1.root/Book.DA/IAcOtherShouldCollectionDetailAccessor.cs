//------------------------------------------------------------------------------
//
// file name：IAcOtherShouldCollectionDetailAccessor.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcOtherShouldCollectionDetail
    /// </summary>
    public partial interface IAcOtherShouldCollectionDetailAccessor : IAccessor
    {
        IList<Model.AcOtherShouldCollectionDetail> Select(Model.AcOtherShouldCollection acOtherShouldCollection);
    }
}

