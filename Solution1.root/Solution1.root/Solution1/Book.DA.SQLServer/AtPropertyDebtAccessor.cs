//------------------------------------------------------------------------------
//
// file name：AtPropertyDebtAccessor.cs
// author: mayanjun
// create date：2011-2-28 15:30:43
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
    /// Data accessor of AtPropertyDebt
    /// </summary>
    public partial class AtPropertyDebtAccessor : EntityAccessor, IAtPropertyDebtAccessor
    {
        public IList<Book.Model.AtPropertyDebt> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtPropertyDebt>("AtPropertyDebt.select_ByDate", ht);
        }
    }
}
