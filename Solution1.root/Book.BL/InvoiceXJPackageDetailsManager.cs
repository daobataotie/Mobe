//------------------------------------------------------------------------------
//
// file name：InvoiceXJPackageDetailsManager.cs
// author: mayanjun
// create date：2012-8-14 17:05:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXJPackageDetails.
    /// </summary>
    public partial class InvoiceXJPackageDetailsManager
    {
		public void Delete(string invoiceXJPackageDetailsId)
		{
			accessor.Delete(invoiceXJPackageDetailsId);
		}

        public void Insert(Model.InvoiceXJPackageDetails invoiceXJPackageDetails)
        {
            accessor.Insert(invoiceXJPackageDetails);
        }
		
        public void Update(Model.InvoiceXJPackageDetails invoiceXJPackageDetails)
        {
            accessor.Update(invoiceXJPackageDetails);
        }

        public IList<Model.InvoiceXJPackageDetails> SelectByHeaderId(string invoiceid)
        {
            return accessor.SelectByHeaderId(invoiceid);
        }

        public void DeleteByHeader(string invoiceid)
        {
            accessor.DeleteByHeader(invoiceid);
        }
    }
}

