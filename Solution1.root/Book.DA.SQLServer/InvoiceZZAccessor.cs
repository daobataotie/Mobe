//------------------------------------------------------------------------------
//
// file name:InvoiceZZAccessor.cs
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
    /// Data accessor of InvoiceZZ
    /// </summary>
    public partial class InvoiceZZAccessor : EntityAccessor, IInvoiceZZAccessor
    {
        #region IInvoiceZZAccessor 成员


        public IList<Book.Model.InvoiceZZ> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceZZ>("InvoiceZZ.select_byTime", pars);
        }

        public IList<Book.Model.InvoiceZZ> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceZZ>("InvoiceZZ.select_byStatus", (int)status);
        }

        #endregion
    }
}
