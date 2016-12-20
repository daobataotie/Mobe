//------------------------------------------------------------------------------
//
// file name：CustomerProductStockManager.cs
// author: peidun
// create date：2009-10-10 下午 02:55:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProductStock.
    /// </summary>
    public partial class CustomerProductStockManager
    {
		
		/// <summary>
		/// Delete CustomerProductStock by primary key.
		/// </summary>
		public void Delete(string stockId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(stockId);
		}

		/// <summary>
		/// Insert a CustomerProductStock.
		/// </summary>
        public void Insert(Model.CustomerProductStock customerProductStock)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerProductStock);
        }
		
		/// <summary>
		/// Update a CustomerProductStock.
		/// </summary>
        public void Update(Model.CustomerProductStock customerProductStock)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerProductStock);
        }
    }
}

