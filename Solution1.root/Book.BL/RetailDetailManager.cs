//------------------------------------------------------------------------------
//
// file name：RetailDetailManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RetailDetail.
    /// </summary>
    public partial class RetailDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete RetailDetail by primary key.
		/// </summary>
		public void Delete(string retailDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(retailDetailId);
		}

		/// <summary>
		/// Insert a RetailDetail.
		/// </summary>
        public void Insert(Model.RetailDetail retailDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(retailDetail);
        }
		
		/// <summary>
		/// Update a RetailDetail.
		/// </summary>
        public void Update(Model.RetailDetail retailDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(retailDetail);
        }
		
    }
}

