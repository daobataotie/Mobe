//------------------------------------------------------------------------------
//
// file name：CustomerAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
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
    /// Data accessor of Customer
    /// </summary>
    public partial class CustomerAccessor : EntityAccessor, ICustomerAccessor
    {

        public IList<Model.Customer> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            Hashtable pars = new Hashtable();
            pars.Add("customerStart", customerStart);
            pars.Add("customerEnd", customerEnd);
            pars.Add("dateStart", dateStart);
            pars.Add("dateEnd", dateEnd);
            return sqlmapper.QueryForList<Model.Customer>("Customer.select_byXSDate", pars);
        }
        public IList<Model.Customer> selectCustomerInXS()
        {
            return sqlmapper.QueryForList<Model.Customer>("Customer.select_customerInXS", null);
        }
        public bool IsExistFullName(Model.Customer customer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", customer.CustomerId == null ? "" : customer.CustomerId);
            ht.Add("fullname", customer.CustomerFullName);
            return sqlmapper.QueryForObject<bool>("Customer.IsExistFullName", ht);
        }
        public bool IsExistShortName(Model.Customer customer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", customer.CustomerId == null ? "" : customer.CustomerId);
            ht.Add("shortname", customer.CustomerShortName);
            return sqlmapper.QueryForObject<bool>("Customer.IsExistShortName", ht);
        }
    }
}
