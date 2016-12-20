//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialDetailManager.cs
// author: peidun
// create date：2010-1-5 15:39:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherMaterialDetail.
    /// </summary>
    public partial class ProduceOtherMaterialDetailManager
    {
		
		/// <summary>
		/// Delete ProduceOtherMaterialDetail by primary key.
		/// </summary>
		public void Delete(string produceOtherMaterialDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceOtherMaterialDetailId);
		}

		/// <summary>
		/// Insert a ProduceOtherMaterialDetail.
		/// </summary>
        public void Insert(Model.ProduceOtherMaterialDetail produceOtherMaterialDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceOtherMaterialDetail);
        }
		
		/// <summary>
		/// Update a ProduceOtherMaterialDetail.
		/// </summary>
        public void Update(Model.ProduceOtherMaterialDetail produceOtherMaterialDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceOtherMaterialDetail);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return accessor.Select(produceOtherMaterial);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> GetOrderById(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return accessor.GetOrderById(produceOtherMaterial);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            return accessor.Select(houseid, startDate, endDate);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectByCondition(string ProduceOtherMaterialDetailId, string productId1, string productId2)
        {
            return accessor.SelectByCondition(ProduceOtherMaterialDetailId, productId1, productId2);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> SelectByState(Model.ProduceOtherMaterial produceMaterial)
        {
            return accessor.SelectByState(produceMaterial);
        }

        public Model.ProduceOtherMaterialDetail SelectByPidHidPosId(string productId, string produceOtherMaterialId, string depotPositionId)
        {
            return accessor.SelectByPidHidPosId(productId, produceOtherMaterialId, depotPositionId);
        }
    }
}

