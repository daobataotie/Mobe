//------------------------------------------------------------------------------
//
// file name：PackageTypeCustomerAccessor.cs
// author: peidun
// create date：2009-08-13 15:38:47
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
    /// Data accessor of PackageTypeCustomer
    /// </summary>
    public partial class PackageTypeCustomerAccessor : EntityAccessor, IPackageTypeCustomerAccessor
    {
        #region IPackageTypeCustomerAccessor Members


        public IList<Book.Model.PackageTypeCustomer> Select(Book.Model.PackageType type)
        {
            return sqlmapper.QueryForList<Model.PackageTypeCustomer>("PackageTypeCustomer.select_by_packageTypeId", type.PackageTypeId);
        }

        public void Delete(Book.Model.PackageType packageType)
        {
            sqlmapper.Delete("PackageTypeCustomer.delete_by_packagetypeid", packageType.PackageTypeId);
        }

        #endregion
    }
}
