//------------------------------------------------------------------------------
//
// file name:InvoiceXODetailAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceXODetail
    /// </summary>
    public partial class InvoiceXODetailAccessor : EntityAccessor, IInvoiceXODetailAccessor
    {
        #region IInvoiceXODetailAccessor 成员


        public IList<Book.Model.InvoiceXODetail> Select(Book.Model.InvoiceXO invoiceXO)
        {
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.select_by_invoiceid", invoiceXO.InvoiceId);
        }


        public void Delete(Book.Model.InvoiceXO invoice)
        {
            sqlmapper.Delete("InvoiceXODetail.delete_by_invoiceid", invoice.InvoiceId);
        }
        public float GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return sqlmapper.QueryForObject<float>("InvoiceXODetail.select_by_InvoiceXODetailId", invoiceXODetailId);
        }
        public Book.Model.InvoiceXODetail GetInvoiceXOAndProductById(string invoiceXODetailId)
        {
            return sqlmapper.QueryForObject<Model.InvoiceXODetail>("InvoiceXODetail.select_by_InvoiceXODetailAndProductId", invoiceXODetailId);
        }
        public Book.Model.InvoiceXODetail GetAllCurrentNum()
        {
            return sqlmapper.QueryForObject<Model.InvoiceXODetail>("InvoiceXODetail.select_by_AllCurrentNum",null);
        }
        public IList<Book.Model.InvoiceXODetail> select_XOnotInMps()
        {
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.select_XOnotInMps", null);
        }

        public IList<Model.InvoiceXODetail> SelectByDateRangeAndPid(string productid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", productid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceXODetail>("InvoiceXODetail.SelectByDateRangeAndPid", ht);
        }
        #endregion
    }
}
