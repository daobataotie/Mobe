//------------------------------------------------------------------------------
//
// file name:InvoiceCGAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:49
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
    /// Data accessor of InvoiceCG
    /// </summary>
    public partial class InvoiceCGAccessor : EntityAccessor, IInvoiceCGAccessor
    {
        #region IInvoiceCGAccessor 成员


        public IList<Book.Model.InvoiceCG> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceCG>("InvoiceCG.select_byTime", pars);
        }

        public void OwedIncrement(Book.Model.InvoiceCG invoice, decimal money)
        {
            this.OwedIncrement(invoice.InvoiceId, money);
        }

        public void OwedDecrement(Book.Model.InvoiceCG invoice, decimal money)
        {
            this.OwedDecrement(invoice.InvoiceId, money);
        }

        public void OwedIncrement(Book.Model.InvoiceCG invoice, decimal? money)
        {
            this.OwedDecrement(invoice.InvoiceId, money.Value);
        }

        public void OwedDecrement(Book.Model.InvoiceCG invoice, decimal? money)
        {
            this.OwedDecrement(invoice.InvoiceId, money.Value);
        }

        public void OwedIncrement(string invoiceId, decimal money)
        {
            System.Collections.Hashtable paras = new Hashtable();
            paras.Add("InvoiceOwed", money);
            paras.Add("InvoiceId", invoiceId);
            sqlmapper.Update("InvoiceCG.owedincrement", paras);
        }

        public void OwedDecrement(string invoiceId, decimal money)
        {
            System.Collections.Hashtable paras = new Hashtable();
            paras.Add("InvoiceOwed", money);
            paras.Add("InvoiceId", invoiceId);
            sqlmapper.Update("InvoiceCG.oweddecrement", paras);
        }

        public void OwedIncrement(string invoiceId, decimal? money)
        {
            this.OwedIncrement(invoiceId, money.Value);
        }

        public void OwedDecrement(string invoiceId, decimal? money)
        {
            this.OwedDecrement(invoiceId, money.Value);
        }

        public IList<Book.Model.InvoiceCG> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceCG>("InvoiceCG.select_byStatus", (int)status);
        }



        public IList<Book.Model.InvoiceCG> Select(DateTime start, DateTime end, string startID, string endID)
        {
            Hashtable pars = new Hashtable();
            pars.Add("start", start);
            pars.Add("end", end);
            pars.Add("startId",startID );
            pars.Add("endId", endID);

            return sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_byDateRengeAndCompanyIDRenge", pars);
        }

        public IList<Book.Model.InvoiceCG> Select1(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("start", start);
            pars.Add("end", end);
            return sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_byDateRenge", pars);
        }
        public IList<Book.Model.InvoiceCG> Select(Model.Supplier supplier)
        {
            return sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_bysupplier", supplier.SupplierId);       
        }
        public IList<Book.Model.InvoiceCG> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd, string productStart, string productEnd)
        {
            IList < Book.Model.InvoiceCG> invoicecg= null;
            Hashtable ht = new Hashtable();
            ht.Add("SupplierStart",SupplierStart);
            ht.Add("SupplierEnd",SupplierEnd);
            ht.Add("dateStart",dateStart);
            ht.Add("dateEnd", dateEnd);            
            ht.Add("productStart", productStart);
            ht.Add("productEnd", productEnd);

            if (string.IsNullOrEmpty(productStart)&&!string.IsNullOrEmpty(productEnd) )
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_bysupplierDateStartNull", ht);
            }
            if (string.IsNullOrEmpty(productEnd)&&!string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_bysupplierDateEndNull", ht);
            }
            if (string.IsNullOrEmpty(productEnd) && string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_bysupplierDateStartEndNULL", ht);
            }
            if (!string.IsNullOrEmpty(productEnd) && !string.IsNullOrEmpty(productStart))
            {
                invoicecg = sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_bysupplierDateQuJian", ht);
            }
            return invoicecg;
        }

        #endregion
    }
}
