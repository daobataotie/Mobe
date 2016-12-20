//------------------------------------------------------------------------------
//
// file name:ProductCategoryAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of ProductCategory
    /// </summary>
    public partial class ProductCategoryAccessor : EntityAccessor, IProductCategoryAccessor
    {

        public bool ExistsName(string productCategoryName, string ProductCategoryId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductCategoryName", productCategoryName);
            ht.Add("ProductCategoryId", ProductCategoryId);
            return sqlmapper.QueryForObject<bool>("ProductCategory.existsName", ht);

        }

        public IList<string> SelectALLName()
        {
            return sqlmapper.QueryForList<string>("ProductCategory.SelectALLName", null);
        }
    }
}
