//------------------------------------------------------------------------------
//
// file name：IInvoiceCTAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCT
    /// </summary>
    public partial interface IInvoiceCTAccessor : IInvoiceAccessor
    {
        IList<Model.InvoiceCT> Select(DateTime start, DateTime end);
        void OwedIncrement(Model.InvoiceCT invoice, decimal money);
        void OwedDecrement(Model.InvoiceCT invoice, decimal money);
        void OwedIncrement(Model.InvoiceCT invoice, decimal? money);
        void OwedDecrement(Model.InvoiceCT invoice, decimal? money);
        void OwedIncrement(string invoice, decimal money);
        void OwedDecrement(string invoice, decimal money);
        void OwedIncrement(string invoice, decimal? money);
        void OwedDecrement(string invoice, decimal? money);

        IList<Model.InvoiceCT> Select(Helper.InvoiceStatus status);
    }
}

