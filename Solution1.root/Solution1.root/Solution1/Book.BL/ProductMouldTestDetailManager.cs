//------------------------------------------------------------------------------
//
// file name：ProductMouldTestDetailManager.cs
// author: mayanjun
// create date：2010-10-4 11:45:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldTestDetail.
    /// </summary>
    public partial class ProductMouldTestDetailManager
    {
		
		/// <summary>
		/// Delete ProductMouldTestDetail by primary key.
		/// </summary>
		public void Delete(string productMouldTestDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(productMouldTestDetailId);
		}

		/// <summary>
		/// Insert a ProductMouldTestDetail.
		/// </summary>
        public void Insert(Model.ProductMouldTestDetail productMouldTestDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(productMouldTestDetail);
        }
		
		/// <summary>
		/// Update a ProductMouldTestDetail.
		/// </summary>
        public void Update(Model.ProductMouldTestDetail productMouldTestDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(productMouldTestDetail);
        }

        public void DeleteByProductMouldTestId(string ProductMouldTestId)
        {
            accessor.DeleteByProductMouldTestId(ProductMouldTestId);
        }
    }
}

