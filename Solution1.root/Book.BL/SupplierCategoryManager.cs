//------------------------------------------------------------------------------
//
// file name：SupplierCategoryManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SupplierCategory.
    /// </summary>
    public partial class SupplierCategoryManager : BaseManager
    {
		
		/// <summary>
		/// Delete SupplierCategory by primary key.
		/// </summary>
		public void Delete(string supplierCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(supplierCategoryId);
		}

		/// <summary>
		/// Insert a SupplierCategory.
		/// </summary>
        public void Insert(Model.SupplierCategory supplierCategory)
        {
			//
			// todo:add other logic here
			//            
            Validate(supplierCategory);

            if (this.Exists(supplierCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.SupplierCategory.PROPERTY_ID);
            }
            supplierCategory.SupplierCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(supplierCategory);
        }
		
		/// <summary>
		/// Update a SupplierCategory.
		/// </summary>
        public void Update(Model.SupplierCategory supplierCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(supplierCategory);

            if (this.ExistsExcept(supplierCategory))
            {
                throw new Helper.InvalidValueException(Model.SupplierCategory.PROPERTY_ID);
            }
            accessor.Update(supplierCategory);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "SupplierCategory";
        //}

        //protected override string GetSettingId()
        //{
        //    return "SupplierCategoryRule";
        //}
        private void Validate(Model.SupplierCategory supplierCategory)
        {
            if (string.IsNullOrEmpty(supplierCategory.Id))
            {
                throw new Helper.RequireValueException(Model.SupplierCategory.PROPERTY_ID);            
            }
            if (string.IsNullOrEmpty(supplierCategory.SupplierCategoryName))
            {
                throw new Helper.RequireValueException(Model.SupplierCategory.PROPERTY_SUPPLIERCATEGORYNAME);
            }
        }
		
    }
}

