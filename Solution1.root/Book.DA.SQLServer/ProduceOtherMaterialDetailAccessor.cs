//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialDetailAccessor.cs
// author: peidun
// create date：2010-1-5 15:39:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of ProduceOtherMaterialDetail
    /// </summary>
    public partial class ProduceOtherMaterialDetailAccessor : EntityAccessor, IProduceOtherMaterialDetailAccessor
    {
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byProduceOtherMaterialId", produceOtherMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> GetOrderById(Model.ProduceOtherMaterial produceOtherMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byProduceOtherMaterialIdOrderByCategoryId", produceOtherMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> SelectByState(Model.ProduceOtherMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.select_byState", produceMaterial.ProduceOtherMaterialId);
        }
        public IList<Book.Model.ProduceOtherMaterialDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid", houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectByCondition(string ProduceOtherMaterialDetailId, string productId1, string productId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProduceOtherMaterialId", ProduceOtherMaterialDetailId);
            ht.Add("productId1", productId1);
            ht.Add("productId2", productId2);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.selectByCondition", ht);
        }

        public Model.ProduceOtherMaterialDetail SelectByPidHidPosId(string productId, string produceOtherMaterialId, string depotPositionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("depotPositionId", depotPositionId);
            ht.Add("ProduceOtherMaterialId", produceOtherMaterialId);
            return sqlmapper.QueryForObject<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.selectByPidHidPosId", ht);
        }

        public IList<Model.ProduceOtherMaterialDetail> SelectForDistributioned(string productid, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productid);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForList<Model.ProduceOtherMaterialDetail>("ProduceOtherMaterialDetail.SelectForDistributioned", ht);
        }
    }
}
