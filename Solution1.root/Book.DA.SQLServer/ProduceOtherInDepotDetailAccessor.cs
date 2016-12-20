//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotDetailAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
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
    /// Data accessor of ProduceOtherInDepotDetail
    /// </summary>
    public partial class ProduceOtherInDepotDetailAccessor : EntityAccessor, IProduceOtherInDepotDetailAccessor
    {
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherInDepot produceOtherInDepot)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.select_byProduceOtherInDepotId", produceOtherInDepot.ProduceOtherInDepotId);
        }
        public IList<Book.Model.ProduceOtherInDepotDetail> Select(Model.ProduceOtherCompact startPronoteHeader, Model.ProduceOtherCompact endPronoteHeader, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startpronoteid", startPronoteHeader == null ? null : startPronoteHeader.ProduceOtherCompactId);
            ht.Add("endpronoteid", endPronoteHeader == null ? null : endPronoteHeader.ProduceOtherCompactId);
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.select_byProduceInDateAndPronote", ht);
        }

        public IList<Model.ProduceOtherInDepotDetail> SelectByCondition(string indepotId, string productId1, string productId2)
        {
            Hashtable ht = new Hashtable();
            ht.Add("indepotId", indepotId);
            ht.Add("productId1", productId1);
            ht.Add("productId2", productId2);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.selectByCondition", ht);
        }
        public void Delete(Model.ProduceOtherInDepot indepot)
        {
            sqlmapper.Delete("ProduceOtherInDepotDetail.deletebyheader", indepot.ProduceOtherInDepotId);
        }

        #region IProduceOtherInDepotDetailAccessor 成员


        public IList<Book.Model.ProduceOtherInDepotDetail> SelectByProduceotherInDepotId(string id)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherInDepotDetail>("ProduceOtherInDepotDetail.SelectByProduceotherInDepotId", id);
        }

        #endregion
    }

}
