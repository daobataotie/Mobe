//------------------------------------------------------------------------------
//
// file name：IInvoiceCGDetailAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.InvoiceCGDetail
    /// </summary>
    public partial interface IInvoiceCGDetailAccessor : IEntityAccessor
    {
        IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice);

        /// <summary>
        /// 删除所属单据的所有明细
        /// </summary>
        void Delete(Model.InvoiceCG invoice);

        IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string startId, string endId);

        IList<Book.Model.InvoiceCGDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid);
        IList<Book.Model.InvoiceCGDetail> SelectCount(Book.Model.InvoiceCO invoice);
        IList<Model.InvoiceCGDetail> Select(Model.InvoiceCG invoice, Model.Product productStart, Model.Product productEnd);
        Model.InvoiceCGDetail SelectByProductIdAndHeadIdAndPositionId(string productId, string invoiceId, string positionId);
        double GetSumByProductIdAndInvoiceId(string productId, string invoiceId);
        IList<Model.InvoiceCGDetail> SelectbyinvoiceIdfz(Model.InvoiceCG invoicecg);
        IList<Model.InvoiceCGDetail> SelectByDateRange(DateTime startdate, DateTime enddate);
        void UpdateInvoiceCGDetailFPQuantityById(string id, string InvoiceCGDetailFPQuantity);
        DateTime SelectLastInvoiceCGDate(string productId, string depotpositionId);

        System.Data.DataTable SelectByConditionCOBiao(DateTime startdate, DateTime enddate, DateTime JHstartdate, DateTime JHenddate, DateTime? FKStartDate, DateTime? FKEndDate, Model.Supplier startSupplier, Model.Supplier endSupplier, Model.Product productStart, Model.Product productEnd, string coidStart, string coidEnd, string CusXOid, Model.Employee empstart, Model.Employee empend);

        IList<Model.InvoiceCGDetail> SelectByConditionCO(DateTime startdate, DateTime enddate, DateTime JHstartdate, DateTime JHenddate, DateTime? FKStartDate, DateTime? FKEndDate, Model.Supplier startSupplier, Model.Supplier endSupplier, Model.Product productStart, Model.Product productEnd, string coidStart, string coidEnd, string CusXOid, Model.Employee empstart, Model.Employee empend);

        decimal? SelectLatelyProductPrice(string productid);
        string SelectByInvoiceId(string invoiceid);
    }
}

