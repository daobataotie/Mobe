//------------------------------------------------------------------------------
//
// file name：SupplierProductAccessor.cs
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
    /// Data accessor of SupplierProduct
    /// </summary>
    public partial class SupplierProductAccessor : EntityAccessor, ISupplierProductAccessor
    {
        public Book.Model.SupplierProduct mGetFirst(string SupplierId)
        {
            return sqlmapper.QueryForObject<Model.SupplierProduct>("SupplierProduct.mGetFirst", SupplierId);
        }

        public Book.Model.SupplierProduct mGetLast(string SupplierId)
        {
            return sqlmapper.QueryForObject<Model.SupplierProduct>("SupplierProduct.mGetLast", SupplierId);
        }

        public Book.Model.SupplierProduct mGetPrev(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<Model.SupplierProduct>("SupplierProduct.mGetPrev", ht);
        }

        public Book.Model.SupplierProduct mGetNext(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<Model.SupplierProduct>("SupplierProduct.mGetNext", ht);
        }

        public bool mHasRows(string SupplierId)
        {
            return sqlmapper.QueryForObject<bool>("SupplierProduct.mHasRows", SupplierId);
        }

        public bool mHasRowsBefore(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<bool>("SupplierProduct.mHasRowsBefore", ht);
        }

        public bool mHasRowsAfter(DateTime InsertDate, string SupplierId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InsertDate", InsertDate);
            ht.Add("SupplierId", SupplierId);
            return sqlmapper.QueryForObject<bool>("SupplierProduct.mHasRowsAfter", ht);
        }

        public IList<Book.Model.SupplierProduct> mSelect(string SupplierId)
        {
            return sqlmapper.QueryForList<Model.SupplierProduct>("SupplierProduct.mSelect", SupplierId);
        }

        public string GetPriceRangeForSupAndProduct(string SupplierId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("SupplierId", SupplierId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<string>("SupplierProduct.GetPriceRangeForSupAndProduct", ht);
        }

        public DataTable SelectALLRefProduct(string ProductId)
        {
            string sql = "SELECT SupplierProductId AS PrimiaryKey,(SELECT SupplierShortName FROM Supplier WHERE Supplier.SupplierId = SupplierProduct.SupplierId) AS SupplierShortName,SupplierProductPriceRange AS PriceRange FROM SupplierProduct WHERE ProductId = '" + ProductId + "'";
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
    }
}
