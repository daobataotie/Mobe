//------------------------------------------------------------------------------
//
// file name：DepotInDetailAccessor.cs
// author: mayanjun
// create date：2010-10-25 16:14:48
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
    /// Data accessor of DepotInDetail
    /// </summary>
    public partial class DepotInDetailAccessor : EntityAccessor, IDepotInDetailAccessor
    {
        public IList<Model.DepotInDetail> GetDetailByDepotInId(string depotInId)
        {
            return sqlmapper.QueryForList<Model.DepotInDetail>("DepotInDetail.GetDetailByDepotInId", depotInId);
        }
        public void Delete(Model.DepotIn depotIn)
        {
            sqlmapper.Delete("DepotInDetail.delete_byheader", depotIn.DepotInId);
        }

        public IList<Model.DepotInDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string InDepotIdStart, string InDepotIdEnd, string DepotIdStart, string DepotIdEnd, Model.Supplier SupplierStart, Model.Supplier SupplierEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate);
            ht.Add("EndDate", EndDate);
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(InDepotIdStart) || !string.IsNullOrEmpty(InDepotIdEnd))
            {
                if (!string.IsNullOrEmpty(InDepotIdStart) && !string.IsNullOrEmpty(InDepotIdEnd))
                    sql.Append("AND DepotIn.DepotInId BETWEEN '" + InDepotIdStart + "' AND '" + InDepotIdEnd + "'");
                else
                    sql.Append("AND DepotIn.DepotInId='" + (string.IsNullOrEmpty(InDepotIdStart) ? InDepotIdEnd : InDepotIdStart) + "'");
            }
            if (!string.IsNullOrEmpty(DepotIdStart) || !string.IsNullOrEmpty(DepotIdEnd))
            {
                if (!string.IsNullOrEmpty(DepotIdStart) && !string.IsNullOrEmpty(DepotIdEnd))
                    sql.Append("AND DepotIn.DepotId BETWEEN '" + DepotIdStart + "' AND '" + DepotIdEnd + "'");
                else
                    sql.Append("AND DepotIn.DepotId='" + (string.IsNullOrEmpty(DepotIdStart) ? DepotIdEnd : DepotIdStart) + "'");
            }
            if (SupplierStart != null || SupplierEnd != null)
            {
                if (SupplierStart != null && SupplierEnd != null)
                    sql.Append("AND SupplierId BETWEEN '" + SupplierStart.SupplierId + "' AND '" + SupplierEnd.SupplierId + "'");
                else
                    sql.Append("AND SupplierId='" + (SupplierStart == null ? SupplierEnd.SupplierId : SupplierStart.SupplierId) + "'");
            }
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.DepotInDetail>("DepotInDetail.SelectByCondition", ht);
        }
    }
}
