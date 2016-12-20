//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentDetailAccessor.cs
// author: mayanjun
// create date：2011-6-10 10:11:50
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
    /// Data accessor of AcOtherShouldPaymentDetail
    /// </summary>
    public partial class AcOtherShouldPaymentDetailAccessor : EntityAccessor, IAcOtherShouldPaymentDetailAccessor
    {
        public IList<Model.AcOtherShouldPaymentDetail> Select(Model.AcOtherShouldPayment acOtherShouldPayment)
        {
            return sqlmapper.QueryForList<Model.AcOtherShouldPaymentDetail>("AcOtherShouldPaymentDetail.getByAcOtherShouldPaymentId", acOtherShouldPayment.AcOtherShouldPaymentId);
        }
    }
}
