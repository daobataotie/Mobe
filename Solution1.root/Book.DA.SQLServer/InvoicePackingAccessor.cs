//------------------------------------------------------------------------------
//
// file name：InvoicePackingAccessor.cs
// author: mayanjun
// create date：2013-1-14 10:58:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;


namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of InvoicePacking
    /// </summary>
    public partial class InvoicePackingAccessor : EntityAccessor, IInvoicePackingAccessor
    {
        public IList<Model.InvoicePacking> SelectByCondition(DateTime startDate, DateTime endDate, string No, string InvoiceOf, string ShippedBy, string Consignee)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd"));

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(No))
                sql.Append(" and InvoiceNO='" + No + "'");
            if (!string.IsNullOrEmpty(InvoiceOf))
                sql.Append(" and InvoiceOf='" + InvoiceOf + "'");
            if (!string.IsNullOrEmpty(ShippedBy))
                sql.Append(" and ShippedById='" + ShippedBy + "'");
            if (!string.IsNullOrEmpty(Consignee))
                sql.Append(" and CONSIGNEEId='" + Consignee + "'");
            sql.Append("ORDER BY InvoicePackingDate desc");

            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.InvoicePacking>("InvoicePacking.SelectByCondition", ht);
        }

        public string SelectCustomerInvoiceId(string id)
        {
            return sqlmapper.QueryForList<string>("InvoicePacking.SelectCustomerInvoiceId", id).Aggregate((i, k) => { return string.Format("{0},{1}", i, k); });
        }
    }
}
