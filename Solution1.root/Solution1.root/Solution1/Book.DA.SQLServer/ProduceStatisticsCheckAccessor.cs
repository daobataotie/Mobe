//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsCheckAccessor.cs
// author: mayanjun
// create date：2011-07-22 10:44:53
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
    /// Data accessor of ProduceStatisticsCheck
    /// </summary>
    public partial class ProduceStatisticsCheckAccessor : EntityAccessor, IProduceStatisticsCheckAccessor
    {
        public IList<Model.ProduceStatisticsCheck> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsCheckId1, string produceStatisticsCheckId2, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceStatisticsCheckId1", produceStatisticsCheckId1);
            ht.Add("produceStatisticsCheckId2", produceStatisticsCheckId2);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceStatisticsCheck>("ProduceStatisticsCheck.selectBycondition", ht);
        }
    }
}
