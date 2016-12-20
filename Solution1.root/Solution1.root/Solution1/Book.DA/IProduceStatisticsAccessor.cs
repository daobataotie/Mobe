//------------------------------------------------------------------------------
//
// file name：IProduceStatisticsAccessor.cs
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
    /// Interface of data accessor of dbo.ProduceStatistics
    /// </summary>
    public partial interface IProduceStatisticsAccessor : IAccessor
    {
        IList<Model.ProduceStatistics> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsId1, string produceStatisticsId2, string PronoteHeaderId0, string PronoteHeaderId1);
    }
}

