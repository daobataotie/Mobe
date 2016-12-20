//------------------------------------------------------------------------------
//
// file name：StockCheckManager.cs
// author: mayanjun
// create date：2010-7-30 11: 43:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.StockCheck.
    /// </summary>
    public partial class StockCheckManager : BaseManager
    {
        private static readonly DA.IStockCheckDetailAccessor stockCheckDetailAccessor = (DA.IStockCheckDetailAccessor)Accessors.Get("StockCheckDetailAccessor");
        private static readonly DA.IStockCheckAccessor stockCheckAccessor = (DA.IStockCheckAccessor)Accessors.Get("StockCheckAccessor");
        private static readonly DA.IStockEditorAccessor stockEditorAccessor = (DA.IStockEditorAccessor)Accessors.Get("StockEditorAccessor");
        private static readonly StockCheckDetailManager detail = new StockCheckDetailManager();
        private static BL.StockManager stockManager = new StockManager();
        private static BL.ProductManager productmanager = new ProductManager();
        private static BL.DepotPositionManager depotPositionManager = new DepotPositionManager();

        /// <summary>
        /// Delete StockCheck by primary key.
        /// </summary>
        public void Delete(string stockCheckId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(stockCheckId);
        }

        public void Delete(Model.StockCheck StockCheck)
        {
            foreach (Model.StockCheckDetail item in StockCheck.Details)
            {
                Model.Stock stock = new Book.Model.Stock();
                stock = stockManager.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
                if (stock != null)
                {
                    stock.StockQuantity1 = (stock.StockQuantity1 == null ? 0 : stock.StockQuantity1) + (item.StockCheckQuantityDiff == null ? 0 : item.StockCheckQuantityDiff);
                    stock.IsNotUpdate = true;
                    stockManager.Update(stock);
                }
                productmanager.UpdateProduct_Stock(item.Product);
            }
            accessor.Delete(StockCheck.StockCheckId);
            Model.StockEditor stockEditor = stockEditorAccessor.Get(StockCheck.StockEditorId);
            stockEditor.IsStockCheck = false;
            stockEditorAccessor.Update(stockEditor);

        }

        /// <summary>
        /// Update a StockCheck.
        /// </summary>
        public void Update(Model.StockCheck stockCheck)
        {
            try
            {
                BL.V.BeginTransaction();

                stockCheck.UpdateTime = DateTime.Now;
                if (stockCheck.Employee0 != null)
                    stockCheck.Employee0Id = stockCheck.Employee0.EmployeeId;

                accessor.Update(stockCheck);

                //13.10.10曹瑞修改：校正不直接修改库存为校正数量，而是当前库存加上差异数量
                IList<Model.StockCheckDetail> detailList = stockCheckDetailAccessor.Select(stockCheck);
                foreach (Model.StockCheckDetail item in detailList)
                {
                    Model.Stock stock = new Book.Model.Stock();
                    stock = stockManager.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
                    if (stock != null)
                    {
                        stock.StockQuantity1 = (stock.StockQuantity1 == null ? 0 : stock.StockQuantity1) + (item.StockCheckQuantityDiff == null ? 0 : item.StockCheckQuantityDiff);
                        stock.IsNotUpdate = true;
                        stockManager.Update(stock);
                    }
                    productmanager.UpdateProduct_Stock(item.Product);
                    stockCheckDetailAccessor.Delete(item.StockCheckDetailId);
                }

                foreach (Model.StockCheckDetail details in stockCheck.Details)
                {
                    if (details.Product == null || string.IsNullOrEmpty(details.Product.ProductId)) continue;
                    stockCheckDetailAccessor.Insert(details);

                    Model.Stock stock = stockManager.GetStockByProductIdAndDepotPositionId(details.ProductId, details.DepotPositionId);
                    if (stock == null)
                    {
                        stock = new Book.Model.Stock();
                        stock.StockCheckDate = stockCheck.StockCheckDate;
                        stock.StockId = Guid.NewGuid().ToString();
                        stock.DepotPositionId = details.DepotPositionId;
                        stock.DepotId = stockCheck.DepotId;
                        stock.ProductId = details.ProductId;
                        stock.StockQuantityOld = 0;
                        stock.StockQuantity1 = details.StockCheckQuantity;
                        stock.IsNotUpdate = true;
                        stockManager.Insert(stock);
                    }
                    else
                    {
                        stock.StockCheckDate = stockCheck.StockCheckDate;
                        stock.DepotPositionId = details.DepotPositionId;
                        stock.StockQuantityOld = stock.StockQuantity1 == null ? 0 : stock.StockQuantity1;
                        stock.StockQuantity1 = (stock.StockQuantity1 == null ? 0 : stock.StockQuantity1) - (details.StockCheckQuantityDiff == null ? 0 : details.StockCheckQuantityDiff);
                        stock.IsNotUpdate = true;
                        stockManager.Update(stock);
                    }

                    //  product.StocksQuantity = stockManager.GetTheCount1OfProductByProductId(product, dic[productId].Depot);
                    //details.Product.StocksQuantity = details.StockCheckQuantity;
                    //修改產品
                    //productmanager.UpdateProduct_Stock(details.Product);
                    details.Product.StocksQuantity = stockManager.GetTheCountByProduct(details.Product);
                    details.Product.LastStockTakeTime = DateTime.Now;
                    productmanager.update(details.Product);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetSettingId()
        {
            return "stockrule";
        }

        protected override string GetInvoiceKind()
        {
            return "stock";
        }

        public string GetNewId()
        {
            string sequencekey = this.GetInvoiceKind().ToLower();
            int sequenceval = SequenceManager.GetCurrentVal(sequencekey) + 1;
            return string.Format("{0}{1:d4}", System.DateTime.Now.ToString("yyyyMMdd"), sequenceval);
        }

        public Model.StockCheck GetDetails(string StockCheckId)
        {
            Model.StockCheck stockCheck = accessor.Get(StockCheckId);
            if (stockCheck != null)
                stockCheck.Details = stockCheckDetailAccessor.Select(stockCheck);
            return stockCheck;
        }

        public void Insert(Model.StockCheck stockCheck)
        {
            //
            // todo:add other logic here
            //
            stockCheck.InsertTime = DateTime.Now;
            stockCheck.UpdateTime = DateTime.Now;

            Validate(stockCheck);

            try
            {
                BL.V.BeginTransaction();

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, stockCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, stockCheck.InsertTime.Value.Year, stockCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, stockCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = invoiceKind;
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                //设置盘点录入表为已校正
                Model.StockEditor stockEditor = stockEditorAccessor.Get(stockCheck.StockEditorId);
                stockEditor.IsStockCheck = true;
                stockEditorAccessor.Update(stockEditor);

                accessor.Insert(stockCheck);

                //Dictionary<string, Model.StockCheckDetail> dic = new Dictionary<string, Book.Model.StockCheckDetail>();
                foreach (Model.StockCheckDetail details in stockCheck.Details)
                {
                    if (details.Product == null || string.IsNullOrEmpty(details.Product.ProductId)) continue;
                    //if (!dic.ContainsKey(details.Product.ProductId))
                    //    dic.Add(details.Product.ProductId, details);

                    stockCheckDetailAccessor.Insert(details);

                    //13.10.10曹瑞修改：校正不直接修改库存为校正数量，而是当前库存加上差异数量
                    Model.Stock stock = stockManager.GetStockByProductIdAndDepotPositionId(details.ProductId, details.DepotPositionId);
                    if (stock == null)
                    {
                        stock = new Book.Model.Stock();
                        stock.StockCheckDate = stockCheck.StockCheckDate;
                        stock.StockId = Guid.NewGuid().ToString();
                        stock.DepotPositionId = details.DepotPositionId;
                        stock.DepotId = stockCheck.DepotId;
                        stock.ProductId = details.ProductId;
                        stock.StockQuantityOld = 0;
                        stock.StockQuantity1 = details.StockCheckQuantity;
                        stock.IsNotUpdate = true;
                        stockManager.Insert(stock);
                    }
                    else
                    {
                        stock.StockCheckDate = stockCheck.StockCheckDate;
                        stock.DepotPositionId = details.DepotPositionId;
                        stock.StockQuantityOld = stock.StockQuantity1 == null ? 0 : stock.StockQuantity1;
                        stock.StockQuantity1 = (stock.StockQuantity1 == null ? 0 : stock.StockQuantity1) - (details.StockCheckQuantityDiff == null ? 0 : details.StockCheckQuantityDiff);
                        stock.IsNotUpdate = true;
                        stockManager.Update(stock);
                    }

                    //  product.StocksQuantity = stockManager.GetTheCount1OfProductByProductId(product, dic[productId].Depot);
                    //details.Product.StocksQuantity = details.StockCheckQuantity;
                    //修改產品
                    productmanager.UpdateProduct_Stock(details.Product);
                }
                //foreach (string productId in dic.Keys)
                //{
                //    Model.Product product = productmanager.Get(productId);
                //    if (product.ProductImage == null)
                //        product.ProductImage = new byte[] { };
                //    if (product.ProductImage1 == null)
                //        product.ProductImage1 = new byte[] { };
                //    if (product.ProductImage2 == null)
                //        product.ProductImage2 = new byte[] { };
                //    if (product.ProductImage3 == null)
                //        product.ProductImage3 = new byte[] { };
                //  //  product.StocksQuantity = stockManager.GetTheCount1OfProductByProductId(product, dic[productId].Depot);
                //    product.StocksQuantity = dic[productId].StockCheckQuantity;
                //    //修改產品
                //    productmanager.update(product);
                //}


                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(Model.StockCheck stockCheck)
        {
            if (string.IsNullOrEmpty(stockCheck.StockCheckId))
            {
                throw new Helper.RequireValueException(Model.StockCheck.PROPERTY_STOCKCHECKID);
            }
            if (string.IsNullOrEmpty(stockCheck.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.StockCheck.PROPERTY_EMPLOYEE0ID);
            }
            if (string.IsNullOrEmpty(stockCheck.DepotId))
            {
                throw new Helper.RequireValueException(Model.StockCheck.PROPERTY_DEPOTID);
            }
            if (stockCheck.InsertTime == null)
            {
                throw new Helper.RequireValueException(Model.StockCheck.PROPERTY_INSERTTIME);
            }
        }

        public Model.StockCheck SelectByStockCheckId(string stockid)
        {
            return accessor.SelectByStockCheckId(stockid);
        }

        public IList<Model.StockCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            return accessor.SelectByDateRage(StartDate, EndDate);
        }

    }
}

