//------------------------------------------------------------------------------
//
// file name：InvoiceZZDetailManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZZDetail.
    /// </summary>
    public partial class InvoiceZZDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceZZDetail by primary key.
		/// </summary>
		public void Delete(string invoiceZZDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceZZDetailId);
		}

		/// <summary>
		/// Insert a InvoiceZZDetail.
		/// </summary>
        public void Insert(Model.InvoiceZZDetail invoiceZZDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceZZDetail);
        }
		
		/// <summary>
		/// Update a InvoiceZZDetail.
		/// </summary>
        public void Update(Model.InvoiceZZDetail invoiceZZDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceZZDetail);
        }        
        public IList<Model.InvoiceZZDetail> Select(string kind, Model.InvoiceZZ invoiceZZ)
        {
            return accessor.Select(kind,invoiceZZ);
        }
    }
}

