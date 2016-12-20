//------------------------------------------------------------------------------
//
// file name:InvoiceQKAccessor.cs
// author: peidun
// create date:2008/7/28 11:05:20
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
    /// Data accessor of InvoiceQK
    /// </summary>
    public partial class InvoiceQKAccessor : EntityAccessor, IInvoiceQKAccessor
    {
        #region IInvoiceQKAccessor Members


        public IList<Book.Model.InvoiceQK> Select(DateTime datetime1, DateTime datetime2)
        {
            Hashtable pars = new Hashtable();
            pars.Add("datetime1", datetime1);
            pars.Add("datetime2", datetime2);
            return sqlmapper.QueryForList<Model.InvoiceQK>("InvoiceQK.select_by_time", pars); ;
        }

        public IList<Model.InvoiceQK> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceQK>("InvoiceQK.select_byStatus", (int)status);
        }

        #endregion
    }
}
