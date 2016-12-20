//------------------------------------------------------------------------------
//
// file name：PCExportReportANSIAccessor.cs
// author: mayanjun
// create date：2012-3-9 17:01:20
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
    /// Data accessor of PCExportReportANSI
    /// </summary>
    public partial class PCExportReportANSIAccessor : EntityAccessor, IPCExportReportANSIAccessor
    {
        public Book.Model.PCExportReportANSI SelectForExpANSI(string InvoiceCusXoid, string productid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXoid", InvoiceCusXoid);
            ht.Add("productid", productid);

            //return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.SelectForExpANSI", ht);
            return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.SelectForExpANSIDetails", ht);

        }

        public IList<Book.Model.PCExportReportANSI> SelectByDateRage(string ExpType, DateTime startdate, DateTime enddate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", string.IsNullOrEmpty(ExpType) ? null : ExpType);
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            StringBuilder sb = new StringBuilder();
            if (customer != null)
                sb.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sb.Append(" and ProductId = '" + product.ProductId + "'");
            ht.Add("sql", sb.ToString());

            return sqlmapper.QueryForList<Model.PCExportReportANSI>("PCExportReportANSI.SelectByDateRage", ht);
        }

        public IList<Book.Model.PCExportReportANSI> SelectForChooseExport(DateTime startdate, DateTime enddate, Book.Model.Product product, Book.Model.Customer customer, string CusXOId, string ExpType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            ht.Add("ExpType", ExpType);
            StringBuilder sb = new StringBuilder();
            if (customer != null)
                sb.Append(" and InvoiceCusXOId IN (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE xocustomerId = '" + customer.CustomerId + "')");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" and InvoiceCusXOId like '%" + CusXOId + "%'");
            if (product != null)
                sb.Append(" and ProductId = '" + product.ProductId + "'");
            if (!string.IsNullOrEmpty(ExpType))
                sb.Append(" and ExpType ='" + ExpType + "'");
            ht.Add("sql", sb.ToString());

            return sqlmapper.QueryForList<Model.PCExportReportANSI>("PCExportReportANSI.SelectByDateRage", ht);
        }

        public Book.Model.PCExportReportANSI mget_last(string ExpType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.mget_last", ht);
        }

        public Book.Model.PCExportReportANSI mget_first(string ExpType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.mget_first", ht);
        }

        public Book.Model.PCExportReportANSI mget_prev(string ExpType, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.mget_prev", ht);
        }

        public Book.Model.PCExportReportANSI mget_next(string ExpType, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForObject<Model.PCExportReportANSI>("PCExportReportANSI.mget_next", ht);
        }

        public bool mhas_rows(string ExpType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            return sqlmapper.QueryForObject<bool>("PCExportReportANSI.mhas_rows", ht);
        }

        public bool mhas_rows_before(string ExpType, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForObject<bool>("PCExportReportANSI.mhas_rows_before", ht);
        }

        public bool mhas_rows_after(string ExpType, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ExpType", ExpType);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForObject<bool>("PCExportReportANSI.mhas_rows_after", ht);
        }

        public IList<Model.PCExportReportANSI> SelectByInvoiceCusId(string invoiceCusId, string type)
        {

            Hashtable ht = new Hashtable();
            ht.Add("invoiceCusId", invoiceCusId);
            ht.Add("JIS", type);
            return sqlmapper.QueryForList<Model.PCExportReportANSI>("PCExportReportANSI.SelectByInvoiceCusId", ht);
        }

    }
}
