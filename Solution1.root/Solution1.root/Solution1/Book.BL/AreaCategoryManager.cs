//------------------------------------------------------------------------------
//
// file name：AreaCategoryManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AreaCategory.
    /// </summary>
    public partial class AreaCategoryManager : BaseManager
    {
		
		/// <summary>
		/// Delete AreaCategory by primary key.
		/// </summary>
		public void Delete(string areaCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(areaCategoryId);
		}

		/// <summary>
		/// Insert a AreaCategory.
		/// </summary>
        public void Insert(Model.AreaCategory areaCategory)
        {
			//
			// todo:add other logic here
			//

            Validate(areaCategory);
            if (this.Exists(areaCategory.Id))
            {
                throw new Helper.InvalidValueException("Id");
            }
            areaCategory.InsertTime = DateTime.Now;
            areaCategory.AreaCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(areaCategory);
        }

        private void Validate(Model.AreaCategory areaCategory)
        {
            if (string.IsNullOrEmpty(areaCategory.AreaCategoryName))
            {
                throw new Helper.RequireValueException(Model.AreaCategory.PROPERTY_AREACATEGORYNAME);
            }
        }
		
		/// <summary>
		/// Update a AreaCategory.
		/// </summary>
        public void Update(Model.AreaCategory areaCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(areaCategory);

            if (this.ExistsExcept(areaCategory))
            {
                throw new Helper.InvalidValueException("Id");
            }
            areaCategory.UpdateTime = DateTime.Now;
            
            accessor.Update(areaCategory);
        }


        //protected override string GetInvoiceKind()
        //{
        //    return "AreaCategory";
        //}

        //protected override string GetSettingId()
        //{
        //    return "AreaCategoryRule";
        //}
    }
}

