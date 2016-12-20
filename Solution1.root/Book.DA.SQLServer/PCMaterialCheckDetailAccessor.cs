//------------------------------------------------------------------------------
//
// file name：PCMaterialCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
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
    /// Data accessor of PCMaterialCheckDetail
    /// </summary>
    public partial class PCMaterialCheckDetailAccessor : EntityAccessor, IPCMaterialCheckDetailAccessor
    {
        public IList<Model.PCMaterialCheckDetail> SelectByPCMaterialCheckId(string id)
        {
            return sqlmapper.QueryForList<Model.PCMaterialCheckDetail>("PCMaterialCheckDetail.SelectByPCMaterialCheckId", id);
        }

        public void DeleteByPCMaterialCheckId(string id)
        {
            sqlmapper.Delete("PCMaterialCheckDetail.DeleteByPCMaterialCheckId", id);
        }

        public IList<Model.PCMaterialCheckDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND pcd.CheckDate between '" + StartDate.ToString("yyyy-MM-dd") + "' and '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(StartPId) && !string.IsNullOrEmpty(EndPId))
            {
                sb.Append(" AND p.Id between '" + StartPId + "' and '" + EndPId + "'");
            }
            if (!string.IsNullOrEmpty(InvoiceCusId))
                sb.Append(" and pc.InvoiceCusId='" + InvoiceCusId + "'");
            sb.Append("  Order by pcd.PCMaterialCheckId,pcd.CheckDate");

            return sqlmapper.QueryForList<Model.PCMaterialCheckDetail>("PCMaterialCheckDetail.SelectByCondition", sb.ToString());
        }
    }
}
