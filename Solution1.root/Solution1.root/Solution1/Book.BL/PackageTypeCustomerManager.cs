//------------------------------------------------------------------------------
//
// file name：PackageTypeCustomerManager.cs
// author: peidun
// create date：2009-08-13 15:38:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PackageTypeCustomer.
    /// </summary>
    public partial class PackageTypeCustomerManager
    {
		
		/// <summary>
		/// Delete PackageTypeCustomer by primary key.
		/// </summary>
		public void Delete(string primaryKey)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(primaryKey);
		}

		/// <summary>
		/// Insert a PackageTypeCustomer.
		/// </summary>
        public void Insert(Model.PackageTypeCustomer packageTypeCustomer)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(packageTypeCustomer);
        }
		
		/// <summary>
		/// Update a PackageTypeCustomer.
		/// </summary>
        public void Update(Model.PackageTypeCustomer packageTypeCustomer)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(packageTypeCustomer);
        }
		
    }
}

