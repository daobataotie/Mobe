//------------------------------------------------------------------------------
//
// file name：IProductMouldSizeAccessor.cs
// author: mayanjun
// create date：2013-2-21 17:11:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMouldSize
    /// </summary>
    public partial interface IProductMouldSizeAccessor : IAccessor
    {
        IList<Model.ProductMouldSize> SelectByDateRage(DateTime StartDate, DateTime EndDate);
    }
}

