//------------------------------------------------------------------------------
//
// file name：InvoicePIAccessor.cs
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
    /// Data accessor of InvoicePI
    /// </summary>
    public partial class InvoicePIAccessor : EntityAccessor, IInvoicePIAccessor
    {
        public IList<Book.Model.InvoicePI> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoicePI>("InvoicePI.select_byTime", pars);
        }


        public IList<Book.Model.InvoicePI> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoicePI>("InvoicePI.select_byStatus", (int)status);
        }
    }
}
