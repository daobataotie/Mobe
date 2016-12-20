//------------------------------------------------------------------------------
//
// file name：InvoicePODetailManager.cs
// author: peidun
// create date：2008-11-29 11:08:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePODetail.
    /// </summary>
    public partial class InvoicePODetailManager
    {
		
		/// <summary>
		/// Delete InvoicePODetail by primary key.
		/// </summary>
		public void Delete(string invoicePODetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoicePODetailId);
		}

		/// <summary>
		/// Insert a InvoicePODetail.
		/// </summary>
        public void Insert(Model.InvoicePODetail invoicePODetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoicePODetail);
        }
		
		/// <summary>
		/// Update a InvoicePODetail.
		/// </summary>
        public void Update(Model.InvoicePODetail invoicePODetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoicePODetail);
        }

        public IList<Model.InvoicePODetail> Select(Model.Department department) 
        {
            return accessor.Select(department);
        }
        public IList<Model.InvoicePODetail> Select(Model.InvoicePI invoice)
        {
            return accessor.Select(invoice);
        }
    }
}

