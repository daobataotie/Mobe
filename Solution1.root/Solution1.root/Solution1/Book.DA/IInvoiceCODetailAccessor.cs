//------------------------------------------------------------------------------
//
// file name：IInvoiceCODetailAccessor.cs
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
    /// Interface of data accessor of dbo.InvoiceCODetail
    /// </summary>
    public partial interface IInvoiceCODetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceCODetail> Select(Model.InvoiceCO invoice);
        void Delete(Model.InvoiceCO invoice);
        IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate);
    }
}

