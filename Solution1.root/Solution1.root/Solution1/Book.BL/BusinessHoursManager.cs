//------------------------------------------------------------------------------
//
// file name：BusinessHoursManager.cs
// author: peidun
// create date：2009-09-02 上午 10:38:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BusinessHours.
    /// </summary>
    public partial class BusinessHoursManager:BaseManager
    {
		
		/// <summary>
		/// Delete BusinessHours by primary key.
		/// </summary>
		public void Delete(string businessHoursId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(businessHoursId);
		}

		/// <summary>
		/// Insert a BusinessHours.
		/// </summary>
        public void Insert(Model.BusinessHours businessHours)
        {
			//
			// todo:add other logic here
			//
            Validate(businessHours);
            businessHours.InsertTime = DateTime.Now;
            businessHours.BusinessHoursId = Guid.NewGuid().ToString();
            accessor.Insert(businessHours);
        }
		
		/// <summary>
		/// Update a BusinessHours.
		/// </summary>
        public void Update(Model.BusinessHours businessHours)
        {
			//
			// todo: add other logic here.
			//
            Validate(businessHours);
            businessHours.UpdateTime = DateTime.Now;
            accessor.Update(businessHours);
        }
        private void Validate(Model.BusinessHours businessHours)
        {
            if (string.IsNullOrEmpty(businessHours.BusinessHoursName))
            {
                throw new Helper.RequireValueException(Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
            }
            if (accessor.IsExistsBusinessName(businessHours.BusinessHoursId, businessHours.BusinessHoursName))
                throw new Helper.InvalidValueException(Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
        }
        public string GetNewId()
        {
            return null;    // return Guid.NewGuid
        }
        public DataSet SelectNoModel()
        {
            return accessor.SelectNoModel();
        }
        public void UpdateDataTable(DataTable accounts)
        {
            accessor.UpdateDataTable(accounts);
        }
    }
}

