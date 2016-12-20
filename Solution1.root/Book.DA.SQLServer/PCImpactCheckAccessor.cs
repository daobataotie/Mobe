//------------------------------------------------------------------------------
//
// file name：PCImpactCheckAccessor.cs
// author: mayanjun
// create date：2011-11-15 13:56:54
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
    /// Data accessor of PCImpactCheck
    /// </summary>
    public partial class PCImpactCheckAccessor : EntityAccessor, IPCImpactCheckAccessor
    {
        public IList<Book.Model.PCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", StartDate);
            ht.Add("enddate", EndDate);
            StringBuilder sql = new StringBuilder();
            if (customer != null)
                sql.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sql.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sql.Append(" and ProductId = '" + product.ProductId + "'");
            ht.Add("sql", sql.ToString());
            IList<Model.PCImpactCheck> a = sqlmapper.QueryForList<Model.PCImpactCheck>("PCImpactCheck.SelectByDateRange", ht);
            return a;
        }

    }
}
