//------------------------------------------------------------------------------
//
// file name:InvoiceCODetailAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceCODetail
    /// </summary>
    public partial class InvoiceCODetailAccessor : EntityAccessor, IInvoiceCODetailAccessor
    {
        public IList<Book.Model.InvoiceCODetail> Select(Book.Model.InvoiceCO invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.select_by_invoiceid", invoice.InvoiceId);
        }
        public IList<Book.Model.InvoiceCODetail> Select(string invoiceId)
        {
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.select_by_invoiceid", invoiceId);
        }

        public void Delete(Book.Model.InvoiceCO invoice)
        {
            sqlmapper.Delete("InvoiceCODetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Model.InvoiceCODetail> SelectByDateRangeAndPid(string pid, DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", pid);
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.SelectByDateRangeAndPid", ht);
        }

        public IList<Book.Model.InvoiceCODetail> SelectByHeaderProRang(string pid, Book.Model.Product productStart, Book.Model.Product productEnd)
        {
            if (string.IsNullOrEmpty(pid))
                return null;
            StringBuilder str = new StringBuilder();
            str.Append(" where invoiceid='" + pid + "'");
            if (productStart != null && productEnd != null)
                str.Append(" and productid in (select productid from product where productname between '" + productStart.ProductName + "'  and '" + productEnd.ProductName + "')");
            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.SelectByHeaderProRang", str.ToString());
        }

        public void UpdateProofUnitPrice(Book.Model.InvoiceCODetail e)
        {
            //修改详细
            Hashtable htDetail = new Hashtable();
            htDetail.Add("InvoiceCODetailId", e.InvoiceCODetailId);
            htDetail.Add("InvoiceCODetailPrice", e.InvoiceCODetailPrice);
            htDetail.Add("InvoiceCODetailMoney", e.InvoiceCODetailMoney);
            sqlmapper.Update("InvoiceCODetail.UpdateProofUnitPrice", htDetail);
            //修改头
            //Hashtable htHeader = new Hashtable();
            //htHeader.Add("InvoiceId", e.Invoice.InvoiceId);
            //htHeader.Add("InvoiceHeji", e.Invoice.InvoiceHeji);
            //htHeader.Add("InvoiceTax", e.Invoice.InvoiceTax);
            //htHeader.Add("InvoiceTotal", e.Invoice.InvoiceTotal);
            //sqlmapper.Update("InvoiceCODetail.UpdateProofUnitPriceHeader", htHeader);
        }

        public IList<Book.Model.InvoiceCODetail> Select(string costartid, string coendid, Book.Model.Supplier SupplierStart, Book.Model.Supplier SupplierEnd, DateTime? dateStart, DateTime? dateEnd, Book.Model.Product productStart, Book.Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd, int? invoiceFlag, Book.Model.Employee empStart, Book.Model.Employee empEnd)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(costartid) && !string.IsNullOrEmpty(coendid))
            {
                sb.Append(" AND InvoiceId BETWEEN '" + costartid + "' AND '" + coendid + "' ");
            }
            if (SupplierStart != null && SupplierEnd != null)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE SupplierId BETWEEN '" + SupplierStart.SupplierId + "' AND '" + SupplierEnd.SupplierId + "')");
            }
            if (dateStart.HasValue && dateEnd.HasValue)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE InvoiceDate BETWEEN '" + dateStart.Value.ToString("yyyy-MM-dd") + "' AND '" + dateEnd.Value.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (productStart != null && productEnd != null)
            {
                sb.Append(" AND ProductId BETWEEN '" + productStart.ProductId + "' AND '" + productEnd.ProductId + "'");
            }
            if (dateJHStart != null && dateJHEnd != null)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE InvoiceYjrq BETWEEN '" + dateJHStart.ToString("yyyy-MM-dd") + "' AND '" + dateJHEnd.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "')");
            }
            if (invoiceFlag.HasValue && invoiceFlag.Value == 1)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE IsClose=0)");
            }
            if (!string.IsNullOrEmpty(cusxoid))
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE InvoiceCustomXOId LIKE '%" + cusxoid + "%')");
            }
            if (empStart != null && empEnd != null)
            {
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceCO WHERE Employee0Id IN (SELECT EmployeeId FROM Employee WHERE IDNo BETWEEN '" + empStart.IDNo + "' AND '" + empEnd.IDNo + "'))");
            }
            sb.Append(" ORDER BY InvoiceId DESC");

            return sqlmapper.QueryForList<Model.InvoiceCODetail>("InvoiceCODetail.SelectByConditionCO", sb.ToString());
        }

    }
}
