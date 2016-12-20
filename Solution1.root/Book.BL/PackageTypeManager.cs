//------------------------------------------------------------------------------
//
// file name：PackageTypeManager.cs
// author: peidun
// create date：2009-08-12 9:45:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PackageType.
    /// </summary>
    public partial class PackageTypeManager : BaseManager
    {
        private static readonly DA.IPackageDetailsAccessor packageDetailsAccessor = (DA.IPackageDetailsAccessor)Accessors.Get("PackageDetailsAccessor");
        private static readonly DA.IPackageTypeCustomerAccessor packageTypeCustomerAccessor = (DA.IPackageTypeCustomerAccessor)Accessors.Get("PackageTypeCustomerAccessor");

		/// <summary>
		/// Delete PackageType by primary key.
		/// </summary>
		public void Delete(string packageTypeId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(packageTypeId);
		}

		/// <summary>
		/// Insert a PackageType.
		/// </summary>
        public void Insert(Model.PackageType packageType)
        {
			//
			// todo:add other logic here
			//
          //  validate(packageType);
            //if (this.Exists(packageType.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.PackageType.PROPERTY_ID);
            //}
            try
            {
                V.BeginTransaction();
                //packageType.CreateDate = DateTime.Now;
                //packageType.InsertTime = DateTime.Now;                
              
                //packageType.PackageTypeId = Guid.NewGuid().ToString();
                //accessor.Insert(packageType);

                foreach (Model.PackageDetails details in packageType.PruoductsDetails)
                {
                    if (string.IsNullOrEmpty(details.PackageDetailsId)) 
                    {
                        details.PackageDetailsId = Guid.NewGuid().ToString();
                    }
                    details.PackageTypeId = packageType.PackageTypeId;
                    details.ProductId = details.Product.ProductId;

                    packageDetailsAccessor.Insert(details);
                }

                foreach (Model.PackageTypeCustomer packageCustomer in packageType.CustomerDetails)
                {
                    if (string.IsNullOrEmpty(packageCustomer.PrimaryKey)) 
                    {
                        packageCustomer.PrimaryKey = Guid.NewGuid().ToString();
                    }
                    packageCustomer.CustomerId = packageCustomer.Customer.CustomerId;
                    packageCustomer.PackageTypeId = packageType.PackageTypeId;

                    packageTypeCustomerAccessor.Insert(packageCustomer);
                }
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
        public void Update(Model.PackageType packageType)
        {
			//
			// todo: add other logic here.
			//
            validate(packageType);
            //if (this.ExistsExcept(packageType))
            //{
            //    throw new Helper.InvalidValueException(Model.PackageType.PROPERTY_ID);
            //}
           // packageType.UpdateTime = DateTime.Now;

         //   packageDetailsAccessor.Delete(packageType);  


           // packageType.PruoductsDetails = packageDetailsAccessor.Select(packageType);
             foreach (Model.PackageDetails details in packageType.PruoductsDetails)
            {
                if (string.IsNullOrEmpty(details.PackageDetailsId))
                {
                    details.PackageDetailsId = Guid.NewGuid().ToString();
                }
                details.PackageTypeId = packageType.PackageTypeId;
                details.ProductId = details.Product.ProductId;

                packageDetailsAccessor.Insert(details);
            }
            //packageTypeCustomerAccessor.Delete(packageType);
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

            accessor.Update(packageType);
        }

        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.PackageType Get(string packageTypeId)
        {
            Model.PackageType type =accessor.Get(packageTypeId);
            type.PruoductsDetails = packageDetailsAccessor.Select(type);
            type.CustomerDetails = packageTypeCustomerAccessor.Select(type);
            return type;
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "PackageType";
        //}

        //protected override string GetSettingId()
        //{
        //    return "PackageTypeRule";
        //}
        private void validate(Model.PackageType package)
        {
            if (string.IsNullOrEmpty(package.Id))
            {
                throw new Helper.RequireValueException(Model.PackageType.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty(package.PackagePypeName))
            {
                throw new Helper.RequireValueException(Model.PackageType.PROPERTY_PACKAGEPYPENAME);
            }            
        }
        public IList<Book.Model.PackageType> Select(Book.Model.Customer customer)
        {
            return accessor.Select(customer);

        }
    }
}

