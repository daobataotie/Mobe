//------------------------------------------------------------------------------
//
// file name：SupplierProcesscategoryAccessor.cs
// author: mayanjun
// create date：2012-8-30 17:02:25
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
    /// Data accessor of SupplierProcesscategory
    /// </summary>
    public partial class SupplierProcesscategoryAccessor : EntityAccessor, ISupplierProcesscategoryAccessor
    {
        public Book.Model.SupplierProcesscategory mGetFirst(string SupplierId)
        {
            return sqlmapper.QueryForObject<Model.SupplierProcesscategory>("SupplierProcesscategory.mGetFirst", SupplierId);
        }

        public Book.Model.SupplierProcesscategory mGetLast(string SupplierId)
        {
            return sqlmapper.QueryForObject<Model.SupplierProcesscategory>("SupplierProcesscategory.mGetLast", SupplierId);
        }

        public Book.Model.SupplierProcesscategory mGetPrev(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<Model.SupplierProcesscategory>("SupplierProcesscategory.mGetPrev", ht);
        }

        public Book.Model.SupplierProcesscategory mGetNext(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<Model.SupplierProcesscategory>("SupplierProcesscategory.mGetNext", ht);
        }

        public bool mHasRows(string SupplierId)
        {
            return sqlmapper.QueryForObject<bool>("SupplierProcesscategory.mHasRows", SupplierId);
        }

        public bool mHasRowsBefore(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<bool>("SupplierProcesscategory.mHasRows", ht);
        }

        public bool mHasRowsAfter(DateTime InsertDate, string SupplierId)
        {
            return sqlmapper.QueryForObject<bool>("SupplierProcesscategory.mHasRows", SupplierId);
        }

        public IList<Book.Model.SupplierProcesscategory> mSelect(string SupplierId)
        {
            return sqlmapper.QueryForList<Model.SupplierProcesscategory>("SupplierProcesscategory.mSelect", SupplierId);
        }

        public DataTable SelectSupplierProduct(string SupplierId)
        {
            string sql = "SELECT aa.ProductId,(SELECT ProductName FROM Product WHERE ProductId = aa.ProductId) AS ProductName,isnull((SELECT TOP 1 InvoiceCGDetailPrice FROM InvoiceCGDetail d LEFT JOIN InvoiceCG h ON h.InvoiceId = d.InvoiceId WHERE d.ProductId = aa.ProductId ORDER BY h.InsertTime DESC ),0) AS Price FROM (SELECT DISTINCT ProductId FROM InvoiceCGDetail WHERE InvoiceId IN (SELECT InvoiceId FROM InvoiceCG WHERE SupplierId = '" + SupplierId + "')) aa";

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }

            return null;
        }

        public IList<Book.Model.SupplierProcesscategory> SelectBySupAndProc(string SupplierId)
        {
            return sqlmapper.QueryForList<Model.SupplierProcesscategory>("SupplierProcesscategory.SelectBySupAndProc", SupplierId);
        }

        public string GetPriceRangeForSupAndProduct(string SupplierId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("SupplierId", SupplierId);
            ht.Add("ProductId", ProductId);

            return sqlmapper.QueryForObject<string>("SupplierProcesscategory.GetPriceRangeForSupAndProduct", ht);
        }
    }
}
