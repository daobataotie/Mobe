//------------------------------------------------------------------------------
//
// file name：IInvoiceHCAccessor.cs
// author: peidun
// create date：2008-11-29 12:15:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceHC
    /// </summary>
    public partial interface IInvoiceHCAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceHC> Select(DateTime start, DateTime end);

        IList<Model.InvoiceHC> Select(Helper.InvoiceStatus status);
    }
}

