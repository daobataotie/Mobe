//------------------------------------------------------------------------------
//
// file name：IProductClassifyDetailAccessor.cs
// author: mayanjun
// create date：2017-08-24 21:36:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductClassifyDetail
    /// </summary>
    public partial interface IProductClassifyDetailAccessor : IAccessor
    {
        IList<Book.Model.ProductClassifyDetail> SelectByHeader(Book.Model.ProductClassify productClassify);

        void DeleteByHeader(string productClassifyId);

        Model.ProductClassifyDetail GetByProductId(string productId);
    }
}
