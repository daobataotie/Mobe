//------------------------------------------------------------------------------
//
// file name：IBGHandbookDepotInDetailAccessor.cs
// author: mayanjun
// create date：2013/12/19 18:37:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookDepotInDetail
    /// </summary>
    public partial interface IBGHandbookDepotInDetailAccessor : IAccessor
    {
        void DeleteByBGHandbookDepotInId(string Id);

        IList<Book.Model.BGHandbookDepotInDetail> SelectByBGHandbookDepotInId(string p);
    }
}

