//------------------------------------------------------------------------------
//
// file name：IProduceOtherExitDetailAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherExitDetail
    /// </summary>
    public partial interface IProduceOtherExitDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceOtherExitDetail> Select(Model.ProduceOtherExitMaterial produceOtherExitMaterial);
        IList<Book.Model.ProduceOtherExitDetail> Select(string houseid, DateTime startDate, DateTime endDate);
        IList<Model.ProduceOtherExitDetail> SelectByProductAndMaterialId(string ProduceOtherExitMaterialId, string productId1, string productId2);
        void Delete(Model.ProduceOtherExitMaterial produceOtherExitMaterial);
    }

}

