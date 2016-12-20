//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsDetailAccessor.cs
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
    /// Data accessor of ProduceStatisticsDetail
    /// </summary>
    public partial class ProduceStatisticsDetailAccessor : EntityAccessor, IProduceStatisticsDetailAccessor
    {
        public IList<Book.Model.ProduceStatisticsDetail> Select(Model.ProduceStatistics produceStatistics)
        {
            return sqlmapper.QueryForList<Model.ProduceStatisticsDetail>("ProduceStatisticsDetail.select_byProduceStatisticsId", produceStatistics.ProduceStatisticsId);
        }
        public IList<Book.Model.ProduceStatisticsDetail> SelectbyPronoteHeaderProcedures(string PronoteHeaderID,string ProceduresId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderID", PronoteHeaderID);
            ht.Add("ProceduresId", ProceduresId);
            return sqlmapper.QueryForList<Model.ProduceStatisticsDetail>("ProduceStatisticsDetail.select_byPronoteHeaderProcedures", ht);
        }
        /// <summary>
        /// 求和
        /// </summary>
        /// <param name="PronoteHeaderID"></param>
        /// <param name="ProceduresId"></param>
        /// <returns></returns>
        public Book.Model.ProduceStatisticsDetail SelectbyPronoteHeaderProceduresSum(string PronoteHeaderID, string ProceduresId)
        {
            if (PronoteHeaderID == null || ProceduresId == null) return null;
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderID", PronoteHeaderID);
            ht.Add("ProceduresId", ProceduresId);
            return sqlmapper.QueryForObject<Model.ProduceStatisticsDetail>("ProduceStatisticsDetail.select_byPronoteHeaderProceduresSum", ht);
        }

        public IList<Model.ProduceStatisticsDetail> SelectByDateRangeAndPronoteHeaderId(DateTime startdate, DateTime enddate, string pronoteHeaderId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate );
            ht.Add("enddate", enddate);
            ht.Add("pronoteHeaderId", pronoteHeaderId);
            return sqlmapper.QueryForList<Model.ProduceStatisticsDetail>("ProduceStatisticsDetail.selectByDateRangeAndPronoteHeaderId", ht);
        }
    }
}
