//------------------------------------------------------------------------------
//
// file name：IBGHandbookIdSetAccessor.cs
// author: mayanjun
// create date：2013-07-05 11:57:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookIdSet
    /// </summary>
    public partial interface IBGHandbookIdSetAccessor : IAccessor
    {
        IList<Model.BGHandbookIdSet> SelectHasUsing();

        IList<string> SelectBGHandbookId();
    }
}

