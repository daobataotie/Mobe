//------------------------------------------------------------------------------
//
// file name：IproductImageAccessor.cs
// author: mayanjun
// create date：2011-2-25 10:53:28
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.productImage
    /// </summary>
    public partial interface IProductImageAccessor : IAccessor
    {
        bool HasRowsBefores(Model.ProductImage e);
        bool HasRowsAfters(Model.ProductImage e);
         Model.ProductImage GetFirsts(string productId);
         Model.ProductImage GetLasts(string productId);
         Model.ProductImage GetNexts(Model.ProductImage e);
         Model.ProductImage GetPrevs(Model.ProductImage e);
         void Delete(string id, string productId);
         IList<Model.ProductImage> Select(Model.Product product);
    }
}

