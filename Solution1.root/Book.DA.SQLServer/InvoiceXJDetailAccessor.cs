//------------------------------------------------------------------------------
//
// file name:InvoiceXJDetailAccessor.cs
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
    /// Data accessor of InvoiceXJDetail
    /// </summary>
    public partial class InvoiceXJDetailAccessor : EntityAccessor, IInvoiceXJDetailAccessor
    {
        public IList<Book.Model.InvoiceXJDetail> Select(Book.Model.InvoiceXJ invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJDetail>("InvoiceXJDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceXJ invoice)
        {
            sqlmapper.Delete("InvoiceXJDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        /// <summary>
        /// 类型为公司产品
        /// </summary>
        /// <returns></returns>
        public IList<Book.Model.InvoiceXJDetail> SelectProductType()
        {
            return sqlmapper.QueryForList<Model.InvoiceXJDetail>("InvoiceXJDetail.select_by_productType", null);

        }

        //根据商品 拉取报价
        public Hashtable getRecursiveBOM(string productid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MaxRelations", null);
            ht.Add("DS", null);

            int MaxRelations = 0;
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                string str_BOMComRoots = @"SELECT '0' as Inumber,cast('0' AS bit) AS IsChecked,*,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney2,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailQuote FROM (SELECT (SELECT ProductName FROM Product WHERE Product.ProductId = b.ProductId) AS ProductName,b.Unit,isnull((SELECT SupplierProductPriceRange FROM SupplierProduct WHERE ProductId = b.ProductId AND SupplierProduct.SupplierId = (SELECT SupplierId FROM Product WHERE Product.ProductId = b.ProductId)),0) AS InvoiceXJDetailPriceRange,'0' AS InvoiceXJDetailPrice,b.UseQuantity as InvoiceXJDetailQuantity,b.PriamryKeyId AS InvoiceXJDetailId,b.BomId,b.ProductId,'000' as ParentId FROM BomComponentInfo b WHERE b.BomId = (SELECT TOP 1 BomId FROM BomParentPartInfo WHERE ProductId = '" + productid + "')) aa";

                SqlDataAdapter sdaBOMComRoots = new SqlDataAdapter(str_BOMComRoots, con);

                sdaBOMComRoots.Fill(ds, "Level" + MaxRelations.ToString());

                bool flag = true;

                while (flag)
                {
                    flag = false;

                    foreach (DataRow dr in ds.Tables["Level" + MaxRelations.ToString()].Rows)
                    {
                        string _detailid = dr["InvoiceXJDetailId"].ToString();
                        string _proId = dr["ProductId"].ToString();
                        string str_SQLIn = @"SELECT '0' as Inumber,cast('0' AS bit) AS IsChecked,*,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney2,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailQuote FROM (SELECT (SELECT ProductName FROM Product WHERE Product.ProductId = b.ProductId) AS ProductName,b.Unit,isnull((SELECT SupplierProductPriceRange FROM SupplierProduct WHERE ProductId = b.ProductId AND SupplierProduct.SupplierId = (SELECT SupplierId FROM Product WHERE Product.ProductId = b.ProductId)),0) AS InvoiceXJDetailPriceRange,'0' AS InvoiceXJDetailPrice,b.UseQuantity as InvoiceXJDetailQuantity,cast(newid() AS varchar(50)) AS InvoiceXJDetailId,b.BomId,b.ProductId,'@DetailId' as ParentId FROM BomComponentInfo b WHERE b.BomId = (SELECT TOP 1 BomId FROM BomParentPartInfo WHERE ProductId = '@PrductId')) aa";
                        str_SQLIn = str_SQLIn.Replace("@DetailId", _detailid).Replace("@PrductId", _proId);

                        sdaBOMComRoots.SelectCommand.CommandText = str_SQLIn;
                        sdaBOMComRoots.SelectCommand.Connection = con;
                        sdaBOMComRoots.Fill(ds, "Level" + (MaxRelations + 1).ToString());

                        if (ds.Tables["Level" + MaxRelations.ToString()] != null && ds.Tables["Level" + MaxRelations.ToString()].Rows.Count > 0)
                            flag = true;
                    }
                    if (flag)
                    {
                        ds.Relations.Add("Rel" + MaxRelations.ToString(),
                            ds.Tables["Level" + MaxRelations.ToString()].Columns["InvoiceXJDetailId"],
                            ds.Tables["Level" + (MaxRelations + 1).ToString()].Columns["ParentId"]
                            );
                        MaxRelations++;
                    }
                }
            }

            ht["MaxRelations"] = MaxRelations;
            ht["DS"] = ds;

            return ht;
        }

        //根据报价单号 拉取报价
        public Hashtable getRecursiveInvoiceXJDetails(string invoiceXJid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MaxRelations", null);
            ht.Add("DS", null);

            int MaxRelations = 0;
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                //string str_BOMComRoots = @"SELECT *,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailQuote FROM (SELECT (SELECT ProductName FROM Product WHERE Product.ProductId = b.ProductId) AS ProductName,b.Unit,isnull((SELECT InvoiceCGDetailPrice FROM InvoiceCGDetail WHERE ProductId = '" + productid + "' AND InvoiceId = (SELECT TOP 1 InvoiceId FROM InvoiceCG WHERE InvoiceId IN (SELECT InvoiceId FROM InvoiceCGDetail WHERE ProductId = '" + productid + "') ORDER BY InvoiceDate DESC)),0) AS InvoiceXJDetailPrice,b.UseQuantity as InvoiceXJDetailQuantity,b.PriamryKeyId,b.BomId,b.ProductId,'000' as ParentId FROM BomComponentInfo b WHERE b.BomId = (SELECT TOP 1 BomId FROM BomParentPartInfo WHERE ProductId = '" + productid + "')) aa";

                string str_BOMComRoots = "SELECT Inumber,cast('0' AS bit) AS IsChecked,InvoiceXJDetailId,InvoiceId,ProductName,InvoiceProductUnit AS Unit,InvoiceXJDetailPrice,InvoiceXJDetailQuantity,InvoiceXJDetailMoney,InvoiceXJDetailMoney2,InvoiceXJDetailQuote,ParentId FROM InvoiceXJDetail WHERE InvoiceId = '" + invoiceXJid + "' AND ParentId = '000'";
                SqlDataAdapter sdaBOMComRoots = new SqlDataAdapter(str_BOMComRoots, con);

                sdaBOMComRoots.Fill(ds, "Level" + MaxRelations.ToString());

                bool flag = true;

                while (flag)
                {
                    flag = false;

                    foreach (DataRow dr in ds.Tables["Level" + MaxRelations.ToString()].Rows)
                    {
                        string _detailid = dr["InvoiceXJDetailId"].ToString();

                        //string str_SQLIn = @"SELECT *,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailMoney,isnull((aa.InvoiceXJDetailPrice*aa.InvoiceXJDetailQuantity),0) AS InvoiceXJDetailQuote FROM (SELECT (SELECT ProductName FROM Product WHERE Product.ProductId = b.ProductId) AS ProductName,b.Unit,isnull((SELECT InvoiceCGDetailPrice FROM InvoiceCGDetail WHERE ProductId = '@PrductId' AND InvoiceId = (SELECT TOP 1 InvoiceId FROM InvoiceCG WHERE InvoiceId IN (SELECT InvoiceId FROM InvoiceCGDetail WHERE ProductId = '@PrductId') ORDER BY InvoiceDate DESC)),0) AS InvoiceXJDetailPrice,b.UseQuantity as InvoiceXJDetailQuantity,b.PriamryKeyId,b.BomId,b.ProductId,'@DetailId' as ParentId FROM BomComponentInfo b WHERE b.BomId = (SELECT TOP 1 BomId FROM BomParentPartInfo WHERE ProductId = '@PrductId')) aa";

                        string str_SQLIn = "SELECT Inumber,cast('0' AS bit) AS IsChecked,InvoiceXJDetailId,InvoiceId,ProductName,InvoiceProductUnit AS Unit,InvoiceXJDetailPrice,InvoiceXJDetailQuantity,InvoiceXJDetailMoney,InvoiceXJDetailMoney2,InvoiceXJDetailQuote,ParentId FROM InvoiceXJDetail WHERE ParentId = '" + _detailid + "'";

                        sdaBOMComRoots.SelectCommand.CommandText = str_SQLIn;
                        sdaBOMComRoots.SelectCommand.Connection = con;
                        sdaBOMComRoots.Fill(ds, "Level" + (MaxRelations + 1).ToString());

                        if (ds.Tables["Level" + MaxRelations.ToString()] != null && ds.Tables["Level" + MaxRelations.ToString()].Rows.Count > 0)
                            flag = true;
                    }
                    if (flag)
                    {
                        ds.Relations.Add("Rel" + MaxRelations.ToString(),
                            ds.Tables["Level" + MaxRelations.ToString()].Columns["InvoiceXJDetailId"],
                            ds.Tables["Level" + (MaxRelations + 1).ToString()].Columns["ParentId"]
                            );
                        MaxRelations++;
                    }
                }

                ht["MaxRelations"] = MaxRelations;
                ht["DS"] = ds;

                return ht;
            }
        }

        public void DeleteByHeaderId(string invoiceid)
        {
            sqlmapper.Delete("InvoiceXJDetail.delete_by_invoiceid", invoiceid);
        }
    }
}
