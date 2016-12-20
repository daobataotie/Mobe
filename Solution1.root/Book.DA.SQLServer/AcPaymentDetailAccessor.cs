//------------------------------------------------------------------------------
//
// file name：AcPaymentDetailAccessor.cs
// author: mayanjun
// create date：2011-6-23 09:29:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of AcPaymentDetail
    /// </summary>
    public partial class AcPaymentDetailAccessor : EntityAccessor, IAcPaymentDetailAccessor
    {
        public IList<Model.AcPaymentDetail> Select(Model.AcPayment acPayment)
        {
            return sqlmapper.QueryForList<Model.AcPaymentDetail>("AcPaymentDetail.selectByAcPayment", acPayment.AcPaymentId);
        }

        public void DeleteByAcPaymentId(string acpaymentId)
        {
            sqlmapper.Delete("AcPaymentDetail.DeleteByAcPaymentId", acpaymentId);
        }

    }
}
