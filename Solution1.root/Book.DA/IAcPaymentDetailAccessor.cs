//------------------------------------------------------------------------------
//
// file name：IAcPaymentDetailAccessor.cs
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
    /// Interface of data accessor of dbo.AcPaymentDetail
    /// </summary>
    public partial interface IAcPaymentDetailAccessor : IAccessor
    {
        void DeleteByAcPaymentId(string acpaymentId);
        IList<Model.AcPaymentDetail> Select(Model.AcPayment acPayment);
    }
}

