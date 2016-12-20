//------------------------------------------------------------------------------
//
// file name：MPSdetailsManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MPSdetails.
    /// </summary>
    public partial class MPSdetailsManager
    {
        
		/// <summary>
		/// Delete MPSdetails by primary key.
		/// </summary>
		public void Delete(string mPSdetailsID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(mPSdetailsID);
		}

		/// <summary>
		/// Insert a MPSdetails.
		/// </summary>
        public void Insert(Model.MPSdetails mPSdetails)
        {
			//
			// todo:add other logic here
			//
            mPSdetails.MPSdetailsId = Guid.NewGuid().ToString();
            accessor.Insert(mPSdetails);
        }
		
		/// <summary>
		/// Update a MPSdetails.
		/// </summary>
        public void Update(Model.MPSdetails mPSdetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(mPSdetails);
        }
        public IList<Book.Model.MPSdetails> Select(Model.MPSheader mPSheader)
        {
            return accessor.Select(mPSheader);
        }
        public IList<Book.Model.MPSdetails> Select(Model.Customer customer)
        {
            return accessor.Select(customer);
        }
        public IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd, string mpsheaderIdStart, string mpsheaderIdEnd, DateTime dateStart, DateTime dateEnd, string productId)
        {
            return accessor.Select(customerStart,customerEnd,mpsheaderIdStart,mpsheaderIdEnd,dateStart,dateEnd,productId);
        }
        public IList<Book.Model.MPSdetails> Select(Model.InvoiceXO invoiceXO)
        {
            return accessor.Select(invoiceXO);
        }
        public double GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return accessor.GetByInvoiceXODetailId(invoiceXODetailId);
        }
        public double GetByMPSdetailsId(string mPSdetailsId)
        {
            return accessor.GetByMPSdetailsId(mPSdetailsId);
        }
        public IList<Book.Model.MPSdetails> SelectState()
        {
            return accessor.SelectState();
        }
        public IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart, customerEnd, dateStart, dateEnd);
        }
    }
}

