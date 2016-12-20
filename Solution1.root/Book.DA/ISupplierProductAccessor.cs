//------------------------------------------------------------------------------
//
// file name：ISupplierProductAccessor.cs
// author: mayanjun
// create date：2012-8-30 17:02:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.SupplierProduct
    /// </summary>
    public partial interface ISupplierProductAccessor : IAccessor
    {
        Model.SupplierProduct mGetFirst(string SupplierId);

        Model.SupplierProduct mGetLast(string SupplierId);

        Model.SupplierProduct mGetPrev(DateTime InsertDate, string SupplierId);

        Model.SupplierProduct mGetNext(DateTime InsertDate, string SupplierId);

        bool mHasRows(string SupplierId);

        bool mHasRowsBefore(DateTime InsertDate, string SupplierId);

        bool mHasRowsAfter(DateTime InsertDate, string SupplierId);

        IList<Model.SupplierProduct> mSelect(string SupplierId);

        string GetPriceRangeForSupAndProduct(string SupplierId, string ProductId);

        System.Data.DataTable SelectALLRefProduct(string ProductId);
    }
}

