//------------------------------------------------------------------------------
//
// file name：IPCExportReportANSIDetailAccessor.cs
// author: mayanjun
// create date：2012-6-13 14:02:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCExportReportANSIDetail
    /// </summary>
    public partial interface IPCExportReportANSIDetailAccessor : IAccessor
    {
        Model.PCExportReportANSIDetail mGetFirst(string FromPC);

        Model.PCExportReportANSIDetail mGetLast(string FromPC);

        Model.PCExportReportANSIDetail mGetPrev(DateTime InsertDate, string FromPC);

        Model.PCExportReportANSIDetail mGetNext(DateTime InsertDate, string FromPC);

        bool mHasRows(string FromPC);

        bool mHasRowsBefore(Model.PCExportReportANSIDetail e, string FromPC);

        bool mHasRowsAfter(Model.PCExportReportANSIDetail e, string FromPC);

        int HasCheckSum(string InvoiceCusXOId, string ProductId, string FromPC);

        IList<Model.PCExportReportANSIDetail> mSelect(string FromPC);

        IList<Model.PCExportReportANSIDetail> SelectByDateRage(DateTime startdate, DateTime enddate, string FromPC);

        IList<Model.PCExportReportANSIDetail> SelectByCondition(DateTime startdate, DateTime enddate, string CusInvoiceXOId, Model.Product product, string pcExpType);

        Model.PCExportReportANSIDetail SelectForExpANSIDetailsSUM(string InvoiceCusXoId, string ProductId);

        Model.PCExportReportANSIDetail SelectForExpCSADetailsSUM(string InvoiceCusXoId, string ProductId);

        Model.PCExportReportANSIDetail SelectForExpCEENDetailsSUM(string InvoiceCusXoId, string ProductId);

        Model.PCExportReportANSIDetail SelectForExpASDetailsSUM(string InvoiceCusXoId, string ProductId);

        Model.PCExportReportANSIDetail SelectForExpJISDetailsSUM(string InvoiceCusXoId, string ProductId);

        IList<Model.PCExportReportANSIDetail> SelectByCusXoIdAndProId(string InvoiceCusXoId, string ProductId);

        void DeleteByFromPC(string FromPC);

        IList<Model.PCExportReportANSIDetail> SelectAllDetail(DateTime startDate,DateTime endDate, string InvoiceCusXoId, string ProductId, string CustomerId, string type);

    }
}

