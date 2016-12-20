//------------------------------------------------------------------------------
//
// file name：InvoicePTDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePTDetail.
    /// </summary>
    public partial class InvoicePTDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoicePTDetail by primary key.
		/// </summary>
		public void Delete(string invoicePTDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoicePTDetailId);
		}

		/// <summary>
		/// Insert a InvoicePTDetail.
		/// </summary>
        public void Insert(Model.InvoicePTDetail invoicePTDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoicePTDetail);
        }
		
		/// <summary>
		/// Update a InvoicePTDetail.
		/// </summary>
        public void Update(Model.InvoicePTDetail invoicePTDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoicePTDetail);
        }
        public IList<Book.Model.InvoicePTDetail> Select(Book.Model.InvoicePT invoicePT)
        {
            return accessor.Select(invoicePT);
        }
		
    }
}

