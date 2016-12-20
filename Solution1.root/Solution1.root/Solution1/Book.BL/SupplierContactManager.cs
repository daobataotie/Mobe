//------------------------------------------------------------------------------
//
// file name：SupplierContactManager.cs
// author: peidun
// create date：2009-08-06 14:53:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SupplierContact.
    /// </summary>
    public partial class SupplierContactManager
    {
		
		/// <summary>
		/// Delete SupplierContact by primary key.
		/// </summary>
		public void Delete(string supplierContactId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(supplierContactId);
		}

		/// <summary>
		/// Insert a SupplierContact.
		/// </summary>
        public void Insert(Model.SupplierContact supplierContact)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(supplierContact);
        }
		
		/// <summary>
		/// Update a SupplierContact.
		/// </summary>
        public void Update(Model.SupplierContact supplierContact)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(supplierContact);
        }
		
    }
}

