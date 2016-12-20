//------------------------------------------------------------------------------
//
// file name:InvoiceXJAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceXJ
    /// </summary>
    public partial class InvoiceXJAccessor : EntityAccessor, IInvoiceXJAccessor
    {
        public IList<Book.Model.InvoiceXJ> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceXJ>("InvoiceXJ.select_byTime", pars);
        }

        public IList<Book.Model.InvoiceXJ> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJ>("InvoiceXJ.select_byStatus", (int)status);
        }

        public IList<Book.Model.InvoiceXJ> Select(DateTime startDate, DateTime endDate, string customerid, string productid, string invoicexjid, string companyid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.ToString("yyyy-MM-dd"));
            ht.Add("customerid", customerid);
            ht.Add("productid", productid);
            ht.Add("invoicexjid", invoicexjid);
            ht.Add("companyid", companyid);

            return sqlmapper.QueryForList<Model.InvoiceXJ>("InvoiceXJ.SelectByHead", ht);
        }
    }
}
