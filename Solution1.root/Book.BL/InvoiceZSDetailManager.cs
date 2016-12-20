//------------------------------------------------------------------------------
//
// file name：InvoiceZSDetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZSDetail.
    /// </summary>
    public partial class InvoiceZSDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceZSDetail by primary key.
		/// </summary>
		public void Delete(string invoiceZSDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceZSDetailId);
		}

		/// <summary>
		/// Insert a InvoiceZSDetail.
		/// </summary>
        public void Insert(Model.InvoiceZSDetail invoiceZSDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceZSDetail);
        }
		
		/// <summary>
		/// Update a InvoiceZSDetail.
		/// </summary>
        public void Update(Model.InvoiceZSDetail invoiceZSDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceZSDetail);
        }
        public IList<Book.Model.InvoiceZSDetail> Select(Model.InvoiceZS invoice)
        {
            return accessor.Select(invoice);
        }
    }
}

