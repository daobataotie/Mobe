//------------------------------------------------------------------------------
//
// file name:InvoiceHZAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceHZ
    /// </summary>
    public partial class InvoiceHZAccessor : EntityAccessor, IInvoiceHZAccessor
    {
        #region IInvoiceHZAccessor 成员


        public IList<Book.Model.InvoiceHZ> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceHZ>("InvoiceHZ.select_byTime", pars);
        }

        #endregion

        #region IInvoiceHZAccessor 成员


        public IList<Book.Model.InvoiceHZ> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceHZ>("InvoiceHZ.select_byStatus", (int)status);
        }

        #endregion
    }
}
