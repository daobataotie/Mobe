//------------------------------------------------------------------------------
//
// file name：IInvoiceCTDetailAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceCTDetail
    /// </summary>
    public partial interface IInvoiceCTDetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceCTDetail> Select(Model.InvoiceCT invoiceCT);
        void Delete(Model.InvoiceCT invoice);
    }
}

