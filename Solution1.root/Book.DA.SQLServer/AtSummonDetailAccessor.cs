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

        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate, string startSubjectId, string endSubjectId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(startSubjectId) || !string.IsNullOrEmpty(endSubjectId))
            {
                if (!string.IsNullOrEmpty(startSubjectId) && !string.IsNullOrEmpty(endSubjectId))
                    sql.Append(" and SubjectId IN (SELECT SubjectId FROM AtAccountSubject WHERE id BETWEEN '" + startSubjectId + "' AND '" + endSubjectId + "')");
                else
                    sql.Append(" and SubjectId = (SELECT SubjectId FROM AtAccountSubject WHERE id = '" + (string.IsNullOrEmpty(startSubjectId) ? endSubjectId : startSubjectId) + "')");
            }
            //sql.Append("group by SubjectId");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.select_byDdateAndSubject", ht);
        }

        public int CountSummon<T>(string lending, string subjectId)
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

        public void DeleteByHeadId(string headid)
        {
            sqlmapper.Delete("AtSummonDetail.DeleteByHeadId", headid);
        }

        public IList<Book.Model.AtSummonDetail> SelectAndSCtype(DateTime startDate, DateTime endDate, string SumonCatetoryType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.ToString("yyyy-MM-dd"));
            ht.Add("SumonCatetoryType", SumonCatetoryType);

            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.SelectAndSCtype", ht);
        }

        public IList<Book.Model.AtSummonDetail> SelectByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", endDate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.SelectByDate", ht);
        }

        public decimal GET_ZFLZ_Yue(string subjectid, DateTime startdate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("subjectid", subjectid);
            ht.Add("startdate", startdate);

            return sqlmapper.QueryForObject<decimal>("AtSummonDetail.GET_ZFLZ_Yue", ht);
        }

        public IList<Model.AtSummonDetail> Select_ZFLZ_GroupSubject(string subjectid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("subjectid", subjectid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.Select_ZFLZ_GroupSubject", ht);
        }

        public IList<Book.Model.AtSummonDetail> Select_ZFLZ_XianJinGroupSubject(string subjectid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("subjectid", subjectid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AtSummonDetail>("AtSummonDetail.Select_ZFLZ_XianJinGroupSubject", ht);
        }

        //public double GET_ZFLZ_YueXIANJin(string subjectid, DateTime startdate, DateTime enddate)
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("subjectid", subjectid);
        //    ht.Add("startdate", startdate);
        //    ht.Add("enddate", enddate);
        //    return sqlmapper.QueryForObject<double>("AtSummonDetail.GET_ZFLZ_YueXIANJin", ht);
        //}

    }
}
