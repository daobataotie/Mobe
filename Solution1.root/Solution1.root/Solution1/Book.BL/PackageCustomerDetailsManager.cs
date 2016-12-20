//------------------------------------------------------------------------------
//
// file name：PackageCustomerDetailsManager.cs
// author: peidun
// create date：2009-11-10 18:27:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PackageCustomerDetails.
    /// </summary>
    public partial class PackageCustomerDetailsManager
    {

		
		/// <summary>
		/// Delete PackageCustomerDetails by primary key.
		/// </summary>
		public void Delete(string packageCustomerDetailsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(packageCustomerDetailsId);
		}

		/// <summary>
		/// Insert a PackageCustomerDetails.
		/// </summary>
        public void Insert(Model.PackageCustomerDetails packageCustomerDetails)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(packageCustomerDetails);
        }
		
		/// <summary>
		/// Update a PackageCustomerDetails.
		/// </summary>
        public void Update(Model.PackageCustomerDetails packageCustomerDetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(packageCustomerDetails);
        }
        public IList<Model.PackageCustomerDetails> Select(string customerProductId)
        {

            return accessor.Select(customerProductId);
        }
        public void Delete(Model.CustomerProducts custeomerProduct)
        {
            accessor.Delete(custeomerProduct);
        }
    }
}

