//------------------------------------------------------------------------------
//
// file name：ProductClassifyDetailAccessor.cs
// author: mayanjun
// create date：2017-08-24 21:36:04
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
    /// Data accessor of ProductClassifyDetail
    /// </summary>
    public partial class ProductClassifyDetailAccessor : EntityAccessor, IProductClassifyDetailAccessor
    {
        public IList<Book.Model.ProductClassifyDetail> SelectByHeader(Book.Model.ProductClassify productClassify)
        {
            return sqlmapper.QueryForList<Model.ProductClassifyDetail>("ProductClassifyDetail.SelectByHeader", productClassify.ProductClassifyId);
        }

        public void DeleteByHeader(string productClassifyId)
        {
            sqlmapper.Delete("ProductClassifyDetail.DeleteByHeader", productClassifyId);
        }

        public Model.ProductClassifyDetail GetByProductId(string productId)
        {
            return sqlmapper.QueryForObject<Model.ProductClassifyDetail>("ProductClassifyDetail.GetByProductId", productId);
        }
    }
}
