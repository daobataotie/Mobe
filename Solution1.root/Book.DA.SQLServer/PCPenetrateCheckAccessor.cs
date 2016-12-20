//------------------------------------------------------------------------------
//
// file name：PCPenetrateCheckAccessor.cs
// author: mayanjun
// create date：2012-3-21 11:02:47
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
    /// Data accessor of PCPenetrateCheck
    /// </summary>
    public partial class PCPenetrateCheckAccessor : EntityAccessor, IPCPenetrateCheckAccessor
    {
        public IList<Book.Model.PCPenetrateCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
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
            return sqlmapper.QueryForList<Model.PCPenetrateCheck>("PCPenetrateCheck.SelectByDateRange", ht);
        }
    }
}
