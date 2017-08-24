//------------------------------------------------------------------------------
//
// file name：ProductClassifyDetailManager.cs
// author: mayanjun
// create date：2017-08-24 21:36:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductClassifyDetail.
    /// </summary>
    public partial class ProductClassifyDetailManager
    {

        /// <summary>
        /// Delete ProductClassifyDetail by primary key.
        /// </summary>
        public void Delete(string productClassifyDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productClassifyDetailId);
        }

        /// <summary>
        /// Insert a ProductClassifyDetail.
        /// </summary>
        public void Insert(Model.ProductClassifyDetail productClassifyDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(productClassifyDetail);
        }

        /// <summary>
        /// Update a ProductClassifyDetail.
        /// </summary>
        public void Update(Model.ProductClassifyDetail productClassifyDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(productClassifyDetail);
        }

        public IList<Book.Model.ProductClassifyDetail> SelectByHeader(Book.Model.ProductClassify productClassify)
        {
            return accessor.SelectByHeader(productClassify);
        }

        public void DeleteByHeader(string productClassifyId)
        {
            accessor.DeleteByHeader(productClassifyId);
        }

        public Model.ProductClassifyDetail GetByProductId(string productId)
        {
            return accessor.GetByProductId(productId);
        }
    }
}
