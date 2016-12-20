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
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProductMouldTest
    /// </summary>
    public partial interface IProductMouldTestAccessor : IAccessor
    {
        bool IsExistId(Model.ProductMouldTest test);
        IList<Book.Model.ProductMouldTest> SelectByDateRage(DateTime StartDate, DateTime EndDate);

        Model.ProductMouldTest SelectByMouldId(string MouldId);

        //void DeleteByMouldId(string MouldId);

        //DataTable SelectOrderByMouldId();

        IList<Model.ProductMouldTest> SelectOrderByMouldId();
    }
}

