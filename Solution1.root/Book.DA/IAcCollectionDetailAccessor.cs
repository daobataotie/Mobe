//------------------------------------------------------------------------------
//
// file name：IAcCollectionDetailAccessor.cs
// author: mayanjun
// create date：2011-6-23 09:29:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcCollectionDetail
    /// </summary>
    public partial interface IAcCollectionDetailAccessor : IAccessor
    {
        void DeleteByAccid(string accid);
        IList<Model.AcCollectionDetail> Select(Model.AcCollection acCollection);
    }
}

