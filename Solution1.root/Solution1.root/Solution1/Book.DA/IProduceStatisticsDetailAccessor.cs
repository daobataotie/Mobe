//------------------------------------------------------------------------------
//
// file name：IProduceStatisticsDetailAccessor.cs
// author: mayanjun
// create date：2011-4-8 09:17:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceStatisticsDetail
    /// </summary>
    public partial interface IProduceStatisticsDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceStatisticsDetail> Select(Model.ProduceStatistics produceStatistics);
        IList<Book.Model.ProduceStatisticsDetail> SelectbyPronoteHeaderProcedures(string PronoteHeaderID, string ProceduresId);
        Book.Model.ProduceStatisticsDetail SelectbyPronoteHeaderProceduresSum(string PronoteHeaderID, string ProceduresId);
        IList<Model.ProduceStatisticsDetail> SelectByDateRangeAndPronoteHeaderId(DateTime startdate, DateTime enddate, string pronoteHeaderId);
    }
}

