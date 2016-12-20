//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollectionAccessor.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
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
    /// Data accessor of AcOtherShouldCollection
    /// </summary>
    public partial class AcOtherShouldCollectionAccessor : EntityAccessor, IAcOtherShouldCollectionAccessor
    {
        public IList<Book.Model.AcOtherShouldCollection> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AcOtherShouldCollection>("AcOtherShouldCollection.SelectByDateRange", ht);
        }


        public IList<Book.Model.AcOtherShouldCollection> SelectByDateRangeAndCustomerCompany(DateTime startdate, DateTime enddate, Book.Model.Customer customer, Book.Model.Company company)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("customer", customer == null ? null : customer.CustomerId);
            ht.Add("company", company == null ? null : company.CompanyId);

            return sqlmapper.QueryForList<Model.AcOtherShouldCollection>("AcOtherShouldCollection.SelectByDateRangeAndCustomerCompany", ht);
        }
    }
}
