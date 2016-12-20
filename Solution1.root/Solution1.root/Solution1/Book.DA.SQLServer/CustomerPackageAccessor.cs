//------------------------------------------------------------------------------
//
// file name：CustomerPackageAccessor.cs
// author: peidun
// create date：2010-2-4 11:15:13
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
    /// Data accessor of CustomerPackage
    /// </summary>
    public partial class CustomerPackageAccessor : EntityAccessor, ICustomerPackageAccessor
    {
        public IList<Model.CustomerPackage> Select(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.CustomerPackage>("CustomerPackage.select_bycustom", customer==null?null:customer.CustomerId);
        }
    }
}
