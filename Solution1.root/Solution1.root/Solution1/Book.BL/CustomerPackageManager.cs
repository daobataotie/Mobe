//------------------------------------------------------------------------------
//
// file name：CustomerPackageManager.cs
// author: peidun
// create date：2010-2-4 11:15:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerPackage.
    /// </summary>
    public partial class CustomerPackageManager : BaseManager
    {
        private static readonly DA.ICustomerPackageDetailAccessor accessorDetail = (DA.ICustomerPackageDetailAccessor)Accessors.Get("CustomerPackageDetailAccessor");


        /// <summary>
        /// Delete CustomerPackage by primary key.
        /// </summary>
        public void Delete(string customerPackageId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(customerPackageId);
        }

        /// <summary>
        /// Insert a CustomerPackage.
        /// </summary>
        public void Insert(Model.CustomerPackage customerPackage)
        {

            if (string.IsNullOrEmpty(customerPackage.Id))
                throw new Helper.RequireValueException(Model.CustomerPackage.PROPERTY_ID);
            if (this.Exists(customerPackage.Id))
                throw new Helper.InvalidValueException(Model.CustomerPackage.PROPERTY_ID);


            customerPackage.InsertTime = DateTime.Now;
            customerPackage.UpdateTime = DateTime.Now;
            accessor.Insert(customerPackage);
            foreach (Model.CustomerPackageDetail customerPackageDetail in customerPackage.detail)
            {
                if (customerPackageDetail.Product == null || string.IsNullOrEmpty(customerPackageDetail.ProductId)) continue;
                accessorDetail.Insert(customerPackageDetail);
            }

        }

        /// <summary>
        /// Update a CustomerPackage.
        /// </summary>
        public void Update(Model.CustomerPackage customerPackage)
        {
            if (string.IsNullOrEmpty(customerPackage.Id))
                throw new Helper.RequireValueException(Model.CustomerPackage.PROPERTY_ID);
            if (this.ExistsExcept(customerPackage))
                throw new Helper.InvalidValueException(Model.CustomerPackage.PROPERTY_ID);
            //
            // todo: add other logic here.
            //
            customerPackage.UpdateTime = DateTime.Now;
            //  accessor.Update(customerPackage);

            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(customerPackage.CustomerPackageId);
                this.Insert(customerPackage);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;

            }
        }
        public IList<Model.CustomerPackage> Select(Model.Customer customer)
        {
            return accessor.Select(customer);
        }
    }
}

