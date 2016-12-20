//------------------------------------------------------------------------------
//
// file name：IInvoiceXJDetailAccessor.cs
// author: peidun
// create date：2008/6/20 15:51:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXJDetail
    /// </summary>
    public partial interface IInvoiceXJDetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceXJDetail> Select(Model.InvoiceXJ invoice);

        void Delete(Model.InvoiceXJ invoice);
        IList<Model.InvoiceXJDetail> SelectProductType();

        Hashtable getRecursiveBOM(string productid);


        Hashtable getRecursiveInvoiceXJDetails(string invoiceXJid);

        void DeleteByHeaderId(string invoiceid);
    }
}

