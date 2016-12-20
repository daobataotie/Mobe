//------------------------------------------------------------------------------
//
// file name：CustomerCategoryManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerCategory.
    /// </summary>
    public partial class CustomerCategoryManager : BaseManager
    {
		
		/// <summary>
		/// Delete CustomerCategory by primary key.
		/// </summary>
		public void Delete(string customerCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerCategoryId);
		}
        public void Delete(Model.CustomerCategory customercategory)
        {

            accessor.Delete(customercategory.CustomerCategoryId);     
        }

		/// <summary>
		/// Insert a CustomerCategory.
		/// </summary>
        public void Insert(Model.CustomerCategory customerCategory)
        {
			//
			// todo:add other logic here
			//
            Validate(customerCategory);

            if (this.Exists(customerCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.CustomerCategory.PROPERTY_ID);
            }

            customerCategory.CustomerCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(customerCategory);            
        }
		
		/// <summary>
		/// Update a CustomerCategory.
		/// </summary>
        public void Update(Model.CustomerCategory customerCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(customerCategory);

            if (this.ExistsExcept(customerCategory))
            {
                throw new Helper.InvalidValueException(Model.CustomerCategory.PROPERTY_ID);
            }

            accessor.Update(customerCategory);
        }
        private void Validate(Model.CustomerCategory customerCategory)
        {
            if (string.IsNullOrEmpty(customerCategory.Id))
            {
                throw new Helper.RequireValueException(Model.CustomerCategory.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty(customerCategory.CustomerCategoryName))
            {
                throw new Helper.RequireValueException(Model.CustomerCategory.PROPERTY_CUSTOMERCATEGORYNAME);
            }
        }
    }
}

