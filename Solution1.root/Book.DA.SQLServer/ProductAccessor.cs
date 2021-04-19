//------------------------------------------------------------------------------
//
// file name:ProductAccessor.cs
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
using Book.DA.SQLServer.SQLDB;
namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of Product
    /// </summary>
    public partial class ProductAccessor : EntityAccessor, IProductAccessor
    {
        #region IProductAccessor 成员
        private const string SQL_SELECT_IdName = "SELECT ProductId,Id ,ProductName,productDescription FROM product";
        public IList<Book.Model.Product> SelectProduct()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_product", null);
        }

        public IList<Model.Product> SelectByProductIds(string productIds)
        {
            productIds = " productId in (" + productIds + ")";
            return sqlmapper.QueryForList<Model.Product>("Product.select_WhereSQL", productIds);
        }

        public System.Data.DataTable SelectDataTable()
        {
            string sql = "select * from product";
            System.Data.SqlClient.SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;

        }
        public void UpdateBeginCost(DataTable dt)
        {
            string sql = "UPDATE Product SET ProductId = @ProductId,ProductCategoryId = @ProductCategoryId,ProductName = @ProductName,ProductBaseUnit = @ProductBaseUnit,ProductBarCode = @ProductBarCode,ProductSpecification = @ProductSpecification,ProductModel = @ProductModel,ProductPriceA = @ProductPriceA,ProductPriceB = @ProductPriceB,ProductPriceC = @ProductPriceC,ProductRetailPrice = @ProductRetailPrice,ProductBeginCost = @ProductBeginCost,ProductStandardCost = @ProductStandardCost,ProductDescription = @ProductDescription WHERE ProductId=@ProductId";
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.UpdateCommand = new SqlCommand(sql, conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductId", SqlDbType.VarChar, 50, "ProductId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductCategoryId", SqlDbType.VarChar, 50, "ProductCategoryId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, "ProductName"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBaseUnit", SqlDbType.VarChar, 50, "ProductBaseUnit"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBarCode", SqlDbType.VarChar, 50, "ProductBarCode"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductSpecification", SqlDbType.VarChar, 50, "ProductSpecification"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductModel", SqlDbType.VarChar, 50, "ProductModel"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceA", SqlDbType.Money, 8, "ProductPriceA"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceB", SqlDbType.Money, 8, "ProductPriceB"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceC", SqlDbType.Money, 8, "ProductPriceC"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductRetailPrice", SqlDbType.Money, 8, "ProductRetailPrice"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductBeginCost", SqlDbType.Money, 8, "ProductBeginCost"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductStandardCost", SqlDbType.Money, 8, "ProductStandardCost"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductDescription", SqlDbType.Text, 16, "ProductDescription"));
            dataAdapter.Update(dt);
        }

        public void UpdateCost1(Book.Model.Product product, decimal? price, double? quantity)
        {
            Hashtable table = new Hashtable();
            table.Add("quantity", quantity == null ? 0 : quantity.Value);
            table.Add("unitprice", price == null ? decimal.Zero : price.Value);
            table.Add("productid", product.ProductId);

            sqlmapper.Update("Product.update_cost1", table);
        }

        public IList<Book.Model.Product> Select(Book.Model.ProductCategory Category)
        {
            string str = "";
            if (Category.CategoryLevel == 1)
            {
                //str = " ProductCategoryId = '" + Category.ProductCategoryId + "' and (productCategoryid2='' or productCategoryid2 is null) and ( productCategoryid3='' or productCategoryid3 is null)";
                //点击大类，显示该类别下所有商品
                str = " ProductCategoryId = '" + Category.ProductCategoryId + "'";
            }
            else if (Category.CategoryLevel == 2)
                str = " productCategoryid2='" + Category.ProductCategoryId + "' and ( productCategoryid3='' or productCategoryid3 is null)";
            else
                str = " productCategoryid3='" + Category.ProductCategoryId + "'";

            Hashtable ht = new Hashtable();
            ht.Add("sql", str);
            return sqlmapper.QueryForList<Model.Product>("Product.select_byCategory", ht);
        }
        public IList<Book.Model.Product> SelectProductByProductCategoryId(Book.Model.ProductCategory Category)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_byCategoryTo", Category.ProductCategoryId);
        }
        public IList<Book.Model.Product> Select(Book.Model.Depot depot)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_byDepot", depot.DepotId);
        }

        public bool ExistsNameInsert(string productName)
        {
            return sqlmapper.QueryForObject<bool>("Product.ExistsNameInsert", productName);
        }

        public bool ExistsNameUpdate(Model.Product product)
        {
            Hashtable pars = new Hashtable();
            pars.Add("newName", product.ProductName);
            pars.Add("oldName", Get(product.ProductId).ProductName);
            string sql = string.Empty;
            if (product.IsCustomerProduct == null || !product.IsCustomerProduct.Value)
                sql = " and (IsCustomerProduct=0 or IsCustomerProduct is null) ";
            pars.Add("sql", sql);
            return sqlmapper.QueryForObject<bool>("Product.ExistsNameUpdate", pars);
        }
        //查询指定客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer)
        {

            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCustomer", customer == null ? null : customer.CustomerId);
        }
        //查询指定类和客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer, Model.ProductCategory cate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerid", customer == null ? "" : customer.CustomerId);
            ht.Add("productcategoryid", cate == null ? "" : cate.ProductCategoryId);
            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCategoryAndCustomer", ht);
        }

        //  //查询指定客户的货品
        public IList<Model.Product> SelectProductByCustomer(Model.Customer customer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerId", customer.CustomerId);
            ht.Add("DeadDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));
            return sqlmapper.QueryForList<Model.Product>("Product.SelectProductByCustomer", ht);
        }

        public IList<Model.Product> SelectAllProductByCustomers(string customerIds, bool isShowUnuseProduct)
        {
            Hashtable ht = new Hashtable();
            ht.Add("CustomerIds", customerIds);
            if (!isShowUnuseProduct)
                ht.Add("sql", " and ProductId in (select CustomerProductProceName from CustomerProducts where (VersionDate IS NULL OR (year(VersionDate) = '1900' AND month(VersionDate) = '01' AND day(VersionDate) = '01') OR VersionDate > GETDATE()) and CustomerId in (" + customerIds + "))");

            return sqlmapper.QueryForList<Model.Product>("Product.SelectAllProductByCustomers", ht);
        }

        public void Delete(Book.Model.Product product, Model.Customer customer)
        {
            Hashtable table = new Hashtable();
            table.Add("productid", product == null ? null : product.ProductId);
            table.Add("customerid", customer == null ? null : customer.CustomerId);
            sqlmapper.Delete("Product.deleteByCustomPro", table);
        }
        //查询指定类和客户的货品和公司货品
        public Model.Product Get(Model.Customer customer, Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerid", customer == null ? "" : customer.CustomerId);
            ht.Add("productid", product == null ? "" : product.ProductId);
            return sqlmapper.QueryForObject<Model.Product>("Product.selectByProductAndCustomer", ht);
        }

        public IList<Model.Product> GetProduct()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.GetProduct", null);
        }

        public IList<Model.Product> GetProductByCondition(string ProductCategoryName, string pt, string depotid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductCategoryName", ProductCategoryName);
            ht.Add("pt", pt);
            ht.Add("DepotId", depotid);
            return sqlmapper.QueryForList<Model.Product>("Product.GetProductByCondition", ht);
        }

        public IList<Model.Product> SelectNotCustomer()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomer", null);
        }
        public IList<Model.Product> SelectNotCustomer1()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomer1", null);
        }
        public IList<Model.Product> SelectNotCustomerByCate(string productCate)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_notcustomerByCate", productCate);
        }
        public IList<Model.Product> SelectByIdOrNameKey(string id, string productName, string customerProductName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("name", productName);
            ht.Add("customerProductName", customerProductName);
            return sqlmapper.QueryForList<Model.Product>("Product.select_byIdOrNameKey", ht);
        }
        public IList<Model.Product> SelectALLIdOrNameKey(string id, string productName, string customerProductName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("id", id);
            ht.Add("name", productName);
            ht.Add("customerProductName", customerProductName);
            return sqlmapper.QueryForList<Model.Product>("Product.select_ALLIdOrNameKey", ht);
        }
        public IList<Model.Product> GetProductReader()
        {
            IList<Model.Product> productList = new List<Model.Product>();
            using (SqlDataReader rdr = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, SQL_SELECT_IdName, null))
            {
                Model.Product temp = new Model.Product();
                //temp.Id = rdr["id"].ToString();

                temp.ProductName = rdr[Model.Product.PRO_ProductName].ToString();
                temp.ProductId = rdr[Model.Product.PRO_ProductId].ToString();
                while (rdr.Read())
                {
                    Model.Product product = new Model.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3));
                    productList.Add(product);
                }

            }
            return productList;
        }
        //根据组装前半成品 查询 裸片加工后商品
        public IList<Model.Product> SelectProceProduct(Model.Product product)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProceebeforeProductId", product.ProceebeforeProductId);
            ht.Add("productId", product.ProductId);

            return sqlmapper.QueryForList<Model.Product>("Product.select_ProceProduct", ht);
        }
        //根据裸片 或加工商品  查询 裸片及加工 等相关商品
        public IList<Model.Product> SelectProceByProduct(Model.Product product)
        {
            string sql = "";
            if (product.IsProcee == true)
                sql = "productId='" + product.ProceebeforeProductId + "' or ProceebeforeProductId ='" + product.ProceebeforeProductId + "'";
            else
                sql = "productId='" + product.ProductId + "' or ProceebeforeProductId ='" + product.ProductId + "'";
            return this.SelectByWhereSQL(sql);
        }
        public IList<Model.Product> SelectByWhereSQL(string sql)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.select_WhereSQL", sql);
        }
        public double? getStockByProduct(string productid)
        {
            return sqlmapper.QueryForObject<double>("Product.select_StockByProduct", productid);
        }
        public Model.Product getStockYFPByProduct(string productid)
        {
            DataSet ds = DbHelperSQL.Query("select (ISNULL(ProduceMaterialDistributioned,0)+isnull( OtherMaterialDistributioned,0)) AS ProduceMaterialDistributioned,ISNULL(StocksQuantity,0) as  StocksQuantity  from product where productid='" + productid + "'");
            Model.Product product = new Book.Model.Product();
            product.StocksQuantity = double.Parse(ds.Tables[0].Rows[0][1].ToString());
            product.ProduceMaterialDistributioned = double.Parse(ds.Tables[0].Rows[0][0].ToString());
            return product;

        }

        public IList<Model.Product> StockPrompt()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.StockPrompt", null);
        }

        //原料换算——查询商品 
        public IList<Book.Model.Product> SelectProductByCondition(string productIdStart, string productIdEnd, string DepotIdStart, string DepotIdEnd, string productCategoryIdStart, string productCategoryIdEnd)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(productIdStart) || !string.IsNullOrEmpty(productIdEnd))
            {
                if (!string.IsNullOrEmpty(productIdStart) && !string.IsNullOrEmpty(productIdEnd))
                {
                    sb.Append(" AND Id BETWEEN '" + productIdStart + "' AND '" + productIdEnd + "'");
                }
                else
                {
                    sb.Append(" AND Id='" + (string.IsNullOrEmpty(productIdStart) ? productIdEnd : productIdStart) + "'");
                }
                if (!string.IsNullOrEmpty(DepotIdStart) || !string.IsNullOrEmpty(DepotIdEnd))
                {
                    if (!string.IsNullOrEmpty(DepotIdStart) && !string.IsNullOrEmpty(DepotIdEnd))
                    {
                        sb.Append(" AND ProductId IN (SELECT ProductId FROM Stock WHERE DepotId IN (SELECT DepotId FROM Depot WHERE Id BETWEEN '" + DepotIdStart + "' AND '" + DepotIdEnd + "') Group BY ProductId )");
                    }
                    else
                    {
                        sb.Append(" AND ProductId IN (SELECT ProductId FROM Stock WHERE DepotId IN (SELECT DepotId FROM Depot WHERE Id ='" + (string.IsNullOrEmpty(DepotIdEnd) ? DepotIdStart : DepotIdEnd) + "') Group BY ProductId )");
                    }
                }
                if (!string.IsNullOrEmpty(productCategoryIdStart) || !string.IsNullOrEmpty(productCategoryIdEnd))
                {
                    if (!string.IsNullOrEmpty(productCategoryIdStart) && !string.IsNullOrEmpty(productCategoryIdEnd))
                    {
                        sb.Append(" AND ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE Id BETWEEN '" + productCategoryIdStart + "' AND '" + productCategoryIdEnd + "' )");
                    }
                    else
                    {
                        sb.Append(" AND ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE Id='" + (string.IsNullOrEmpty(productCategoryIdStart) ? productCategoryIdEnd : productCategoryIdStart) + "' )");
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(DepotIdStart) || !string.IsNullOrEmpty(DepotIdEnd))
                {
                    if (!string.IsNullOrEmpty(DepotIdStart) && !string.IsNullOrEmpty(DepotIdEnd))
                    {
                        sb.Append(" AND ProductId IN (SELECT ProductId FROM Stock WHERE DepotId IN (SELECT DepotId FROM Depot WHERE Id BETWEEN '" + DepotIdStart + "' AND '" + DepotIdEnd + "') Group BY ProductId )");
                    }
                    else
                    {
                        sb.Append(" AND ProductId IN (SELECT ProductId FROM Stock WHERE DepotId IN (SELECT DepotId FROM Depot WHERE Id ='" + (string.IsNullOrEmpty(DepotIdEnd) ? DepotIdStart : DepotIdEnd) + "') Group BY ProductId )");
                    }
                }
                if (!string.IsNullOrEmpty(productCategoryIdStart) || !string.IsNullOrEmpty(productCategoryIdEnd))
                {
                    if (!string.IsNullOrEmpty(productCategoryIdStart) && !string.IsNullOrEmpty(productCategoryIdEnd))
                    {
                        sb.Append(" AND ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE Id BETWEEN '" + productCategoryIdStart + "' AND '" + productCategoryIdEnd + "' )");
                    }
                    else
                    {
                        sb.Append(" AND ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE Id='" + (string.IsNullOrEmpty(productCategoryIdStart) ? productCategoryIdEnd : productCategoryIdStart) + "' )");
                    }
                }
            }
            return sqlmapper.QueryForList<Model.Product>("Product.SelectProductByCondition", sb.ToString());
        }

        //原料换算——查询商品 
        public IList<Model.Product> SelectProductsByProductIds(string Productids)
        {
            string sql = " AND productId in (" + Productids + ")";
            return sqlmapper.QueryForList<Model.Product>("Product.SelectProductsByProductIds", sql);
        }

        public void UpdateSimple(Model.Product product)
        {
            sqlmapper.Update("Product.Update_SimpleProduct", product);
        }

        public IList<Model.Product> SelectIdAndStock(string startCategory_Id, string endCategory_Id)
        {
            string sql = "select p.id,ProductId,ProductName,ProductVersion,isnull(StocksQuantity,0) as StocksQuantity,pc1.ProductCategoryName,pc2.ProductCategoryName as ProductCategoryName2,pc3.ProductCategoryName as ProductCategoryName3,MaterialIds,MaterialNum,(select CnName from ProductUnit where ProductUnitId=p.DepotUnitId) as CnName,isnull(p.NetWeight,0) as NetWeight from Product p left join ProductCategory pc1 on p.ProductCategoryId=pc1.ProductCategoryId left join ProductCategory pc2 on p.ProductCategoryId2=pc2.ProductCategoryId left join ProductCategory pc3 on p.ProductCategoryId3=pc3.ProductCategoryId ";

            if (!string.IsNullOrEmpty(startCategory_Id))
            {
                if (!string.IsNullOrEmpty(endCategory_Id))
                    sql += " where pc1.Id between '" + startCategory_Id + "' and '" + endCategory_Id + "'";

                else
                    sql += " where pc1.Id ='" + startCategory_Id + "'";
            }


            return this.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        public IList<Model.Product> SelectProductIdAndName()
        {
            string sql = "select ProductId,ProductName from Product ";

            return this.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        public DataTable SelectProductCategoryByProductIds(string productIds)
        {
            string sql = "select p.MaterialIds,p.MaterialNum,p.ProductId,pc1.ProductCategoryName as ProductCategoryName1,pc2.ProductCategoryName as ProductCategoryName2,pc3.ProductCategoryName as ProductCategoryName3 from Product p left join ProductCategory pc1 on p.ProductCategoryId=pc1.ProductCategoryId left join ProductCategory pc2 on p.ProductCategoryId2=pc2.ProductCategoryId left join ProductCategory pc3 on p.ProductCategoryId3=pc3.ProductCategoryId where ProductId in " + productIds + "";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public IList<Model.Product> GetProductBaseInfo()
        {
            return sqlmapper.QueryForList<Model.Product>("Product.GetProductBaseInfo", null);
        }

        #endregion

        #region /*CdmiN--2011年9月29日16:05:38*/ 更新product表,使其与stock表中数据对应
        public void UpdateProduct_Stock(Book.Model.Product pro)
        {
            sqlmapper.Update("Product.update_stock", pro.ProductId);
        }
        #endregion

        public string SelectCustomerProductNameByProductIds(string productIds)
        {
            string sql = "select CustomerProductName+',' from Product where ProductId in (" + productIds + ") for xml path('')";
            object value = this.QueryObject(sql);

            return (value == null ? "" : value.ToString());
        }

        public double SelectStocksQuantityByStock(string productId)
        {
            return sqlmapper.QueryForObject<double>("Product.SelectStocksQuantityByStock", productId);
        }

        //查询所有商品的 Id，商品名称，类别名称
        public IList<Model.Product> SelectAllIdAndName()
        {
            string sql = "select p.id,ProductId,ProductName,pc1.ProductCategoryName,pc2.ProductCategoryName as ProductCategoryName2,pc3.ProductCategoryName as ProductCategoryName3,MaterialIds,MaterialNum,isnull(p.NetWeight,0) as NetWeight from Product p left join ProductCategory pc1 on p.ProductCategoryId=pc1.ProductCategoryId left join ProductCategory pc2 on p.ProductCategoryId2=pc2.ProductCategoryId left join ProductCategory pc3 on p.ProductCategoryId3=pc3.ProductCategoryId where NetWeight<>0 and NetWeight is not null";

            return this.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }
    }
}
