//------------------------------------------------------------------------------
//
// file name：AtBillsIncomeAccessor.cs
// author: mayanjun
// create date：2010-11-22 14:21:21
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
    /// Data accessor of AtBillsIncome
    /// </summary>
    public partial class AtBillsIncomeAccessor : EntityAccessor, IAtBillsIncomeAccessor
    {
        public IList<Book.Model.AtBillsIncome> SelectAtBillsIncomeByBillsOften(string billsOften, string collectionAccount)
        {
            Hashtable pars = new Hashtable();
            pars.Add("BillsOften", billsOften);
            pars.Add("CollectionAccount", collectionAccount);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_AtBillsIncomeBillsOften", pars);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdate", ht);
        }
        public IList<Book.Model.AtBillsIncome> Select(DateTime startDate, DateTime endDate,string startCollectionAccount,string endCollectionAccount)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("startCollectionAccount", startCollectionAccount);
            ht.Add("endCollectionAccount", endCollectionAccount);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdateAndBank", ht);
        }
        public IList<Book.Model.AtBillsIncome> SelectOferAndDate(DateTime startDate, DateTime endDate, string billsOften)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("BillsOften", billsOften);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_byDdate2", ht);
        }
        public IList<Book.Model.AtBillsIncome> SelectDuiXianByDate(DateTime startDate, DateTime endDate, DateTime daoQiDate1, DateTime daoQiDate2,string billsOften, string IncomeCategory)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("daoQiDate1", daoQiDate1);
            ht.Add("daoQiDate2", daoQiDate2);
            ht.Add("BillsOften", billsOften);
            ht.Add("IncomeCategory", IncomeCategory);
            return sqlmapper.QueryForList<Model.AtBillsIncome>("AtBillsIncome.select_DuiXianByDate", ht);
        }


    }
}
