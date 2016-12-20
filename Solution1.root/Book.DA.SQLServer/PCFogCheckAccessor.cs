//------------------------------------------------------------------------------
//
// file name：PCFogCheckAccessor.cs
// author: mayanjun
// create date：2012-3-16 17:42:23
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
    /// Data accessor of PCFogCheck
    /// </summary>
    public partial class PCFogCheckAccessor : EntityAccessor, IPCFogCheckAccessor
    {
        public IList<Book.Model.PCFogCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            StringBuilder sql = new StringBuilder();
            if (customer != null)
                sql.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sql.Append(" and ProductId = '" + product.ProductId + "'");
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<Model.PCFogCheck>("PCFogCheck.SelectByDateRange", ht);
        }

        public IList<Model.PCFogCheck> SelectByDate(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.PCFogCheck>("PCFogCheck.SelectByDate", ht);
        }
    }
}
