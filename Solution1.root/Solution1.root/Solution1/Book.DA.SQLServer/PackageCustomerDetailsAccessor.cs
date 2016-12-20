//------------------------------------------------------------------------------
//
// file name：PackageCustomerDetailsAccessor.cs
// author: peidun
// create date：2009-11-10 18:27:47
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
    /// Data accessor of PackageCustomerDetails
    /// </summary>
    public partial class PackageCustomerDetailsAccessor : EntityAccessor, IPackageCustomerDetailsAccessor
    {
        public IList<Model.PackageCustomerDetails> Select(string customerProductId)
		{
            return sqlmapper.QueryForList<Model.PackageCustomerDetails>("PackageCustomerDetails.select_byCustomerProduct",customerProductId);
		}
        public void Delete(Model.CustomerProducts custeomerProduct)
		{
           sqlmapper.Delete("PackageCustomerDetails.deleteByCostomerProductId",custeomerProduct.CustomerProductId);
		}
    }
}
