//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckAccessor.cs
// author: mayanjun
// create date：2013-1-28 15:42:34
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
    /// Data accessor of PCBoxFootCheck
    /// </summary>
    public partial class PCBoxFootCheckAccessor : EntityAccessor, IPCBoxFootCheckAccessor
    {
        public IList<Model.PCBoxFootCheck> SelectByRage(DateTime StartDate, DateTime EndDate, string InvoiceCusXOId, string PronoteHeaderId, Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate.ToString("yyyy-MM-dd"));
            ht.Add("EndDate", EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"));

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(InvoiceCusXOId))
            {
                //sql.Append("AND InvoiceXOId='" + InvoiceXOId + "'");
                sql.Append(" AND (InvoiceXOId in (select Invoiceid from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusXOId + "') or PronoteHeaderId in (select PronoteHeaderID from PronoteHeader where InvoiceXOId in (select Invoiceid from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusXOId + "')))");
            }
            if (!string.IsNullOrEmpty(PronoteHeaderId))
                sql.Append(" AND PronoteHeaderId='" + PronoteHeaderId + "'");
            if (product != null)
                sql.Append(" AND ProductId='" + product.ProductId + "'");
            sql.Append(" ORDER BY CheckDate desc");
            ht.Add("sql", sql);
            return sqlmapper.QueryForList<Model.PCBoxFootCheck>("PCBoxFootCheck.SelectByRage", ht);
        }
    }
}
