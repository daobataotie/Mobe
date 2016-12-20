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
            pars.Add("startId", startID);
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
        public IList<Book.Model.InvoiceCG> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime dateStart, DateTime dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" where  invoicedate between '" + dateStart.ToString("yyyy-MM-dd") + "' and '" + dateEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (SupplierStart != null && SupplierEnd != null)
                sql.Append("  and supplierId in(select  supplierId from supplier where id between '" + SupplierStart.Id + "' and '" + SupplierEnd.Id + "')");
            if (productStart != null && productEnd != null)
                sql.Append(" and invoiceid in (select invoiceid from invoicecgdetail where productid  in (select productid from product where ProductName between '" + productStart.ProductName + "' and  '" + productEnd.ProductName + "'))");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and  invoiceid in (select invoiceid from invoicecgdetail where invoicecoid in(select invoiceid from invoiceco  where InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + cusxoid + "'))) ");
            if (!string.IsNullOrEmpty(costartid) && !string.IsNullOrEmpty(coendid))
                sql.Append(" and  invoiceid in (select invoiceid from invoicecgdetail where invoicecoid   between   '" + costartid + "'  and '" + coendid + "')");
            //if (dateJHStart != null && dateJHEnd != null)
            sql.Append(" and  invoiceid in (select invoiceid from invoicecgdetail where invoicecoid in(select invoiceid from invoiceco where InvoiceYjrq  between  '" + dateJHStart.ToString("yyyy-MM-dd") + "'  and '" + dateJHEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'))");
            sql.Append("  order by InvoiceId desc");
            //Hashtable ht = new Hashtable();
            //ht.Add("SupplierStart", SupplierStart == null ? null : SupplierStart.Id);
            //ht.Add("SupplierEnd", SupplierEnd == null ? null : SupplierEnd.Id);
            //ht.Add("dateStart", dateStart);
            //ht.Add("dateEnd", dateEnd);
            //ht.Add("productStart", productStart == null ? null : productStart.ProductName);
            //ht.Add("productEnd", productEnd == null ? null : productEnd.ProductName);
            //ht.Add("cusxoid", string.IsNullOrEmpty(cusxoid) ? null : cusxoid);
            //ht.Add("dateJHStart", dateJHStart);
            //ht.Add("dateJHEnd", dateJHEnd);
            //ht.Add("costartid", costartid);
            //ht.Add("coendid", coendid);
            return sqlmapper.QueryForList<Book.Model.InvoiceCG>("InvoiceCG.select_sql", sql.ToString());

        }

        #endregion
    }
}
