//------------------------------------------------------------------------------
//
// file name：WorkhouselogManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Workhouselog.
    /// </summary>
    public partial class WorkhouselogManager:BaseManager
    {
		
		/// <summary>
		/// Delete Workhouselog by primary key.
		/// </summary>
		public void Delete(string workhouselogID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(workhouselogID);
		}

		/// <summary>
		/// Insert a Workhouselog.
		/// </summary>
        public void Insert(Model.Workhouselog workhouselog)
        {
			//
			// todo:add other logic here
			//
            Validate(workhouselog);
            workhouselog.InsertTime = DateTime.Now;
            workhouselog.WorkhouselogID = Guid.NewGuid().ToString();
            accessor.Insert(workhouselog);
        }
		
		/// <summary>
		/// Update a Workhouselog.
		/// </summary>
        public void Update(Model.Workhouselog workhouselog)
        {
			//
			// todo: add other logic here.
			//
            Validate(workhouselog);
            workhouselog.UpdateTime = DateTime.Now;
            accessor.Update(workhouselog);
        }
        private void Validate(Model.Workhouselog workHouselog)
        {
            if (string.IsNullOrEmpty(workHouselog.WorkHouseId))
            {
                throw new Helper.RequireValueException(Model.Workhouselog.PROPERTY_WORKHOUSEID);
            }
        }
    }
}

