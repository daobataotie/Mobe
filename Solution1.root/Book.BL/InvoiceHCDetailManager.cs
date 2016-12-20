//------------------------------------------------------------------------------
//
// file name：InvoiceHCDetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHCDetail.
    /// </summary>
    public partial class InvoiceHCDetailManager
    {
		
		/// <summary>
		/// Delete InvoiceHCDetail by primary key.
		/// </summary>
		public void Delete(string invoiceHCDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceHCDetailId);
		}

		/// <summary>
		/// Insert a InvoiceHCDetail.
		/// </summary>
        public void Insert(Model.InvoiceHCDetail invoiceHCDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceHCDetail);
        }
		
		/// <summary>
		/// Update a InvoiceHCDetail.
		/// </summary>
        public void Update(Model.InvoiceHCDetail invoiceHCDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceHCDetail);
        }

        public IList<Book.Model.InvoiceHCDetail> Select(Book.Model.InvoiceHC invoice)
        {
            return accessor.Select(invoice);
        }
    }
}

