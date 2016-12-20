//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitDetailAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:17
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
    /// Data accessor of ProduceOtherExitDetail
    /// </summary>
    public partial class ProduceOtherExitDetailAccessor : EntityAccessor, IProduceOtherExitDetailAccessor
    {
        public IList<Book.Model.ProduceOtherExitDetail> Select(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherExitDetail>("ProduceOtherExitDetail.select_byProduceOtherExitMaterialId", produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }
        public IList<Book.Model.ProduceOtherExitDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid", houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherExitDetail>("ProduceOtherExitDetail.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceOtherExitDetail> SelectByProductAndMaterialId(string ProduceOtherExitMaterialId,string productId1,string productId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProduceOtherExitMaterialId", ProduceOtherExitMaterialId);
            ht.Add("productId1",productId1);
            ht.Add("productId2",productId2);
            return sqlmapper.QueryForList<Model.ProduceOtherExitDetail>("ProduceOtherExitDetail.selectByProductAndMaterialId", ht);
        }
        public void Delete(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
        {
             sqlmapper.Delete("ProduceOtherExitDetail.delete_byheader", produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }
    }
}
