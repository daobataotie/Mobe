//------------------------------------------------------------------------------
//
// file name:InvoiceQOAccessor.cs
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
    /// Data accessor of InvoiceQO
    /// </summary>
    public partial class InvoiceQOAccessor : EntityAccessor, IInvoiceQOAccessor
    {
        #region IInvoiceQOAccessor 成员


        public IList<Book.Model.InvoiceQO> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceQO>("InvoiceQO.select_byTime", pars);
        }

        #endregion

        #region IInvoiceQOAccessor 成员


        public IList<Book.Model.InvoiceQO> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceQO>("InvoiceQO.select_byStatus", (int)status);
        }

        #endregion
    }
}
