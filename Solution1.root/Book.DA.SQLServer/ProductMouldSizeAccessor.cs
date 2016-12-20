//------------------------------------------------------------------------------
//
// file name：ProductMouldSizeAccessor.cs
// author: mayanjun
// create date：2013-2-21 17:11:18
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
    /// Data accessor of ProductMouldSize
    /// </summary>
    public partial class ProductMouldSizeAccessor : EntityAccessor, IProductMouldSizeAccessor
    {
        public IList<Model.ProductMouldSize> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            return sqlmapper.QueryForList<Model.ProductMouldSize>("ProductMouldSize.SelectByDateRage", ht);
        }
    }
}
