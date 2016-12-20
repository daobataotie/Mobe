//------------------------------------------------------------------------------
//
// file name：InvoicePOAccessor.cs
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
    /// Data accessor of InvoicePO
    /// </summary>
    public partial class InvoicePOAccessor : EntityAccessor, IInvoicePOAccessor
    {
        public IList<Book.Model.InvoicePO> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoicePO>("InvoicePO.select_byTime", pars);
        }


        public IList<Book.Model.InvoicePO> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoicePO>("InvoicePO.select_byStatus", (int)status);
        }
    }
}
