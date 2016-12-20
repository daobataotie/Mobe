//------------------------------------------------------------------------------
//
// file name：IProductCategoryAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductCategory
    /// </summary>
    public partial interface IProductCategoryAccessor : IEntityAccessor
    {
        bool ExistsPrimary(string productCategoryId);
        bool ExistsName(string productCategoryName, string ProductCategoryId);

        IList<string> SelectALLName();
    }
}

