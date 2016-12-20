//------------------------------------------------------------------------------
//
// file name：IAcPaymentAccessor.cs
// author: mayanjun
// create date：2011-6-23 09:29:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcPayment
    /// </summary>
    public partial interface IAcPaymentAccessor : IAccessor
    {
        IList<Model.AcPayment> SelectByDateRange(DateTime starttime, DateTime endtime);
    }
}

