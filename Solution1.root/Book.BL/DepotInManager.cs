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
    public partial class DepotInManager : BaseManager
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
                foreach (Model.DepotInDetail depotInDetail in depotIn.Details)
                {
                    if (depotInDetail.Product == null || depotInDetail.Product.ProductId == null) continue;
                    // product.MpsStockQuantity=
                    depotInDetail.DepotPosition = depotPositionAccessor.Get(depotInDetail.DepotPositionId);
                    stockAccessor.Decrement(depotInDetail.DepotPosition, depotInDetail.Product, depotInDetail.DepotInQuantity);
                    productManager.UpdateProduct_Stock(depotInDetail.Product);
                }
                this.Delete(depotIn.DepotInId);

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
                TiGuiExists(depotIn);


                string invoiceKind = GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, depotIn.DepotInDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, depotIn.DepotInDate.Value.Year, depotIn.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, depotIn.DepotInDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(depotIn);

                foreach (Model.DepotInDetail depotInDetail in depotIn.Details)
                {
                    //if (depotInDetail.ProductId == null || depotInDetail.Product == null) continue;
                    depotInDetail.DepotInId = depotIn.DepotInId;
                    depotInDetail.DepotPosition = depotPositionAccessor.Get(depotInDetail.DepotPositionId);
                    depotInDetailsAccessor.Insert(depotInDetail);

                    if (depotInDetail.Product == null)
                        depotInDetail.Product = productAccessor.Get(depotInDetail.ProductId);
                    Model.Stock temp = stockAccessor.GetStockByProductIdAndDepotPositionId(depotInDetail.ProductId, depotInDetail.DepotPositionId);
                    if (temp != null && temp.StockQuantity1 == 0)
                    {
                        temp.Descriptions = depotInDetail.Descriptions;
                        stockAccessor.Update(temp);
                    }
                    stockAccessor.Increment(depotInDetail.DepotPosition, depotInDetail.Product, depotInDetail.DepotInQuantity.Value);
                    productManager.UpdateProduct_Stock(depotInDetail.Product);

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
                if (item.ProductId == null) continue;
                if (item.DepotInQuantity != null && item.DepotInQuantity.Value >= 0)
                    IsNullOrZero = true;
                istrue = true;
                if (item.DepotPositionId == null || item.DepotPositionId == "")
                    throw new Helper.RequireValueException(Model.DepotInDetail.PRO_DepotPositionId);

            }

            if (istrue == false)
                throw new Helper.RequireValueException("ProductDetail Is Null");
            if (IsNullOrZero == false)
                throw new Helper.MessageValueException("數量不能為空！");
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
                    //item.Product.StocksQuantity -= item.DepotInQuantity;
                    //productManager.update(item.Product);
                    stockAccessor.Decrement(item.DepotPosition, item.Product, item.DepotInQuantity);
                    productManager.UpdateProduct_Stock(item.Product);
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
                    //if (detail.Product == null || detail.Product.ProductId == null) continue;
                    detail.DepotPosition = depotPositionAccessor.Get(detail.DepotPositionId);
                    detail.DepotInId = depotIn.DepotInId;
                    depotInDetailsAccessor.Insert(detail);
                    //影响库存

                    if (detail.Product == null)
                        detail.Product = productAccessor.Get(detail.ProductId);
                    Model.Stock temp = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                    if (temp != null && temp.StockQuantity1 == 0)
                    {
                        temp.Descriptions = detail.Descriptions;
                        stockAccessor.Update(temp);
                    }

                    stockAccessor.Increment(detail.DepotPosition, detail.Product, detail.DepotInQuantity);
                    productManager.UpdateProduct_Stock(detail.Product);

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


        protected override string GetSettingId()
        {
            return "depotInRule";
        }

        protected override string GetInvoiceKind()
        {
            return "depotIn";
        }

        public Model.DepotIn GetDetails(Model.DepotIn depotIn)
        {
            depotIn = this.Get(depotIn.DepotInId);
            if (depotIn != null)
                depotIn.Details = depotInDetailsAccessor.GetDetailByDepotInId(depotIn.DepotInId);
            return depotIn;
        }

        public IList<Model.DepotIn> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }

        private void TiGuiExists(Model.DepotIn model)
        {
            if (this.ExistsPrimary(model.DepotInId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.DepotInDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.DepotInDate.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.DepotInDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.DepotInId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        public IList<Model.DepotIn> SelectByDateAndOther(DateTime startDate, DateTime endDate, Model.Product product, string depotInId, Model.Employee employee, Model.Employee employee0, string depotId, Model.Supplier supplier)
        {
            return accessor.SelectByDateAndOther(startDate, endDate, product, depotInId, employee, employee0, depotId, supplier);
        }
    }
}

