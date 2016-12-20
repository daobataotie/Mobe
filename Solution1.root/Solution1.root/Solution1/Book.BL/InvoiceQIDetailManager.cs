//------------------------------------------------------------------------------
//
// file name：InvoiceQIDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceQIDetail.
    /// </summary>
    public partial class InvoiceQIDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceQIDetail by primary key.
		/// </summary>
		public void Delete(string invoiceQIDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceQIDetailId);
		}

		/// <summary>
		/// Insert a InvoiceQIDetail.
		/// </summary>
        public void Insert(Model.InvoiceQIDetail invoiceQIDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceQIDetail);
        }
		
		/// <summary>
		/// Update a InvoiceQIDetail.
		/// </summary>
        public void Update(Model.InvoiceQIDetail invoiceQIDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceQIDetail);
        }

        public IList<Book.Model.InvoiceQIDetail> Select(Book.Model.InvoiceQI invoiceQI)
        {
            return accessor.Select(invoiceQI);
        }
    }
}

