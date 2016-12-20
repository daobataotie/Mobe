//------------------------------------------------------------------------------
//
// file name:InvoiceFKAccessor.cs
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
    /// Data accessor of InvoiceFK
    /// </summary>
    public partial class InvoiceFKAccessor : EntityAccessor, IInvoiceFKAccessor
    {
        #region IInvoiceFKAccessor 成员


        public IList<Book.Model.InvoiceFK> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceFK>("InvoiceFK.select_byTime", pars);
        }

        public IList<Book.Model.InvoiceFK> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceFK>("InvoiceFK.select_byStatus", (int)status);
        }

        #endregion
    }
}
