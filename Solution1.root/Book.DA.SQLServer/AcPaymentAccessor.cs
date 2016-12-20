//------------------------------------------------------------------------------
//
// file name：AcPaymentAccessor.cs
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
    /// Data accessor of AcPayment
    /// </summary>
    public partial class AcPaymentAccessor : EntityAccessor, IAcPaymentAccessor
    {
        public IList<Model.AcPayment> SelectByDateRange(DateTime starttime, DateTime endtime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", starttime);
            ht.Add("EndDate", endtime);
            return sqlmapper.QueryForList<Model.AcPayment>("AcPayment.selectForDateRange", ht);
        }
    }
}
