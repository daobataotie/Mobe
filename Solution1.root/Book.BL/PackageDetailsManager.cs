//------------------------------------------------------------------------------
//
// file name：PackageDetailsManager.cs
// author: peidun
// create date：2009-08-12 9:45:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PackageDetails.
    /// </summary>
    public partial class PackageDetailsManager : BaseManager
    {
        private static readonly DA.IPackageDetailsAccessor packageDetailsAccessor = (DA.IPackageDetailsAccessor)Accessors.Get("PackageDetailsAccessor");
		
		/// <summary>
		/// Delete PackageDetails by primary key.
		/// </summary>
		public void Delete(string packageDetailsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(packageDetailsId);
		}
        public void Delete(Model.PackageDetails packagedetails)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(packagedetails.PackageDetailsId);
        }
		/// <summary>
		/// Insert a PackageDetails.
		/// </summary>
        public void Insert(Model.PackageDetails packageDetails)
        {
			//
			// todo:add other logic here
			//
            try
            {
                V.BeginTransaction();
                packageDetails.PackageDetailsId = Guid.NewGuid().ToString();
                accessor.Insert(packageDetails);
                //packageType.CreateDate = DateTime.Now;
                //packageType.InsertTime = DateTime.Now;                

                //packageType.PackageTypeId = Guid.NewGuid().ToString();
                //accessor.Insert(packageType);

                //foreach (Model.PackageDetails details in packageType.PruoductsDetails)
                //{
                //    if (string.IsNullOrEmpty(details.PackageDetailsId))
                //    {
                //        details.PackageDetailsId = Guid.NewGuid().ToString();
                //    }
                //    details.PackageTypeId = packageType.PackageTypeId;
                //    details.ProductId = details.Product.ProductId;

                //    packageDetailsAccessor.Insert(details);
                //}

                //foreach (Model.PackageTypeCustomer packageCustomer in packageType.CustomerDetails)
                //{
                //    if (string.IsNullOrEmpty(packageCustomer.PrimaryKey))
                //    {
                //        packageCustomer.PrimaryKey = Guid.NewGuid().ToString();
                //    }
                //    packageCustomer.CustomerId = packageCustomer.Customer.CustomerId;
                //    packageCustomer.PackageTypeId = packageType.PackageTypeId;

                //    packageTypeCustomerAccessor.Insert(packageCustomer);
                //}
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a PackageType.
        /// </summary>

		
		/// <summary>
		/// Update a PackageDetails.
		/// </summary>
        public void Update(Model.PackageDetails packageDetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(packageDetails);
           
        }
       
		
    }
}

