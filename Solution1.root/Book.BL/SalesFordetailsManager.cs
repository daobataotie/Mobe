//------------------------------------------------------------------------------
//
// file name：SalesFordetailsManager.cs
// author: peidun
// create date：2009-12-17 15:29:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SalesFordetails.
    /// </summary>
    public partial class SalesFordetailsManager
    {
		
		/// <summary>
		/// Delete SalesFordetails by primary key.
		/// </summary>
		public void Delete(string salesFordetailsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(salesFordetailsId);
		}

		/// <summary>
		/// Insert a SalesFordetails.
		/// </summary>
        public void Insert(Model.SalesFordetails salesFordetails)
        {
			//
			// todo:add other logic here
			//
            salesFordetails.SalesFordetailsId = Guid.NewGuid().ToString();
            salesFordetails.InsertTime = DateTime.Now;

            accessor.Insert(salesFordetails);
        }
		
		/// <summary>
		/// Update a SalesFordetails.
		/// </summary>
        public void Update(Model.SalesFordetails salesFordetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(salesFordetails);
        }
        public IList<Model.SalesFordetails> Getdetails(Model.SalesForHeader salesForHeader)
        {
            return accessor.Getdetails(salesForHeader);
        }
        public IList<Model.SalesFordetails> GetdetailsByProductId(string productId)
        {
            return accessor.GetdetailsByProductId(productId);
        }
    }
}

