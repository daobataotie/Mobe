//------------------------------------------------------------------------------
//
// file name：InvoiceJRDetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceJRDetail.
    /// </summary>
    public partial class InvoiceJRDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceJRDetail by primary key.
		/// </summary>
		public void Delete(string invoiceJRDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceJRDetailId);
		}

		/// <summary>
		/// Insert a InvoiceJRDetail.
		/// </summary>
        public void Insert(Model.InvoiceJRDetail invoiceJRDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceJRDetail);
        }
		
		/// <summary>
		/// Update a InvoiceJRDetail.
		/// </summary>
        public void Update(Model.InvoiceJRDetail invoiceJRDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceJRDetail);            
        }
        public IList<Model.InvoiceJRDetail> Select(Model.Supplier supper)
        {
            return accessor.Select(supper);
        }

        public IList<Model.InvoiceJRDetail> Select(Model.InvoiceHC invoice)
        {
            return accessor.Select(invoice);
        }

        public IList<Model.InvoiceJRDetail> Select(Model.InvoiceJR invoicejr)
        {
            return accessor.Select(invoicejr);
        }
    }
}

