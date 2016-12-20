//------------------------------------------------------------------------------
//
// file name：MPSdetailsAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:41
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
    /// Data accessor of MPSdetails
    /// </summary>
    public partial class MPSdetailsAccessor : EntityAccessor, IMPSdetailsAccessor
    {
        public IList<Book.Model.MPSdetails> Select(Model.MPSheader mPSheader)
        {
            return sqlmapper.QueryForList<Model.MPSdetails>("MPSdetails.select_byMPSheaderId", mPSheader.MPSheaderId);
        }
        public IList<Book.Model.MPSdetails> SelectState()
        {
            return sqlmapper.QueryForList<Model.MPSdetails>("MPSdetails.select_byState", null);
        }
        public IList<Book.Model.MPSdetails> Select(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.MPSdetails>("MPSdetails.select_byCustomerId", customer.CustomerId);
        }
        public IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd, string mpsheaderIdStart, string mpsheaderIdEnd, DateTime dateStart, DateTime dateEnd, string productId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerStart", customerStart);
            ht.Add("customerEnd", customerEnd);
            ht.Add("mpsheaderIdStart", mpsheaderIdStart);
            ht.Add("mpsheaderIdEnd", mpsheaderIdEnd);
            ht.Add("dateStart", dateStart);
            ht.Add("dateEnd", dateEnd);
            ht.Add("productId", productId);
            return sqlmapper.QueryForList<Book.Model.MPSdetails>("MPSdetails.selectbydateProCustomer", ht);
        }
        public IList<Book.Model.MPSdetails> Select(Model.InvoiceXO invoiceXO)
        {
            return sqlmapper.QueryForList<Model.MPSdetails>("MPSdetails.select_byInvoiceXOId", invoiceXO.InvoiceId);
        }
        public double GetByMPSdetailsId(string mPSdetailsId)
        {
            return sqlmapper.QueryForObject<double>("MPSdetails.select_byMpsDetailId", mPSdetailsId);
        }
        public double GetByInvoiceXODetailId(string invoiceXODetailId)
        {
            return sqlmapper.QueryForObject<double>("MPSdetails.select_byInvoiceXODetailId", invoiceXODetailId);
        }

        public IList<Book.Model.MPSdetails> Select(string customerStart, string customerEnd,   DateTime dateStart, DateTime dateEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            return sqlmapper.QueryForList<Book.Model.MPSdetails>("MPSdetails.select_byCustomerANDdate", ht);
        }
    }
}
