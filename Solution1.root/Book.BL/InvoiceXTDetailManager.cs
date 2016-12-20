//------------------------------------------------------------------------------
//
// file name：InvoiceXTDetailManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXTDetail.
    /// </summary>
    public partial class InvoiceXTDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceXTDetail by primary key.
		/// </summary>
		public void Delete(string invoiceXTDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceXTDetailId);
		}

		/// <summary>
		/// Insert a InvoiceXTDetail.
		/// </summary>
        public void Insert(Model.InvoiceXTDetail invoiceXTDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceXTDetail);
        }
		
		/// <summary>
		/// Update a InvoiceXTDetail.
		/// </summary>
        public void Update(Model.InvoiceXTDetail invoiceXTDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceXTDetail);
        }
        public IList<Model.InvoiceXTDetail> Select(Model.InvoiceXT invoiceXT)
        {
            return accessor.Select(invoiceXT);
        }
    }
}

