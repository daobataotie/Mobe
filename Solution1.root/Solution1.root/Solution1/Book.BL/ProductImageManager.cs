//------------------------------------------------------------------------------
//
// file name：productImageManager.cs
// author: mayanjun
// create date：2011-2-25 10:53:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.productImage.
    /// </summary>
    public partial class ProductImageManager
    {

        /// <summary>
        /// Delete productImage by primary key.
        /// </summary>
        public void Delete(string imageId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(imageId);
        }

        /// <summary>
        /// Insert a productImage.
        /// </summary>
        public void Insert(Model.ProductImage productImage)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(productImage);
        }

        /// <summary>
        /// Update a productImage.
        /// </summary>
        public void Update(Model.ProductImage productImage)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(productImage);
        }

        public bool HasRowsBefores(Model.ProductImage e)
        {
            return accessor.HasRowsBefores(e);
        }
        public bool HasRowsAfters(Model.ProductImage e)
        {
            return accessor.HasRowsAfters(e);
        }
        public Model.ProductImage GetFirsts(string productId)
        {
            return accessor.GetFirsts(productId);
        }
        public Model.ProductImage GetLasts(string productId)
        {
            return accessor.GetLasts(productId);
        }
        public Model.ProductImage GetNexts(Model.ProductImage e)
        {
            return accessor.GetNexts(e);
        }
        public Model.ProductImage GetPrevs(Model.ProductImage e)
        {
            return accessor.GetPrevs(e);
        }
        public void Delete(string id, string productId)
        {
            accessor.Delete(id, productId);
        }

        public IList<Model.ProductImage> Select(Model.Product product)
        {
            return accessor.Select(product);
        }
    }
}

