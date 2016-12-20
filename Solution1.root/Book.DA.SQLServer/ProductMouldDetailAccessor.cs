//------------------------------------------------------------------------------
//
// file name：ProductMouldDetailAccessor.cs
// author: mayanjun
// create date：2010-10-4 10:19:39
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
    /// Data accessor of ProductMouldDetail
    /// </summary>
    public partial class ProductMouldDetailAccessor : EntityAccessor, IProductMouldDetailAccessor
    {
        public void Delete(Model.Product Product)
        {
            sqlmapper.Delete("ProductMouldDetail.delete_byProductId", Product.ProductId);
        }

        public void DeleteByMouldId(string MouldId) 
        {
            sqlmapper.Delete("ProductMouldDetail.DeleteByMouldId", MouldId);   
        }

        public IList<Model.ProductMouldDetail>  Select(Model.Product Product)
        {
            return  sqlmapper.QueryForList<Model.ProductMouldDetail>("ProductMouldDetail.select_byProductId", Product.ProductId);
        }
    }
}
