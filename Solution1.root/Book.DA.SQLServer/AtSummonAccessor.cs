//------------------------------------------------------------------------------
//
// file name：AtSummonAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:40:43
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
    /// Data accessor of AtSummon
    /// </summary>
    public partial class AtSummonAccessor : EntityAccessor, IAtSummonAccessor
    {
        public IList<Book.Model.AtSummon> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AtSummon>("AtSummon.SelectByDateRage", ht);
        }

        public IList<Model.AtSummon> SelectByCondition(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" and SummonDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(startId) || !string.IsNullOrEmpty(endId))
            {
                if (!string.IsNullOrEmpty(startId) && !string.IsNullOrEmpty(endId))
                    sql.Append(" and Id between '" + startId + "' and '" + endId + "'");
                else
                    sql.Append(" and Id='" + (string.IsNullOrEmpty(startId) ? endId : startId) + "'");
            }
            sql.Append(" order by Id");

            return sqlmapper.QueryForList<Model.AtSummon>("AtSummon.SelectByCondition", sql.ToString());
        }
    }
}
