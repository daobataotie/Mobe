//------------------------------------------------------------------------------
//
// file name：PCIncomingCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/11/8 20:10:09
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
    /// Data accessor of PCIncomingCheckDetail
    /// </summary>
    public partial class PCIncomingCheckDetailAccessor : EntityAccessor, IPCIncomingCheckDetailAccessor
    {
        public IList<Model.PCIncomingCheckDetail> SelectByPrimaryId(string id)
        {
            return sqlmapper.QueryForList<Model.PCIncomingCheckDetail>("PCIncomingCheckDetail.SelectByPrimaryId", id);
        }

        public void DeleteByPrimaryId(string id)
        {
            sqlmapper.Delete("PCIncomingCheckDetail.DeleteByPrimaryId", id);
        }

        public IList<Model.PCIncomingCheckDetail> SelectByCondition(DateTime startdate, DateTime enddate, string lotnumber)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(lotnumber))
                sql.Append(" and pc.Note='" + lotnumber + "'");
            sql.Append(" order by pcd.CheckDate");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.PCIncomingCheckDetail>("PCIncomingCheckDetail.SelectByCondition", ht);
        }
    }
}
