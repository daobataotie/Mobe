//------------------------------------------------------------------------------
//
// file name:InvoiceSKAccessor.cs
// author: peidun
// create date:2008/6/6 14:48:21
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
    /// Data accessor of InvoiceSK
    /// </summary>
    public partial class InvoiceSKAccessor : EntityAccessor, IInvoiceSKAccessor
    {
        #region IInvoiceSKAccessor 成员


        public IList<Book.Model.InvoiceSK> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceSK>("InvoiceSK.select_byTime", pars);
        }

        #endregion

        #region IInvoiceSKAccessor 成员


        public IList<Book.Model.InvoiceSK> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceSK>("InvoiceSK.select_byStatus", (int)status);
        }

        #endregion
    }
}
