//------------------------------------------------------------------------------
//
// file name:InvoiceCOAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceCO
    /// </summary>
    public partial class InvoiceCOAccessor : EntityAccessor, IInvoiceCOAccessor
    {
        public IList<Book.Model.InvoiceCO> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_byTime", pars);
        }

        public IList<Book.Model.InvoiceCO> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_byStatus", (int)status);
        }
        public IList<Book.Model.InvoiceCO> Select(Model.Supplier supplier)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.select_bySupplier", supplier.SupplierId);
        }
        public void Updates(Model.InvoiceCO invoiceCO)
        {
            this.Update<Model.InvoiceCO>(invoiceCO);
        }

        public IList<Model.InvoiceCO> SelectbySupplierAndinvoiceId(Model.Supplier supplier, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("supperId", supplier == null ? null : supplier.SupplierId);
            ht.Add("invoiceId", invoiceId == "" ? null : invoiceId);
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.selectbySupplierAndinvoiceId", ht);
        }

        public IList<Model.InvoiceCO> SelectByMrsHeaderId(string MrsHeaderId)
        {
            return sqlmapper.QueryForList<Model.InvoiceCO>("InvoiceCO.selectByMrsHeaderId", MrsHeaderId);
        }

        /// <summary>
        /// invoiceFlag  2代表结案
        /// </summary>
        /// <param name="SupplierStart"></param>
        /// <param name="SupplierEnd"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="productStart"></param>
        /// <param name="productEnd"></param>
        /// <param name="cusxoid"></param>
        /// <param name="invoiceFlag"></param>
        /// <returns></returns>
        public IList<Book.Model.InvoiceCO> SelectDateRangAndWhere(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime? dateStart, DateTime? dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, int invoiceFlag)
        {
            Hashtable ht = new Hashtable();
            ht.Add("dateStart", dateStart);
            ht.Add("dateEnd", dateEnd);
            StringBuilder sql = new StringBuilder();
            if (SupplierStart != null&&SupplierEnd!=null)
                sql.Append(" and  SupplierId in (select SupplierId from Supplier where Id between '" + SupplierStart.Id + "' and '" + SupplierEnd.Id + "')" ) ;
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and    InvoiceCustomXOId like '%" + cusxoid + "%' ");
            if (productStart != null&&productEnd!=null)
                sql.Append(" and  InvoiceId in(select invoiceid from invoicecodetail where productid in(select productid from product where productname  between '" + productStart.ProductName + "' and '" + productEnd.ProductName + "'))  ");                   
            if (invoiceFlag == 1 )//只顯示未結案
                sql.Append(" and  IsClose=0");
            if (dateJHStart != null && dateJHEnd!= null)
                sql.Append(" and InvoiceYjrq between '" + dateJHStart.ToString("yyyy-MM-dd") + "'  and  '" + dateJHEnd.ToString("yyyy-MM-dd") + "'");
            if (costartid != null && coendid != null)
                sql.Append(" and  invoiceid between  '" + costartid + "' and '" + coendid + "' ");
            sql.Append(" order by InvoiceId desc");
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<Book.Model.InvoiceCO>("InvoiceCO.select_where", ht);


        }
    }
}
