//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsAccessor.cs
// author: mayanjun
// create date：2011-4-8 09:17:41
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
    /// Data accessor of ProduceStatistics
    /// </summary>
    public partial class ProduceStatisticsAccessor : EntityAccessor, IProduceStatisticsAccessor
    {
        public IList<Model.ProduceStatistics> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsId1, string produceStatisticsId2,string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceStatisticsId1", produceStatisticsId1);
            ht.Add("produceStatisticsId2", produceStatisticsId2);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceStatistics>("ProduceStatistics.selectBycondition", ht);
        }
    }
}
