//------------------------------------------------------------------------------
//
// file name：IProduceOtherInDepotAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherInDepot
    /// </summary>
    public partial interface IProduceOtherInDepotAccessor : IAccessor
    {
        IList<Model.ProduceOtherInDepot> SelectByCondition(DateTime startdate, DateTime enddate, string supperId1, string supperId2, string ProduceOtherCompactId1, string ProduceOtherCompactId2);
    }
}

