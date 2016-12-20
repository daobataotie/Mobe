//------------------------------------------------------------------------------
//
// file name：InvoiceHCAccessor.cs
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
    /// Data accessor of InvoiceHC
    /// </summary>
    public partial class InvoiceHCAccessor : EntityAccessor, IInvoiceHCAccessor
    {
        public IList<Book.Model.InvoiceHC> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceHC>("InvoiceHC.select_byTime", pars);
        }


        public IList<Book.Model.InvoiceHC> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceHC>("InvoiceHC.select_byStatus", (int)status);
        }
    }
}
