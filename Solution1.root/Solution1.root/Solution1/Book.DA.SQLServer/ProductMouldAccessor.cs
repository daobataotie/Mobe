//------------------------------------------------------------------------------
//
// file name：ProductMouldAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:43
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
    /// Data accessor of ProductMould
    /// </summary>
    public partial class ProductMouldAccessor : EntityAccessor, IProductMouldAccessor
    {
        public IList<Model.ProductMould> SelectProductMouldByProductMouldTestId(string ProductMouldTestId)
        {
            return sqlmapper.QueryForList<Model.ProductMould>("ProductMould.SelectProductMouldByProductMouldTestId", ProductMouldTestId);
        }
    }
}
