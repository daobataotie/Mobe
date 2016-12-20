//------------------------------------------------------------------------------
//
// file name：SupplierContactAccessor.cs
// author: peidun
// create date：2009-08-06 14:53:57
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of SupplierContact
    /// </summary>
    public partial class SupplierContactAccessor : EntityAccessor, ISupplierContactAccessor
    {
        #region ISupplierContactAccessor Members


        public IList<Book.Model.SupplierContact> Select(Book.Model.Supplier supplier)
        {
            if (supplier == null)
                return (IList<Book.Model.SupplierContact>)new List<Model.SupplierContact>();
            return sqlmapper.QueryForList<Model.SupplierContact>("SupplierContact.selectbysupplier", supplier.SupplierId);
        }

        public void Delete(Book.Model.Supplier supplier)
        {
            sqlmapper.Delete("SupplierContact.delete_by_supplierid", supplier.SupplierId);
        }

        #endregion
    }
}
