//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentAccessor.cs
// author: mayanjun
// create date：2011-6-10 10:11:50
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
    /// Data accessor of AcOtherShouldPayment
    /// </summary>
    public partial class AcOtherShouldPaymentAccessor : EntityAccessor, IAcOtherShouldPaymentAccessor
    {
        public IList<Book.Model.AcOtherShouldPayment> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.AcOtherShouldPayment>("AcOtherShouldPayment.SelectByDateRange", ht);
        }

        public IList<Book.Model.AcOtherShouldPayment> SelectByDateRangeAndSupCompany(DateTime startdate, DateTime enddate, Book.Model.Supplier supplier, Book.Model.Company company)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("supplier", supplier == null ? null : supplier.SupplierId);
            ht.Add("company", company == null ? null : company.CompanyId);
            return sqlmapper.QueryForList<Model.AcOtherShouldPayment>("AcOtherShouldPayment.SelectByDateRangeAndSupCompany", ht);
        }

    }
}
