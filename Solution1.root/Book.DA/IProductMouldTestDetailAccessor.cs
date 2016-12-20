//------------------------------------------------------------------------------
//
// file name：IProductMouldTestDetailAccessor.cs
// author: mayanjun
// create date：2010-10-4 11:45:52
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMouldTestDetail
    /// </summary>
    public partial interface IProductMouldTestDetailAccessor : IAccessor
    {
        void DeleteByProductMouldTestId(string ProductMouldTestId);
        void DeleteByMouldId(string MouldId);
    }
}

