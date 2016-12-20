//------------------------------------------------------------------------------
//
// file name：ProduceInDepotDetailAccessor.cs
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
    /// Data accessor of ProduceInDepotDetail
    /// </summary>
    public partial class ProduceInDepotDetailAccessor : EntityAccessor, IProduceInDepotDetailAccessor
    {
        public IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot)
        {
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_byProduceInDepotId", produceInDepot.ProduceInDepotId);
        }
        public IList<Book.Model.ProduceInDepotDetail> Select(Model.PronoteHeader startPronoteHeader,Model.PronoteHeader endPronoteHeader,DateTime startDate,DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startpronoteid",startPronoteHeader==null?null:startPronoteHeader.PronoteHeaderID);
            ht.Add("endpronoteid", endPronoteHeader == null ? null : endPronoteHeader.PronoteHeaderID);
            ht.Add("startdate", startDate );
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_byProduceInDateAndPronote", ht);
        }
    }
}
