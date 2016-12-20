//------------------------------------------------------------------------------
//
// file name：IProduceOtherCompactAccessor.cs
// author: peidun
// create date：2010-1-4 15:32:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherCompact
    /// </summary>
    public partial interface IProduceOtherCompactAccessor : IAccessor
    {
        IList<Model.ProduceOtherCompact> SelectIsInDepot();
        IList<Model.ProduceOtherCompact> SelectIsInDepotMaterial();
        IList<Model.ProduceOtherCompact> SelectByMRSHeaderId(string MrsHeaderId);
        IList<Book.Model.ProduceOtherCompact> SelectThreeMonth();
        IList<Book.Model.ProduceOtherCompact> GetByDate(DateTime startDate, DateTime endDate);
    }
}

