//------------------------------------------------------------------------------
//
// file name：ISupplierContactAccessor.cs
// author: peidun
// create date：2009-08-06 14:53:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.SupplierContact
    /// </summary>
    public partial interface ISupplierContactAccessor : IAccessor
    {
        IList<Book.Model.SupplierContact> Select(Book.Model.Supplier supplier);

        void Delete(Book.Model.Supplier supplier);
    }
}

