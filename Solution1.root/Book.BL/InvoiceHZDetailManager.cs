//------------------------------------------------------------------------------
//
// file name：InvoiceHZDetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceHZDetail.
    /// </summary>
    public partial class InvoiceHZDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceHZDetail by primary key.
		/// </summary>
		public void Delete(string invoiceHZDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceHZDetailId);
		}

		/// <summary>
		/// Insert a InvoiceHZDetail.
		/// </summary>
        public void Insert(Model.InvoiceHZDetail invoiceHZDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceHZDetail);
        }
		
		/// <summary>
		/// Update a InvoiceHZDetail.
		/// </summary>
        public void Update(Model.InvoiceHZDetail invoiceHZDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceHZDetail);
        }
        public IList<Model.InvoiceHZDetail> Select(Model.InvoiceHZ invoice) 
        {
            return accessor.Select(invoice);
        }
    }
}

