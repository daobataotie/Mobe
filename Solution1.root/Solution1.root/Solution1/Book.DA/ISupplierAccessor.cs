//------------------------------------------------------------------------------
//
// file name：ISupplierAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Supplier
    /// </summary>
    public partial interface ISupplierAccessor : IAccessor
    {
        //void Update(Book.Model.Supplier supplier, string newId);
        //IList<Model.Supplier> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd);

        IList<Book.Model.Supplier> Select(Model.SupplierCategory supplierCategory);
    }
}

