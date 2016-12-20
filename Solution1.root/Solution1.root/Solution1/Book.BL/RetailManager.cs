//------------------------------------------------------------------------------
//
// file name：RetailManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Retail.
    /// </summary>
    public partial class RetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete Retail by primary key.
		/// </summary>
		public void Delete(string retailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(retailId);
		}

		/// <summary>
		/// Insert a Retail.
		/// </summary>
        public void Insert(Model.Retail retail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(retail);
        }
		
		/// <summary>
		/// Update a Retail.
		/// </summary>
        public void Update(Model.Retail retail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(retail);
        }
		
    }
}

