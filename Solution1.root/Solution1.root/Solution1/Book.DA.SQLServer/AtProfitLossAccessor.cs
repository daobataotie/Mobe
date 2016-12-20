//------------------------------------------------------------------------------
//
// file name：AtProfitLossAccessor.cs
// author: mayanjun
// create date：2011-2-25 10:53:29
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
    /// Data accessor of AtProfitLoss
    /// </summary>
    public partial class AtProfitLossAccessor : EntityAccessor, IAtProfitLossAccessor
    {
        public IList<Book.Model.AtProfitLoss> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtProfitLoss>("AtProfitLoss.select_ByDate", ht);
        }
    }
}
