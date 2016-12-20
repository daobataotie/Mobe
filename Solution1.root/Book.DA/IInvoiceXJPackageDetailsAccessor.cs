//------------------------------------------------------------------------------
//
// file name：IInvoiceXJPackageDetailsAccessor.cs
// author: mayanjun
// create date：2012-8-14 17:05:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXJPackageDetails
    /// </summary>
    public partial interface IInvoiceXJPackageDetailsAccessor : IAccessor
    {
        IList<Model.InvoiceXJPackageDetails> SelectByHeaderId(string invoiceid);

        void DeleteByHeader(string invoiceid);
    }

}

