//------------------------------------------------------------------------------
//
// file name：IInvoiceXJProcessAccessor.cs
// author: mayanjun
// create date：2010-8-25 16:07:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceXJProcess
    /// </summary>
    public partial interface IInvoiceXJProcessAccessor : IAccessor
    {
         IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJDetail InvoiceXJDetail);
         void Delete(Model.InvoiceXJDetail InvoiceXJDetail);
         IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJ InvoiceXJ);
         void Delete(Model.InvoiceXJ InvoiceXJ);

         void DeleteByHeaderId(string invoiceid);
    }
}

