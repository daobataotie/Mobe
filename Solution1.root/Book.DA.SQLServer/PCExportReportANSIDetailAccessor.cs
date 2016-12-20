//------------------------------------------------------------------------------
//
// file name：PCExportReportANSIDetailAccessor.cs
// author: mayanjun
// create date：2012-6-13 14:02:26
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
    /// Data accessor of PCExportReportANSIDetail
    /// </summary>
    public partial class PCExportReportANSIDetailAccessor : EntityAccessor, IPCExportReportANSIDetailAccessor
    {
        public Book.Model.PCExportReportANSIDetail mGetFirst(string FromPC)
        {
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.mGetFirst", FromPC);
        }

        public Book.Model.PCExportReportANSIDetail mGetLast(string FromPC)
        {
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.mGetLast", FromPC);
        }

        public Book.Model.PCExportReportANSIDetail mGetPrev(DateTime InsertDate, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.mGetPrev", ht);
        }

        public Book.Model.PCExportReportANSIDetail mGetNext(DateTime InsertDate, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.mGetNext", ht);
        }

        public bool mHasRows(string FromPC)
        {
            return sqlmapper.QueryForObject<bool>("PCExportReportANSIDetail.mHasRows", FromPC);
        }

        public bool mHasRowsBefore(Book.Model.PCExportReportANSIDetail e, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForObject<bool>("PCExportReportANSIDetail.mHasRowsBefore", ht);
        }

        public bool mHasRowsAfter(Book.Model.PCExportReportANSIDetail e, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertTime", e.InsertTime.Value);
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForObject<bool>("PCExportReportANSIDetail.mHasRowsAfter", ht);
        }

        public IList<Book.Model.PCExportReportANSIDetail> mSelect(string FromPC)
        {
            return sqlmapper.QueryForList<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.mSelect", FromPC);
        }

        public IList<Book.Model.PCExportReportANSIDetail> SelectByDateRage(DateTime startdate, DateTime enddate, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForList<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectByDateRage", ht);
        }

        public void DeleteByFromPC(string FromPC)
        {
            sqlmapper.Delete("PCExportReportANSIDetail.DeleteByFromPC", FromPC);
        }

        public Book.Model.PCExportReportANSIDetail SelectForExpANSIDetailsSUM(string InvoiceCusXOId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXOId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectForExpANSIDetailsSUM", ht);
        }

        public IList<Book.Model.PCExportReportANSIDetail> SelectByCusXoIdAndProId(string InvoiceCusXoId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForList<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectByCusXoIdAndProId", ht);
        }

        public Book.Model.PCExportReportANSIDetail SelectForExpCSADetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectForExpCSADetailsSUM", ht);
        }

        public int HasCheckSum(string InvoiceCusXoId, string ProductId, string FromPC)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            ht.Add("FromPC", FromPC);
            return sqlmapper.QueryForObject<int>("PCExportReportANSIDetail.SelectHasCheckSUM", ht);
        }

        public Book.Model.PCExportReportANSIDetail SelectForExpCEENDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectForExpCEENDetailsSUM", ht);
        }

        public Book.Model.PCExportReportANSIDetail SelectForExpASDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectForExpASDetailsSUM", ht);
        }

        public Book.Model.PCExportReportANSIDetail SelectForExpJISDetailsSUM(string InvoiceCusXoId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceCusXOId", InvoiceCusXoId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectForExpJISDetailsSUM", ht);
        }

        public IList<Book.Model.PCExportReportANSIDetail> SelectByCondition(DateTime startdate, DateTime enddate, string CusInvoiceXOId, Book.Model.Product product, string pcExpType)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd"));
            ht.Add("CusInvoiceXOId", string.IsNullOrEmpty(CusInvoiceXOId) ? null : CusInvoiceXOId);
            ht.Add("productId", product == null ? null : product.ProductId);
            ht.Add("pcExpType", string.IsNullOrEmpty(pcExpType) ? null : pcExpType);
            return sqlmapper.QueryForList<Model.PCExportReportANSIDetail>("PCExportReportANSIDetail.SelectByCondition", ht);
        }

        public IList<Model.PCExportReportANSIDetail> SelectAllDetail(DateTime startDate, DateTime endDate, string InvoiceCusXoId, string ProductId, string CustomerId, string type)
        {
            StringBuilder str = new StringBuilder();
            str.Append("SELECT PCExportReportANSIDetailId,CheckDate,InvoiceQuantity,MustCheckSum,HasCheckSum,PassSum,FromPC,InvoiceCusXOId,(select top 1 productname from product where product.productid = productid) as ProductName FROM PCExportReportANSIDetail WHERE CheckDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "'");
            if (!string.IsNullOrEmpty(InvoiceCusXoId))
                str.Append(" AND InvoiceCusXOId='" + InvoiceCusXoId + "'");
            if (ProductId != null)
                str.Append(" AND ProductId='" + ProductId + "'");
            if (CustomerId != null)
                str.Append(" AND CustomerId='" + CustomerId + "'");
            str.Append(" AND TestType='" + type + "'");
            str.Append(" ORDER BY CheckDate");
            return this.DataReaderBind<Model.PCExportReportANSIDetail>(str.ToString(), null, CommandType.Text);
        }
    }
}
