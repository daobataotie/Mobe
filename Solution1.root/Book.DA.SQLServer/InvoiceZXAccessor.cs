//------------------------------------------------------------------------------
//
// file name：InvoiceZXAccessor.cs
// author: mayanjun
// create date：2012-10-29 14:32:19
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
    /// Data accessor of InvoiceZX
    /// </summary>
    public partial class InvoiceZXAccessor : EntityAccessor, IInvoiceZXAccessor
    {
        public IList<Model.InvoiceZX> selectByPackingId(string PackingId)
        {
            return sqlmapper.QueryForList<Model.InvoiceZX>("InvoiceZX.selectByPackingId", PackingId);
        }

        public IList<Model.InvoiceZX> SelectPackingRecord(DateTime dateStart, DateTime dateEnd, Model.Customer customer, Model.Customer XOcustomer)
        {
            StringBuilder str = new StringBuilder();
            if (customer != null)
                str.Append(" and CustomerId='" + customer.CustomerId + "'");
            if (XOcustomer != null)
                str.Append(" and xocustomerId='" + XOcustomer.CustomerId + "'");
            str.Append("  ORDER BY InvoiceDate desc");
            Hashtable ha = new Hashtable();
            ha.Add("dateStart", dateStart);
            ha.Add("dateEnd", dateEnd);
            ha.Add("sql", str.ToString());
            return sqlmapper.QueryForList<Model.InvoiceZX>("InvoiceZX.selectByTimeAndCustomer", ha);

        }

        public int SelectHasPackingNum(string productId)
        {
            return sqlmapper.QueryForObject<int>("InvoiceZX.selectHasPackingNum", productId);
        }
    }
}
