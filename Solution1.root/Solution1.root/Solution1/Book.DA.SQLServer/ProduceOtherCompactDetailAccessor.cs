//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactDetailAccessor.cs
// author: peidun
// create date：2010-1-4 15:32:39
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
    /// Data accessor of ProduceOtherCompactDetail
    /// </summary>
    public partial class ProduceOtherCompactDetailAccessor : EntityAccessor, IProduceOtherCompactDetailAccessor
    {
        public IList<Book.Model.ProduceOtherCompactDetail> Select(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byProduceOtherCompactId", produceOtherCompact.ProduceOtherCompactId);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> SelectCompactDetailAndFlag(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byProduceOtherCompactIdAndFlag", produceOtherCompact.ProduceOtherCompactId);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> SelectIsInDepot(Model.ProduceOtherCompact produceOtherCompact)
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.selectbyCompactIdIsInDepot", produceOtherCompact.ProduceOtherCompactId);
        }

        public double GetByMPSdetail(string mPSDetailId)
        {
            return sqlmapper.QueryForObject<double>("ProduceOtherCompactDetail.select_byMPSdetail", mPSDetailId);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            return sqlmapper.QueryForList<Book.Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_byCustomerANDdate", ht);
        }

        public IList<Model.ProduceOtherCompactDetail> SelectByConditoin(string cid1, string cid2, DateTime startdate, DateTime enddate, string pid0, string pid1, string sid0, string sid1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("cid1", cid1);
            ht.Add("cid2", cid2);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("pid0", pid0);
            ht.Add("pid1", pid1);
            ht.Add("sid0", sid0);
            ht.Add("sid1", sid1);
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.selectBycondition", ht);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> GetThreeMaths()
        {
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.Select_ThreeMaths", null);
        }
        public IList<Book.Model.ProduceOtherCompactDetail> GetDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();

            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceOtherCompactDetail>("ProduceOtherCompactDetail.select_GetToDate", ht);
        }
    }
}
