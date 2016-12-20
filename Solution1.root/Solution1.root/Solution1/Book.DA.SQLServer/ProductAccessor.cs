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
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceA", SqlDbType.Money,8, "ProductPriceA"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceB", SqlDbType.Money,8, "ProductPriceB"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductPriceC", SqlDbType.Money,8, "ProductPriceC"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProductRetailPrice", SqlDbType.Money,8, "ProductRetailPrice"));
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
            return sqlmapper.QueryForList<Model.Product>("Product.select_byCategory", Category.ProductCategoryId);
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
            return sqlmapper.QueryForObject<bool>("Product.ExistsNameUpdate", pars);
        }
        //查询指定客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer)
        {

            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCustomer",customer==null?null: customer.CustomerId);
        }
        //查询指定类和客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer,Model.ProductCategory cate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("customerid",customer==null?"":customer.CustomerId );
            ht.Add("productcategoryid",cate==null?"":cate.ProductCategoryId);
            return sqlmapper.QueryForList<Model.Product>("Product.SelectByCategoryAndCustomer", ht);
        }
        
        //  //查询指定客户的货品
        public IList<Model.Product> SelectProductByCustomer(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.Product>("Product.SelectProductByCustomer", customer.CustomerId);
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

        public IList<Model.Product> GetProductByCondition(string ProductCategoryName, string pt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProductCategoryName", ProductCategoryName);
            ht.Add("pt", pt);
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
            ht.Add("id",id);
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
            IList<Model.Product> productList=new List<Model.Product>();
             using(SqlDataReader rdr=SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction,CommandType.Text,SQL_SELECT_IdName,null))
             {
                Model.Product temp=new Model.Product();
                //temp.Id = rdr["id"].ToString();
                
                 temp.ProductName = rdr[Model.Product.PRO_ProductName].ToString();
                 temp.ProductId = rdr[Model.Product.PRO_ProductId].ToString();
                 while(rdr.Read())
                 {
                   Model.Product product=new Model.Product(rdr.GetString(0),rdr.GetString(1),rdr.GetString(2),rdr.GetString(3));
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

        #endregion
    }
}
