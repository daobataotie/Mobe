//------------------------------------------------------------------------------
//
// file name：IBGHandbookRangeDetailAccessor.cs
// author: mayanjun
// create date：2013-4-17 15:13:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookRangeDetail
    /// </summary>
    public partial interface IBGHandbookRangeDetailAccessor : IAccessor
    {
        IList<Model.BGHandbookRangeDetail> SelectByBGHandbookId(string Id, string type);

        void DeleteByBGHandbookId(string Id);
    }
}

