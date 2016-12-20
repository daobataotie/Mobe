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

            invoice.InsertTime = DateTime.Now;
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

            if (invoice.Depot == null)
                throw new Helper.RequireValueException(Model.InvoiceCG.PRO_DepotId);

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //int count = 0;
            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                if (string.IsNullOrEmpty(detail.DepotPositionId) || detail.DepotPosition == null)
                {
                    //throw new Helper.RequireValueException(Model.InvoiceCGDetail.PROPERTY_DEPOTPOSITIONID);
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
            Model.InvoiceCO invoiceCO = invoice.InvoiceCO; //修改订单状态      
            _ValidateForInsert(invoice);
            if (this.HasRows(invoice.InvoiceId))
                throw new Helper.InvalidValueException(Model.InvoiceCG.PRO_InvoiceCOId);
            int flag = 0;
            foreach (Model.InvoiceCGDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (!string.IsNullOrEmpty( detail.DepotPositionId))
                    flag=1;
            }
            if(flag==0)
                throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);

            if (invoice.Employee0 == null)
                throw new Helper.MessageValueException("操作員不能為空！");

            invoice.SupplierId = invoice.Supplier.SupplierId;
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
                if (detail.DepotPositionId == null)
                   continue;
                Model.Product p = detail.Product;
                p.ProductId = detail.Product.ProductId;
                detail.InvoiceId = invoice.InvoiceId;
                detail.DepotPositionId = detail.DepotPositionId;
                invoiceCGDetailAccessor.Insert(detail);

                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                if (codetail != null)
                {
                    if (detail.InvoiceCGDetailQuantity == codetail.NoArrivalQuantity)
                    {
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    }
                    else
                    {
                        //if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
                        //{
                        //    throw new Helper.InvalidValueException("CGQgtCOQ");
                        //}
                        //进货量大于订单数量 修改订单数量
                        if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
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
                    }                   
                    codetail.NoArrivalQuantity -= detail.InvoiceCGDetailQuantity;
                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                    codetail.ArrivalQuantity = codetail.OrderQuantity - codetail.NoArrivalQuantity;
                    invoiceCODetailAccessor.Update(codetail);

                  

                    invoiceCOAccessor.Update(invoiceCO);
                }

                //更新产品信息
                Model.Product pro = detail.Product;
                pro.OrderOnWayQuantity -= detail.InvoiceCGDetailQuantity;
                pro.OrderOnWayQuantity=pro.OrderOnWayQuantity < 0 ? 0 : pro.OrderOnWayQuantity;
                pro.ProductNearCGDate = DateTime.Now;
                pro.StocksQuantity += detail.InvoiceCGDetailQuantity;

                productManager.update(pro);
                //修改货位库存。
                stockAccessor.Increment(detail.DepotPosition, pro, detail.InvoiceCGDetailQuantity);
                // 成本
                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
            }
            if (invoiceCO != null)
                this.invoiceCOManager.UpdateInvoiceFlag(invoiceCO);



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

                                if (codetail == null)
                                {
                                    return;
                                }
                                else
                                {
                                    codetail.NoArrivalQuantity += detail.InvoiceCGDetailQuantity;
                                    codetail.ArrivalQuantity = codetail.OrderQuantity - codetail.NoArrivalQuantity;
                                    if (codetail.NoArrivalQuantity == 0)
                                        codetail.DetailsFlag = 2;
                                    else if (codetail.NoArrivalQuantity == codetail.OrderQuantity)
                                        codetail.DetailsFlag = 0;
                                    else
                                        codetail.DetailsFlag = 1;
                                    invoiceCODetailAccessor.Update(codetail);
                                }

                                //更新产品信息
                                Model.Product pro = detail.Product;
                                pro.OrderOnWayQuantity += detail.InvoiceCGDetailQuantity;
                                pro.ProductNearCGDate = DateTime.Now;
                                pro.StocksQuantity -= detail.InvoiceCGDetailQuantity;
                                productManager.update(pro);
                                //修改货位库存。
                                stockAccessor.Decrement(detail.DepotPosition, pro, detail.InvoiceCGDetailQuantity);
                                // 成本
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice,cgQuantity);
                            }
                            this.invoiceCOManager.UpdateInvoiceFlag(invoice.InvoiceCO);
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
        public IList<Book.Model.InvoiceCG> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd, string productStart, string productEnd)
        {
            return accessor.Select(SupplierStart, SupplierEnd, dateStart, dateEnd, productStart, productEnd);
        }


    }
}

