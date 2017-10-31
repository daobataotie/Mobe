//------------------------------------------------------------------------------
//
// file name：PCMouldOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:01
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
    /// Data accessor of PCMouldOnlineCheckDetail
    /// </summary>
    public partial class PCMouldOnlineCheckDetailAccessor : EntityAccessor, IPCMouldOnlineCheckDetailAccessor
    {
        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("PCMouldOnlineCheckDetail.DeleteByHeaderId", id);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByCondition(DateTime OnlineDateStart, DateTime OnlineDateEnd, DateTime CheckDateStart, DateTime CheckDateEnd, string productId, string invoiceCusId)
        {
            Hashtable ht = new Hashtable();
            StringBuilder sql = new StringBuilder();

            //sql.Append(" and PCMouldOnlineCheckId in (select PCMouldOnlineCheckId from PCMouldOnlineCheck where PCMouldOnlineCheckDate between '" + DateStart.ToString("yyyy-MM-dd") + "' and '" + DateEnd.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm") + "')");
            sql.Append(" and OnlineDate between '" + OnlineDateStart.ToString("yyyy-MM-dd") + "' and '" + OnlineDateEnd.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm") + "' and CheckDate between '" + CheckDateStart.ToString("yyyy-MM-dd") + "' and '" + CheckDateEnd.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm") + "'");

            if (!string.IsNullOrEmpty(productId))
                sql.Append(" and ProductId='" + productId + "'");
            if (!string.IsNullOrEmpty(invoiceCusId))
                sql.Append(" and InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceCusId + "')");

            ht.Add("sql", sql);

            return sqlmapper.QueryForList<Model.PCMouldOnlineCheckDetail>("PCMouldOnlineCheckDetail.SelectByCondition", ht);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByInvoiceCusId(string invoiceCusId)
        {
            return sqlmapper.QueryForList<Model.PCMouldOnlineCheckDetail>("PCMouldOnlineCheckDetail.SelectByInvoiceCusId", invoiceCusId);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.PCMouldOnlineCheckDetail>("PCMouldOnlineCheckDetail.SelectByHeaderId", id);
        }
    }
}
