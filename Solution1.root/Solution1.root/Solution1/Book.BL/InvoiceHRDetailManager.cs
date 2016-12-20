//------------------------------------------------------------------------------
//
// file name：InvoiceHRDetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHRDetail.
    /// </summary>
    public partial class InvoiceHRDetailManager
    {
		
		/// <summary>
		/// Delete InvoiceHRDetail by primary key.
		/// </summary>
		public void Delete(string invoiceHRDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceHRDetailId);
		}

		/// <summary>
		/// Insert a InvoiceHRDetail.
		/// </summary>
        public void Insert(Model.InvoiceHRDetail invoiceHRDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceHRDetail);
        }
		
		/// <summary>
		/// Update a InvoiceHRDetail.
		/// </summary>
        public void Update(Model.InvoiceHRDetail invoiceHRDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceHRDetail);
        }        
    }
}

