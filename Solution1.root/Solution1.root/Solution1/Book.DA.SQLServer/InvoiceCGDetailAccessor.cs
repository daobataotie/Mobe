//------------------------------------------------------------------------------
//
// file name:InvoiceCGDetailAccessor.cs
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
    /// Data accessor of InvoiceCGDetail
    /// </summary>
    public partial class InvoiceCGDetailAccessor : EntityAccessor, IInvoiceCGDetailAccessor
    {
        #region IInvoiceCGDetailAccessor Members


        public IList<Book.Model.InvoiceCGDetail> Select(Book.Model.InvoiceCG invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceCG invoice)
        {
            sqlmapper.Delete("InvoiceCGDetail.delete_by_invoiceid", invoice.InvoiceId);
        }


        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startDate", startDate);
            pars.Add("endDate", endDate);
            pars.Add("startId", startId);
            pars.Add("endId", endId);
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyDateReangeAndIdReange", pars);
        }

        public IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startDate", startDate);
            pars.Add("endDate", endDate);
            pars.Add("csid", csid);
            pars.Add("ceid", ceid);
            pars.Add("psid", psid);
            pars.Add("peid", peid);
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyDateReangeAndProductReangeCompanyReange", pars);

        }
        public IList<Book.Model.InvoiceCGDetail> SelectCount(Book.Model.InvoiceCO invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.select_count", invoice.InvoiceId);
        }

        public IList<Model.InvoiceCGDetail> SelectbyinvoiceIdfz(Model.InvoiceCG invoicecg)
        {
            return sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectbyinvoiceIdfz", invoicecg.InvoiceId);
        }

        public IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice, string productStart, string productEnd)
        {
            IList<Book.Model.InvoiceCGDetail> invoicecg = null;
            Hashtable ht = new Hashtable();

            ht.Add("invoiceId", invoice.InvoiceId);
            ht.Add("productStart", productStart);
            ht.Add("productEnd", productEnd);

            if (string.IsNullOrEmpty(productStart) && !string.IsNullOrEmpty(productEnd))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdQuJianStartNULL", ht);
            }
            if (string.IsNullOrEmpty(productEnd) && !string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdQuJianEndNULL", ht);
            }
            if (string.IsNullOrEmpty(productEnd) && string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdQuJianStartEndNULL", ht);
            }
            if (!string.IsNullOrEmpty(productEnd) && !string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdQuJian", ht);
            }
            return invoicecg;
        }

        public Model.InvoiceCGDetail SelectByProductIdAndHeadIdAndPositionId(string productId, string invoiceId, string positionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("invoiceId", invoiceId);
            ht.Add("depotpositionId", positionId);
            return sqlmapper.QueryForObject<Model.InvoiceCGDetail>("InvoiceCGDetail.selectByProductIdAndHeadIdAndPositionId", ht);
        }

        public double GetSumByProductIdAndInvoiceId(string productId, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("invoiceId", invoiceId);
            return sqlmapper.QueryForObject<double>("InvoiceCGDetail.GetSumByProductIdAndInvoiceId", ht);
        }

        #endregion
    }
}
