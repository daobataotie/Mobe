//------------------------------------------------------------------------------
//
// file name：IProduceStatisticsCheckDetailAccessor.cs
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
    /// Interface of data accessor of dbo.ProduceStatisticsCheckDetail
    /// </summary>
    public partial interface IProduceStatisticsCheckDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceStatisticsCheckDetail> Select(Model.ProduceStatisticsCheck produceStatisticsCheck);
    }
}

