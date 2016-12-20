//------------------------------------------------------------------------------
//
// file name：productImageAccessor.cs
// author: mayanjun
// create date：2011-2-25 10:53:29
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
    /// Data accessor of productImage
    /// </summary>
    public partial class ProductImageAccessor : EntityAccessor, IProductImageAccessor
    {
        public bool HasRowsBefores(Model.ProductImage e)
        {
            return sqlmapper.QueryForObject<bool>("ProductImage.hasrowsbefore", e);
        }
        public bool HasRowsAfters(Model.ProductImage e)
        {
            return sqlmapper.QueryForObject<bool>("ProductImage.hasrowsafter", e);
        }
        public Model.ProductImage GetFirsts(string productId)
        {
            return sqlmapper.QueryForObject<Model.ProductImage>("ProductImage.getfirst", productId);
        }
        public Model.ProductImage GetLasts(string productId)
        {
            return sqlmapper.QueryForObject<Model.ProductImage>("ProductImage.getlast", productId);
        }
        public Model.ProductImage GetNexts(Model.ProductImage e)
        {
            return sqlmapper.QueryForObject<Model.ProductImage>("ProductImage.getnext", e);
        }
        public Model.ProductImage GetPrevs(Model.ProductImage e)
        {
            return sqlmapper.QueryForObject<Model.ProductImage>("ProductImage.getprev", e);
        }
        public void Delete(string id, string productId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ImageId", id);
            ht.Add("productId", productId);
            sqlmapper.Delete("ProductImage.mydelete", ht);
        }
        public IList<Model.ProductImage> Select(Model.Product product)
        {
            return sqlmapper.QueryForList<Model.ProductImage>("ProductImage.selectByproductId", product.ProductId);
        }
    }
}
