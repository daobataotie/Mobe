//------------------------------------------------------------------------------
//
// file name：StockCheckDetailManager.cs
// author: mayanjun
// create date：2010-7-30  11:43:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.StockCheckDetail.
    /// </summary>
    public partial class StockCheckDetailManager : BaseManager
    {
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IDepotAccessor depotAccessor = (DA.IDepotAccessor)Accessors.Get("DepotAccessor");
        /// <summary>
        /// Delete StockCheckDetail by primary key.
        /// </summary>
        public void Delete(string stockCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(stockCheckDetailId);
        }

        /// <summary>
        /// Insert a StockCheckDetail.
        /// </summary>
        public void Insert(Model.StockCheckDetail stockCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(stockCheckDetail);
        }

        /// <summary>
        /// Update a StockCheckDetail.
        /// </summary>
        public void Update(Model.StockCheckDetail stockCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(stockCheckDetail);
        }
        public IList<Book.Model.StockCheckDetail> Select(Book.Model.StockCheck stockCheck)
        {
            return accessor.Select(stockCheck);
        }

        public void Delete(Book.Model.StockCheck invoice)
        {
            accessor.Delete(invoice);
        }

        public Model.StockCheckDetail SelectStockcheck(Model.StockCheckDetail stockcheck)
        {
            return accessor.SelectStockcheck(stockcheck);
        }

        public IList<Model.StockCheckDetail> GetStockCheckDetailByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.GetStockCheckDetailByDate(startDate, endDate);
        }

        public double GetNumsByProductIdAndDepositionId(string positionId, string productid)
        {
            return accessor.GetNumsByProductIdAndDepositionId(positionId, productid);
        }

        public Model.StockCheckDetail SelectByProductIdAndPositionIdAndStockCheckId(string positionId, string productId, string stockCheckId)
        {
            return accessor.SelectByProductIdAndPositionIdAndStockCheckId(positionId, productId, stockCheckId);
        }

        public DataSet SelectDataSet()
        {
            return accessor.SelectDataSet();

        }
        public DataSet SelectDataSet(DateTime start, DateTime end)
        {
            return accessor.SelectDataSet(start, end);
        }
        public void UpdateDataTable(System.Data.DataTable dt)
        {

            try
            {
                V.BeginTransaction();
                accessor.UpdateDataTable(dt);
                foreach (DataRow item in dt.Rows)
                {
                    Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(item["ProductId"].ToString(), item["DepotPositionId"].ToString());
                    if (stock != null)
                    {
                        stock.StockQuantity1 = double.Parse(item["StockCheckQuantity"].ToString());
                        stockAccessor.Update(stock);
                    }
                    Model.Product product = productAccessor.Get(item["ProductId"].ToString());
                    Model.Depot depot = depotAccessor.Get(item["DepotId"].ToString());
                    if (product != null)
                    {
                        product.StocksQuantity = stockAccessor.GetTheCount1OfProductByProductId(product, depot);
                        productAccessor.Update(product);
                    }
                }
                V.CommitTransaction();
            }
            catch (Exception)
            {
                V.RollbackTransaction();
                throw;
            }
        }
        public IList<Model.StockCheckDetail> SelectByProductId(string productId)
        {
            return accessor.SelectByProductId(productId);
        }

        public IList<Model.StockCheckDetail> SelectRangeDataDiffCheck(DateTime startTime, DateTime endTime)
        {
            return accessor.SelectRangeDataDiffCheck(startTime, endTime);
        }
    }
}

