//------------------------------------------------------------------------------
//
// file name：IAcOtherShouldPaymentAccessor.cs
// author: mayanjun
// create date：2011-6-10 10:11:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcOtherShouldPayment
    /// </summary>
    public partial interface IAcOtherShouldPaymentAccessor : IAccessor
    {
        IList<Model.AcOtherShouldPayment> SelectByDateRange(DateTime startdate, DateTime enddate);
        IList<Model.AcOtherShouldPayment> SelectByDateRangeAndSupCompany(DateTime startdate, DateTime enddate, Model.Supplier supplier,Model.Company company);
    }
}

