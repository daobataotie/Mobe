//------------------------------------------------------------------------------
//
// file name：PCInputCheckAccessor.cs
// author: mayanjun
// create date：2015/4/18 上午 11:58:03
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
    /// Data accessor of PCInputCheck
    /// </summary>
    public partial class PCInputCheckAccessor : EntityAccessor, IPCInputCheckAccessor
    {
        public IList<Model.PCInputCheck> SelectByCondition(DateTime startdate, DateTime enddate, string productid, string testProductid, string supplierid, string lotnumber, bool IsClosed)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(productid))
                sql.Append(" and ProductId='" + productid + "'");
            if (!string.IsNullOrEmpty(testProductid))
                sql.Append(" and TestProductId='" + testProductid + "'");
            if (!string.IsNullOrEmpty(supplierid))
                sql.Append(" and SupplierId='" + supplierid + "'");
            if (!string.IsNullOrEmpty(lotnumber))
                sql.Append(" and LotNumber='" + lotnumber + "'");
            if (IsClosed)
                sql.Append(" and (IsClosed=0 or IsClosed is null)");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.PCInputCheck>("PCInputCheck.SelectByCondition", ht);
        }

        public IList<Model.PCInputCheck> SelectByInvoiceCusId(string invoiceCusId)
        {
            return sqlmapper.QueryForList<Model.PCInputCheck>("PCInputCheck.SelectByInvoiceCusId", invoiceCusId);
        }

        public bool ExistsLotNumberInsert(string lotNumber, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("LotNumber", lotNumber);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<bool>("PCInputCheck.ExistsLotNumberInsert", ht);
        }

        public bool ExistsLotNumberUpdate(string lotNumber, string PCInputCheckId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("LotNumber", lotNumber);
            ht.Add("PCInputCheckId", PCInputCheckId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<bool>("PCInputCheck.ExistsLotNumberUpdate", ht);
        }

        public void UpdateIsClosed(Model.PCInputCheck model)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PCInputCheckId", model.PCInputCheckId);
            ht.Add("IsClosed", model.IsClosed);
            sqlmapper.Update("PCInputCheck.UpdateIsClosed", ht);
        }
    }
}
