//------------------------------------------------------------------------------
//
// file name：InvoiceCFDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCFDetail.
    /// </summary>
    public partial class InvoiceCFDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceCFDetail by primary key.
		/// </summary>
		public void Delete(string invoiceCFDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceCFDetailId);
		}

		/// <summary>
		/// Insert a InvoiceCFDetail.
		/// </summary>
        public void Insert(Model.InvoiceCFDetail invoiceCFDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCFDetail);
        }
		
		/// <summary>
		/// Update a InvoiceCFDetail.
		/// </summary>
        public void Update(Model.InvoiceCFDetail invoiceCFDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCFDetail);
        }

        public IList<Model.InvoiceCFDetail> Select(string kind, Model.InvoiceCF invoice)
        {
            return accessor.Select(kind, invoice);
        }
    }
}

