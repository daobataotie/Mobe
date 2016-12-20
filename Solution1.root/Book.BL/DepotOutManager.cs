//------------------------------------------------------------------------------
//
// file name：DepotOutManager.cs
// author: mayanjun
// create date：2010-10-15 15:41:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotOut.
    /// </summary>
    public partial class DepotOutManager : BaseManager
    {
        private static readonly DA.IDepotOutDetailAccessor depotOutDetailAccessor = (DA.IDepotOutDetailAccessor)Accessors.Get("DepotOutDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IProduceMaterialdetailsAccessor produceMaterialDetailsAccessor = (DA.IProduceMaterialdetailsAccessor)Accessors.Get("ProduceMaterialdetailsAccessor");
        private static readonly DA.IProduceOtherMaterialDetailAccessor produceOtherMaterialDetailAccessor = (DA.IProduceOtherMaterialDetailAccessor)Accessors.Get("ProduceOtherMaterialDetailAccessor");

        private static readonly DA.IDepotPositionAccessor depotPositionAccessor = (DA.IDepotPositionAccessor)Accessors.Get("DepotPositionAccessor");
        private static readonly ProductManager productManager = new ProductManager();

        public void Delete(string depotOutId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(depotOutId);
        }

        public void Delete(Model.DepotOut depotOut)
        {
            try
            {
                V.BeginTransaction();
                this.Delete(depotOut.DepotOutId);
                if (depotOut.SourceType == "委外領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        //if (depotOutDetails.ProduceOtherMaterialDetailId != null)
                        //{
                        //    //已分配数量
                        //    product.OtherMaterialDistributioned = product.OtherMaterialDistributioned + depotOutDetails.DepotOutDetailQuantity;
                        //    if (product.OtherMaterialDistributioned < 0)
                        //        product.OtherMaterialDistributioned = 0;              

                        //}
                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.Product.ProductId, depotOutDetails.DepotPositionId);
                        if (stock != null)
                        {
                            stock.StockQuantity1 = Convert.ToDouble(stock.StockQuantity1) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            stockAccessor.Update(stock);
                            //若不等null 說明不是從採購訂單過來的數據
                            if (depotOutDetails.ProduceOtherMaterialDetailId != null)
                            {
                                Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = produceOtherMaterialDetailAccessor.Get(depotOutDetails.ProduceOtherMaterialDetailId);
                                if (produceOtherMaterialDetail != null)
                                {
                                    if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity == null)
                                        produceOtherMaterialDetail.OtherMaterialALLUserQuantity = 0;
                                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity -= Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity = (produceOtherMaterialDetail.OtherMaterialALLUserQuantity < 0 ? 0 : produceOtherMaterialDetail.OtherMaterialALLUserQuantity);
                                    if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity >= produceOtherMaterialDetail.OtherMaterialQuantity)
                                    {
                                        produceOtherMaterialDetail.DepotOutStateDetail = 2;
                                    }
                                    else
                                    {
                                        if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity > 0)
                                        {
                                            produceOtherMaterialDetail.DepotOutStateDetail = 1;
                                        }
                                        else
                                        {
                                            produceOtherMaterialDetail.DepotOutStateDetail = 0;
                                        }
                                    }
                                    produceOtherMaterialDetail.Distributioned = Convert.ToDouble(produceOtherMaterialDetail.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceOtherMaterialDetailAccessor.Update(produceOtherMaterialDetail);
                                    UpdateInvoiceXOFlagT(produceOtherMaterialDetail.ProduceOtherMaterial);
                                    product.OtherMaterialDistributioned = Convert.ToDouble(product.OtherMaterialDistributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (product.OtherMaterialDistributioned < 0)
                                        product.OtherMaterialDistributioned = 0;

                                    //更新与该商品有关的单据的已分配数量
                                    IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                    IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                    foreach (var itempo in polist)
                                    {
                                        itempo.Distributioned = Convert.ToDouble(itempo.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempo.Distributioned = itempo.Distributioned < 0 ? 0 : itempo.Distributioned;
                                        produceOtherMaterialDetailAccessor.Update(itempo);
                                    }
                                    foreach (var itempd in pdlist)
                                    {
                                        itempd.Distributioned = Convert.ToDouble(itempd.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempd.Distributioned = itempd.Distributioned < 0 ? 0 : itempd.Distributioned;
                                        produceMaterialDetailsAccessor.Update(itempd);
                                    }
                                }
                            }
                        }

                        //修改商品表库存

                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                        productManager.update(product);
                    }
                }
                else if (depotOut.SourceType == "領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);

                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(product.ProductId, depotOutDetails.DepotPositionId);
                        if (stock != null)
                        {
                            stock.StockQuantity1 = Convert.ToDouble(stock.StockQuantity1) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            stockAccessor.Update(stock);
                            //若不等null 說明不是從採購訂單過來的數據
                            if (depotOutDetails.ProduceMaterialdetailsID != null)
                            {
                                Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                                if (produceMaterialDetail != null)
                                {
                                    if (produceMaterialDetail.Materialprocesedsum == null)
                                        produceMaterialDetail.Materialprocesedsum = 0;
                                    produceMaterialDetail.Materialprocesedsum -= Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (produceMaterialDetail.Materialprocesedsum >= produceMaterialDetail.Materialprocessum)
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 2;
                                    }
                                    else
                                    {
                                        if (produceMaterialDetail.Materialprocesedsum > 0)
                                        {
                                            produceMaterialDetail.DepotOutStateDetail = 1;
                                        }
                                        else
                                        {
                                            produceMaterialDetail.DepotOutStateDetail = 0;
                                        }
                                    }
                                    produceMaterialDetail.Distributioned = Convert.ToDouble(produceMaterialDetail.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                                    UpdateInvoiceXOFlag(produceMaterialDetail.ProduceMaterial);
                                    //已分配数量
                                    product.ProduceMaterialDistributioned = Convert.ToDouble(product.ProduceMaterialDistributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (product.ProduceMaterialDistributioned < 0)
                                        product.ProduceMaterialDistributioned = 0;

                                    //更新与该商品有关的单据的已分配数量
                                    IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                    IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                    foreach (var itempo in polist)
                                    {
                                        itempo.Distributioned = Convert.ToDouble(itempo.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempo.Distributioned = itempo.Distributioned < 0 ? 0 : itempo.Distributioned;
                                        produceOtherMaterialDetailAccessor.Update(itempo);
                                    }
                                    foreach (var itempd in pdlist)
                                    {
                                        itempd.Distributioned = Convert.ToDouble(itempd.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempd.Distributioned = itempd.Distributioned < 0 ? 0 : itempd.Distributioned;
                                        produceMaterialDetailsAccessor.Update(itempd);
                                    }
                                }
                            }
                        }
                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                        productManager.update(product);
                    }
                }
                else
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        //if (depotOutDetails.ProduceMaterialdetailsID != null)
                        //{
                        //    //已分配数量
                        //    product.ProduceMaterialDistributioned = product.ProduceMaterialDistributioned + depotOutDetails.DepotOutDetailQuantity;
                        //    if (product.ProduceMaterialDistributioned < 0)
                        //        product.ProduceMaterialDistributioned = 0;
                        //}
                        depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);
                        //影响库存
                        stockAccessor.Increment(depotOutDetails.DepotPosition, product, depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);

                        //修改商品表库存
                        //Model.Product product = productAccessor.Get(depotOutDetails.ProductId);
                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                        productManager.UpdateProduct_Stock(product);
                        // productManager.update(product);
                    }
                }
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.DepotOut depotOut)
        {
            validate(depotOut);
            try
            {
                V.BeginTransaction();
                depotOut.InsertTime = System.DateTime.Now;
                depotOut.UpdateTime = System.DateTime.Now;
                TiGuiExists(depotOut);


                string invoiceKind = GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, depotOut.DepotOutDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, depotOut.DepotOutDate.Value.Year, depotOut.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, depotOut.DepotOutDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                //foreach (Model.DepotOutDetail item in depotOut.Details)
                //{
                //    GetStockQuantity(item);
                //}
                accessor.Insert(depotOut);
                if (depotOut.SourceType == "委外領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        GetStockQuantity(depotOutDetails);

                        if (depotOutDetails.Product == null || depotOutDetails.Product.ProductId == null) continue;

                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        if (!string.IsNullOrEmpty(depotOutDetails.ProduceOtherMaterialDetailId))
                        {
                            //委外已分配数量
                            product.OtherMaterialDistributioned = product.OtherMaterialDistributioned == null ? 0 : product.OtherMaterialDistributioned - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            if (product.OtherMaterialDistributioned < 0)
                                product.OtherMaterialDistributioned = 0;
                            //  productManager.update(product);

                        }

                        Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = produceOtherMaterialDetailAccessor.Get(depotOutDetails.ProduceOtherMaterialDetailId);
                        //if (produceMaterialDetail.Materialprocessum.Value - (produceMaterialDetail.Materialprocesedsum == null?0:produceMaterialDetail.Materialprocesedsum.Value) < depotOutDetails.DepotOutDetailQuantity.Value)
                        //    throw new global::Helper.InvalidValueException(Model.ProduceMaterialdetails.PRO_Materialprocessum);
                        depotOutDetails.DepotOutId = depotOut.DepotOutId;
                        depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);

                        //影响库存
                        stockAccessor.Decrement(depotOutDetails.DepotPosition, product, depotOutDetails.DepotOutDetailQuantity.Value);
                        product.StocksQuantity = (product.StocksQuantity == null ? 0 : product.StocksQuantity.Value) - (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        productManager.update(product);

                        //depotOutDetails.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(depotOutDetails.Product, depotOut.Depot);
                        //depotOutDetails.CurrentStockQuantity = depotOutDetails.Product.StocksQuantity;
                        depotOutDetails.HasOutQuantity = depotOutDetails.DepotOutDetailQuantity;
                        depotOutDetailAccessor.Insert(depotOutDetails);
                        if (produceOtherMaterialDetail != null)
                        {
                            if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity == null)
                                produceOtherMaterialDetail.OtherMaterialALLUserQuantity = 0;
                            produceOtherMaterialDetail.OtherMaterialALLUserQuantity += Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity >= produceOtherMaterialDetail.OtherMaterialQuantity)
                            {
                                produceOtherMaterialDetail.DepotOutStateDetail = 2;
                            }
                            else
                            {
                                if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity > 0)
                                {
                                    produceOtherMaterialDetail.DepotOutStateDetail = 1;
                                }
                                else
                                {
                                    produceOtherMaterialDetail.DepotOutStateDetail = 0;
                                }
                            }
                            produceOtherMaterialDetail.Distributioned = Convert.ToDouble(produceOtherMaterialDetail.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            produceOtherMaterialDetailAccessor.Update(produceOtherMaterialDetail);
                            UpdateInvoiceXOFlagT(produceOtherMaterialDetail.ProduceOtherMaterial);

                            //更新与该商品有关的单据的已分配数量
                            IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                            IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                            foreach (var item in polist)
                            {
                                item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                produceOtherMaterialDetailAccessor.Update(item);
                            }
                            foreach (var item in pdlist)
                            {
                                item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                produceMaterialDetailsAccessor.Update(item);
                            }
                        }

                    }
                }
                else if (depotOut.SourceType == "領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        GetStockQuantity(depotOutDetails);

                        if (depotOutDetails.Product == null || depotOutDetails.Product.ProductId == null) continue;
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        if (depotOutDetails.ProduceMaterialdetailsID != null)
                        {
                            //商品表已分配数量
                            product.ProduceMaterialDistributioned = Convert.ToDouble(product.ProduceMaterialDistributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            if (product.ProduceMaterialDistributioned < 0)
                                product.ProduceMaterialDistributioned = 0;
                            //  productManager.update(product);

                        }
                        Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                        //if (produceMaterialDetail.Materialprocessum.Value - (produceMaterialDetail.Materialprocesedsum == null?0:produceMaterialDetail.Materialprocesedsum.Value) < depotOutDetails.DepotOutDetailQuantity.Value)
                        //    throw new global::Helper.InvalidValueException(Model.ProduceMaterialdetails.PRO_Materialprocessum);
                        depotOutDetails.DepotOutId = depotOut.DepotOutId;
                        depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);

                        //影响库存
                        stockAccessor.Decrement(depotOutDetails.DepotPosition, product, depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        product.StocksQuantity = (product.StocksQuantity == null ? 0 : product.StocksQuantity.Value) - (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        productManager.update(product);
                        //depotOutDetails.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(depotOutDetails.Product, depotOut.Depot);
                        //depotOutDetails.CurrentStockQuantity = product.StocksQuantity;
                        depotOutDetails.HasOutQuantity = depotOutDetails.DepotOutDetailQuantity;
                        depotOutDetailAccessor.Insert(depotOutDetails);
                        if (depotOutDetails.ProduceMaterialdetailsID != null)
                        {
                            if (produceMaterialDetail != null)
                            {
                                if (produceMaterialDetail.Materialprocesedsum == null)
                                    produceMaterialDetail.Materialprocesedsum = 0;
                                produceMaterialDetail.Materialprocesedsum += (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity);
                                if (produceMaterialDetail.Materialprocesedsum >= produceMaterialDetail.Materialprocessum)
                                {
                                    produceMaterialDetail.DepotOutStateDetail = 2;
                                }
                                else
                                {
                                    if (produceMaterialDetail.Materialprocesedsum > 0)
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 1;
                                    }
                                    else
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 0;
                                    }
                                }
                                produceMaterialDetail.Distributioned = Convert.ToDouble(produceMaterialDetail.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                                UpdateInvoiceXOFlag(produceMaterialDetail.ProduceMaterial);

                                //更新与该商品有关的单据的已分配数量
                                IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                foreach (var item in polist)
                                {
                                    item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                    produceOtherMaterialDetailAccessor.Update(item);
                                }
                                foreach (var item in pdlist)
                                {
                                    item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                    produceMaterialDetailsAccessor.Update(item);
                                }
                            }
                        }

                    }


                    #region
                    // Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.ProductId, depotOutDetails.DepotPositionId);
                    //if (stock != null)
                    //{                      
                    //    stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity;
                    //    stockAccessor.Update(stock);
                    //    //若不等null 說明不是從採購訂單過來的數據

                    //}
                    //else
                    //{
                    //    stock = new Book.Model.Stock();
                    //    stock.StockId = Guid.NewGuid().ToString();
                    //    stock.DepotPositionId = depotOutDetails.DepotPositionId;
                    //    stock.ProductId = depotOutDetails.ProductId;
                    //    stock.DepotId = depotOut.DepotId;
                    //    stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity.Value;
                    //    stockAccessor.Insert(stock);
                    //}        
                    #endregion

                }
                else
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        GetStockQuantity(depotOutDetails);

                        if (depotOutDetails.Product == null || depotOutDetails.Product.ProductId == null) continue;
                        depotOutDetails.DepotOutId = depotOut.DepotOutId;
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        //depotOutDetails.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(depotOutDetails.Product, depotOut.Depot);
                        //depotOutDetails.CurrentStockQuantity = product.StocksQuantity;
                        depotOutDetails.HasOutQuantity = depotOutDetails.DepotOutDetailQuantity;
                        depotOutDetailAccessor.Insert(depotOutDetails);
                        depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);
                        //影响库存
                        stockAccessor.Decrement(depotOutDetails.DepotPosition, product, depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) - (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        productManager.UpdateProduct_Stock(product);

                    }
                }
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;
            }
        }

        private static void GetStockQuantity(Model.DepotOutDetail item)
        {
            Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
            if (stock == null || Convert.ToDecimal(stock.StockQuantity1) < Convert.ToDecimal(item.DepotOutDetailQuantity))
                throw new Helper.MessageValueException("序号" + item.Inumber + "：" + item.Product + "\r出货数量不能大于货位库存");
        }

        public void UpdateInvoiceXOFlag(Model.ProduceMaterial produceMaterial)
        {
            int flag = 0;
            IList<Model.ProduceMaterialdetails> list = produceMaterialDetailsAccessor.Select(produceMaterial);
            foreach (Model.ProduceMaterialdetails detail in list)
            {
                flag += detail.DepotOutStateDetail == null ? 0 : detail.DepotOutStateDetail.Value;
            }
            if (flag == 0)
                produceMaterial.DepotOutState = 0;
            else if (flag < list.Count * 2)
                produceMaterial.DepotOutState = 1;
            else if (flag == list.Count * 2)
                produceMaterial.DepotOutState = 2;
            new BL.ProduceMaterialManager().UpdateDepotOutState(produceMaterial);
        }

        public void UpdateInvoiceXOFlagT(Model.ProduceOtherMaterial produceMaterial)
        {
            int flag = 0;
            IList<Model.ProduceOtherMaterialDetail> list = produceOtherMaterialDetailAccessor.Select(produceMaterial);
            foreach (Model.ProduceOtherMaterialDetail detail in list)
            {
                flag += detail.DepotOutStateDetail == null ? 0 : detail.DepotOutStateDetail.Value;
            }
            if (flag == 0)
                produceMaterial.DepotOutState = 0;
            else if (flag < list.Count * 2)
                produceMaterial.DepotOutState = 1;
            else if (flag == list.Count * 2)
                produceMaterial.DepotOutState = 2;
            new BL.ProduceOtherMaterialManager().UpdateDepotOutState(produceMaterial);
        }

        public void Update(Model.DepotOut depotOut)
        {
            // validate(depotOut);
            try
            {
                V.BeginTransaction();
                if (string.IsNullOrEmpty(depotOut.DepotId))
                    throw new Helper.RequireValueException(Model.DepotOut.PRO_DepotOutId);
                if (string.IsNullOrEmpty(depotOut.EmployeeId))
                    throw new Helper.RequireValueException(Model.DepotOut.PRO_EmployeeId);

                foreach (Model.DepotOutDetail item in depotOut.Details)
                {
                    if (item.Product == null || item.Product.ProductId == null) continue;
                    if (item.DepotPositionId == null)
                        throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_DEPOTPOSITIONID);

                    if (item.DepotOutDetailId == null)
                        throw new Helper.RequireValueException(Model.DepotOutDetail.PRO_DepotPositionId);
                    //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
                    //if (stock == null)
                    //    throw new Helper.MessageValueException("产品：" + item.Product.ProductName + "\r當前貨位無庫存");

                }
                //foreach (Model.DepotOutDetail item in depotOut.Details)
                //{
                //    Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);
                //    if (stock == null || stock.StockQuantity1 < item.DepotOutDetailQuantity)
                //        throw new Helper.MessageValueException("" + item.Product + "\r出貨數量不能大於貨位庫存");
                //}

                depotOut.UpdateTime = DateTime.Now;
                accessor.Update(depotOut);
                //Model.DepotOut depotOutOld = this.GetDetails(depotOut.DepotOutId);
                //foreach (Model.DepotOutDetail item in depotOut.Details)
                //{
                //    if (item.DepotPositionId == null||item.Product==null)
                //        throw new Helper.RequireValueException(Model.DepotOutDetail.PROPERTY_DEPOTPOSITIONID);
                //    //depotOutDetails.DepotPositionId = depotOutDetails.DepotPosition.DepotPositionId;
                //}
                //返回库存和领料单已领数量

                //foreach (Model.DepotOutDetail item in depotOutOld.Details)
                //{
                //    Model.Product product = productManager.Get(item.ProductId);
                //    item.DepotPosition = depotPositionAccessor.Get(item.DepotPositionId);
                //    product.StocksQuantity = (product.StocksQuantity == null ? 0 : product.StocksQuantity.Value) + (item.DepotOutDetailQuantity == null ? 0 : item.DepotOutDetailQuantity.Value);
                //    stockAccessor.Increment(item.DepotPosition, item.Product, item.DepotOutDetailQuantity == null ? 0 : item.DepotOutDetailQuantity.Value);
                //    if (depotOutOld.SourceType == "領料單")
                //    {
                //        Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(item.ProduceMaterialdetailsID);
                //        if (produceMaterialDetail != null)
                //        {
                //            if (produceMaterialDetail.Materialprocesedsum == null)
                //                produceMaterialDetail.Materialprocesedsum = 0;
                //            produceMaterialDetail.Materialprocesedsum -= (item.DepotOutDetailQuantity == null ? 0 : item.DepotOutDetailQuantity.Value);
                //            produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                //            product.ProduceMaterialDistributioned = product.ProduceMaterialDistributioned + item.DepotOutDetailQuantity;
                //            if (product.ProduceMaterialDistributioned < 0)
                //                product.ProduceMaterialDistributioned = 0;
                //        }
                //    }
                //    else if (depotOutOld.SourceType == "委外領料單")
                //    {
                //        if (!string.IsNullOrEmpty(item.ProduceOtherMaterialDetailId))
                //        {
                //            Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = produceOtherMaterialDetailAccessor.Get(item.ProduceOtherMaterialDetailId);

                //            if (produceOtherMaterialDetail != null)
                //            {
                //                if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity == null)
                //                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity = 0;
                //                produceOtherMaterialDetail.OtherMaterialALLUserQuantity -= (item.DepotOutDetailQuantity == null ? 0 : item.DepotOutDetailQuantity.Value);
                //                produceOtherMaterialDetailAccessor.Update(produceOtherMaterialDetail);
                //                product.OtherMaterialDistributioned = item.Product.OtherMaterialDistributioned + item.DepotOutDetailQuantity;
                //                if (product.OtherMaterialDistributioned < 0)
                //                    product.OtherMaterialDistributioned = 0;
                //            }
                //        }
                //    }
                //    else
                //    {

                //    }
                //    productManager.update(product);
                //}


                //判断不能大于未领料数量
                //foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                //{
                //    Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                //    if (produceMaterialDetail != null)
                //    {
                //        if (produceMaterialDetail.Materialprocessum.Value - (produceMaterialDetail.Materialprocesedsum == null ? 0 : produceMaterialDetail.Materialprocesedsum.Value) < (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value))
                //            throw new global::Helper.InvalidValueException(Model.ProduceMaterialdetails.PRO_Materialprocessum);
                //    }
                //}
                #region  删除详细
                IList<Model.DepotOutDetail> DepotOutDetailList = depotOutDetailAccessor.SelectByDepotOutId(depotOut.DepotOutId);
                if (depotOut.SourceType == "委外領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in DepotOutDetailList)
                    {
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        //if (depotOutDetails.ProduceOtherMaterialDetailId != null)
                        //{
                        //    //已分配数量
                        //    product.OtherMaterialDistributioned = product.OtherMaterialDistributioned + depotOutDetails.DepotOutDetailQuantity;
                        //    if (product.OtherMaterialDistributioned < 0)
                        //        product.OtherMaterialDistributioned = 0;              

                        //}
                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.Product.ProductId, depotOutDetails.DepotPositionId);
                        if (stock != null)
                        {
                            stock.StockQuantity1 = Convert.ToDouble(stock.StockQuantity1) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            stockAccessor.Update(stock);
                            //若不等null 說明不是從採購訂單過來的數據
                            if (depotOutDetails.ProduceOtherMaterialDetailId != null)
                            {
                                Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = produceOtherMaterialDetailAccessor.Get(depotOutDetails.ProduceOtherMaterialDetailId);
                                if (produceOtherMaterialDetail != null)
                                {
                                    if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity == null)
                                        produceOtherMaterialDetail.OtherMaterialALLUserQuantity = 0;
                                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity -= Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity = (produceOtherMaterialDetail.OtherMaterialALLUserQuantity < 0 ? 0 : produceOtherMaterialDetail.OtherMaterialALLUserQuantity);
                                    if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity >= produceOtherMaterialDetail.OtherMaterialQuantity)
                                    {
                                        produceOtherMaterialDetail.DepotOutStateDetail = 2;
                                    }
                                    else
                                    {
                                        if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity > 0)
                                        {
                                            produceOtherMaterialDetail.DepotOutStateDetail = 1;
                                        }
                                        else
                                        {
                                            produceOtherMaterialDetail.DepotOutStateDetail = 0;
                                        }
                                    }
                                    produceOtherMaterialDetail.Distributioned = Convert.ToDouble(produceOtherMaterialDetail.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceOtherMaterialDetailAccessor.Update(produceOtherMaterialDetail);
                                    UpdateInvoiceXOFlagT(produceOtherMaterialDetail.ProduceOtherMaterial);
                                    //商品委外领料单已分配数量
                                    product.OtherMaterialDistributioned = Convert.ToDouble(product.OtherMaterialDistributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (product.OtherMaterialDistributioned < 0)
                                        product.OtherMaterialDistributioned = 0;

                                    //更新与该商品有关的单据的已分配数量
                                    IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                    IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                    foreach (var itempo in polist)
                                    {
                                        itempo.Distributioned = Convert.ToDouble(itempo.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempo.Distributioned = itempo.Distributioned < 0 ? 0 : itempo.Distributioned;
                                        produceOtherMaterialDetailAccessor.Update(itempo);
                                    }
                                    foreach (var itempd in pdlist)
                                    {
                                        itempd.Distributioned = Convert.ToDouble(itempd.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempd.Distributioned = itempd.Distributioned < 0 ? 0 : itempd.Distributioned;
                                        produceMaterialDetailsAccessor.Update(itempd);
                                    }
                                }
                            }
                        }

                        //修改商品表库存

                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                        productManager.update(product);
                        depotOutDetailAccessor.Delete(depotOutDetails.DepotOutDetailId);
                    }
                }
                else if (depotOut.SourceType == "領料單")
                {
                    foreach (Model.DepotOutDetail depotOutDetails in DepotOutDetailList)
                    {
                        Model.Product product = productManager.Get(depotOutDetails.ProductId);

                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(product.ProductId, depotOutDetails.DepotPositionId);
                        if (stock != null)
                        {
                            stock.StockQuantity1 += depotOutDetails.DepotOutDetailQuantity;
                            stockAccessor.Update(stock);
                            //若不等null 說明不是從採購訂單過來的數據
                            if (depotOutDetails.ProduceMaterialdetailsID != null)
                            {
                                Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                                if (produceMaterialDetail != null)
                                {
                                    if (produceMaterialDetail.Materialprocesedsum == null)
                                        produceMaterialDetail.Materialprocesedsum = 0;
                                    produceMaterialDetail.Materialprocesedsum -= Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (produceMaterialDetail.Materialprocesedsum >= produceMaterialDetail.Materialprocessum)
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 2;
                                    }
                                    else
                                    {
                                        if (produceMaterialDetail.Materialprocesedsum > 0)
                                        {
                                            produceMaterialDetail.DepotOutStateDetail = 1;
                                        }
                                        else
                                        {
                                            produceMaterialDetail.DepotOutStateDetail = 0;
                                        }
                                    }
                                    produceMaterialDetail.Distributioned = Convert.ToDouble(produceMaterialDetail.Distributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                                    UpdateInvoiceXOFlag(produceMaterialDetail.ProduceMaterial);
                                    //已分配数量
                                    product.ProduceMaterialDistributioned = Convert.ToDouble(product.ProduceMaterialDistributioned) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                    if (product.ProduceMaterialDistributioned < 0)
                                        product.ProduceMaterialDistributioned = 0;

                                  //更新与该商品有关的单据的已分配数量
                                    IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                    IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                    foreach (var itempo in polist)
                                    {
                                        itempo.Distributioned = Convert.ToDouble(itempo.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempo.Distributioned = itempo.Distributioned < 0 ? 0 : itempo.Distributioned;
                                        produceOtherMaterialDetailAccessor.Update(itempo);
                                    }
                                    foreach (var itempd in pdlist)
                                    {
                                        itempd.Distributioned = Convert.ToDouble(itempd.Distributioned) - Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                                        itempd.Distributioned = itempd.Distributioned < 0 ? 0 : itempd.Distributioned;
                                        produceMaterialDetailsAccessor.Update(itempd);
                                    }
                                }
                            }
                        }
                        product.StocksQuantity = Convert.ToDouble(product.StocksQuantity) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                        productManager.update(product);
                        depotOutDetailAccessor.Delete(depotOutDetails.DepotOutDetailId);
                    }
                }
                else
                {
                    foreach (Model.DepotOutDetail depotOutDetails in DepotOutDetailList)
                    {
                        //Model.Product product = productManager.Get(depotOutDetails.ProductId);
                        //if (depotOutDetails.ProduceMaterialdetailsID != null)
                        //{
                        //    //已分配数量
                        //    product.ProduceMaterialDistributioned = product.ProduceMaterialDistributioned + depotOutDetails.DepotOutDetailQuantity;
                        //    if (product.ProduceMaterialDistributioned < 0)
                        //        product.ProduceMaterialDistributioned = 0;
                        //}
                        //depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);
                        //stockAccessor.Increment(depotOutDetails.DepotPosition, product, depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        //影响库存
                        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.Product.ProductId, depotOutDetails.DepotPositionId);
                        if (stock != null)
                        {
                            stock.StockQuantity1 = Convert.ToDouble(stock.StockQuantity1) + Convert.ToDouble(depotOutDetails.DepotOutDetailQuantity);
                            stockAccessor.Update(stock);
                        }
                        //此方法其实只需要一个productId，来根据stock表的库存来修改product的库存。
                        productManager.UpdateProduct_Stock(depotOutDetails.Product);
                        // productManager.update(product);
                        depotOutDetailAccessor.Delete(depotOutDetails.DepotOutDetailId);
                    }
                }
                #endregion
                //accessorDetail.Delete(depotOut);

                //添加详细
                if (depotOut.SourceType == "委外領料單")
                {
                    foreach (Model.DepotOutDetail detail in depotOut.Details)
                    {
                        GetStockQuantity(detail);
                        //Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(detail.ProductId, detail.DepotPositionId);
                        //if (stock == null || stock.StockQuantity1 < 0)
                        //    throw new Helper.MessageValueException("" + detail.Product + "\r出貨數量不能大於貨位庫存");
                        if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                        detail.DepotOut = depotOut;
                        detail.DepotOutId = depotOut.DepotOutId;
                        detail.DepotPosition = depotPositionAccessor.Get(detail.DepotPositionId);

                        //影响库存
                        detail.Product = productManager.Get(detail.ProductId);
                        detail.Product.StocksQuantity = (detail.Product.StocksQuantity == null ? 0 : detail.Product.StocksQuantity.Value) - (detail.DepotOutDetailQuantity == null ? 0 : detail.DepotOutDetailQuantity.Value);

                        stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.DepotOutDetailQuantity);

                        //detail.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(detail.Product, depotOut.Depot);
                        //detail.CurrentStockQuantity = detail.Product.StocksQuantity;
                        detail.HasOutQuantity = detail.DepotOutDetailQuantity;
                        accessorDetail.Insert(detail);
                        if (!string.IsNullOrEmpty(detail.ProduceOtherMaterialDetailId))
                        {
                            Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = produceOtherMaterialDetailAccessor.Get(detail.ProduceOtherMaterialDetailId);

                            if (produceOtherMaterialDetail != null)
                            {
                                if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity == null)
                                    produceOtherMaterialDetail.OtherMaterialALLUserQuantity = 0;
                                produceOtherMaterialDetail.OtherMaterialALLUserQuantity = produceOtherMaterialDetail.OtherMaterialALLUserQuantity + (detail.DepotOutDetailQuantity == null ? 0 : detail.DepotOutDetailQuantity.Value);
                                if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity >= produceOtherMaterialDetail.OtherMaterialQuantity)
                                {
                                    produceOtherMaterialDetail.DepotOutStateDetail = 2;
                                }
                                else
                                {
                                    if (produceOtherMaterialDetail.OtherMaterialALLUserQuantity > 0)
                                    {
                                        produceOtherMaterialDetail.DepotOutStateDetail = 1;
                                    }
                                    else
                                    {
                                        produceOtherMaterialDetail.DepotOutStateDetail = 0;
                                    }
                                }
                                produceOtherMaterialDetail.Distributioned = Convert.ToDouble(produceOtherMaterialDetail.Distributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);

                                produceOtherMaterialDetailAccessor.Update(produceOtherMaterialDetail);
                                UpdateInvoiceXOFlagT(produceOtherMaterialDetail.ProduceOtherMaterial);
                                //商品委外领料单已分配数量
                                detail.Product.OtherMaterialDistributioned = Convert.ToDouble(detail.Product.OtherMaterialDistributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);
                                if (detail.Product.OtherMaterialDistributioned < 0)
                                    detail.Product.OtherMaterialDistributioned = 0;

                                //更新与该商品有关的单据的已分配数量
                                IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceOtherMaterialDetail.ProductId, produceOtherMaterialDetail.ProduceOtherMaterial.InsertTime.Value);
                                foreach (var itempo in polist)
                                {
                                    itempo.Distributioned = Convert.ToDouble(itempo.Distributioned) + Convert.ToDouble(detail.DepotOutDetailQuantity);
                                    itempo.Distributioned = itempo.Distributioned < 0 ? 0 : itempo.Distributioned;
                                    produceOtherMaterialDetailAccessor.Update(itempo);
                                }
                                foreach (var itempd in pdlist)
                                {
                                    itempd.Distributioned = Convert.ToDouble(itempd.Distributioned) + Convert.ToDouble(detail.DepotOutDetailQuantity);
                                    itempd.Distributioned = itempd.Distributioned < 0 ? 0 : itempd.Distributioned;
                                    produceMaterialDetailsAccessor.Update(itempd);
                                }
                            }
                        }
                        productManager.update(detail.Product);
                    }
                }
                else if (depotOut.SourceType == "領料單")
                {
                    foreach (Model.DepotOutDetail detail in depotOut.Details)
                    {
                        GetStockQuantity(detail);

                        if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                        //  detail.InvoiceCGDetailId = Guid.NeGuid().ToString();                   

                        detail.DepotOut = depotOut;
                        detail.DepotOutId = depotOut.DepotOutId;
                        detail.DepotPosition = depotPositionAccessor.Get(detail.DepotPositionId);
                        //影响库存
                        detail.Product = productManager.Get(detail.ProductId);
                        detail.Product.StocksQuantity = (detail.Product.StocksQuantity == null ? 0 : detail.Product.StocksQuantity.Value) - (detail.DepotOutDetailQuantity == null ? 0 : detail.DepotOutDetailQuantity.Value);

                        stockAccessor.Decrement(detail.DepotPosition, detail.Product, detail.DepotOutDetailQuantity == null ? 0 : detail.DepotOutDetailQuantity.Value);

                        //detail.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(detail.Product, depotOut.Depot);
                        //detail.CurrentStockQuantity = detail.Product.StocksQuantity;
                        detail.HasOutQuantity = detail.DepotOutDetailQuantity;
                        accessorDetail.Insert(detail);
                        if (!string.IsNullOrEmpty(detail.ProduceMaterialdetailsID))
                        {
                            Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(detail.ProduceMaterialdetailsID);
                            if (produceMaterialDetail != null)
                            {
                                if (produceMaterialDetail.Materialprocesedsum == null)
                                    produceMaterialDetail.Materialprocesedsum = 0;
                                produceMaterialDetail.Materialprocesedsum = produceMaterialDetail.Materialprocesedsum + (detail.DepotOutDetailQuantity == null ? 0 : detail.DepotOutDetailQuantity.Value);
                                if (produceMaterialDetail.Materialprocesedsum >= produceMaterialDetail.Materialprocessum)
                                {
                                    produceMaterialDetail.DepotOutStateDetail = 2;
                                }
                                else
                                {
                                    if (produceMaterialDetail.Materialprocesedsum > 0)
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 1;
                                    }
                                    else
                                    {
                                        produceMaterialDetail.DepotOutStateDetail = 0;
                                    }
                                }
                                produceMaterialDetail.Distributioned = Convert.ToDouble(produceMaterialDetail.Distributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);

                                produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                                UpdateInvoiceXOFlag(produceMaterialDetail.ProduceMaterial);
                                //商品领料单已分配数量
                                detail.Product.ProduceMaterialDistributioned = Convert.ToDouble(detail.Product.ProduceMaterialDistributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);
                                if (detail.Product.ProduceMaterialDistributioned < 0)
                                    detail.Product.ProduceMaterialDistributioned = 0;

                                //更新与该商品有关的单据的已分配数量
                                IList<Model.ProduceOtherMaterialDetail> polist = produceOtherMaterialDetailAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                IList<Model.ProduceMaterialdetails> pdlist = produceMaterialDetailsAccessor.SelectForDistributioned(produceMaterialDetail.ProductId, produceMaterialDetail.ProduceMaterial.InsertTime.Value);
                                foreach (var item in polist)
                                {
                                    item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);
                                    item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                    produceOtherMaterialDetailAccessor.Update(item);
                                }
                                foreach (var item in pdlist)
                                {
                                    item.Distributioned = Convert.ToDouble(item.Distributioned) - Convert.ToDouble(detail.DepotOutDetailQuantity);
                                    item.Distributioned = item.Distributioned < 0 ? 0 : item.Distributioned;
                                    produceMaterialDetailsAccessor.Update(item);
                                }
                            }
                        }
                        productManager.update(detail.Product);

                    }
                }
                else
                {
                    foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                    {
                        GetStockQuantity(depotOutDetails);

                        if (depotOutDetails.Product == null || depotOutDetails.Product.ProductId == null) continue;
                        depotOutDetails.DepotOutId = depotOut.DepotOutId;
                        depotOutDetails.Product = productManager.Get(depotOutDetails.ProductId);
                        //depotOutDetails.CurrentDepotQuantity = stockAccessor.GetTheCount1OfProductByProductId(depotOutDetails.Product, depotOut.Depot);
                        //depotOutDetails.CurrentStockQuantity = depotOutDetails.Product.StocksQuantity;
                        depotOutDetails.HasOutQuantity = depotOutDetails.DepotOutDetailQuantity;
                        depotOutDetailAccessor.Insert(depotOutDetails);
                        depotOutDetails.DepotPosition = depotPositionAccessor.Get(depotOutDetails.DepotPositionId);
                        //影响库存
                        stockAccessor.Decrement(depotOutDetails.DepotPosition, depotOutDetails.Product, depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value);
                        depotOutDetails.Product.StocksQuantity = (depotOutDetails.Product.StocksQuantity == null ? 0 : depotOutDetails.Product.StocksQuantity.Value) - (depotOutDetails.DepotOutDetailQuantity == null ? 0 : depotOutDetails.DepotOutDetailQuantity.Value) + (depotOutDetails.HasOutQuantity == null ? 0 : depotOutDetails.HasOutQuantity.Value);
                        productManager.UpdateProduct_Stock(depotOutDetails.Product);

                    }
                }



                #region
                //Dictionary<string, Model.DepotOutDetail> dic = new Dictionary<string, Book.Model.DepotOutDetail>();
                //IList<Model.DepotOutDetail> templist = depotOutDetailAccessor.GetDepotOutDetailByDepotOutId(depotOut.DepotOutId);
                //foreach (Model.DepotOutDetail item in templist)
                //{
                //    dic.Add(item.DepotOutDetailId, item);
                //}

                //foreach (Model.DepotOutDetail depotOutDetails in depotOut.Details)
                //{

                //    if (dic.ContainsKey(depotOutDetails.DepotOutDetailId))
                //        dic.Remove(depotOutDetails.DepotOutDetailId);

                //    if (depotOutDetails.Product == null || depotOutDetails.Product.ProductId == null) continue;
                //    if (depotOutDetails.DepotPosition == null)
                //        throw new Helper.RequireValueException(Model.DepotOutDetail.PROPERTY_DEPOTPOSITIONID);

                //    Model.DepotOutDetail Detail = depotOutDetailAccessor.Get(depotOutDetails.DepotOutDetailId);
                //    if (Detail != null)
                //    {
                //        depotOutDetails.DepotOutId = depotOut.DepotOutId;
                //        depotOutDetails.DepotPositionId = depotOutDetails.DepotPosition.DepotPositionId;
                //        if (depotOutDetails.Product.ProductId == Detail.ProductId)
                //        {
                //            //if (depotOutDetails.DepotOutDetailQuantity.Value == Detail.DepotOutDetailQuantity.Value) continue;

                //            Quantity = Detail.DepotOutDetailQuantity.Value - depotOutDetails.DepotOutDetailQuantity.Value;
                //            depotOutDetailAccessor.Update(depotOutDetails);

                //            Model.Product product = productAccessor.Get(depotOutDetails.Product.ProductId);
                //            if (product != null)
                //            {
                //                product.StocksQuantity -= Quantity;
                //                product.UpdateTime = System.DateTime.Now;
                //                productAccessor.Update(product);
                //            }

                //            Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.ProductId, depotOutDetails.DepotPosition.DepotPositionId);
                //            if (stock != null)
                //            {
                //                if (stock.StockQuantity1 < Quantity)
                //                    throw new Helper.MessageValueException("产品：" + stock.Product + "\r出库数量大于当前货位的数量");
                //                stock.StockQuantity1 -= Quantity;
                //                stockAccessor.Update(stock);
                //            }
                //            else
                //            {
                //                stock = new Book.Model.Stock();
                //                stock.StockId = Guid.NewGuid().ToString();
                //                stock.DepotPositionId = depotOutDetails.DepotPosition.DepotPositionId;
                //                stock.ProductId = depotOutDetails.ProductId;
                //                stock.DepotId = depotOut.DepotId;
                //                stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity.Value;
                //                stock.StockQuantityOld = 0;
                //                stockAccessor.Insert(stock);
                //            }
                //        }
                //        else   //产品不同时
                //        {
                //            depotOutDetailAccessor.Update(depotOutDetails);
                //            Model.Product tempproduct = productAccessor.Get(Detail.ProductId);
                //            tempproduct.StocksQuantity -= Detail.Product.StocksQuantity;
                //            productAccessor.Update(tempproduct);

                //            Model.Stock tempStock = stockAccessor.GetStockByProductIdAndDepotPositionId(Detail.ProductId, Detail.DepotPosition.DepotPositionId);
                //            tempStock.StockQuantity1 -= Detail.Product.StocksQuantity;
                //            stockAccessor.Update(tempStock);

                //            Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.Product.ProductId, depotOutDetails.DepotPosition.DepotPositionId);

                //            if (stock != null)
                //            {

                //                if (stock.StockQuantity1 < depotOutDetails.DepotOutDetailQuantity)
                //                    throw new Helper.MessageValueException("产品：" + stock.Product + "\r出库数量大于当前货位的数量");

                //                stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity;
                //                stockAccessor.Update(stock);
                //                //若不等null 說明不是從採購訂單過來的數據
                //                if (depotOutDetails.ProduceMaterialdetailsID != null)
                //                {
                //                    Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                //                    produceMaterialDetail.Materialprocesedsum += depotOutDetails.DepotOutDetailQuantity;
                //                    produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                //                }
                //            }
                //            else
                //            {
                //                stock = new Book.Model.Stock();
                //                stock.StockId = Guid.NewGuid().ToString();
                //                stock.DepotPositionId = depotOutDetails.DepotPosition.DepotPositionId;
                //                stock.ProductId = depotOutDetails.ProductId;
                //                stock.DepotId = depotOut.DepotId;
                //                stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity.Value;
                //                stock.StockQuantityOld = 0;
                //                stockAccessor.Insert(stock);
                //            }

                //            Model.Product product = productAccessor.Get(depotOutDetails.ProductId);
                //            product.StocksQuantity -= depotOutDetails.DepotOutDetailQuantity;
                //            productAccessor.Update(product);


                //        }
                //    }
                //    else
                //    {
                //        depotOutDetails.DepotOutId = depotOut.DepotOutId;

                //        depotOutDetailAccessor.Insert(depotOutDetails);

                //        Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(depotOutDetails.Product.ProductId, depotOutDetails.DepotPosition.DepotPositionId);

                //        if (stock != null)
                //        {

                //            if (stock.StockQuantity1 < depotOutDetails.DepotOutDetailQuantity)
                //                throw new Helper.MessageValueException("产品：" + stock.Product + "\r出库数量大于当前货位的数量");
                //            stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity;
                //            stockAccessor.Update(stock);
                //            //若不等null 說明不是從採購訂單過來的數據
                //            if (depotOutDetails.ProduceMaterialdetailsID != null)
                //            {
                //                Model.ProduceMaterialdetails produceMaterialDetail = produceMaterialDetailsAccessor.Get(depotOutDetails.ProduceMaterialdetailsID);
                //                produceMaterialDetail.Materialprocesedsum += depotOutDetails.DepotOutDetailQuantity;
                //                produceMaterialDetailsAccessor.Update(produceMaterialDetail);
                //            }
                //        }
                //        else
                //        {
                //            stock = new Book.Model.Stock();
                //            stock.StockId = Guid.NewGuid().ToString();
                //            stock.DepotPositionId = depotOutDetails.DepotPosition.DepotPositionId;
                //            stock.ProductId = depotOutDetails.ProductId;
                //            stock.DepotId = depotOut.DepotId;
                //            stock.StockQuantity1 -= depotOutDetails.DepotOutDetailQuantity.Value;
                //            stock.StockQuantityOld = 0;
                //            stockAccessor.Insert(stock);
                //        }

                //        Model.Product product = productAccessor.Get(depotOutDetails.ProductId);
                //        product.StocksQuantity -= depotOutDetails.DepotOutDetailQuantity;
                //        productAccessor.Update(product);

                //    }
                //}
                //foreach (string item in dic.Keys)
                //{
                //    Model.DepotOutDetail depot = depotOutDetailAccessor.Get(item);

                //    Model.Product tempproduct = productAccessor.Get(depot.ProductId);
                //    tempproduct.StocksQuantity -= depot.DepotOutDetailQuantity;
                //    productAccessor.Update(tempproduct);

                //    Model.Stock tempStock = stockAccessor.GetStockByProductIdAndDepotPositionId(depot.ProductId, depot.DepotPosition.DepotPositionId);
                //    tempStock.StockQuantity1 -= depot.DepotOutDetailQuantity;
                //    stockAccessor.Update(tempStock);

                //    depotOutDetailAccessor.Delete(item);
                //}
                #endregion

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
            return "depotoutRule";
        }

        protected override string GetInvoiceKind()
        {
            return "depotout";
        }

        private void validate(Book.Model.DepotOut depotOut)
        {
            if (string.IsNullOrEmpty(depotOut.DepotId))
                throw new Helper.RequireValueException(Model.DepotOut.PRO_DepotOutId);
            if (string.IsNullOrEmpty(depotOut.DepotOutDate.ToString()))
                throw new Helper.RequireValueException(Model.DepotOut.PRO_DepotOutDate);
            if (string.IsNullOrEmpty(depotOut.EmployeeId))
                throw new Helper.RequireValueException(Model.DepotOut.PRO_EmployeeId);
            bool istrue = false;
            bool IsNullOrZero = false;
            foreach (Model.DepotOutDetail item in depotOut.Details)
            {
                if (item.Product == null || item.Product.ProductId == null) continue;
                if (item.DepotOutDetailQuantity != null && item.DepotOutDetailQuantity.Value > 0)
                    IsNullOrZero = true;
                if (item.DepotPositionId == null)
                    throw new Helper.RequireValueException(Model.DepotPosition.PROPERTY_DEPOTPOSITIONID);
                istrue = true;
                if (item.DepotOutDetailId == null)
                    throw new Helper.RequireValueException(Model.DepotOutDetail.PRO_DepotPositionId);
                Model.Stock stock = stockAccessor.GetStockByProductIdAndDepotPositionId(item.ProductId, item.DepotPositionId);

                //if (stock == null || stock.StockQuantity1 < item.DepotOutDetailQuantity)
                //    throw new Helper.MessageValueException("產品：" + item.Product.ProductName + "\r出庫數量大於當前的貨位數量");

            }

            if (istrue == false)
                throw new Helper.RequireValueException("ProductDetail Is Null");
            if (IsNullOrZero == false)
                throw new Helper.MessageValueException("數量不能為空或者零！");
        }

        public IList<Model.DepotOut> SelectByDateRange(DateTime startdate, DateTime enddate)
        {

            return accessor.SelectByDateRange(startdate, enddate);
        }

        private void TiGuiExists(Model.DepotOut model)
        {
            if (this.ExistsPrimary(model.DepotOutId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.DepotOutDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.DepotOutDate.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.DepotOutDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.DepotOutId = this.GetId(model.DepotOutDate.Value);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }
    }
}

