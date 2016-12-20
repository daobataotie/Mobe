//------------------------------------------------------------------------------
//
// file name：InvoiceJRAccessor.cs
// author: peidun
// create date：2008-11-29 12:52:19
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
    /// Data accessor of InvoiceJR
    /// </summary>
    public partial class InvoiceJRAccessor : EntityAccessor, IInvoiceJRAccessor
    {
        #region IInvoiceJRAccessor 成员

        public IList<Book.Model.InvoiceJR> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceJR>("InvoiceJR.select_byTime", pars);
        }


        public IList<Book.Model.InvoiceJR> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceJR>("InvoiceJR.select_byStatus", (int)status);
        }

        public IList<Model.InvoiceJR> Select(Model.InvoiceJR invoicejr)
        {
            return sqlmapper.QueryForList<Model.InvoiceJR>("InvoiceJR.select_by_supperId", invoicejr.InvoiceId);
        }

        #endregion
    }
}
