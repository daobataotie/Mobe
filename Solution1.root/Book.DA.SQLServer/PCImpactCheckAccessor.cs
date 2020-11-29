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


        #region 适用于首件上线检查表

        public Book.Model.PCImpactCheck PFCGetFirst(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.PCImpactCheck>("PCImpactCheck.PFCGetFirst", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.PCImpactCheck PFCGetLast(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<Model.PCImpactCheck>("PCImpactCheck.PFCGetLast", PCFirstOnlineCheckDetailId);
        }

        public Book.Model.PCImpactCheck PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.PCImpactCheck>("PCImpactCheck.PFCGetPrev", ht);
        }

        public Book.Model.PCImpactCheck PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", InsertDate);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<Model.PCImpactCheck>("PCImpactCheck.PFCGetNext", ht);
        }

        public bool PFCHasRows(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForObject<bool>("PCImpactCheck.PFCHasRows", PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRowsBefore(Book.Model.PCImpactCheck e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("PCImpactCheck.PFCHasRowsBefore", ht);
        }

        public bool PFCHasRowsAfter(Book.Model.PCImpactCheck e, string PCFirstOnlineCheckDetailId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("PCFirstOnlineCheckDetailId", PCFirstOnlineCheckDetailId);
            return sqlmapper.QueryForObject<bool>("PCImpactCheck.PFCHasRowsAfter", ht);
        }

        public IList<Model.PCImpactCheck> PFCSelect(string PCFirstOnlineCheckDetailId)
        {
            return sqlmapper.QueryForList<Model.PCImpactCheck>("PCImpactCheck.PFCSelect", PCFirstOnlineCheckDetailId);
        }
        
        #endregion
    }
}
