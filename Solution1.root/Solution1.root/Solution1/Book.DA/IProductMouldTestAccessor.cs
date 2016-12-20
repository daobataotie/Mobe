//------------------------------------------------------------------------------
//
// file name：IProductMouldTestAccessor.cs
// author: mayanjun
// create date：2010-9-24 16:24:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMouldTest
    /// </summary>
    public partial interface IProductMouldTestAccessor : IAccessor
    {
        bool IsExistId(Model.ProductMouldTest test);
    }
}

