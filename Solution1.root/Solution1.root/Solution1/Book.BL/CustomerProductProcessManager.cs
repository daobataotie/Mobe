//------------------------------------------------------------------------------
//
// file name：CustomerProductProcessManager.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProductProcess.
    /// </summary>
    public partial class CustomerProductProcessManager
    {
		
		/// <summary>
		/// Delete CustomerProductProcess by primary key.
		/// </summary>
		public void Delete(string customerProductProcessId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerProductProcessId);
		}

		/// <summary>
		/// Insert a CustomerProductProcess.
		/// </summary>
        public void Insert(Model.CustomerProductProcess customerProductProcess)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerProductProcess);
        }
		
		/// <summary>
		/// Update a CustomerProductProcess.
		/// </summary>
        public void Update(Model.CustomerProductProcess customerProductProcess)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerProductProcess);
        }

        public IList<Book.Model.CustomerProductProcess> SelectProcessCategory(Book.Model.CustomerProducts customerProducts) 
        {
            return accessor.SelectProcessCategory(customerProducts);
        }
    }
}

