//------------------------------------------------------------------------------
//
// file name：IPCExportReportANSIAccessor.cs
// author: mayanjun
// create date：2012-3-9 17:01:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCExportReportANSI
    /// </summary>
    public partial interface IPCExportReportANSIAccessor : IAccessor
    {
        Model.PCExportReportANSI SelectForExpANSI(string InvoiceCusXoid, string productid);
        
        IList<Model.PCExportReportANSI> SelectByDateRage(string ExpType,DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId);

        IList<Model.PCExportReportANSI> SelectForChooseExport(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId, string ExpType);

        Model.PCExportReportANSI mget_last(string ExpType);

        Model.PCExportReportANSI mget_first(string ExpType);

        Model.PCExportReportANSI mget_prev(string ExpType, DateTime InsertTime);

        Model.PCExportReportANSI mget_next(string ExpType, DateTime InsertTime);

        bool mhas_rows(string ExpType);

        bool mhas_rows_before(string ExpType, DateTime InsertTime);

        bool mhas_rows_after(string ExpType, DateTime InsertTime); 

        //IList<Model.PCExportReportANSI> Mhas_rows_of(string ExportReportId, string ExpType);

        IList<Model.PCExportReportANSI> SelectByInvoiceCusId(string invoiceCusId, string type);
    }
}

