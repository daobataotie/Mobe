//------------------------------------------------------------------------------
//
// file name：ISupplierProcesscategoryAccessor.cs
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
    /// Interface of data accessor of dbo.SupplierProcesscategory
    /// </summary>
    public partial interface ISupplierProcesscategoryAccessor : IAccessor
    {
        Model.SupplierProcesscategory mGetFirst(string SupplierId);

        Model.SupplierProcesscategory mGetLast(string SupplierId);

        Model.SupplierProcesscategory mGetPrev(DateTime InsertDate, string SupplierId);

        Model.SupplierProcesscategory mGetNext(DateTime InsertDate, string SupplierId);

        bool mHasRows(string SupplierId);

        bool mHasRowsBefore(DateTime InsertDate, string SupplierId);

        bool mHasRowsAfter(DateTime InsertDate, string SupplierId);

        System.Data.DataTable SelectSupplierProduct(string SupplierId);

        IList<Model.SupplierProcesscategory> SelectBySupAndProc(string SupplierId);

        IList<Model.SupplierProcesscategory> mSelect(string SupplierId);

        string GetPriceRangeForSupAndProduct(string SupplierId, string ProductId);
    }
}

