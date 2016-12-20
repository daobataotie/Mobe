//------------------------------------------------------------------------------
//
// file name：CustomerProductsBomManager.cs
// author: peidun
// create date：2009-10-13 上午 11:45:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProductsBom.
    /// </summary>
    public partial class CustomerProductsBomManager
    {
		
		/// <summary>
		/// Delete CustomerProductsBom by primary key.
		/// </summary>
		public void Delete(string priamryKeyId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(priamryKeyId);
		}

		/// <summary>
		/// Insert a CustomerProductsBom.
		/// </summary>
        public void Insert(Model.CustomerProductsBom customerProductsBom)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerProductsBom);
        }
		
		/// <summary>
		/// Update a CustomerProductsBom.
		/// </summary>
        public void Update(Model.CustomerProductsBom customerProductsBom)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerProductsBom);
        }
    }
}

