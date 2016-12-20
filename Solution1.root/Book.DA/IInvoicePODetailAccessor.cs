//------------------------------------------------------------------------------
//
// file name：IInvoicePODetailAccessor.cs
// author: peidun
// create date：2008-11-29 12:15:14
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoicePODetail
    /// </summary>
    public partial interface IInvoicePODetailAccessor : IEntityAccessor
    {
        IList<Model.InvoicePODetail> Select(Book.Model.InvoicePO invoice);

        void Delete(Model.InvoicePO invoice);

        IList<Model.InvoicePODetail> Select(Model.Department depart);

        IList<Model.InvoicePODetail> Select(Model.InvoicePI invoice);
   
    }
}

