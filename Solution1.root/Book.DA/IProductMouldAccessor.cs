//------------------------------------------------------------------------------
//
// file name：IProductMouldAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMould
    /// </summary>
    public partial interface IProductMouldAccessor : IAccessor
    {
        IList<Model.ProductMould> SelectProductMouldByProductMouldTestId(string ProductMouldTestId);

        IList<Model.ProductMould> SelectByDateRage(DateTime StartDate, DateTime EndDate,string MouldId,string MouldName,Model.MouldCategory mouldCategory);

        Model.ProductMould SelectByMouldId(string MouldId);
    }
}

