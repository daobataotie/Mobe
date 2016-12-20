//------------------------------------------------------------------------------
//
// file name：ICustomerMarksAccessor.cs
// author: mayanjun
// create date：2013-5-8 13:43:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.CustomerMarks
    /// </summary>
    public partial interface ICustomerMarksAccessor : IAccessor
    {
        IList<Model.CustomerMarks> SelectByCustomerId(string customerId);

        void DeleteByCustomerId(string customerId);

        IList<Model.CustomerMarks> SelectByInvoicePackingId(string InvoicePackingId);

        void DeleteByInvoicePackingId(string InvoicePackingId);
    }
}

