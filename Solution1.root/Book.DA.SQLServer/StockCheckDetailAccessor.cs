//------------------------------------------------------------------------------
//
// file name：StockCheckDetailAccessor.cs
// author: mayanjun
// create date：2010-7-30  11:43:34
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
    /// Data accessor of StockCheckDetail
    /// </summary>
    public partial class StockCheckDetailAccessor : EntityAccessor, IStockCheckDetailAccessor
    {
        #region IStockCheckDetailAccessor 成员


        public IList<Book.Model.StockCheckDetail> Select(Book.Model.StockCheck stockCheck)
        {
            return sqlmapper.QueryForList<Book.Model.StockCheckDetail>("StockCheckDetail.select_by_StockCheckId", stockCheck.StockCheckId);
        }

        #endregion

        #region IStockCheckDetailAccessor 成员


        public void Delete(Book.Model.StockCheck invoice)
        {
            sqlmapper.Delete("StockCheckDetail.Delete_by_StockCheckId", invoice.StockCheckId);
        }

        public Model.StockCheckDetail SelectStockcheck(Model.StockCheckDetail stockcheck)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", stockcheck.ProductId);
            return sqlmapper.QueryForObject<Model.StockCheckDetail>("StockCheckDetail.Select_by_ProductidAndDepotId", ht);
        }

        public IList<Model.StockCheckDetail> GetStockCheckDetailByDate(DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            return sqlmapper.QueryForList<Model.StockCheckDetail>("StockCheckDetail.Select_by_Rangedate", ht);
        }

        public double GetNumsByProductIdAndDepositionId(string positionId, string productid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("depositionId", positionId);
            ht.Add("productid", productid);
            return sqlmapper.QueryForObject<double>("StockCheckDetail.GetNumsByProductIdAndDepositionId", ht);
        }

        public Model.StockCheckDetail SelectByProductIdAndPositionIdAndStockCheckId(string positionId, string productId, string stockCheckId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("stockcheckId", stockCheckId);
            ht.Add("depositionId", positionId);
            ht.Add("productId", productId);
            return sqlmapper.QueryForObject<Model.StockCheckDetail>("StockCheckDetail.SelectByProductIdAndPositionIdAndStockCheckId", ht);
        }

        public DataSet SelectDataSet()
        {
            string strSql = "select StockCheck.StockCheckDate,ISNULL(StockCheckBookQuantity,0) StockCheckBookQuantity,StockCheckDetail.DepotId,ISNULL(StockCheckQuantity,0) StockCheckQuantity,DepotPosition.Id postid,Depot.DepotName,product.ProductDescription, StockCheckDetail.ProductId,StockCheckDetail.StockCheckId,StockCheckDetail.DepotPositionId,Product.ProductName,Product.CustomerProductName,Product.id proId,StockCheckDetailId  FROM ((((StockCheckDetail INNER JOIN  StockCheck ON StockCheckDetail.StockCheckId=StockCheck.StockCheckId) INNER JOIN Product ON Product.ProductId = StockCheckDetail.ProductId ) INNER JOIN DepotPosition ON StockCheckDetail.DepotPositionId = DepotPosition.DepotPositionId)INNER JOIN Depot ON Depot.depotid=StockCheckDetail.depotid) order by  proId";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlmapper.DataSource.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds;
        }

        public DataSet SelectDataSet(DateTime start,DateTime end)
        {
            string strSql = "select StockCheck.StockCheckDate,ISNULL(StockCheckBookQuantity,0) StockCheckBookQuantity,StockCheckDetail.DepotId,ISNULL(StockCheckQuantity,0) StockCheckQuantity,DepotPosition.Id postid,Depot.DepotName, StockCheckDetail.ProductId,StockCheckDetail.DepotPositionId,Product.ProductName,StockCheckDetail.StockCheckId,Product.CustomerProductName,Product.id proId,StockCheckDetailId  FROM ((((StockCheckDetail INNER JOIN  StockCheck ON StockCheckDetail.StockCheckId=StockCheck.StockCheckId) INNER JOIN Product ON Product.ProductId = StockCheckDetail.ProductId ) INNER JOIN DepotPosition ON StockCheckDetail.DepotPositionId = DepotPosition.DepotPositionId)INNER JOIN Depot ON Depot.depotid=StockCheckDetail.depotid) "
                + " where  StockCheck.StockCheckDate between '" + start + "' and '" + end + "' order by  proId  and StockCheckBookQuantity <>StockCheckQuantity";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(strSql, sqlmapper.DataSource.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            return ds;
        }

        public void UpdateDataTable(DataTable stockCheckDetail)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.UpdateCommand = new SqlCommand("update dbo.StockCheckDetail set StockCheckQuantity=@StockCheckQuantity where StockCheckDetailId=@StockCheckDetailId", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@StockCheckDetailId", SqlDbType.VarChar, 50, "StockCheckDetailId"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@StockCheckQuantity", SqlDbType.Float, 32, "StockCheckQuantity"));
            dataAdapter.Update(stockCheckDetail);

        }

        public IList<Model.StockCheckDetail> SelectByProductId(string productId)
        {
            return sqlmapper.QueryForList<Model.StockCheckDetail>("StockCheckDetail.SelectByProductId", productId);
        }

        public IList<Model.StockCheckDetail> SelectRangeDataDiffCheck(DateTime startTime, DateTime endTime)
        {
            Hashtable ht=new Hashtable();
            ht.Add("startDate", startTime);
            ht.Add("endDate", endTime);
            return sqlmapper.QueryForList<Model.StockCheckDetail>("StockCheckDetail.SelectRangeDataDiffCheck", ht);
        }

        #endregion
    }
}
