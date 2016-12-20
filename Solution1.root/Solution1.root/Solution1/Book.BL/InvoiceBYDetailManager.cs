//------------------------------------------------------------------------------
//
// file name：InvoiceBYDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceBYDetail.
    /// </summary>
    public partial class InvoiceBYDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceBYDetail by primary key.
		/// </summary>
		public void Delete(string invoiceBYDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceBYDetailId);
		}

		/// <summary>
		/// Insert a InvoiceBYDetail.
		/// </summary>
        public void Insert(Model.InvoiceBYDetail invoiceBYDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceBYDetail);
        }
		
		/// <summary>
		/// Update a InvoiceBYDetail.
		/// </summary>
        public void Update(Model.InvoiceBYDetail invoiceBYDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceBYDetail);
        }
        public IList<Book.Model.InvoiceBYDetail> Select(Book.Model.InvoiceBY invoiceBY)
        {
            return accessor.Select(invoiceBY);
        }
    }
}

