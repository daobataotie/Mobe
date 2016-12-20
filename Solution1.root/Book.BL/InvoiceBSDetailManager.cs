//------------------------------------------------------------------------------
//
// file name：InvoiceBSDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceBSDetail.
    /// </summary>
    public partial class InvoiceBSDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceBSDetail by primary key.
		/// </summary>
		public void Delete(string invoiceBSDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceBSDetailId);
		}

		/// <summary>
		/// Insert a InvoiceBSDetail.
		/// </summary>
        public void Insert(Model.InvoiceBSDetail invoiceBSDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceBSDetail);
        }
		
		/// <summary>
		/// Update a InvoiceBSDetail.
		/// </summary>
        public void Update(Model.InvoiceBSDetail invoiceBSDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceBSDetail);
        }

        public IList<Book.Model.InvoiceBSDetail> Select(Book.Model.InvoiceBS invoiceBS)
        {
            return accessor.Select(invoiceBS);
        }		
    }
}

