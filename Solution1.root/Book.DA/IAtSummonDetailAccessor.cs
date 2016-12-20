//------------------------------------------------------------------------------
//
// file name：IAtSummonDetailAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtSummonDetail
    /// </summary>
    public partial interface IAtSummonDetailAccessor : IAccessor
    {
        IList<Model.AtSummonDetail> Select(Model.AtSummon atSummon);
        IList<Book.Model.AtSummonDetail> SelectAndSCtype(DateTime startDate, DateTime endDate, string SumonCatetoryType);
        int CountSummonTo(string lending, string subjectId);
        IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate, string startSubjectId, string endSubjectId);
        void DeleteByHeadId(string headid);
        IList<Book.Model.AtSummonDetail> SelectByDate(DateTime startDate, DateTime endDate);
        decimal GET_ZFLZ_Yue(string subjectid, DateTime startdate);
        //double GET_ZFLZ_YueXIANJin(string subjectid, DateTime startdate, DateTime enddate);

        IList<Model.AtSummonDetail> Select_ZFLZ_GroupSubject(string subjectid, DateTime startdate, DateTime enddate);
        IList<Model.AtSummonDetail> Select_ZFLZ_XianJinGroupSubject(string subjectid, DateTime startdate, DateTime enddate);
    }
}

