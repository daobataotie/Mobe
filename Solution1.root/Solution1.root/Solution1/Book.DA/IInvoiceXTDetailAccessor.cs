//------------------------------------------------------------------------------
//
// file name：IInvoiceXTDetailAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXTDetail
    /// </summary>
    public partial interface IInvoiceXTDetailAccessor : IEntityAccessor
    {
        System.Collections.Generic.IList<Model.InvoiceXTDetail> Select(Model.InvoiceXT invoiceXT);

        void Delete(Model.InvoiceXT invoice);
    }
}

