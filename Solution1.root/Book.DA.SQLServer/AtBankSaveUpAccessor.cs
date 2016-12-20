//------------------------------------------------------------------------------
//
// file name：AtBankSaveUpAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:51:10
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
    /// Data accessor of AtBankSaveUp
    /// </summary>
    public partial class AtBankSaveUpAccessor : EntityAccessor, IAtBankSaveUpAccessor
    {
        public IList<Book.Model.AtBankSaveUp> Select(DateTime startDate, DateTime endDate, string bankAccountId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("BankAccountId", bankAccountId);
            return sqlmapper.QueryForList<Model.AtBankSaveUp>("AtBankSaveUp.select_byDdateAndBank", ht);
        }
    }
}
