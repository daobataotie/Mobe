//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-24 17:38:16
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
    /// Data accessor of PCDoubleImpactCheck
    /// </summary>
    public partial class PCDoubleImpactCheckAccessor : EntityAccessor, IPCDoubleImpactCheckAccessor
    {
        public IList<Book.Model.PCDoubleImpactCheck> SelectByDateRage(DateTime startdate, DateTime enddate, int pcFlag, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("pcFlag", pcFlag);
            StringBuilder sql = new StringBuilder();
            if (customer != null)
                sql.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId like '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sql.Append(" and ProductId = '" + product.ProductId + "'");
            ht.Add("sql", sql.ToString());
            IList<Model.PCDoubleImpactCheck> a = sqlmapper.QueryForList<Model.PCDoubleImpactCheck>("PCDoubleImpactCheck.SelectByDateRage", ht);
            return a;
        }
    }
}
