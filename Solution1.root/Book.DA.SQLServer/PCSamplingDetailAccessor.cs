//------------------------------------------------------------------------------
//
// file name：PCSamplingDetailAccessor.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
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
    /// Data accessor of PCSamplingDetail
    /// </summary>
    public partial class PCSamplingDetailAccessor : EntityAccessor, IPCSamplingDetailAccessor
    {

        public IList<Model.PCSamplingDetail> SelectByPCMaterialCheckId(string id)
        {
            return sqlmapper.QueryForList<Model.PCSamplingDetail>("PCSamplingDetail.SelectByPCMaterialCheckId", id);
        }

        public void DeleteByPCMaterialCheckId(string id)
        {
            sqlmapper.Delete("PCSamplingDetail.DeleteByPCMaterialCheckId", id);
        }

        public IList<Model.PCSamplingDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND pcd.PCSamplingDetailDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(startPId) && !string.IsNullOrEmpty(endPId))
            {
                sb.Append(" AND p.Id between '" + startPId + "' and '" + endPId + "'");
            }
            if (!string.IsNullOrEmpty(invoiceCusId))
                sb.Append(" and pc.InvoiceCusId='" + invoiceCusId + "'");
            sb.Append("  order by pcd.PCSamplingId,pcd.PCSamplingDetailDate");
            return sqlmapper.QueryForList<Model.PCSamplingDetail>("PCSamplingDetail.SelectByCondition", sb.ToString());
        }
    }
}
