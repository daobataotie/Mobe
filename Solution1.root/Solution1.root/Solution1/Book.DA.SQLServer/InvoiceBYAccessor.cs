//------------------------------------------------------------------------------
//
// file name:InvoiceBYAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
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
    /// Data accessor of InvoiceBY
    /// </summary>
    public partial class InvoiceBYAccessor : EntityAccessor, IInvoiceBYAccessor
    {
        #region IInvoiceBYAccessor 成员


        public IList<Book.Model.InvoiceBY> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceBY>("InvoiceBY.select_byTime", pars);
        }

        #endregion

        #region IInvoiceBYAccessor 成员


        public IList<Book.Model.InvoiceBY> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceBY>("InvoiceBY.select_byStatus", (int)status);
        }

        #endregion
    }
}
