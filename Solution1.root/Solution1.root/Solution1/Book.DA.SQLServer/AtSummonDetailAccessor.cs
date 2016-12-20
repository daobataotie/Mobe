//------------------------------------------------------------------------------
//
// file name：AtSummonDetailAccessor.cs
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
    /// Data accessor of AtSummonDetail
    /// </summary>
    public partial class AtSummonDetailAccessor : EntityAccessor, IAtSummonDetailAccessor
    {
        public IList<Model.AtSummonDetail> Select(Model.AtSummon atSummon)
        {
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.getBySummonId", atSummon.SummonId);
        }
        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.select_byDdate", ht);
        }

        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate,string startSubjectId,string endSubjectId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("startSubjectId", startSubjectId);
            ht.Add("endSubjectId", endSubjectId);
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.select_byDdateAndSubject", ht);
        }
        public int CountSummon<T>(string lending,string subjectId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("lending", lending);
            ht.Add("subjectId", subjectId);
            return sqlmapper.QueryForObject<int>(typeof(T).Name + ".getByLendAndSubjectId", ht);
        }
        public int CountSummonTo(string lending, string subjectId)
        {
            return this.CountSummon<Model.AtSummonDetail>(lending, subjectId);
        }
    }
}
