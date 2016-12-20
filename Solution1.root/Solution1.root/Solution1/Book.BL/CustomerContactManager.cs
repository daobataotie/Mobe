//------------------------------------------------------------------------------
//
// file name：CustomerContactManager.cs
// author: peidun
// create date：2009-08-06 14:53:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerContact.
    /// </summary>
    public partial class CustomerContactManager
    {
		
		/// <summary>
		/// Delete CustomerContact by primary key.
		/// </summary>
		public void Delete(string customerContactId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(customerContactId);
		}

		/// <summary>
		/// Insert a CustomerContact.
		/// </summary>
        public void Insert(Model.CustomerContact customerContact)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerContact);
        }
		
		/// <summary>
		/// Update a CustomerContact.
		/// </summary>
        public void Update(Model.CustomerContact customerContact)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerContact);
        }
		
    }
}

