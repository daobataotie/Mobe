//------------------------------------------------------------------------------
//
// file name：IInvoiceZXDetailAccessor.cs
// author: mayanjun
// create date：2012-10-29 14:32:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceZXDetail
    /// </summary>
    public partial interface IInvoiceZXDetailAccessor : IAccessor
    {
        IList<Model.InvoiceZXDetail> SelectByInvoiceZXId(string id);
        void DeleteByInvoiceZXId(string id);
    }
}

