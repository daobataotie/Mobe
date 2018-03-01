//------------------------------------------------------------------------------
//
// file name：IProductAccessor.cs
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
    /// Interface of data accessor of dbo.Product
    /// </summary>
    public partial interface
        IProductAccessor : IEntityAccessor
    {
        IList<Book.Model.Product> SelectProduct();

        System.Data.DataTable SelectDataTable();
        void UpdateBeginCost(System.Data.DataTable dt);
        IList<Model.Product> SelectByProductIds(string productIds);
        /// <summary>
        /// 更新当前成本
        /// </summary>
        /// <param name="product"></param>
        void UpdateCost1(Model.Product product, decimal? price, double? quantity);

        IList<Model.Product> Select(Model.ProductCategory Category);
        IList<Model.Product> Select(Model.Depot depot);
        bool ExistsNameInsert(string productName);
        bool ExistsNameUpdate(Model.Product product);
        IList<Model.Product> Select(Model.Customer customer);
        //查询指定类型和客户的货品和公司货品
        IList<Model.Product> Select(Model.Customer customer, Model.ProductCategory cate);
        IList<Model.Product> SelectProductByCustomer(Model.Customer customer);
        Model.Product Get(Model.Customer customer, Model.Product product);
        void Delete(Book.Model.Product product, Model.Customer customer);
        IList<Model.Product> GetProduct();
        IList<Model.Product> GetProductByCondition(string ProductCategoryName, string pt, string depotid);
        IList<Book.Model.Product> SelectProductByProductCategoryId(Book.Model.ProductCategory Category);
        IList<Model.Product> SelectNotCustomer();
        IList<Model.Product> SelectNotCustomer1();
        IList<Model.Product> SelectNotCustomerByCate(string productCate);
        IList<Model.Product> SelectByIdOrNameKey(string id, string productName, string customerProductName);
        IList<Model.Product> GetProductReader();
        //根据组装前半成品 查询 裸片加工后商品
        IList<Model.Product> SelectProceProduct(Model.Product product);
        IList<Model.Product> SelectProceByProduct(Model.Product product);
        IList<Model.Product> SelectALLIdOrNameKey(string id, string productName, string customerProductName);
        //更新product表,使其与stock表中数据对应 CdmiN--2011年9月29日16:05:38
        void UpdateProduct_Stock(Book.Model.Product pro);
        double? getStockByProduct(string productid);
        //已分配 和 庫存
        Model.Product getStockYFPByProduct(string productid);

        IList<Model.Product> StockPrompt();
        IList<Book.Model.Product> SelectProductByCondition(string productStart, string productEnd, string DepotIdStart, string DepotIdEnd, string productCategoryIdStart, string productCategoryIdEnd);
        IList<Model.Product> SelectProductsByProductIds(string Productids);

        void UpdateSimple(Book.Model.Product product);

        IList<Model.Product> SelectIdAndStock(string categoryId);

        IList<Model.Product> SelectProductIdAndName();

        DataTable SelectProductCategoryByProductIds(string productIds);
    }
}

