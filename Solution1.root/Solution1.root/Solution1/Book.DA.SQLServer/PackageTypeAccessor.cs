//------------------------------------------------------------------------------
//
// file name：PackageTypeAccessor.cs
// author: peidun
// create date：2009-08-12 9:45:12
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
    /// Data accessor of PackageType
    /// </summary>
    public partial class PackageTypeAccessor : EntityAccessor, IPackageTypeAccessor
    {

        public IList<Book.Model.PackageType> Select(Book.Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.PackageType>("PackageType.all_byCustomer", customer.CustomerId);
        }

    }
}
