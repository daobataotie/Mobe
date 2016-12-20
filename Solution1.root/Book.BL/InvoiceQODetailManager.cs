//------------------------------------------------------------------------------
//
// file name：InvoiceQODetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceQODetail.
    /// </summary>
    public partial class InvoiceQODetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceQODetail by primary key.
		/// </summary>
		public void Delete(string invoiceQODetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceQODetailId);
		}

		/// <summary>
		/// Insert a InvoiceQODetail.
		/// </summary>
        public void Insert(Model.InvoiceQODetail invoiceQODetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceQODetail);
        }
		
		/// <summary>
		/// Update a InvoiceQODetail.
		/// </summary>
        public void Update(Model.InvoiceQODetail invoiceQODetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceQODetail);
        }
        public IList<Book.Model.InvoiceQODetail> Select(Book.Model.InvoiceQO invoiceQO)
        {
            return accessor.Select(invoiceQO);
        }
    }
}

