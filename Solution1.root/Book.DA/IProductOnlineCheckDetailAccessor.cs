//------------------------------------------------------------------------------
//
// file name：IProductOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-3-25 17:51:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductOnlineCheckDetail
    /// </summary>
    public partial interface IProductOnlineCheckDetailAccessor : IAccessor
    {
        IList<Model.ProductOnlineCheckDetail> SelectByProductOnlineCheckId(string ProductOnlineCheckId);
        void DelectByProductOnlineCheckId(string ProductOnlineCheckId);
    }
}

