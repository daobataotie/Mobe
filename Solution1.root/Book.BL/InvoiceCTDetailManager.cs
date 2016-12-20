//------------------------------------------------------------------------------
//
// file name：InvoiceCTDetailManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCTDetail.
    /// </summary>
    public partial class InvoiceCTDetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceCTDetail by primary key.
		/// </summary>
		public void Delete(string invoiceCTDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceCTDetailId);
		}

		/// <summary>
		/// Insert a InvoiceCTDetail.
		/// </summary>
        public void Insert(Model.InvoiceCTDetail invoiceCTDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCTDetail);
        }
		
		/// <summary>
		/// Update a InvoiceCTDetail.
		/// </summary>
        public void Update(Model.InvoiceCTDetail invoiceCTDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCTDetail);
        }
        public IList<Model.InvoiceCTDetail> Select(Model.InvoiceCT invoiceCT) 
        {
            return accessor.Select(invoiceCT);
        }
    }
}

