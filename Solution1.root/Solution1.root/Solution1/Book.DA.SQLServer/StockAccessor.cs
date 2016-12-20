//------------------------------------------------------------------------------
//
// file name:StockAccessor.cs
// author: peidun
// create date:2008/6/6  10:00:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of Stock
    /// </summary>
    public partial class StockAccessor : EntityAccessor, IStockAccessor
    {
        #region IStockAccessor 成员


        public DataTable SelectDataTable()
        {
            return this.SelectDataTable("");
        }
        public DataTable SelectDataTable(string strWhere)
        {
            string strSql = "SELECT DepotPosition.*, Product.*,Product.id spid, Stock.*,Depot.*,ProductUnit.* FROM ((((Product INNER JOIN Stock ON Product.ProductId = Stock.ProductId) INNER JOIN DepotPosition ON Stock.DepotPositionId = DepotPosition.DepotPositionId) INNER JOIN Depot ON Depot.depotid=DepotPosition.depotid) INNER JOIN ProductUnit ON product.DepotUnitId = ProductUnit.ProductUnitId and product.productid = stock.productid) ";
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql += @" where " + strWhere;
            }
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlmapper.DataSource.ConnectionString);
            DataTable Stocks = new DataTable();
            dataAdapter.Fill(Stocks);
            return Stocks;
        }

        public System.Data.DataTable SelectDataTable(Model.Product product)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select sum(StockQuantity0) as StockBeginQuantity,");
            sb.Append("sum(StockQuantity1) as StockCurrentQuantity,");

            sb.Append("sum(StockBeginJR) as StockBeginJR1,");
            sb.Append("sum(StockBeginJC) as StockBeginJC1,");
            sb.Append("sum(StockCurrentJR) as StockCurrentJR1,");
            sb.Append("sum(StockCurrentJC) as StockCurrentJC1 ");
            sb.Append("from stock where productId = '" + product.ProductId + "'");

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sb.ToString(), sqlmapper.DataSource.ConnectionString);
            DataTable table = new DataTable("QuantityAndJR");
            dataAdapter.Fill(table);
            return table;
        }

        public void UpdateDataTable(DataTable stocks)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.UpdateCommand = new SqlCommand("update dbo.Stock set productid=@productid,DepotPositionId=@depotPositionId,stockquantity0=@stockquantity0,stockquantity1=@stockquantity1,stockquantityD=@stockquantityD,stockquantityU=@stockquantityU,StockBeginJR =@stockBeginJR,StockBeginJC = @stockBeginJC,StockCurrentJR = @StockCurrentJR,StockCurrentJC = @StockCurrentJC where stockid=@stockid", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@productid", SqlDbType.VarChar, 50, "Productid"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@depotPositionId", SqlDbType.VarChar, 50, "DepotPositionId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockquantity0", SqlDbType.Float, 32, "StockQuantity0"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockquantity1", SqlDbType.Float, 32, "StockQuantity1"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockquantityD", SqlDbType.Float, 32, "StockQuantityD"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockquantityU", SqlDbType.Float, 32, "StockQuantityU"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockid", SqlDbType.VarChar, 50, "StockId"));

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockBeginJR", SqlDbType.Float, 32, "StockBeginJR"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockCurrentJR", SqlDbType.Float, 32, "StockCurrentJR"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockBeginJC", SqlDbType.Float, 32, "StockBeginJC"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockCurrentJC", SqlDbType.Float, 32, "StockCurrentJC"));
            dataAdapter.Update(stocks);
        }


        public bool Exists(Book.Model.DepotPosition depotPosition, Book.Model.Product product)
        {
            Hashtable paras = new Hashtable();
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            return sqlmapper.QueryForObject<int>("Stock.count_productId_depotid", paras) > 0 ? true : false;
        }

        public void Increment(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);

            if (!Exists(depotPosition, product))
            {
                Model.Stock stock = new Book.Model.Stock();
                stock.StockId = Guid.NewGuid().ToString();
                stock.DepotPositionId = depotPosition.DepotPositionId;
                stock.DepotId = depotPosition.DepotId;
                stock.ProductId = product.ProductId;
                stock.StockQuantity1 = quantity;
                this.Insert(stock);
            }
            else
                sqlmapper.Update("Stock.increment", paras);
        }

        public void Decrement(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            sqlmapper.Update("Stock.decrement", paras);
        }

        public void Increment(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double? quantity)
        {
            this.Increment(depotPosition, product, quantity.Value);
        }

        public void Decrement(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double? quantity)
        {
            this.Decrement(depotPosition, product, quantity.Value);
        }
        public void IncrementJR(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            sqlmapper.Update("Stock.incrementjr", paras);
        }

        public void IncrementJC(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            sqlmapper.Update("Stock.incrementjc", paras);
        }
        public void DecrementJR(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            sqlmapper.Update("Stock.decrementjr", paras);
        }
        public void DecrementJC(Book.Model.DepotPosition depotPosition, Book.Model.Product product, double quantity)
        {
            Hashtable paras = new Hashtable();
            paras.Add("Quantity", quantity);
            paras.Add("positionId", depotPosition.DepotPositionId);
            paras.Add("ProductId", product.ProductId);
            sqlmapper.Update("Stock.decrementjc", paras);
        }
        public IList<Book.Model.Stock> Select(string id)
        {
            return sqlmapper.QueryForList<Model.Stock>("Stock.select_byProductID", id);
        }

        public IList<Book.Model.Stock> Select(Model.Depot depot)
        {
            return sqlmapper.QueryForList<Model.Stock>("Stock.select_byDepotID", depot.DepotId);
        }
        public IList<Book.Model.Stock> Select(Model.Stock stock)
        {
            Hashtable paras = new Hashtable();
            paras.Add("depotId", stock.Depot.DepotId);
            paras.Add("productId", stock.ProductId);
            return sqlmapper.QueryForList<Model.Stock>("GetTheCountOfProductByProductId", paras);

        }

        public Model.Stock GetStockByPidAndDid(Model.Stock stock)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", stock.ProductId);
            ht.Add("did", stock.DepotId);
            return sqlmapper.QueryForObject<Model.Stock>("Stock.select_by_productidAndDepotId", ht);

        }

        public IList<Model.Stock> SelectNotZeroByPidAndDid(string productId, string depotId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", productId);
            ht.Add("did", depotId); ;
            return sqlmapper.QueryForList<Model.Stock>("Stock.selectNotZeroByPidAndDid", ht);
        }

        public IList<Model.Stock> GetStockByPidAndDid(string productId, string depotId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", productId);
            ht.Add("did", depotId); ;
            return sqlmapper.QueryForList<Model.Stock>("Stock.select_by_productidAndDepotId", ht);
        }

        public double GetTheCount1OfProductByProductId(Model.Product product, Model.Depot depot)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", product == null ? null : product.ProductId);
            ht.Add("depotId", depot.DepotId);
            return sqlmapper.QueryForObject<double>("Stock.GetTheCount1OfProductByProductId", ht);
        }
        public double GetTheCount0OfProductByProductId(Model.Product product, Model.Depot depot)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", product == null ? null : product.ProductId);
            ht.Add("depotId", depot.DepotId);
            return sqlmapper.QueryForObject<double>("Stock.GetTheCount0OfProductByProductId", ht);
        }

        public double GetTheCountByProduct(Model.Product product)
        {
            return sqlmapper.QueryForObject<double>("Stock.GetTheCountByProduct", product.ProductId);
        }

        public DataSet SelectDataSet()
        {
            string strSql = "SELECT DepotPosition.Id as postid ,DepotPosition.DepotPositionId, Product.ProductName,Product.CustomerProductName,Product.id proId, Stock.*,isnull( Stock.StockQuantityOld,0) stocknums,Depot.*,ProductUnit.* FROM ((((Product INNER JOIN Stock ON Product.ProductId = Stock.ProductId) INNER JOIN DepotPosition ON Stock.DepotPositionId = DepotPosition.DepotPositionId) INNER JOIN Depot ON Depot.depotid=DepotPosition.depotid) INNER JOIN ProductUnit ON product.DepotUnitId = ProductUnit.ProductUnitId and product.productid = stock.productid) order by  proId";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlmapper.DataSource.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds;
        }
        public void UpdateDataTable1(DataTable stocks)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.UpdateCommand = new SqlCommand("update dbo.Stock set stockquantity1=@stockquantity1 where stockid=@stockid", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockid", SqlDbType.VarChar, 50, "StockId"));

            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@stockquantity1", SqlDbType.Float, 32, "StockQuantity1"));

            dataAdapter.Update(stocks);
        }

        public Model.Stock GetStockByProductIdAndDepotPositionId(string productid, string depotpositionId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productid);
            ht.Add("depotPositionId", depotpositionId);
            return sqlmapper.QueryForObject<Model.Stock>("Stock.GetStockByProductIdAndDepotPositionId", ht);
        }
        /// <summary>
        /// 关键字查询
        /// </summary>
        /// <param name="proName"></param>
        /// <returns></returns>
        public DataTable SelectDataTableProName(string proName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new System.Data.SqlClient.SqlCommand();
            da.SelectCommand.Connection = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            da.SelectCommand.CommandText = "select (select DepotName from Depot where DepotId =(select DepotId from depotposition where depotpositionId = stock.depotpositionId)) as DepotName,(select productName from product where productid = stock.productid)  productName,(select ProductDescription from product where productid = stock.productid)  ProductDescription,(select id from product where productid = stock.productid) spid, (select CnName from ProductUnit INNER JOIN product ON  product.DepotUnitId = ProductUnit.ProductUnitId and product.productid = stock.productid) CnName,  (select Id from depotposition where depotpositionId = stock.depotpositionId) as DepotPositionId,StockQuantity1   as Quantity from stock WHERE ProductId IN(SELECT ProductId FROM product WHERE ProductName LIKE '%" + proName + "%')";
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 商品窗体库存历史记录
        /// </summary>
        /// <returns></returns>
        public IList<Model.StockSeach> SelectReaderByPro(string productId, DateTime startDate, DateTime endDate)
        {
            IList<Model.StockSeach> list = new List<Model.StockSeach>();
            //出仓库存记录
            // string sqlDepotOut="SELECT '出倉單' as InvoiceType,DepotOutId   as InvoiceNO,(select DepotOutDate  FROM DepotOut where DepotOut.DepotOutId = DepotOutDetail.DepotOutId) as InvoiceDate ,DepotOutDetail.DepotOutDetailQuantity AS InvoiceQuantity,ProductUnit AS  InvoiceUnit,(SELECT DepotName FROM Depot WHERE Depot.DepotId=(SELECT DepotId FROM DepotOut WHERE DepotOut.DepotOutId=DepotOutDetail.DepotOutId )) AS DepotName,(SELECT id FROM DepotPosition WHERE DepotPosition.DepotPositionId=DepotOutDetail.DepotPositionId) AS PositionName,(SELECT StocksQuantity FROM product WHERE   product.ProductId=DepotOutDetail.ProductId) AS Stock1,Description  FROM DepotOutDetail WHERE ProductId='45a84d3b-6561-47bc-8101-35a76ba2be78'";
            //入仓库存记录
            //string sqlDepotIn = "SELECT '入仓单' as InvoiceType,DepotInId as InvoiceNO,(select DepotInDate  FROM depotIn where depotIn.DepotInId = DepotInDetail.DepotInId) as InvoiceDate ,DepotInDetail.DepotInQuantity AS InvoiceQuantity,ProductUnit AS  InvoiceUnit,(SELECT DepotName FROM Depot WHERE Depot.DepotId=(SELECT DepotId FROM depotIn WHERE depotIn.DepotInId=DepotInDetail.DepotInId )) AS DepotName,(SELECT id FROM DepotPosition WHERE DepotPosition.DepotPositionId=DepotInDetail.DepotPositionId) AS PositionName,(SELECT StocksQuantity FROM product WHERE   product.ProductId=DepotInDetail.ProductId) AS Stock1,Description  FROM DepotInDetail WHERE ProductId='45a84d3b-6561-47bc-8101-35a76ba2be78'";
            string[] sqls ={"SELECT '出倉單' as InvoiceType,DepotOutId   as InvoiceNO,(select DepotOutDate  FROM DepotOut where DepotOut.DepotOutId = DepotOutDetail.DepotOutId) as InvoiceDate ,DepotOutDetail.DepotOutDetailQuantity AS InvoiceQuantity,ProductUnit AS  InvoiceUnit,(SELECT DepotName FROM Depot WHERE Depot.DepotId=(SELECT DepotId FROM DepotOut WHERE DepotOut.DepotOutId=DepotOutDetail.DepotOutId )) AS DepotName,(SELECT id FROM DepotPosition WHERE DepotPosition.DepotPositionId=DepotOutDetail.DepotPositionId) AS PositionName,(SELECT StocksQuantity FROM product WHERE   product.ProductId=DepotOutDetail.ProductId) AS Stock1,Description  FROM DepotOutDetail WHERE ProductId='"+productId+"' and DepotOutId in (select DepotOutId from DepotOut where (DepotOutDate between '"+startDate.ToString("yyyy-MM-dd")+"' and '"+endDate.ToString("yyyy-MM-dd")+"') or ('"+startDate.ToString("yyyy-MM-dd")+"' is null and '"+endDate.ToString("yyyy-MM-dd")+"' is null))" ,
         "SELECT '入仓单' as InvoiceType,DepotInId as InvoiceNO,(select DepotInDate  FROM depotIn where depotIn.DepotInId = DepotInDetail.DepotInId) as InvoiceDate ,DepotInDetail.DepotInQuantity AS InvoiceQuantity,ProductUnit AS  InvoiceUnit,(SELECT DepotName FROM Depot WHERE Depot.DepotId=(SELECT DepotId FROM depotIn WHERE depotIn.DepotInId=DepotInDetail.DepotInId )) AS DepotName,(SELECT id FROM DepotPosition WHERE DepotPosition.DepotPositionId=DepotInDetail.DepotPositionId) AS PositionName,(SELECT StocksQuantity FROM product WHERE   product.ProductId=DepotInDetail.ProductId) AS Stock1,Description  FROM DepotInDetail WHERE ProductId='"+productId+"' and DepotInId in (select DepotInId from DepotIn where (DepotInDate between '"+startDate.ToString("yyyy-MM-dd")+"' and '"+endDate.ToString("yyyy-MM-dd")+"') or ('"+startDate.ToString("yyyy-MM-dd")+"' is null and '"+endDate.ToString("yyyy-MM-dd")+"' is null ))"
                           };
            for (int m = 0; m < sqls.Length; m++)
            {
                #region
                Model.StockSeach stockSeach = new Model.StockSeach();
                using (SqlDataReader dataReader = SQLDB.SqlHelper.ExecuteReader(SQLDB.SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqls[m], null))
                {
                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            foreach (var item in stockSeach.GetType().GetProperties())
                            {
                                string fieldName = item.Name;//属性名
                                //判断当前迭代出的属性名称是否和迭代出的dataReader的列名称一致
                                if (item.Name.ToLower().Equals(dataReader.GetName(i).ToLower()))
                                {
                                    //从DataReader中读取值
                                    object _value = dataReader[fieldName];
                                    //将当前dataReader的单列值赋予相匹配的属性,否则赋予一个null值.
                                    if (_value != null && _value != DBNull.Value)
                                        item.SetValue(stockSeach, _value, null);
                                    else
                                        item.SetValue(stockSeach, null, null);
                                }
                            }
                        }
                        list.Add(stockSeach);

                    }
                }
                #endregion
            }
            return list;

        }

        #endregion
    }
}