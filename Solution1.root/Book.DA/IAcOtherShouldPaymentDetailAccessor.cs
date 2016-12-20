//------------------------------------------------------------------------------
//
// file name：IAcOtherShouldPaymentDetailAccessor.cs
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
    /// Interface of data accessor of dbo.AcOtherShouldPaymentDetail
    /// </summary>
    public partial interface IAcOtherShouldPaymentDetailAccessor : IAccessor
    {
        IList<Model.AcOtherShouldPaymentDetail> Select(Model.AcOtherShouldPayment acOtherShouldPayment);
    }
}

