//------------------------------------------------------------------------------
//
// file name：AtSummonDetailManager.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtSummonDetail.
    /// </summary>
    public partial class AtSummonDetailManager
    {
        public void Delete(string summonDetailId)
        {
            accessor.Delete(summonDetailId);
        }

        public void Insert(Model.AtSummonDetail atSummonDetail)
        {
            accessor.Insert(atSummonDetail);
        }

        public void Update(Model.AtSummonDetail atSummonDetail)
        {
            accessor.Update(atSummonDetail);
        }

        public IList<Model.AtSummonDetail> Select(Model.AtSummon atSummon)
        {
            return accessor.Select(atSummon);
        }

        public IList<Book.Model.AtSummonDetail> SelectAndSCtype(DateTime startDate, DateTime endDate, string SumonCatetoryType)
        {
            return accessor.SelectAndSCtype(startDate, endDate, SumonCatetoryType);
        }

        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate, string startSubjectId, string endSubjectId)
        {
            return accessor.Select(startDate, endDate, startSubjectId, endSubjectId);
        }

        public int CountSummonTo(string lending, string subjectId)
        {
            return accessor.CountSummonTo(lending, subjectId);
        }

        public void DeleteByHeadId(string headid)
        {
            accessor.DeleteByHeadId(headid);
        }

        public IList<Model.AtSummonDetail> SelectByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.SelectByDate(startDate, endDate);
        }

        public decimal GET_ZFLZ_Yue(string subjectid, DateTime startdate)
        {
            return accessor.GET_ZFLZ_Yue(subjectid, startdate);
        }

        public IList<Model.AtSummonDetail> Select_ZFLZ_GroupSubject(string subjectid, DateTime startdate, DateTime enddate)
        {
            return accessor.Select_ZFLZ_GroupSubject(subjectid, startdate, enddate);
        }

        public IList<Model.AtSummonDetail> Select_ZFLZ_XianJinGroupSubject(string subjectid, DateTime startdate, DateTime enddate)
        {
            return accessor.Select_ZFLZ_XianJinGroupSubject(subjectid, startdate, enddate);
        }



        //public double GET_ZFLZ_YueXIANJin(string subjectid, DateTime startdate, DateTime enddate)
        //{
        //    return accessor.GET_ZFLZ_YueXIANJin(subjectid, startdate, enddate);
        //}
    }
}

