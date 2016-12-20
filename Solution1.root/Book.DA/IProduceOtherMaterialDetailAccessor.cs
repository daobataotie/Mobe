//------------------------------------------------------------------------------
//
// file name：IProduceOtherMaterialDetailAccessor.cs
// author: peidun
// create date：2010-1-5 15:39:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceOtherMaterialDetail
    /// </summary>
    public partial interface IProduceOtherMaterialDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceOtherMaterialDetail> Select(Model.ProduceOtherMaterial produceOtherMaterial);
        IList<Book.Model.ProduceOtherMaterialDetail> GetOrderById(Model.ProduceOtherMaterial produceOtherMaterial);
        IList<Book.Model.ProduceOtherMaterialDetail> Select(string houseid, DateTime startDate, DateTime endDate);
        IList<Model.ProduceOtherMaterialDetail> SelectByCondition(string ProduceOtherMaterialDetailId, string productId1, string productId2);
        IList<Book.Model.ProduceOtherMaterialDetail> SelectByState(Model.ProduceOtherMaterial produceMaterial);
        Model.ProduceOtherMaterialDetail SelectByPidHidPosId(string productId, string produceOtherMaterialId, string depotPositionId);
        IList<Model.ProduceOtherMaterialDetail> SelectForDistributioned(string productid, DateTime InsertTime);
    }
}

