//------------------------------------------------------------------------------
//
// file name：InvoiceCODetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCODetail.
    /// </summary>
    public partial class InvoiceCODetailManager : BaseManager
    {
		
		/// <summary>
		/// Delete InvoiceCODetail by primary key.
		/// </summary>
		public void Delete(string invoiceCODetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceCODetailId);
		}

		/// <summary>
		/// Insert a InvoiceCODetail.
		/// </summary>
        public void Insert(Model.InvoiceCODetail invoiceCODetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceCODetail);
        }
		
		/// <summary>
		/// Update a InvoiceCODetail.
		/// </summary>
        public void Update(Model.InvoiceCODetail invoiceCODetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceCODetail);
        }
        public IList<Model.InvoiceCODetail> Select(Model.InvoiceCO invoice) 
        {
            return accessor.Select(invoice);
        }

        public IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRangeAndPid(pid, startdate, enddate);
        }
    }
}

