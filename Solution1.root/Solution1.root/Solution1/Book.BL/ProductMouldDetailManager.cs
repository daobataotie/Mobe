//------------------------------------------------------------------------------
//
// file name：ProductMouldDetailManager.cs
// author: mayanjun
// create date：2010-10-4 10:19:29
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldDetail.
    /// </summary>
    public partial class ProductMouldDetailManager
    {
		
		/// <summary>
		/// Delete ProductMouldDetail by primary key.
		/// </summary>
		public void Delete(string productMouldDetail)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(productMouldDetail);
		}

		/// <summary>
		/// Insert a ProductMouldDetail.
		/// </summary>
        public void Insert(Model.ProductMouldDetail productMouldDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(productMouldDetail);
        }
		
		/// <summary>
		/// Update a ProductMouldDetail.
		/// </summary>
        public void Update(Model.ProductMouldDetail productMouldDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(productMouldDetail);
        }
        public void Delete(Model.Product Product)
        {
            accessor.Delete(Product);
        }
        public IList<Model.ProductMouldDetail> Select(Model.Product Product)
        {
            return accessor.Select(Product);
        }
    }
}

