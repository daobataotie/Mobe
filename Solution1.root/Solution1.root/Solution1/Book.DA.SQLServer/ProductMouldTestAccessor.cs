//------------------------------------------------------------------------------
//
// file name：ProductMouldTestAccessor.cs
// author: mayanjun
// create date：2010-9-24 16:24:34
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
    /// Data accessor of ProductMouldTest
    /// </summary>
    public partial class ProductMouldTestAccessor : EntityAccessor, IProductMouldTestAccessor
    {
        public bool IsExistId(Model.ProductMouldTest test)
        {
            Hashtable ht = new Hashtable();
            ht.Add("tid", test.ProductMouldTestId);
            ht.Add("id", test.Id);
            return sqlmapper.QueryForObject<bool>("ProductMouldTest.IsExistId", ht);
        }
    }
}
