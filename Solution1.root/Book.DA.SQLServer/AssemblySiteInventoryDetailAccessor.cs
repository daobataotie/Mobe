//------------------------------------------------------------------------------
//
// file name：AssemblySiteInventoryDetailAccessor.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
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
    /// Data accessor of AssemblySiteInventoryDetail
    /// </summary>
    public partial class AssemblySiteInventoryDetailAccessor : EntityAccessor, IAssemblySiteInventoryDetailAccessor
    {
        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("AssemblySiteInventoryDetail.DeleteByHeaderId", id);
        }

        public IList<Model.AssemblySiteInventoryDetail> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.AssemblySiteInventoryDetail>("AssemblySiteInventoryDetail.SelectByHeaderId", id);
        }

        public IList<Model.AssemblySiteInventoryDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, bool state)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("ProductId", productid);
            if (state)
                ht.Add("sql", " and InvoiceState is null or InvoiceState=0");

            return sqlmapper.QueryForList<Model.AssemblySiteInventoryDetail>("AssemblySiteInventoryDetail.SelectByDateRage", ht);
        }
    }
}
