//------------------------------------------------------------------------------
//
// file name：WorkHouseManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.WorkHouse.
    /// </summary>
    public partial class WorkHouseManager:BaseManager
    {
		
		/// <summary>
		/// Delete WorkHouse by primary key.
		/// </summary>
		public void Delete(string workhouseid)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(workhouseid);
		}

		/// <summary>
		/// Insert a WorkHouse.
		/// </summary>
        public void Insert(Model.WorkHouse workHouse)
        {
			//
			// todo:add other logic here
			//
            Validate(workHouse);
            workHouse.InsertTime = DateTime.Now;
            workHouse.WorkHouseId = Guid.NewGuid().ToString();
            accessor.Insert(workHouse);
        }
		
		/// <summary>
		/// Update a WorkHouse.
		/// </summary>
        public void Update(Model.WorkHouse workHouse)
        {
			//
			// todo: add other logic here.
			//
            Validate(workHouse);
            workHouse.UpdateTime = DateTime.Now;
            accessor.Update(workHouse);
        }
        private void Validate(Model.WorkHouse workHouse)
        {
            if (string.IsNullOrEmpty(workHouse.Workhousename))
            {
                throw new Helper.RequireValueException(Model.WorkHouse.PROPERTY_WORKHOUSENAME);
            }
        }
        //protected override string GetSettingId()
        //{
        //    return "whRule";
        //}
        //protected override string GetInvoiceKind()
        //{
        //    return "wh";
        //}
    }
}

