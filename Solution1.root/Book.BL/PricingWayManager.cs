//------------------------------------------------------------------------------
//
// file name：PricingWayManager.cs
// author: peidun
// create date：2009-11-18 14:52:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PricingWay.
    /// </summary>
    public partial class PricingWayManager:BaseManager
    {
		
		/// <summary>
		/// Delete PricingWay by primary key.
		/// </summary>
		public void Delete(string pricingwayID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pricingwayID);
		}

		/// <summary>
		/// Insert a PricingWay.
		/// </summary>
        public void Insert(Model.PricingWay pricingWay)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pricingWay);
        }
		
		/// <summary>
		/// Update a PricingWay.
		/// </summary>
        public void Update(Model.PricingWay pricingWay)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pricingWay);
        }
    }
}

