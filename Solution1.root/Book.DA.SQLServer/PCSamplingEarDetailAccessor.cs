//------------------------------------------------------------------------------
//
// file name：PCSamplingEarDetailAccessor.cs
// author: mayanjun
// create date：2015/10/31 16:25:11
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
    /// Data accessor of PCSamplingEarDetail
    /// </summary>
    public partial class PCSamplingEarDetailAccessor : EntityAccessor, IPCSamplingEarDetailAccessor
    {

        public IList<Model.PCSamplingEarDetail> SelectByPCMaterialCheckId(string id)
        {
            return sqlmapper.QueryForList<Model.PCSamplingEarDetail>("PCSamplingEarDetail.SelectByPCMaterialCheckId", id);
        }

        public void DeleteByPCMaterialCheckId(string id)
        {
            sqlmapper.Delete("PCSamplingEarDetail.DeleteByPCMaterialCheckId", id);
        }

        public IList<Model.PCSamplingEarDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND pcd.PCSamplingEarDetailDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(startPId) && !string.IsNullOrEmpty(endPId))
            {
                sb.Append(" AND p.Id between '" + startPId + "' and '" + endPId + "'");
            }
            if (!string.IsNullOrEmpty(invoiceCusId))
                sb.Append(" and pc.InvoiceCusId='" + invoiceCusId + "'");
            sb.Append("  order by pcd.PCSamplingEarId,pcd.PCSamplingEarDetailDate");
            return sqlmapper.QueryForList<Model.PCSamplingEarDetail>("PCSamplingEarDetail.SelectByCondition", sb.ToString());
        }
    }
}
