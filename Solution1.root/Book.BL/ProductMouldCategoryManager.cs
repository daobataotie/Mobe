//------------------------------------------------------------------------------
//
// file name：ProductMouldCategoryManager.cs
// author: mayanjun
// create date：2013-3-7 14:17:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldCategory.
    /// </summary>
    public partial class ProductMouldCategoryManager
    {

        /// <summary>
        /// Delete ProductMouldCategory by primary key.
        /// </summary>
        public void Delete(string productMouldCategoryId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productMouldCategoryId);
        }

        /// <summary>
        /// Insert a ProductMouldCategory.
        /// </summary>
        public void Insert(Model.ProductMouldCategory productMouldCategory)
        {
            //
            // todo:add other logic here
            //
            Validate(productMouldCategory);
            productMouldCategory.InsertTime = DateTime.Now;
            productMouldCategory.UpdateTime = DateTime.Now;
            accessor.Insert(productMouldCategory);
        }

        /// <summary>
        /// Update a ProductMouldCategory.
        /// </summary>
        public void Update(Model.ProductMouldCategory productMouldCategory)
        {
            //
            // todo: add other logic here.
            //
            Validate(productMouldCategory);
            productMouldCategory.UpdateTime = DateTime.Now;
            accessor.Update(productMouldCategory);
        }

        private void Validate(Model.ProductMouldCategory model)
        {
            if (string.IsNullOrEmpty(model.Id))
                throw new Helper.InvalidValueException(Model.ProductMouldCategory.PRO_Id);
            if (string.IsNullOrEmpty(model.CategoryName))
                throw new Helper.InvalidValueException(Model.ProductMouldCategory.PRO_CategoryName);
        }
    }
}

