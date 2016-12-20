//------------------------------------------------------------------------------
//
// file name：InvoicePIDetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePIDetail.
    /// </summary>
    public partial class InvoicePIDetailManager
    {
		
		/// <summary>
		/// Delete InvoicePIDetail by primary key.
		/// </summary>
		public void Delete(string invoicePIDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoicePIDetailId);
		}

		/// <summary>
		/// Insert a InvoicePIDetail.
		/// </summary>
        public void Insert(Model.InvoicePIDetail invoicePIDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoicePIDetail);
        }
		
		/// <summary>
		/// Update a InvoicePIDetail.
		/// </summary>
        public void Update(Model.InvoicePIDetail invoicePIDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoicePIDetail);
        }
		
    }
}

