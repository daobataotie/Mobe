//------------------------------------------------------------------------------
//
// file name：InvoiceJCAccessor.cs
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
    /// Data accessor of InvoiceJC
    /// </summary>
    public partial class InvoiceJCAccessor : EntityAccessor, IInvoiceJCAccessor
    {
        #region IInvoiceJCAccessor 成员


        public IList<Book.Model.InvoiceJC> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceJC>("InvoiceJC.select_byTime", pars);
        }


        public IList<Book.Model.InvoiceJC> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceJC>("InvoiceJC.select_byStatus", (int)status);
        }

        #endregion
    }
}
