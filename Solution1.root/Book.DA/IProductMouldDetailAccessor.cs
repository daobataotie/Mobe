//------------------------------------------------------------------------------
//
// file name：IProductMouldDetailAccessor.cs
// author: mayanjun
// create date：2010-10-4 10:19:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMouldDetail
    /// </summary>
    public partial interface IProductMouldDetailAccessor : IAccessor
    {
        void Delete(Model.Product Product);
        void DeleteByMouldId(string MouldId);
        IList<Model.ProductMouldDetail> Select(Model.Product Product);
    }
}

