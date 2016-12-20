//------------------------------------------------------------------------------
//
// file name：InvoiceCJDetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCJDetail.
    /// </summary>
    public partial class InvoiceCJDetailManager : BaseManager
    {
        
		
		/// <summary>
		/// Delete InvoiceCJDetail by primary key.
		/// </summary>
		public void Delete(string invoiceCJDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceCJDetailId);
		}

		/// <summary>
		/// Insert a InvoiceCJDetail.
		/// </summary>
        public void Insert(Model.InvoiceCJDetail invoiceCJDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCJDetail);
        }
		
		/// <summary>
		/// Update a InvoiceCJDetail.
		/// </summary>
        public void Update(Model.InvoiceCJDetail invoiceCJDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCJDetail);
        }
        public IList<Book.Model.InvoiceCJDetail> Select(Book.Model.InvoiceCJ invoiceCJ)
        {
            return accessor.Select(invoiceCJ);
        }
    
    }
}

