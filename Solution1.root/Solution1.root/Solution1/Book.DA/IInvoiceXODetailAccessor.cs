//------------------------------------------------------------------------------
//
// file name：IInvoiceXODetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceXODetail
    /// </summary>
    public partial interface IInvoiceXODetailAccessor : IEntityAccessor
    {
        IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO);

        void Delete(Model.InvoiceXO invoice);

        float GetByInvoiceXODetailId(string invoiceXODetailId);
        Book.Model.InvoiceXODetail GetInvoiceXOAndProductById(string invoiceXODetailId);
        Book.Model.InvoiceXODetail GetAllCurrentNum();
        IList<Book.Model.InvoiceXODetail> select_XOnotInMps();
        IList<Model.InvoiceXODetail> SelectByDateRangeAndPid(string productid, DateTime startdate, DateTime enddate);
    }
}

