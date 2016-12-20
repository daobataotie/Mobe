//------------------------------------------------------------------------------
//
// file name：InvoiceXJProcessAccessor.cs
// author: mayanjun
// create date：2010-8-25 16:07:42
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
    /// Data accessor of InvoiceXJProcess
    /// </summary>
    public partial class InvoiceXJProcessAccessor : EntityAccessor, IInvoiceXJProcessAccessor
    {
        public IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJDetail InvoiceXJDetail)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJProcess>("InvoiceXJProcess.select_by_invoicexjdetail", InvoiceXJDetail.InvoiceXJDetailId);

        }

        public void Delete(Model.InvoiceXJDetail InvoiceXJDetail)
        {
            sqlmapper.Delete("InvoiceXJProcess.delete_by_invoicexjdetail", InvoiceXJDetail.InvoiceXJDetailId);

        }

        public IList<Book.Model.InvoiceXJProcess> Select(Model.InvoiceXJ InvoiceXJ)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJProcess>("InvoiceXJProcess.select_by_invoicexjid", InvoiceXJ.InvoiceId);

        }

        public void Delete(Model.InvoiceXJ InvoiceXJ)
        {
            sqlmapper.Delete("InvoiceXJProcess.delete_by_invoicexj", InvoiceXJ.InvoiceId);
        }

        public void DeleteByHeaderId(string invoiceid)
        {
            sqlmapper.Delete("InvoiceXJProcess.delete_by_invoicexj", invoiceid);
        }
    }
}
