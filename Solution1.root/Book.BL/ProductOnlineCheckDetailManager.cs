//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheckDetailManager.cs
// author: mayanjun
// create date：2013-3-25 17:51:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductOnlineCheckDetail.
    /// </summary>
    public partial class ProductOnlineCheckDetailManager
    {

        /// <summary>
        /// Delete ProductOnlineCheckDetail by primary key.
        /// </summary>
        public void Delete(string productOnlineCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productOnlineCheckDetailId);
        }

        /// <summary>
        /// Insert a ProductOnlineCheckDetail.
        /// </summary>
        public void Insert(Model.ProductOnlineCheckDetail productOnlineCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(productOnlineCheckDetail);
        }

        /// <summary>
        /// Update a ProductOnlineCheckDetail.
        /// </summary>
        public void Update(Model.ProductOnlineCheckDetail productOnlineCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(productOnlineCheckDetail);
        }

        public IList<Model.ProductOnlineCheckDetail> SelectByProductOnlineCheckId(string ProductOnlineCheckId)
        {
            return accessor.SelectByProductOnlineCheckId(ProductOnlineCheckId);
        }
        public void DelectByProductOnlineCheckId(string ProductOnlineCheckId)
        {
            accessor.DelectByProductOnlineCheckId(ProductOnlineCheckId);
        }
    }
}

