//------------------------------------------------------------------------------
//
// file name：DepotInManager.cs
// author: mayanjun
// create date：2010-10-25 16:14:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotIn.
    /// </summary>
    public partial class DepotInManager
    {
        private static readonly DA.IDepotInDetailAccessor depotInDetailsAccessor = (DA.IDepotInDetailAccessor)Accessors.Get("DepotInDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly ProductManager productManager = new ProductManager();
        private static readonly DA.IDepotPositionAccessor depotPositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        /// <summary>
        /// Delete DepotIn by primary key.
        /// </summary>
        public void Delete(string depotInId)
        {
            accessor.Delete(depotInId);
        }


        public void Delete(Model.DepotIn depotIn)
        {
            try
            {
                V.BeginTransaction();
                this.Delete(depotIn.DepotInId);

                foreach (Model.DepotInDetail depotInDetail in depotIn.Details)
                {
                    if (depotInDetail.Product == null || depotInDetail.Product.ProductId == null) continue;
                    depotInDetail.Product.StocksQuantity -= depotInDetail.DepotInQuantity.Value;
                    depotInDetail.Product.UpdateTime = System.DateTime.Now;
                    depotInDetail.DepotPosition = depotPositionAccessor.Get(depotInDetail.DepotPositionId);
                    productManager.update(depotInDetail.Product);
                    //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    //if (stock != null)
                    //{
                    //    stock.StockQuantity1 -= depotInDetail.DepotInQuantity.Value;
                    //    stockAccessor.Update(stock);
                    //}
                    stockAccessor.Decrement(depotInDetail.DepotPosition, depotInDetail.Product, depotInDetail.DepotInQuantity);
                }

                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a DepotIn.
        /// </summary>
        public void Insert(Model.DepotIn depotIn)
        {

            validate(depotIn);

            try
            {
                V.BeginTransaction();

                depotIn.InsertTime = System.DateTime.Now;
                depotIn.UpdateTime = System.DateTime.Now;

                string invoiceKind = GetKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, depotIn.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, depotIn.InsertTime.Value.Year, depotIn.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, depotIn.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(depotIn);

                foreach (Model.DepotInDetail depotInDetail in depotIn.Details)
                {
                    if (depotInDetail.Product == null || depotInDetail.Product.ProductId == null) continue;
                    depotInDetail.DepotInId = depotIn.DepotInId;
                    depotInDetail.DepotPosition = depotPositionAccessor.Get(depotInDetail.DepotPositionId);
                    depotInDetailsAccessor.Insert(depotInDetail);
                    Model.Product tempProduct = productManager.Get(depotInDetail.ProductId);
                    tempProduct.StocksQuantity += depotInDetail.DepotInQuantity.Value;
                    tempProduct.UpdateTime = System.DateTime.Now;
                    productManager.update(tempProduct);
                    Model.Stock temp = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    if (temp != null && temp.StockQuantity1 == 0)
                    {
                        temp.Descriptions = depotInDetail.Descriptions;
                        stockAccessor.Update(temp);
                    }
                    stockAccessor.Increment(depotInDetail.DepotPosition, depotInDetail.Product, depotInDetail.DepotInQuantity.Value);

                }

                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        private void validate(Book.Model.DepotIn depotIn)
        {
            if (string.IsNullOrEmpty(depotIn.DepotInId))
                throw new Helper.RequireValueException(Model.DepotIn.PRO_DepotInId);
            //if (this.ExistsPrimary(depotIn.DepotInId))
            //    throw new Helper.InvalidValueException(Model.DepotIn.PROPERTY_DEPOTINID);
            bool istrue = false;
            bool IsNullOrZero = false;
            foreach (Model.DepotInDetail item in depotIn.Details)
            {
                if (item.Product == null || item.Product.ProductId == null) continue;
                if (item.DepotInQuantity != null && item.DepotInQuantity.Value > 0)
                    IsNullOrZero = true;
                istrue = true;
                if (item.DepotPositionId == null || item.DepotPositionId == "")
                    throw new Helper.RequireValueException(Model.DepotInDetail.PRO_DepotPositionId);

            }

            if (istrue == false)
                throw new Helper.RequireValueException("ProductDetail Is Null");
            if (IsNullOrZero == false)
                throw new Helper.MessageValueException("數量不能為空或者零！");
        }

        /// <summary>
        /// Update a DepotIn.
        /// </summary>
        public void Update(Model.DepotIn depotIn)
        {
            validate(depotIn);
            try
            {
                V.BeginTransaction();
                depotIn.UpdateTime = DateTime.Now;
                accessor.Update(depotIn);
                //返回库存
                Model.DepotIn depotInOld = this.GetDetails(depotIn);
                foreach (Model.DepotInDetail item in depotInOld.Details)
                {
                    item.DepotPosition = depotPositionAccessor.Get(item.DepotPositionId);
                    item.Product.StocksQuantity -= item.DepotInQuantity;
                    productManager.update(item.Product);
                    stockAccessor.Decrement(item.DepotPosition, item.Product, item.DepotInQuantity);
                }
                //删除详细                          
                depotInDetailsAccessor.Delete(depotIn);

                //上次新增時和這次修改時的差值
                //double Quantity = 0;
                //string DepotInDetailId = "";
                //Dictionary<string, Model.DepotInDetail> dic = new Dictionary<string, Book.Model.DepotInDetail>();
                //IList<Model.DepotInDetail> templist = depotInDetailsAccessor.GetDetailByDepotInId(depotIn.DepotInId);
                //foreach (Model.DepotInDetail item in templist)
                //{
                //    dic.Add(item.DepotInDetailId, item);
                //}

                //添加详细
                foreach (Model.DepotInDetail detail in depotIn.Details)
                {
                    //if (dic.ContainsKey(depotInDetail.DepotInDetailId))
                    //    dic.Remove(depotInDetail.DepotInDetailId);
                    if (detail.Product == null || detail.Product.ProductId == null) continue;
                    detail.DepotPosition = depotPositionAccessor.Get(detail.DepotPositionId);
                    depotInDetailsAccessor.Insert(detail);
                    //影响库存
                    detail.Product = productAccessor.Get(detail.ProductId);
                    detail.Product.StocksQuantity += detail.DepotInQuantity;
                    productManager.update(detail.Product);

                    Model.Stock temp = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                    if (temp != null && temp.StockQuantity1 == 0)
                    {
                        temp.Descriptions = detail.Descriptions;
                        stockAccessor.Update(temp);
                    }

                    stockAccessor.Increment(detail.DepotPosition, detail.Product, detail.DepotInQuantity);

                    #region
                    //Model.DepotInDetail detail = depotInDetailsAccessor.Get(DepotInDetailId);
                    //不存在，说明是新入库的詳細
                    //if (detail == null)
                    //{
                    //    depotInDetail.DepotInId = depotIn.DepotInId;
                    //    depotInDetailsAccessor.Insert(depotInDetail);

                    //    Model.Product product = productAccessor.Get(depotInDetail.Product.ProductId);
                    //    if (product != null)
                    //    {
                    //        product.StocksQuantity += depotInDetail.DepotInQuantity.Value;
                    //        product.UpdateTime = System.DateTime.Now;
                    //        productAccessor.Update(product);
                    //    }

                    //    Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    //    if (stock != null)
                    //    {
                    //        stock.StockQuantity1 += depotInDetail.DepotInQuantity.Value;
                    //        stockAccessor.Update(stock);
                    //    }
                    //    else
                    //    {
                    //        stock = new Book.Model.Stock();
                    //        stock.StockId = Guid.NewGuid().ToString();
                    //        stock.DepotPositionId = depotInDetail.DepotPositionId;
                    //        stock.DepotId = depotIn.DepotId;
                    //        stock.Product = depotInDetail.Product;
                    //        stock.ProductId = stock.Product.ProductId;
                    //        stock.StockQuantity1 = depotInDetail.DepotInQuantity.Value;
                    //        stock.StockQuantityOld = 0;
                    //        stockAccessor.Insert(stock);
                    //    }
                    //}

                    //else  //存在时：有可能产品已经被修改
                    //{
                    //    depotInDetail.DepotInId = depotIn.DepotInId;
                    //    //从字典中取出上次保存的此入库详细的产品，如果和本次的产品相同
                    //    if (depotInDetail.Product.ProductId == detail.ProductId)
                    //    {
                    //        //判断本次数量和上次数量是否相同，若相同库存表和产品表以及入库详细均不用保存
                    //        if (depotInDetail.DepotInQuantity.Value == detail.DepotInQuantity.Value) continue;

                    //        Quantity = detail.DepotInQuantity.Value - depotInDetail.DepotInQuantity.Value;
                    //        depotInDetail.DepotInId = depotIn.DepotInId;
                    //        depotInDetailsAccessor.Update(depotInDetail);

                    //        Model.Product product = productAccessor.Get(depotInDetail.Product.ProductId);
                    //        if (product != null)
                    //        {
                    //            product.StocksQuantity -= Quantity;
                    //            product.UpdateTime = System.DateTime.Now;
                    //            productAccessor.Update(product);
                    //        }

                    //        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    //        if (stock != null)
                    //        {
                    //            stock.StockQuantity1 -= Quantity;
                    //            stockAccessor.Update(stock);
                    //        }
                    //        else
                    //        {
                    //            stock = new Book.Model.Stock();
                    //            stock.StockId = Guid.NewGuid().ToString();
                    //            stock.DepotPositionId = depotInDetail.DepotPositionId;
                    //            stock.ProductId = depotInDetail.ProductId;
                    //            stock.DepotId = depotIn.DepotId;
                    //            stock.StockQuantity1 = depotInDetail.DepotInQuantity.Value;
                    //            stock.StockQuantityOld = 0;
                    //            stockAccessor.Insert(stock);
                    //        }
                    //    }
                    //    else   //产品不同时
                    //    {
                    //        depotInDetailsAccessor.Update(depotInDetail);
                    //        Model.Product tempproduct = productAccessor.Get(detail.ProductId);
                    //        tempproduct.StocksQuantity -= detail.Product.StocksQuantity;
                    //        productAccessor.Update(tempproduct);

                    //        Model.Stock tempStock = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                    //        tempStock.StockQuantity1 -= detail.Product.StocksQuantity;
                    //        stockAccessor.Update(tempStock);

                    //        Model.Product product = productAccessor.Get(depotInDetail.Product.ProductId);
                    //        if (product != null)
                    //        {
                    //            product.StocksQuantity += depotInDetail.DepotInQuantity.Value;
                    //            product.UpdateTime = System.DateTime.Now;
                    //            productAccessor.Update(product);
                    //        }

                    //        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    //        if (stock != null)
                    //        {
                    //            stock.StockQuantity1 += depotInDetail.DepotInQuantity.Value;
                    //            stockAccessor.Update(stock);
                    //        }
                    //        else
                    //        {
                    //            stock = new Book.Model.Stock();
                    //            stock.StockId = Guid.NewGuid().ToString();
                    //            stock.DepotPositionId = depotInDetail.DepotPositionId;
                    //            stock.DepotId = depotIn.DepotId;
                    //            stock.Product = depotInDetail.Product;
                    //            stock.ProductId = stock.Product.ProductId;
                    //            stock.StockQuantity1 = depotInDetail.DepotInQuantity.Value;
                    //            stock.StockQuantityOld = 0;
                    //            stockAccessor.Insert(stock);
                    //        }



                    //    }

                    // }
                    #endregion
                }

                //foreach (string item in dic.Keys)
                //{
                //    Model.DepotInDetail depot = depotInDetailsAccessor.Get(item);

                //    depotInDetailsAccessor.Delete(item);

                //    Model.Product tempproduct = productAccessor.Get(depot.ProductId);
                //    tempproduct.StocksQuantity -= depot.DepotInQuantity;
                //    productAccessor.Update(tempproduct);

                //    Model.Stock tempStock = stockAccessor.GetStockByProductIdAndDepotPositionId(depot.ProductId, depot.DepotPositionId);
                //    tempStock.StockQuantity1 -= depot.DepotInQuantity;
                //    stockAccessor.Update(tempStock);
                //}

                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        string GetKind()
        {
            return "depotin";
        }

        public string GetNewId()
        {
            int sequenceval = SequenceManager.GetCurrentVal(GetKind());
            return string.Format("{0}{1:d5}", System.DateTime.Now.ToString("yyyyMMdd"), sequenceval + 1);
        }

        public Model.DepotIn GetDetails(Model.DepotIn depotIn)
        {
            depotIn = this.Get(depotIn.DepotInId);
            if (depotIn != null)
                depotIn.Details = depotInDetailsAccessor.GetDetailByDepotInId(depotIn.DepotInId);
            return depotIn;
        }
    }
}

