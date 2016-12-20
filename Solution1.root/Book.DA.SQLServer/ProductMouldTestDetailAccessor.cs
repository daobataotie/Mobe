//------------------------------------------------------------------------------
//
// file name：ProductMouldTestDetailAccessor.cs
// author: mayanjun
// create date：2010-10-4 11:45:52
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
    /// Data accessor of ProductMouldTestDetail
    /// </summary>
    public partial class ProductMouldTestDetailAccessor : EntityAccessor, IProductMouldTestDetailAccessor
    {
        public void DeleteByProductMouldTestId(string ProductMouldTestId)
        {
            sqlmapper.Delete("ProductMouldTestDetail.DeleteByProductMouldTestId", ProductMouldTestId==null?"":ProductMouldTestId);
        }

        public void DeleteByMouldId(string MouldId)
        {
            sqlmapper.Delete("ProductMouldDetail.DeleteByMouldId", MouldId);
        }

        
    }
}
