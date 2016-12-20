//------------------------------------------------------------------------------
//
// file name：IInvoiceZGDetailAccessor.cs
// author: mayanjun
// create date：2012-11-19 14:13:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZGDetail
    /// </summary>
    public partial interface IInvoiceZGDetailAccessor : IAccessor
    {
        void DeleteByInvoiceZGId(string InvoiceZGId);

        IList<Model.InvoiceZGDetail> SelectByInvoiceZGId(string InvoiceZGId);
    }
}

