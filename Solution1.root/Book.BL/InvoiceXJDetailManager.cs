//------------------------------------------------------------------------------
//
// file name：InvoiceXJDetailManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXJDetail.
    /// </summary>
    public partial class InvoiceXJDetailManager : BaseManager
    {
        public void Delete(string invoiceXJDetailId)
        {
            accessor.Delete(invoiceXJDetailId);
        }

        public void Insert(Model.InvoiceXJDetail invoiceXJDetail)
        {
            accessor.Insert(invoiceXJDetail);
        }

        public void Update(Model.InvoiceXJDetail invoiceXJDetail)
        {
            accessor.Update(invoiceXJDetail);
        }

        public IList<Model.InvoiceXJDetail> Select(Model.InvoiceXJ invoiceXJ)
        {
            return accessor.Select(invoiceXJ);
        }

        /// <summary>
        /// 产品类型为公司产品
        /// </summary>
        /// <returns></returns>
        public IList<Model.InvoiceXJDetail> SelectProductType()
        {
            return accessor.SelectProductType();
        }

        public System.Collections.Hashtable getRecursiveBOM(string productid)
        {
            return accessor.getRecursiveBOM(productid);
        }

        public System.Collections.Hashtable getRecursiveInvoiceXJDetails(string invoiceXJid)
        {
            return accessor.getRecursiveInvoiceXJDetails(invoiceXJid);
        }

        public void DeleteByHeaderId(string invoiceid)
        {
            accessor.DeleteByHeaderId(invoiceid);
        }
    }
}

