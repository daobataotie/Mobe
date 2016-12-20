//------------------------------------------------------------------------------
//
// file name：InvoiceJCDetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceJCDetail.
    /// </summary>
    public partial class InvoiceJCDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceJCDetail by primary key.
		/// </summary>
		public void Delete(string invoiceJCDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceJCDetailId);
		}

		/// <summary>
		/// Insert a InvoiceJCDetail.
		/// </summary>
        public void Insert(Model.InvoiceJCDetail invoiceJCDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceJCDetail);
        }
		
		/// <summary>
		/// Update a InvoiceJCDetail.
		/// </summary>
        public void Update(Model.InvoiceJCDetail invoiceJCDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceJCDetail);
        }


        public IList<Model.InvoiceJCDetail> Select(Model.Supplier supper)
        {
            return accessor.Select(supper);
        }

        public IList<Model.InvoiceJCDetail> Select(Model.InvoiceHR invoice)
        {
            return accessor.Select(invoice);
        }

        public IList<Book.Model.InvoiceJCDetail> Select(Book.Model.InvoiceJC invoice)
        {
            return accessor.Select(invoice);
        }
    }
}

