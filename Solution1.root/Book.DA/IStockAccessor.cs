//------------------------------------------------------------------------------
//
// file name：IStockAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:48
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Stock
    /// </summary>
    public partial interface IStockAccessor : IEntityAccessor
    {
        System.Data.DataTable SelectDataTable();
        void UpdateDataTable(System.Data.DataTable stocks);

        System.Data.DataTable SelectDataTable(string strWhere);
        System.Data.DataTable SelectDataTable(Model.Product product);

        void Increment(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        void Decrement(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        void Increment(Model.DepotPosition depotPosition, Model.Product product, double? quantity);
        void Decrement(Model.DepotPosition depotPosition, Model.Product product, double? quantity);
        void IncrementJR(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        void DecrementJR(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        void IncrementJC(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        void DecrementJC(Model.DepotPosition depotPosition, Model.Product product, double quantity);
        bool Exists(Model.DepotPosition depotPosition, Model.Product product);
        IList<Model.Stock> Select(string proid);
        IList<Model.Stock> Select(Model.Depot Depot);
        IList<Model.Stock> Select(Model.Stock stock);
        Model.Stock GetStockByPidAndDid(Model.Stock stock);
        double GetTheCount1OfProductByProductId(Model.Product product, Model.Depot depot);
        double GetTheCount0OfProductByProductId(Model.Product product, Model.Depot depot);
        double GetTheCountByProduct(Model.Product product);
        System.Data.DataSet SelectDataSet();
        void UpdateDataTable1(System.Data.DataTable stocks);
        Model.Stock GetStockByProductIdAndDepotPositionId(string productid, string depotpositionId);
        DataTable SelectDataTableProName(string proName);
        IList<Model.StockSeach> SelectReaderByPro(string productId, DateTime startDate, DateTime endDate);
        IList<Model.Stock> GetStockByPidAndDid(string productId, string depotId);
        IList<Model.Stock> SelectNotZeroByPidAndDid(string productId, string depotId);

        System.Data.DataTable SelectDepotDistributedByproduct(string productid);
        IList<Model.StockSeach> SelectJiShi(string productId, DateTime startDate, DateTime endDate);
        DateTime? Get0DateByProduct(string productid);
        double SelectStockQuantity0(string productid);
        double SelectStockQuantity1(string productId, string depotpositionId);
        double SelectJiShidistributioned(string productId, DateTime startDate, DateTime endDate);

        System.Data.DataTable SelectProductNoDepotout(double years, string productCategoryId);

        IList<Model.StockSeach> SelectOutAndInDepot(DateTime startDate, DateTime endDate, string startProductCategory, string endProductCategory, string depotId, string handBookId);
    }
}

