//------------------------------------------------------------------------------
//
// file name：PCFirstOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
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
    /// Data accessor of PCFirstOnlineCheckDetail
    /// </summary>
    public partial class PCFirstOnlineCheckDetailAccessor : EntityAccessor, IPCFirstOnlineCheckDetailAccessor
    {
        public IList<Model.PCFirstOnlineCheckDetail> SelectByHeaderId(string pCFirstOnlineCheckId)
        {
            return sqlmapper.QueryForList<Model.PCFirstOnlineCheckDetail>("PCFirstOnlineCheckDetail.SelectByHeaderId", pCFirstOnlineCheckId);
        }

        public void DelectByHeaderId(string pCFirstOnlineCheckId)
        {
            sqlmapper.Delete("PCFirstOnlineCheckDetail.DelectByHeaderId", pCFirstOnlineCheckId);
        }

        public IList<Model.PCFirstOnlineCheckDetail> SelectByCondition(DateTime startDate, DateTime endDate, string CustomerInvoiceXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.Date.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"));

            if (!string.IsNullOrEmpty(CustomerInvoiceXOId))
                ht.Add("sql", "and ph.InvoiceCusId='" + CustomerInvoiceXOId + "'");
            //else
            //    ht.Add("sql", null);

            return sqlmapper.QueryForList<Model.PCFirstOnlineCheckDetail>("PCFirstOnlineCheckDetail.SelectByCondition", ht);
        }
    }
}
