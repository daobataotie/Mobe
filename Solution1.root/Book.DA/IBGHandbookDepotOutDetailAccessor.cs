//------------------------------------------------------------------------------
//
// file name：IBGHandbookDepotOutDetailAccessor.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbookDepotOutDetail
    /// </summary>
    public partial interface IBGHandbookDepotOutDetailAccessor : IAccessor
    {
        IList<Model.BGHandbookDepotOutDetail> SelectByBGHandbookDepotOutId(string bGHandbookDepotOutId);

        void DeleteByBGHandbookDepotOutId(string bGHandbookDepotOutId);
    }
}

