//------------------------------------------------------------------------------
//
// file name：IBGProductDepotOutDetailAccessor.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGProductDepotOutDetail
    /// </summary>
    public partial interface IBGProductDepotOutDetailAccessor : IAccessor
    {
        IList<Book.Model.BGProductDepotOutDetail> SelectByBGProductDepotOutId(string bGProductDepotOutId);
        double SumQuantityByHandbook(string bGHandbookId, string bGHandbookProductId);
    }
}

