//------------------------------------------------------------------------------
//
// file name：PronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
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
    /// Data accessor of PronoteHeader
    /// </summary>
    public partial class PronoteHeaderAccessor : EntityAccessor, IPronoteHeaderAccessor
    {
        public IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();

            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.PronoteHeader>("PronoteHeader.select_GetByDate", ht);
        }
        public IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_byCustomerANDdate", ht);
        }
    }
}
