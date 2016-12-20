//------------------------------------------------------------------------------
//
// file name：StockManager.cs
// author: peidun
// create date：2008/6/6  10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Stock.
    /// </summary>
    public partial class StockManager : BaseManager
    {
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private ProductManager productManager = new ProductManager();
        /// <summary>
        /// Delete Stock by primary key.
        /// </summary>
        public void Delete(string stockId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(stockId);
        }

        /// <summary>
        /// Insert a Stock.
        /// </summary>
        public void Insert(Model.Stock stock)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(stock);
        }

        public void Insert(IList<Model.Stock> list)
        {
            foreach (Model.Stock item in list)
            {
                Model.Stock stock = this.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
                if (stock != null)
                {
                    if (stock.IsNotUpdate != null)
                    {
                        if (!stock.IsNotUpdate.Value)
                            stock.StockQuantity0 = item.StockQuantity0;
                        else
                        {
                            stock.StockQuantity0 = item.StockQuantity0;
                            stock.StockQuantity1 = item.StockQuantity0;
                        }
                    }
                    else
                    {
                        stock.StockQuantity0 = item.StockQuantity0;
                        stock.StockQuantity1 = item.StockQuantity0;
                    }

                    this.Update(stock);
                    Model.Product pro = productAccessor.Get(item.ProductId);
                    pro.StocksQuantity = this.GetTheCountByProduct(item.Product);
                    productManager.update(pro);
                }
                else
                {
                    this.Insert(item);
                    Model.Product pro = productAccessor.Get(item.ProductId);
                    pro.StocksQuantity = this.GetTheCountByProduct(item.Product);
                    productManager.update(pro);
                }
            }
        }

        /// <summary>
        /// Update a Stock.
        /// </summary>
        public void Update(Model.Stock stock)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(stock);
        }
        public System.Data.DataTable SelectDataTable()
        {
            return this.SelectDataTable("");
        }
        public System.Data.DataTable SelectDataTable(string strWhere)
        {
            return accessor.SelectDataTable(strWhere);
        }

        public System.Data.DataTable SelectDataTable(Model.Product product)
        {
            return accessor.SelectDataTable(product);
        }

        public void UpdateDataTable(System.Data.DataTable dt)
        {
            accessor.UpdateDataTable(dt);
        }
        public IList<Model.Stock> Select(string proid)
        {
            return accessor.Select(proid);
        }
        public IList<Model.Stock> Select(Model.Depot depot)
        {
            return accessor.Select(depot);
        }
        /// <summary>
        /// 查询指定产品和库房下库存数
        /// </summary>
        public IList<Model.Stock> Select(Model.Stock stock)
        {
            return accessor.Select(stock);
        }

        public Model.Stock GetStockByPidAndDid(Model.Stock stock)
        {
            return accessor.GetStockByPidAndDid(stock);

        }

        public double GetTheCount1OfProductByProductId(Model.Product product,Model.Depot depot)
        {
            return accessor.GetTheCount1OfProductByProductId(product,depot);
        }

        public double GetTheCount0OfProductByProductId(Model.Product product, Model.Depot depot)
        {
            return accessor.GetTheCount0OfProductByProductId(product,depot);
        }

        public double GetTheCountByProduct(Model.Product product)
        {
            return accessor.GetTheCountByProduct(product);
        }


        public DataSet SelectDataSet()
        {
            return accessor.SelectDataSet();

        }
        public void UpdateDataTable1(DataTable stocks)
        {
            accessor.UpdateDataTable1(stocks);
        }

        public Model.Stock GetStockByProductIdAndDepotPositionId(string productid, string depotpositionId)
        {
            return accessor.GetStockByProductIdAndDepotPositionId(productid, depotpositionId);
        }
        public DataTable SelectDataTableProName(string proName)
        {
            return accessor.SelectDataTableProName(proName);
        }
        public IList<Model.StockSeach> SelectReaderByPro(string productId,DateTime startDate,DateTime endDate)
        {
            return accessor.SelectReaderByPro(productId,startDate,endDate);
        }

        public IList<Model.Stock> GetStockByPidAndDid(string productId, string depotId)
        {
            return accessor.GetStockByPidAndDid(productId, depotId);
        }

        public IList<Model.Stock> SelectNotZeroByPidAndDid(string productId, string depotId)
        {
            return accessor.SelectNotZeroByPidAndDid(productId, depotId);
        }
    }
}

