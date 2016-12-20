//------------------------------------------------------------------------------
//
// file name：IProduceStatisticsCheckAccessor.cs
// author: mayanjun
// create date：2011-07-22 10:44:52
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceStatisticsCheck
    /// </summary>
    public partial interface IProduceStatisticsCheckAccessor : IAccessor
    {
        IList<Model.ProduceStatisticsCheck> SelectBycondition(DateTime starDate, DateTime endDate, string produceStatisticsCheckId1, string produceStatisticsCheckId2, string PronoteHeaderId0, string PronoteHeaderId1);
    }
}

