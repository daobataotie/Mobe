//------------------------------------------------------------------------------
//
// file name：InvoiceCGManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// 采购单
    /// </summary>
    public partial class InvoiceCGManager : InvoiceManager
    {
        private static readonly DA.IInvoiceCGDetailAccessor invoiceCGDetailAccessor = (DA.IInvoiceCGDetailAccessor)Accessors.Get("InvoiceCGDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.IInvoiceCODetailAccessor invoiceCODetailAccessor = (DA.IInvoiceCODetailAccessor)Accessors.Get("InvoiceCODetailAccessor");
        private static readonly DA.IInvoiceCOAccessor invoiceCOAccessor = (DA.IInvoiceCOAccessor)Accessors.Get("InvoiceCOAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly ProductManager productManager = new ProductManager();
        private BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new BGHandbookDetail1Manager();
        private BL.InvoiceCOManager invoiceCOManager = new InvoiceCOManager();
        #region Select

        public IList<Model.InvoiceCG> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }


        public IList<Model.InvoiceCG> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceCG Get(string invoiceId)
        {
            Model.InvoiceCG invoice = accessor.Get(invoiceId);
            if (invoice != null)
                invoice.Details = invoiceCGDetailAccessor.Select(invoice);
            return invoice;
        }

        public Model.InvoiceCG GetDetails(Model.InvoiceCG invoicecg)
        {
            Model.InvoiceCG invoice = accessor.Get(invoicecg.InvoiceId);
            if (invoice != null)
            {
                invoice.Details = invoiceCGDetailAccessor.Select(invoice);
            }
            return invoice;
        }

        //public Model.InvoiceCG GetDetail(Model.InvoiceCG invoiceCG)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    invoiceCG.Details = invoiceCGDetailAccessor.Select(invoiceCG);
        //    invoiceCG.PositionAndNumsSet.Clear();
        //    foreach (Model.InvoiceCGDetail item in invoiceCG.Details)
        //    {
        //        if (!dic.ContainsKey(item.InvoiceCODetailId))
        //        {
        //            invoiceCG.PositionAndNumsSet.Add(item);
        //            dic.Add(item.InvoiceCODetailId, item.ProductId);
        //        }
        //        else
        //        {
        //            if (item.ProductId != dic[item.InvoiceCODetailId])
        //                dic.Add(item.InvoiceCODetailId, item.ProductId);
        //            else
        //            {
        //                foreach (Model.InvoiceCGDetail temp in invoiceCG.PositionAndNumsSet)
        //                {
        //                    if (temp.ProductId == item.ProductId)
        //                        temp.InvoiceCGDetailQuantity += item.InvoiceCGDetailQuantity;
        //                }
        //            }
        //        }
        //    }

        //    return invoiceCG;
        //}
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            invoice.InsertTime = invoice.InvoiceDate;
            // invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceCG)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _Update((Model.InvoiceCG)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCG)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCG))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCG)invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "CG";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        #endregion

        #region Validation

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoiceCG)
        {
            base._ValidateForInsert(invoiceCG);

            Model.InvoiceCG invoice = invoiceCG as Model.InvoiceCG;

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Company");

            //if (invoice.Depot == null)
            //    throw new Helper.RequireValueException(Model.InvoiceCG.PRO_DepotId);

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //int count = 0;
            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                if (string.IsNullOrEmpty(detail.DepotPositionId) || detail.DepotPosition == null)
                {
                    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);
                }
                //count++;
            }
            //if (count == 0)
            //    throw new Helper.RequireValueException("InvoiceCGDetailQuantityIsZero");

        }

        #endregion

        #endregion

        #region Helpers

        private void _TurnNormal(Model.InvoiceCG invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        private void _TurnNull(Model.InvoiceCG invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _Insert(Model.InvoiceCG invoice)
        {
            //   Model.InvoiceCO invoiceCO = invoice.InvoiceCO; //修改订单状态      
            _ValidateForInsert(invoice);
            if (this.HasRows(invoice.InvoiceId))
                throw new Helper.InvalidValueException(Model.InvoiceCG.PRO_InvoiceCOId);
            // int flag = 0;
            //foreach (Model.InvoiceCGDetail detail in invoice.Details)
            //{
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
            //    if (!string.IsNullOrEmpty( detail.DepotPositionId))
            //        flag=1;
            //}
            //if(flag==0)
            //    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);

            if (invoice.Employee0 == null)
                throw new Helper.MessageValueException("操作員不能為空！");

            invoice.SupplierId = invoice.Supplier.SupplierId;
            if (invoice.Depot != null)
                invoice.DepotId = invoice.Depot.DepotId;

            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;
            //过账人
            invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            //过账时间
            invoice.InvoiceGZTime = DateTime.Now;
            invoice.InvoiceLRTime = DateTime.Now;

            accessor.Insert(invoice);

            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                Model.Product p = productAccessor.Get(detail.ProductId);

                detail.InvoiceId = invoice.InvoiceId;
                detail.DepotPositionId = detail.DepotPositionId;
                invoiceCGDetailAccessor.Insert(detail);
                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                if (codetail != null)
                {
                    //if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
                    //{
                    //    throw new Helper.InvalidValueException("CGQgtCOQ");
                    //}
                    //进货量大于订单数量 修改订单数量
                    if (detail.InvoiceCGDetailQuantity >= codetail.NoArrivalQuantity)
                    {
                        //invoiceCO.InvoiceHeji-=codetail.InvoiceCODetailMoney;
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                        // codetail.OrderQuantity = detail.InvoiceCGDetailQuantity + codetail.ArrivalQuantity;
                        // codetail.InvoiceCODetailMoney = codetail.InvoiceCODetailPrice *decimal.Parse(codetail.OrderQuantity.Value.ToString());
                        // codetail.TotalMoney = codetail.InvoiceCODetailMoney;
                        // invoiceCO.InvoiceHeji += codetail.InvoiceCODetailMoney;
                        //暂时 未考虑税率
                        // invoiceCO.InvoiceTotal = invoiceCO.InvoiceHeji;
                    }
                    else
                    {
                        if (codetail.ArrivalQuantity == 0)
                        { codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay; }
                        else
                            codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                    }

                    codetail.ArrivalQuantity = Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(detail.InvoiceCGDetailQuantity);
                    double noArr = codetail.NoArrivalQuantity.Value;
                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);


                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;

                    invoiceCODetailAccessor.Update(codetail);
                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);




                    //修改手册要进量
                    if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                    {
                        bGHandbookDetail1Manager.UpdateYidingweiru(codetail, 0 - (noArr - codetail.NoArrivalQuantity.Value));
                    }

                    p.OrderOnWayQuantity += codetail.NoArrivalQuantity.Value - noArr;

                }

                //更新产品信息00


                p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) - Convert.ToDouble(detail.InvoiceCGDetailQuantity);
                p.OrderOnWayQuantity = p.OrderOnWayQuantity < 0 ? 0 : p.OrderOnWayQuantity;
                p.ProductNearCGDate = DateTime.Now;
                if (codetail != null)//单价 以后 在进库单 保存
                    p.NewestCost = codetail.InvoiceCODetailPrice.HasValue ? codetail.InvoiceCODetailPrice.Value : 0;
                if (!string.IsNullOrEmpty(detail.DepotPositionId) && detail.InvoiceCGDetaiInQuantity != 0)
                {
                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) + detail.InvoiceCGDetaiInQuantity;
                    //修改货位库存。
                    stockAccessor.Increment(detail.DepotPosition, p, detail.InvoiceCGDetaiInQuantity);
                }
                // 成本
                // productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
                productManager.update(p);
                //修改手册已进
                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                {
                    bGHandbookDetail1Manager.UpdateBeeIn(detail.HandbookId, detail.HandbookProductId, detail.InvoiceCGDetailQuantity.Value);

                }

            }

            //应收应付
            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
        }

        public void Updates(Model.InvoiceCG invoice)
        {
            accessor.Update(invoice);
        }

        private void _Update(Model.InvoiceCG invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;

            Model.InvoiceCG invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:

                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            invoice.UpdateTime = DateTime.Now;
                            invoice.SupplierId = invoice.Supplier.SupplierId;
                            invoice.DepotId = invoice.Depot.DepotId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);

                            invoiceCGDetailAccessor.Delete(invoiceOriginal);
                            foreach (Model.InvoiceCGDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceCGDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceCGDetailAccessor.Insert(detail);


                            }

                            break;

                        case Helper.InvoiceStatus.Normal:

                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                    }
                    break;
                case Helper.InvoiceStatus.Normal:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();

                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:

                            foreach (Model.InvoiceCGDetail detail in invoice.Details)
                            {
                                //修改订单未到和实际到数量   
                                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);
                                double noArr = 0;
                                if (codetail != null)
                                {
                                    codetail.ArrivalQuantity = Convert.ToDouble(codetail.ArrivalQuantity) - Convert.ToDouble(detail.InvoiceCGDetailQuantity);
                                    noArr = codetail.NoArrivalQuantity.Value;

                                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity + codetail.InvoiceCTQuantity);
                                    if (codetail.NoArrivalQuantity < 0)
                                        codetail.NoArrivalQuantity = 0;
                                    if (codetail.NoArrivalQuantity == 0)
                                        codetail.DetailsFlag = 2;
                                    else if (codetail.NoArrivalQuantity == codetail.OrderQuantity)
                                        codetail.DetailsFlag = 0;
                                    else
                                        codetail.DetailsFlag = 1;
                                    invoiceCODetailAccessor.Update(codetail);
                                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);


                                    //修改手册要进量
                                    if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                    {
                                        bGHandbookDetail1Manager.UpdateYidingweiru(codetail, codetail.NoArrivalQuantity.Value - noArr);

                                    }
                                }

                                //更新产品信息
                                //if (detail.DepotPosition != null)
                                //{
                                Model.Product pro = detail.Product;
                                if (codetail != null)
                                    pro.OrderOnWayQuantity = Convert.ToDouble(pro.OrderOnWayQuantity) + codetail.NoArrivalQuantity.Value - noArr;
                                pro.ProductNearCGDate = DateTime.Now;
                                if (!string.IsNullOrEmpty(detail.DepotPositionId) && detail.InvoiceCGDetaiInQuantity != 0)
                                {
                                    pro.StocksQuantity = Convert.ToDouble(pro.StocksQuantity) - Convert.ToDouble(detail.InvoiceCGDetaiInQuantity);

                                    //修改货位库存。
                                    stockAccessor.Decrement(detail.DepotPosition, pro, detail.InvoiceCGDetaiInQuantity);
                                }
                                productManager.update(pro);
                                //}
                                //返回手册已进
                                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                {

                                    bGHandbookDetail1Manager.UpdateBeeIn(detail.HandbookId, detail.HandbookProductId, 0 - detail.InvoiceCGDetailQuantity.Value);
                                }

                                // 成本
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
                            }
                            //  this.invoiceCOManager.UpdateInvoiceFlag(invoice.InvoiceCO);
                            //应收应付
                            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }
        #endregion

        public IList<Book.Model.InvoiceCG> Select(DateTime start, DateTime end, string startID, string endID)
        {
            return accessor.Select(start, end, startID, endID);
        }

        public IList<Book.Model.InvoiceCG> Select1(DateTime start, DateTime end)
        {
            return accessor.Select1(start, end);
        }
        public IList<Book.Model.InvoiceCG> Select(Model.Supplier supplier)
        {
            return accessor.Select(supplier);
        }
        public IList<Book.Model.InvoiceCG> Select(string costartid, string coendid, Model.Supplier SupplierStart, Model.Supplier SupplierEnd, DateTime dateStart, DateTime dateEnd, Model.Product productStart, Model.Product productEnd, string cusxoid, DateTime dateJHStart, DateTime dateJHEnd)
        {
            return accessor.Select(costartid, coendid, SupplierStart, SupplierEnd, dateStart, dateEnd, productStart, productEnd, cusxoid, dateJHStart, dateJHEnd);
        }


    }
}

