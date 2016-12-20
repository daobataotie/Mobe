//------------------------------------------------------------------------------
//
// file name：InvoiceXJProcessManager.cs
// author: mayanjun
// create date：2010-8-25 16:07:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXJProcess.
    /// </summary>
    public partial class InvoiceXJProcessManager
    {
		
		/// <summary>
		/// Delete InvoiceXJProcess by primary key.
		/// </summary>
		public void Delete(string invoiceXJProcessId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(invoiceXJProcessId);
		}

		/// <summary>
		/// Insert a InvoiceXJProcess.
		/// </summary>
        public void Insert(Model.InvoiceXJProcess invoiceXJProcess)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(invoiceXJProcess);
        }
		
		/// <summary>
		/// Update a InvoiceXJProcess.
		/// </summary>
        public void Update(Model.InvoiceXJProcess invoiceXJProcess)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(invoiceXJProcess);
        }
        public void Update(IList<Model.InvoiceXJProcess> detail, Model.InvoiceXJDetail InvoiceXJDetail)
        {
            //
            // todo: add other logic here.
            //
            this.Delete(InvoiceXJDetail);
            foreach (Model.InvoiceXJProcess InvoiceXJProcess in detail)
            {
                if (InvoiceXJProcess.ProcessCategoryId == null) continue;
                //InvoiceXJProcess.ProcessId = InvoiceXJProcess.Process == null ? null : InvoiceXJProcess.Process.ProcessId;
                accessor.Insert(InvoiceXJProcess);
            }
        }
        public IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJDetail InvoiceXJDetail)
        {
            return accessor.Select(InvoiceXJDetail);
        }
        public void Delete(Model.InvoiceXJDetail InvoiceXJDetail)
        {
            accessor.Delete(InvoiceXJDetail);
        }
        public IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJ InvoiceXJ)
        {
               return   accessor.Select(InvoiceXJ);
        }
        public void Delete(Model.InvoiceXJ InvoiceXJ)
        {
            accessor.Delete(InvoiceXJ);
        }

        public void DeleteByHeaderId(string invoiceid)
        {
            accessor.DeleteByHeaderId(invoiceid);
        }
    }
}

