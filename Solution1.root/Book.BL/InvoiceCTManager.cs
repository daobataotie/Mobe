//------------------------------------------------------------------------------
//
// file name：InvoiceCTManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCT.
    /// </summary>
    public partial class InvoiceCTManager : InvoiceManager
    {
        private static readonly DA.IInvoiceCTDetailAccessor invoiceCTDetailAccessor = (DA.IInvoiceCTDetailAccessor)Accessors.Get("InvoiceCTDetailAccessor");
        private static readonly DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        private static readonly DA.IInvoiceCODetailAccessor invoiceCODetailAccessor = (DA.IInvoiceCODetailAccessor)Accessors.Get("InvoiceCODetailAccessor");
        private BL.InvoiceCOManager invoiceCOManager = new InvoiceCOManager();
        //private static readonly DA.ITransactionController transactionController = (DA.ITransactionController)Accessors.Get("TransactionController");
        private BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new BGHandbookDetail1Manager();

        #region Select

        public IList<Model.InvoiceCT> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceCT> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceCT Get(string invoiceId)
        {
            Model.InvoiceCT invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceCTDetailAccessor.Select(invoice);
            return invoice;
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceCT)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _Update((Model.InvoiceCT)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCT)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCT))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCT)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceCT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceCT invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "CT";
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
            //
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceCT)invoice);

        }

        #endregion

        #endregion

        #region Helpers

        private void _Insert(Model.InvoiceCT invoice)
        {
            _ValidateForInsert(invoice);

            invoice.SupplierId = invoice.Supplier.SupplierId;

            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1 == null ? null : invoice.Employee1.EmployeeId;


            if (invoice.InvoiceStatus == (int)Helper.InvoiceStatus.Normal)
            {
                invoice.InvoiceGZTime = DateTime.Now; ;
                //过账人
                invoice.Employee2Id = invoice.Employee2 == null ? null : invoice.Employee2.EmployeeId;
            }
            accessor.Insert(invoice);


            //Model.Depot depot = invoice.Depot;

            // 库存
            foreach (Model.InvoiceCTDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                Model.Product p = productAccessor.Get(detail.ProductId);

                detail.InvoiceId = invoice.InvoiceId;

                invoiceCTDetailAccessor.Insert(detail);
                // p.ProductNearCGDate = DateTime.Now;            
                if (!string.IsNullOrEmpty(detail.DepotPositionId))
                {
                    stockAccessor.Decrement(new DepotPositionManager().Get(detail.DepotPositionId), detail.Product, detail.InvoiceCTDetailQuantity.Value);
                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                    // productAccessor.UpdateProduct_Stock(detail.Product);
                }
                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                if (codetail != null)
                {
                    //if (detail.InvoiceCGDetailQuantity > codetail.NoArrivalQuantity)
                    //{
                    //    throw new Helper.InvalidValueException("CGQgtCOQ");
                    //}
                    //进货量大于订单数量 修改订单数量
                    if (detail.InvoiceCTDetailQuantity >= codetail.NoArrivalQuantity)
                    {
                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                    }
                    else
                    {
                        if (codetail.ArrivalQuantity == 0)
                        { codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay; }
                        else
                            codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                    }
                    codetail.InvoiceCTQuantity = Convert.ToDouble(codetail.InvoiceCTQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                    double noArr = codetail.NoArrivalQuantity.Value;

                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);

                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                    invoiceCODetailAccessor.Update(codetail);
                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);
                    if (!p.OrderOnWayQuantity.HasValue) p.OrderOnWayQuantity = 0;
                    p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);



                    //修改手册要进量
                    if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                    {
                        bGHandbookDetail1Manager.UpdateYidingweiru(codetail, codetail.NoArrivalQuantity.Value - noArr);
                    }
                }
                productAccessor.Update(p);

                //修改手册已进
                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                {
                    bGHandbookDetail1Manager.UpdateBeeIn(detail.HandbookId, detail.HandbookProductId, 0 - detail.InvoiceCTDetailQuantity.Value);


                }

                // 成本
                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, -cgQuantity);

                // 库存
                //stockAccessor.Decrement(invoice.Depot, detail.Product, cgQuantity.Value);
            }
            // 应付
            //companyAccessor.DecrementP(invoice.Company, invoice.InvoiceOwed.Value);
        }

        private void _Update(Model.InvoiceCT invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceCT invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            invoice.UpdateTime = DateTime.Now;
                            invoice.SupplierId = invoice.Supplier.SupplierId;
                            //invoice.DepotId = invoice.Depot.DepotId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);

                            invoiceCTDetailAccessor.Delete(invoice);
                            foreach (Model.InvoiceCTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceCTDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceCTDetailAccessor.Insert(detail);
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
                            foreach (Model.InvoiceCTDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                Model.Product p = productAccessor.Get(detail.ProductId);
                                if (!string.IsNullOrEmpty(detail.DepotPositionId))
                                {
                                    stockAccessor.Increment(new DepotPositionManager().Get(detail.DepotPositionId), detail.Product, detail.InvoiceCTDetailQuantity.Value);
                                    p.StocksQuantity = Convert.ToDouble(p.StocksQuantity) + Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    // productAccessor.UpdateProduct_Stock(detail.Product);
                                }
                                Model.InvoiceCODetail codetail = invoiceCODetailAccessor.Get(detail.InvoiceCODetailId);

                                if (codetail != null)
                                {
                                    codetail.InvoiceCTQuantity = Convert.ToDouble(codetail.InvoiceCTQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    double noArr = codetail.NoArrivalQuantity.Value;

                                    codetail.NoArrivalQuantity = Convert.ToDouble(codetail.OrderQuantity) - Convert.ToDouble(codetail.ArrivalQuantity) + Convert.ToDouble(codetail.InvoiceCTQuantity);
                                    codetail.NoArrivalQuantity = codetail.NoArrivalQuantity < 0 ? 0 : codetail.NoArrivalQuantity;
                                    //进货量大于订单数量 修改订单数量
                                    if (detail.InvoiceCTDetailQuantity >= codetail.NoArrivalQuantity)
                                    {
                                        codetail.DetailsFlag = (int)Helper.DetailsFlag.AllArrived;
                                    }
                                    else
                                    {
                                        if (codetail.ArrivalQuantity == 0)
                                        {
                                            codetail.DetailsFlag = (int)Helper.DetailsFlag.OnTheWay;
                                        }
                                        else
                                            codetail.DetailsFlag = (int)Helper.DetailsFlag.PartArrived;
                                    }
                                    invoiceCODetailAccessor.Update(codetail);
                                    if (!p.OrderOnWayQuantity.HasValue) p.OrderOnWayQuantity = 0;
                                    p.OrderOnWayQuantity = Convert.ToDouble(p.OrderOnWayQuantity) - Convert.ToDouble(detail.InvoiceCTDetailQuantity);
                                    // this.UpdateSql("update product set OrderOnWayQuantity=OrderOnWayQuantity-" + detail.InvoiceCTDetailQuantity + " where productid='" + codetail.ProductId + "'");
                                    this.invoiceCOManager.UpdateInvoiceFlag(codetail.Invoice);

                                    //修改手册要进量
                                    if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                    {
                                        bGHandbookDetail1Manager.UpdateYidingweiru(codetail, codetail.NoArrivalQuantity.Value - noArr);

                                    }
                                }
                                //修改手册已进
                                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                {
                                    bGHandbookDetail1Manager.UpdateBeeIn(detail.HandbookId, detail.HandbookProductId, 0 - detail.InvoiceCTDetailQuantity.Value);


                                }


                                productAccessor.Update(p);


                                //if (detail.InvoiceProductUnit == detail.Product.ProductOuterPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value) / Convert.ToDecimal(p.ProductInnerUnitRelationship.Value);
                                //    cgQuantity = detail.InvoiceCTDetailQuantity * p.ProductBaseUnitRelationship * p.ProductInnerUnitRelationship;
                                //}
                                //else if (detail.InvoiceProductUnit == detail.Product.ProductInnerPackagingUnit)
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice / Convert.ToDecimal(p.ProductBaseUnitRelationship.Value);
                                //    cgQuantity = detail.InvoiceCTDetailQuantity * p.ProductBaseUnitRelationship;
                                //}
                                //else
                                //{
                                //    p.ProductCurrentCGPrice = detail.InvoiceCTDetailPrice;
                                //    cgQuantity = detail.InvoiceCTDetailQuantity;
                                //}
                                //productAccessor.UpdateCost1(p, p.ProductCurrentCGPrice, cgQuantity);
                                //stockAccessor.Increment(invoice.Depot, p,z.Value);
                            }
                            //companyAccessor.IncrementP(invoice.Company, invoice.InvoiceOwed.Value);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }


        private void Validate(Model.InvoiceCT invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Company");

            //if (invoice.Depot == null)
            //    throw new Helper.RequireValueException("Depot");

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            //foreach (Model.InvoiceCTDetail detail in invoice.Details)
            //{
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;                
            //    //if (detail.InvoiceCTDetailMoney0 == 0)
            //{
            //    throw new Helper.RequireValueException("Details");
            //}


            //if (string.IsNullOrEmpty(detail.DepotPositionId) || detail.DepotPosition == null)
            //{
            //    throw new Helper.RequireValueException(Model.InvoiceCGDetail.PRO_DepotPositionId);
            //}
            // }
        }

        #endregion

    }
}

